using GameSelector.Model;
using GameSelector.NfcReader;
using GameSelector.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

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

        public override void Start(Action<object> stop)
        {
            _userView.Start(stop);

            UpdateGameCodes();
        }

        private void UpdateGameCodes()
        {
            var codes = _gameDataBridge.GetAllGames().Select(g => g.Code);
            _userView.SetGameCodes(codes.ToArray());
        }

        private IEnumerable<Game> GetGamesNotPlayed(IEnumerable<Game> gamesAvailable, IEnumerable<PlayedGame> playedGames)
        {
            var playedGameIds = playedGames.Select(pg => pg.Game.Id).ToHashSet();
            return gamesAvailable.Where(game => (!playedGameIds.Contains(game.Id)) && game.Active);
        }

        private static readonly Random rng = new Random();

        private bool FindNewGameFor(Group group, out Game newGame)
        {
            newGame = null;

            var gamesAvailable = _gameDataBridge.GetAllGamesAvailable();
            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);

            // First game is always completely random
            if (!playedGames.Any())
            {
                newGame = gamesAvailable.ToArray()[rng.Next(gamesAvailable.Count())];
                return true;
            }

            // Make sure no game is played twice by the same group
            var remainingGames = GetGamesNotPlayed(gamesAvailable, playedGames);

            // If this leaves no games to be played, the group has played all games.
            if (!remainingGames.Any())
            {
                return false;
            }

            var lastPlayedCategory = playedGames.OrderByDescending(g => g.EndTime)
                .First()
                .Game
                .Category;

            // Remove all games which are in the same category as the last played game, but only if that leaves games to be played
            // Otherwise we just accept that the group might play a similar game to the last one
            if (remainingGames.Where(g => g.Category != lastPlayedCategory).Any())
            {
                remainingGames = remainingGames.Where(g => g.Category != lastPlayedCategory);
            }

            // Remove all games which do not have the highest priority.
            remainingGames = remainingGames
                .GroupBy(game => game.Priority)
                .OrderByDescending(grouping => grouping.Key)
                .First();

            // These are all of the possible games to select from.
            var selectFrom = remainingGames.ToArray();

            newGame = selectFrom[rng.Next(selectFrom.Length)];
            return true;
        }

        private bool SetNewGameFor(Group group, Game newGame)
        {
            Debug.Assert(group is not null);

            group.CurrentlyPlaying = newGame;
            group.StartTime = DateTime.Now;

            var newNdefData = newGame is null ?
                new NdefMessage(group.ScoutingName, group.Name) :
                new NdefMessage(
                    group.ScoutingName,
                    group.Name,
                    newGame.Code,
                    group.StartTime.Value.ToString("HH:mm:ss")
                );

            if (_nfcReader.WriteMessage(newNdefData))
            {
                _groupDataBridge.UpdateGroup(group);
                return true;
            }

            return false;
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
            var success = FindNewGameFor(group, out var newGame);
            success = SetNewGameFor(group, newGame);
            if (!success) return;

            _loggedInUser = _currentCard;

            if (newGame == null)
            {
                _userView.ShowNoGamesLeft();
                return;
            }

            _userView.ShowGame(GameDataView.FromGame(newGame), GroupDataView.FromGroup(group));
        }

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
