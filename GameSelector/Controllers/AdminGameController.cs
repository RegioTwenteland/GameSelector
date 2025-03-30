using GameSelector.Model;
using GameSelector.Views;
using GameSelector.Views.AdminGameView;
using GameSelector.Views.AdminGenericView;
using GameSelector.Views.AdminGroupView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.Core;

namespace GameSelector.Controllers
{
    internal class AdminGameController : AbstractController
    {
        private AdminGenericViewAdapter _adminGenericView;
        private AdminGameViewAdapter _adminGameView;
        private IGameDataBridge _gameDataBridge;
        private IGroupDataBridge _groupDataBridge;
        private IPlayedGameDataBridge _playedGameDataBridge;
        private MessageSender _messageSender;

        private readonly TimeSpan GameTimeoutCheckInterval = TimeSpan.FromMinutes(1);

        public AdminGameController(
            AdminGenericViewAdapter adminGenericView,
            AdminGameViewAdapter adminGameView,
            IGameDataBridge gameDataBridge,
            IGroupDataBridge groupDataBridge,
            IPlayedGameDataBridge playedGameDataBridge,
            MessageSender messageSender
        )
        {
            _adminGenericView = adminGenericView;
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
                { "RequestGameTimeoutCheck", OnRequestTimeoutCheck },
            });
        }

        private void OnGameUpdated(object sender, GameUpdatedEventArgs e)
        {
            var gdv = GameDataView.FromGame(e.Game);
            _adminGameView.UpdateGame(gdv);
        }

        public override void Start(Action<object> stop)
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
            game.Category = gameDataView.Category;
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
            Debug.Assert(message.Value is GameDataView);

            var gdv = message.Value as GameDataView;

            var newGame = new Game
            {
                Code = gdv.Code ?? string.Empty,
                Description = gdv.Description ?? string.Empty,
                Category = gdv.Category ?? string.Empty,
                Active = gdv.Active,
                Priority = gdv.Priority,
                Remarks = gdv.Remarks ?? string.Empty,
                Timeout = gdv.TimeoutMinutes <= 0 ? TimeSpan.FromMinutes(15) : new TimeSpan(gdv.TimeoutMinutes),
                MaxPlayerAmount = Math.Max(1, gdv.MaxPlayerAmount),
            };

            _gameDataBridge.InsertGame(newGame); // populates ID field

            _adminGameView.NewGame(GameDataView.FromGame(newGame));
        }

        private void OnRequestDeleteGame(Message message)
        {
            Debug.Assert(message.Value is GameDataView);

            var gameDataView = (GameDataView)message.Value;

            var game = _gameDataBridge.GetGame(gameDataView.Id);

            // Do some sanity checks:
            if (_groupDataBridge.GetAllGroupsPlaying(game).Any())
            {
                _adminGenericView.ShowError("Verwijderen mislukt: Spel wordt momenteel gespeeld");
                return;
            }

            var playedGames = _playedGameDataBridge.GetPlayedGamesByGame(game);
            if (playedGames.Any())
            {
                _adminGenericView.ShowError("Verwijderen mislukt: Spel is al gespeeld");
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
