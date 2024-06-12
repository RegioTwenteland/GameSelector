using GameSelector.Model;
using GameSelector.Views;
using GameSelector.Views.AdminGameView;
using GameSelector.Views.AdminScaffoldView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GameSelector.Controllers
{
    internal class AdminGameController : AbstractController
    {
        private AdminScaffoldViewAdapter _adminScaffoldView;
        private AdminGameViewAdapter _adminGameView;
        private IGameDataBridge _gameDataBridge;
        private IGroupDataBridge _groupDataBridge;
        private IPlayedGameDataBridge _playedGameDataBridge;
        private MessageSender _messageSender;

        private readonly TimeSpan GameTimeoutCheckInterval = TimeSpan.FromMinutes(1);

        public AdminGameController(
            AdminScaffoldViewAdapter adminScaffoldView,
            AdminGameViewAdapter adminGameView,
            IGameDataBridge gameDataBridge,
            IGroupDataBridge groupDataBridge,
            IPlayedGameDataBridge playedGameDataBridge,
            MessageSender messageSender
        )
        {
            _adminScaffoldView = adminScaffoldView;
            _adminGameView = adminGameView;
            _gameDataBridge = gameDataBridge;
            _groupDataBridge = groupDataBridge;
            _playedGameDataBridge = playedGameDataBridge;
            _messageSender = messageSender;

            _gameDataBridge.GameUpdated += OnGameUpdated;

            SetMessageHandlers(new Dictionary<string, Action<Message>>
            {
                { "RequestSaveGame", OnRequestSaveGame },
                { "RequestNewGame", OnRequestNewGame },
                { "RequestDeleteGame", OnRequestDeleteGame },
                { "RequestForceEndGameForGroup", OnRequestForceEndGameForGroup },
                { "RequestPlayedGames", OnRequestPlayedGames },
                { "RequestGameTimeoutCheck", OnRequestTimeoutCheck },
            });
        }

        private void OnGameUpdated(object sender, GameUpdatedEventArgs e)
        {
            var gdv = GameDataView.FromGame(e.Game);
            _adminGameView.UpdateGame(gdv);
        }

        public override void Start(Action stop)
        {
            UpdateGamesList(_gameDataBridge.GetAllGames());

            _messageSender.Send(new Message("RequestGameTimeoutCheck", null));
        }

        private void UpdateGamesList(IEnumerable<Game> games)
        {
            var gdv = games.Select(g => GameDataView.FromGame(g));
            _adminGameView.SetGamesList(gdv);
        }

        private void OnRequestSaveGame(Message message)
        {
            Debug.Assert(message.Value is GameDataView);

            var gameDataView = (GameDataView)message.Value;

            var game = _gameDataBridge.GetGame(gameDataView.Id);

            game.Code = gameDataView.Code;
            game.Description = gameDataView.Description;
            game.Explanation = gameDataView.Explanation;
            game.Active = gameDataView.Active;
            game.Priority = gameDataView.Priority;
            game.Remarks = gameDataView.Remarks;
            game.Timeout = TimeSpan.FromMinutes(gameDataView.TimeoutMinutes);
            game.MaxPlayerAmount = gameDataView.MaxPlayerAmount;

            _gameDataBridge.UpdateGame(game);

            _adminGameView.UpdateGame(gameDataView);
        }

        private void OnRequestNewGame(Message message)
        {
            Debug.Assert(message.Value is null);

            var newGame = new Game
            {
                Code = "Nieuw",
                Description = "Spel",
                Explanation = string.Empty,
                Active = true,
                Priority = 10,
                Timeout = TimeSpan.FromMinutes(15),
                MaxPlayerAmount = 1
            };

            _gameDataBridge.InsertGame(newGame);

            var games = _gameDataBridge.GetAllGames().Select(g => GameDataView.FromGame(g));
            _adminGameView.SetGamesList(games);
            _adminGameView.SetGameSelected(games.First(g => g.Code == "Nieuw"));
        }

        private void OnRequestDeleteGame(Message message)
        {
            Debug.Assert(message.Value is GameDataView);

            var gameDataView = (GameDataView)message.Value;

            var game = _gameDataBridge.GetGame(gameDataView.Id);

            // Do some sanity checks:
            if (_groupDataBridge.GetAllGroupsPlaying(game).Any())
            {
                _adminScaffoldView.ShowError("Verwijderen mislukt: Spel wordt momenteel gespeeld");
                return;
            }

            var playedGames = _playedGameDataBridge.GetPlayedGamesByGame(game);
            if (playedGames.Any())
            {
                _adminScaffoldView.ShowError("Verwijderen mislukt: Spel is al gespeeld");
                return;
            }

            _gameDataBridge.DeleteGame(game);

            UpdateGamesList(_gameDataBridge.GetAllGames());
        }

        private void ForceEndGame(Group group)
        {
            var game = _gameDataBridge.GetGameBeingPlayedBy(group);

            var playedGame = new PlayedGame
            {
                Player = group,
                Game = game,
                StartTime = group.StartTime ?? DateTime.MinValue,
                EndTime = DateTime.MinValue,
            };

            _playedGameDataBridge.InsertPlayedGame(playedGame);
            group.CurrentlyPlaying = null;
            group.StartTime = null;

            _groupDataBridge.UpdateGroup(group);
        }

        private void OnRequestForceEndGameForGroup(Message message)
        {
            Debug.Assert(message.Value is GroupDataView);

            var gdv = (GroupDataView)message.Value;

            var group = _groupDataBridge.GetGroup(gdv.Id);

            ForceEndGame(group);
        }

        private void OnRequestPlayedGames(Message message)
        {
            Debug.Assert(message.Value is long);
            var id = (long)message.Value;

            var group = new Group
            {
                Id = id,
            };

            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);

            _adminGameView.ShowPlayedGames(playedGames);
        }

        private void OnRequestTimeoutCheck(Message message)
        {
            Debug.Assert(message.Value is null);

            var allGroups = _groupDataBridge.GetAllGroups();

            foreach (var group in allGroups)
            {
                if (!group.StartTime.HasValue)
                {
                    continue;
                }

                var game = _gameDataBridge.GetGameBeingPlayedBy(group);

                if (game == null)
                {
                    continue;
                }

                var now = DateTime.Now;

                var deadline = group.StartTime.Value + game.Timeout;

                if (now >= deadline)
                {
                    ForceEndGame(group);
                }
            }

            Task.Delay(GameTimeoutCheckInterval).ContinueWith(t => _messageSender.Send(new Message("RequestGameTimeoutCheck", null)));
        }
    }
}
