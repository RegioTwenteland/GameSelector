using GameSelector.Model;
using GameSelector.Views;
using NfcReader;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        private bool _allowNewUserLogin = true;

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
                { "UserLogin", OnUserLogin },
                { "AnimationComplete", OnAnimationComplete },
            });
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            switch (_gameState.CurrentState)
            {
                case GameState.State.Paused:
                    _userView.ShowPaused();
                    break;
                case GameState.State.Playing:
                    _userView.ShowUnpaused();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void Start(Action stop)
        {
            _userView.Start(stop);
        }

        private List<Game> GetPossibleGames(Group group)
        {
            var games = _gameDataBridge.GetAllGamesNotOccupied();
            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);

            var playedGameIds = new HashSet<long>();
            foreach (var game in playedGames)
            {
                playedGameIds.Add(game.GameId);
            }

            var possibleGames = new List<Game>(games.Count);
            foreach (var game in games)
            {
                if (!playedGameIds.Contains(game.Id))
                {
                    possibleGames.Add(game);
                }
            }

            return possibleGames;
        }

        private bool SelectNewGameFor(Group group, out Game newGame)
        {
            newGame = null;

            var possibleGames = GetPossibleGames(group);

            Game selectedGame = null;

            NdefMessage newNdefData = null;

            if (possibleGames.Count > 0)
            {
                // find game with highest prio
                var recordPriority = -1L;
                foreach (var game in possibleGames)
                {
                    if (game.Priority > recordPriority)
                    {
                        recordPriority = game.Priority;
                        selectedGame = game;
                    }
                }

                selectedGame.StartTime = DateTime.Now;
                selectedGame.OccupiedBy = group;

                newGame = selectedGame;

                newNdefData = new NdefMessage(
                    group.ScoutingName,
                    group.Name,
                    selectedGame.Code,
                    selectedGame.StartTime.Value.ToString("HH:mm:ss")
                );

                _gameDataBridge.UpdateGame(selectedGame);
            }

            if (newNdefData == null)
            {
                newNdefData = new NdefMessage(
                    group.ScoutingName,
                    group.Name
                );
            }

            var success = _nfcReader.WriteMessage(newNdefData);

            return success;
        }

        private void EndGameFor(Group group)
        {
            var currentGame = _gameDataBridge.GetGameOccupiedBy(group);
            if (currentGame == null) return;

            Debug.Assert(currentGame.StartTime.HasValue);

            var playedGame = new PlayedGame
            {
                PlayerId = group.Id,
                GameId = currentGame.Id,
                StartTime = currentGame.StartTime ?? DateTime.MinValue,
                EndTime = DateTime.Now,
            };

            _playedGameDataBridge.InsertPlayedGame(playedGame);

            currentGame.OccupiedBy = null;
            currentGame.StartTime = null;

            _gameDataBridge.UpdateGame(currentGame);
        }

        private void OnUserLogin(object value)
        {
            Debug.Assert(value is string);

            if (!_allowNewUserLogin) return;
            if (_gameState.CurrentState == GameState.State.Paused) return;

            var cardId = (string)value;
            var group = _groupDataBridge.GetGroup(cardId);

            if (group == null) return;

            EndGameFor(group);
            var success = SelectNewGameFor(group, out var newGame);

            if (!success) return;

            if (newGame == null)
            {
                _userView.ShowNoGamesLeft();
                return;
            }

            _allowNewUserLogin = false;
            _userView.ShowGame(GameDataView.FromGame(newGame));
        }

        private void OnAnimationComplete(object value)
        {
            Debug.Assert(value is null);

            _allowNewUserLogin = true;
        }
    }
}
