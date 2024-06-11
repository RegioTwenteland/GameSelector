using GameSelector.Model;
using GameSelector.NfcReader;
using GameSelector.Views;
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
            _gameDataBridge.GameUpdated += OnGameUpdated;

            SetMessageHandlers(new Dictionary<string, Action<Message>>
            {
                { "CardInserted", OnCardInserted },
                { "CardEjected", OnCardEjected},
                { "AnimationComplete", OnAnimationComplete },
                { "UserViewReady", OnUserViewReady },
                { "TestUserView", OnTestUserView }
            });
        }

        private void OnTestUserView(Message message)
        {
            Debug.Assert(message.Value is null);

            var occupant = _groupDataBridge.GetAllGroups().OrderByDescending(g => g.Name.Length + g.ScoutingName.Length).First();
            var games = _gameDataBridge.GetAllGames();
            foreach (var game in games)
            {
                _userView.ShowGameImmediate(GameDataView.FromGame(game), GroupDataView.FromGroup(occupant));
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

        private void OnGameUpdated(object sender, GameUpdatedEventArgs e)
        {
            UpdateGameCodes();
        }

        public override void Start(Action stop)
        {
            _userView.Start(stop);

            UpdateGameCodes();
        }

        private void UpdateGameCodes()
        {
            var codes = _gameDataBridge.GetAllGames().Select(g => g.Code);
            _userView.SetGameCodes(codes.ToArray());
        }

        private IEnumerable<Game> GetPossibleGames(IEnumerable<Game> gamesAvailable, IEnumerable<PlayedGame> playedGames)
        {
            var playedGameIds = playedGames.Select(pg => pg.Game.Id).ToHashSet();
            return gamesAvailable.Where(game => (!playedGameIds.Contains(game.Id)) && game.Active);
        }

        private static readonly Random rng = new Random();

        private bool SelectNewGameFor(Group group, out Game newGame)
        {
            newGame = null;

            var gamesAvailable = _gameDataBridge.GetAllGamesAvailable();
            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);

            var possibleGames = GetPossibleGames(gamesAvailable, playedGames).ToList();

            Game selectedGame = null;

            NdefMessage newNdefData = null;

            if (possibleGames.Count > 0)
            {
                if (playedGames.Any())
                {
                    var highestPrioGames = possibleGames
                        .GroupBy(game => game.Priority)
                        .OrderByDescending(grouping => grouping.Key)
                        .First()
                        .ToList();

                    selectedGame = highestPrioGames[rng.Next(highestPrioGames.Count)];
                }
                else
                {
                    // The first game a group plays is completely random.
                    selectedGame = possibleGames[rng.Next(possibleGames.Count)];
                }

                group.StartTime = DateTime.Now;
                group.CurrentlyPlaying = selectedGame;

                newNdefData = new NdefMessage(
                    group.ScoutingName,
                    group.Name,
                    selectedGame.Code,
                    group.StartTime.Value.ToString("HH:mm:ss")
                );
            }

            var success = _nfcReader.WriteMessage(newNdefData ?? new NdefMessage(group.ScoutingName, group.Name));

            if (success && selectedGame != null)
            {
                _groupDataBridge.UpdateGroup(group);
                newGame = selectedGame;
            }

            return success;
        }

        private void EndGameFor(Game currentGame, Group group)
        {
            if (currentGame == null) return;
            if (!group.StartTime.HasValue) return;

            var playedGame = new PlayedGame
            {
                Player = group,
                Game = currentGame,
                StartTime = group.StartTime ?? DateTime.MinValue,
                EndTime = DateTime.Now,
            };

            _playedGameDataBridge.InsertPlayedGame(playedGame);

            group.StartTime = null;

            _groupDataBridge.UpdateGroup(group);
        }

        private void LogUserIn()
        {
            var group = _groupDataBridge.GetGroup(_currentCard);

            if (group == null) return;

            if (group.IsAdmin) return; // admin groups can't play games

            var currentGame = group.CurrentlyPlaying;

            if (currentGame != null && (DateTime.Now - group.StartTime < TimeSpan.FromMinutes(GlobalSettings.GameTimeoutMinutes)))
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

            _userView.ShowGame(GameDataView.FromGame(newGame), GroupDataView.FromGroup(group));
        }

        private bool beep = false;
        private void OnCardInserted(Message message)
        {
            Debug.Assert(message.Value is string);

            _currentCard = (string)message.Value;

            if (_loggedInUser == string.Empty && _gameState.CurrentState != GameState.State.Paused)
            {
                LogUserIn();
            }

            _nfcReader.Beep();
        }

        private bool _readyOnEject = false;

        private void OnCardEjected(Message message)
        {
            Debug.Assert(message.Value is null);

            _currentCard = string.Empty;

            if (_readyOnEject)
            {
                _readyOnEject = false;
                _userView.ShowReady();
            }
        }

        private void OnAnimationComplete(Message message)
        {
            Debug.Assert(message.Value is null);

            if (_currentCard == _loggedInUser)
            {
                _readyOnEject = true;
            }
            else
            {
                _userView.ShowReadyAfter(TimeSpan.FromSeconds(5));
            }
        }

        private void OnUserViewReady(Message message)
        {
            Debug.Assert(message.Value is null);

            _loggedInUser = string.Empty;
        }
    }
}
