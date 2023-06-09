﻿using GameSelector.Model;
using GameSelector.Views;
using NfcReader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace GameSelector.Controllers
{
    internal class UserController : AbstractController
    {
        private GameState _gameState;
        private UserViewAdapter _userView;
        private UserIdentificationView _userIdentificationView;
        private INfcReader _nfcReader;
        private IGroupDataBridge _groupDataBridge;
        private IGameDataBridge _gameDataBridge;
        private IPlayedGameDataBridge _playedGameDataBridge;

        private string _currentCard = string.Empty;
        private string _loggedInUser = string.Empty;

        public UserController(
            GameState gameState,
            UserIdentificationView userIdentificationView,
            UserViewAdapter userView,
            INfcReader nfcReader,
            IGroupDataBridge groupDataBridge,
            IGameDataBridge gameDataBridge,
            IPlayedGameDataBridge playedGameDataBridge
        )
        {
            _gameState = gameState;
            _userIdentificationView = userIdentificationView;
            _userView = userView;
            _nfcReader = nfcReader;
            _groupDataBridge = groupDataBridge;
            _gameDataBridge = gameDataBridge;
            _playedGameDataBridge = playedGameDataBridge;

            userView.Ready += (s, e) => _gameState.StateChanged += OnStateChanged;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "CardInserted", OnCardInserted },
                { "CardEjected", OnCardEjected},
                { "AnimationComplete", OnAnimationComplete },
                { "UserViewReady", OnUserViewReady },
                { "TestUserView", OnTestUserView }
            });
        }

        private void OnTestUserView(object obj)
        {
            Debug.Assert(obj is null);

            var occupant = _groupDataBridge.GetAllGroups().OrderByDescending(g => g.Name.Length + g.ScoutingName.Length).First();
            var games = _gameDataBridge.GetAllGames();
            foreach (var game in games)
            {
                game.OccupiedBy = occupant;
                _userView.ShowGameImmediate(GameDataView.FromGame(game));
                Thread.Sleep(1000);
            }
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            switch (_gameState.CurrentState)
            {
                case GameState.State.Paused:
                    _userView.ShowPaused();
                    break;
                case GameState.State.Playing:
                    _userView.ShowReady();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void Start(Action stop)
        {
            _userView.Start(stop);

            var codes = _gameDataBridge.GetAllGames().Select(g => g.Code);
            _userView.SetGameCodes(codes.ToArray());
        }

        private List<Game> GetPossibleGames(List<Game> gamesNotOccupied, List<PlayedGame> playedGames, Group group)
        {
            var playedGameIds = new HashSet<long>();
            foreach (var game in playedGames)
            {
                playedGameIds.Add(game.Game.Id);
            }

            var possibleGames = new List<Game>(gamesNotOccupied.Count);
            foreach (var game in gamesNotOccupied)
            {
                if (!playedGameIds.Contains(game.Id) && game.Active)
                {
                    possibleGames.Add(game);
                }
            }

            return possibleGames;
        }

        private static Random rng = new Random();

        private void ShuffleList<T>(IList<T> list)
        {
            var n = list.Count;

            while (n > 1)
            {
                --n;
                int k = rng.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }

        private bool SelectNewGameFor(Group group, out Game newGame)
        {
            newGame = null;

            var gamesNotOccupied = _gameDataBridge.GetAllGamesNotOccupied();
            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);

            var possibleGames = GetPossibleGames(gamesNotOccupied, playedGames, group);

            Game selectedGame = null;

            NdefMessage newNdefData = null;

            if (possibleGames.Count > 0)
            {
                if (playedGames.Count >= 1)
                {
                    var prioGames = possibleGames.Where(g => g.HasPriority).ToList();
                    var normalGames = possibleGames.Where(g => !g.HasPriority).ToList();
                    ShuffleList(prioGames);
                    ShuffleList(normalGames);
                    possibleGames = prioGames.Concat(normalGames).ToList();
                }
                else
                {
                    // The first game a group plays is completely random.
                    ShuffleList(possibleGames);
                }

                selectedGame = possibleGames[0];

                selectedGame.StartTime = DateTime.Now;
                selectedGame.OccupiedBy = group;

                newNdefData = new NdefMessage(
                    group.ScoutingName,
                    group.Name,
                    selectedGame.Code,
                    selectedGame.StartTime.Value.ToString("HH:mm:ss")
                );
            }

            var success = _nfcReader.WriteMessage(newNdefData ?? new NdefMessage(group.ScoutingName, group.Name));

            if (success && selectedGame != null)
            {
                _gameDataBridge.UpdateGame(selectedGame);
                newGame = selectedGame;
            }

            return success;
        }

        private void EndGameFor(Game currentGame, Group group)
        {
            if (currentGame == null) return;

            Debug.Assert(currentGame.StartTime.HasValue);

            var playedGame = new PlayedGame
            {
                Player = group,
                Game = currentGame,
                StartTime = currentGame.StartTime ?? DateTime.MinValue,
                EndTime = DateTime.Now,
            };

            _playedGameDataBridge.InsertPlayedGame(playedGame);

            currentGame.OccupiedBy = null;
            currentGame.StartTime = null;

            _gameDataBridge.UpdateGame(currentGame);
        }

        private void LogUserIn()
        {
            var group = _groupDataBridge.GetGroup(_currentCard);

            if (group == null) return;

            if (group.IsAdmin) return; // admin groups can't play games

            var currentGame = _gameDataBridge.GetGameOccupiedBy(group);

            if (currentGame != null && (DateTime.Now - currentGame.StartTime < TimeSpan.FromMinutes(GlobalSettings.GameTimeoutMinutes)))
            {
                _loggedInUser = _currentCard;
                _userView.ShowAlreadyPlaying(GameDataView.FromGame(currentGame));
                return;
            }

            EndGameFor(currentGame, group);
            var success = SelectNewGameFor(group, out var newGame);
            if (!success) return;

            _loggedInUser = _currentCard;

            if (newGame == null)
            {
                _userView.ShowNoGamesLeft();
                return;
            }

            _userView.ShowGame(GameDataView.FromGame(newGame));
        }

        private void OnCardInserted(object value)
        {
            Debug.Assert(value is string);

            _currentCard = (string)value;

            if (_loggedInUser == string.Empty && _gameState.CurrentState != GameState.State.Paused)
                LogUserIn();
        }

        private bool _readyOnEject = false;

        private void OnCardEjected(object value)
        {
            Debug.Assert(value is null);

            _currentCard = string.Empty;

            if (_readyOnEject)
            {
                _readyOnEject = false;
                _userView.ShowReady();
            }
        }

        private bool _tempDone = false;

        private void OnAnimationComplete(object value)
        {
            Debug.Assert(value is null);

            if (_currentCard == _loggedInUser)
            {
                _readyOnEject = true;
            }
            else
            {
                _userView.ShowReadyAfter(TimeSpan.FromSeconds(5));
            }
        }

        private void OnUserViewReady(object value)
        {
            Debug.Assert(value is null);

            _loggedInUser = string.Empty;
        }
    }
}
