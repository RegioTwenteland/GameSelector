using GameSelector.Model;
using GameSelector.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.Controllers
{
    internal class AdminGameController : AbstractController
    {
        private AdminViewAdapter _adminView;
        private IGameDataBridge _gameDataBridge;
        private IGroupDataBridge _groupDataBridge;
        private IPlayedGameDataBridge _playedGameDataBridge;

        public AdminGameController(
            AdminViewAdapter adminView,
            IGameDataBridge gameDataBridge,
            IGroupDataBridge groupDataBridge,
            IPlayedGameDataBridge playedGameDataBridge
        )
        {
            _adminView = adminView;
            _gameDataBridge = gameDataBridge;
            _groupDataBridge = groupDataBridge;
            _playedGameDataBridge = playedGameDataBridge;

            _gameDataBridge.GameUpdated += OnGameUpdated;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "RequestSaveGame", OnRequestSaveGame },
                { "RequestNewGame", OnRequestNewGame },
                { "RequestDeleteGame", OnRequestDeleteGame },
                { "RequestForceEndGame", OnRequestForceEndGame },
                { "RequestPlayedGames", OnRequestPlayedGames }
            });
        }

        private void OnGameUpdated(object sender, GameUpdatedEventArgs e)
        {
            var gdv = GameDataView.FromGame(e.Game);
            _adminView.UpdateGame(gdv);
            _adminView.SetGameSelected(gdv);
        }

        public override void Start(Action stop)
        {
            UpdateGamesList(_gameDataBridge.GetAllGames());
        }

        private void UpdateGamesList(List<Game> games)
        {
            var gdv = games.Select(g => GameDataView.FromGame(g));
            _adminView.SetGamesList(gdv);
        }

        private void OnRequestSaveGame(object value)
        {
            Debug.Assert(value is GameDataView);

            var gameDataView = (GameDataView)value;

            var game = _gameDataBridge.GetGame(gameDataView.Id);

            game.Code = gameDataView.Code;
            game.Description = gameDataView.Description;
            game.Explanation = gameDataView.Explanation;
            game.Active = gameDataView.Active;
            game.Color = gameDataView.Color;
            game.HasPriority = gameDataView.HasPriority;
            game.Remarks = gameDataView.Remarks;
            game.Timeout = TimeSpan.FromMinutes(gameDataView.TimeoutMinutes);

            _gameDataBridge.UpdateGame(game);

            _adminView.UpdateGame(gameDataView);
        }

        private void OnRequestNewGame(object value)
        {
            Debug.Assert(value is null);

            var newGame = new Game
            {
                Code = "Nieuw",
                Description = "Spel",
                Explanation = string.Empty,
                Active = true,
                Color = string.Empty
            };

            _gameDataBridge.InsertGame(newGame);

            var games = _gameDataBridge.GetAllGames().Select(g => GameDataView.FromGame(g));
            _adminView.SetGamesList(games);
            _adminView.SetGameSelected(games.First(g => g.Code == "Nieuw"));
        }

        private void OnRequestDeleteGame(object value)
        {
            Debug.Assert(value is GameDataView);

            var gameDataView = (GameDataView)value;

            var game = _gameDataBridge.GetGame(gameDataView.Id);

            // Do some sanity checks:
            if (game.OccupiedBy != null)
            {
                _adminView.ShowError("Verwijderen mislukt: Spel wordt momenteel gespeeld");
                return;
            }

            var playedGames = _playedGameDataBridge.GetPlayedGamesByGame(game);
            if (playedGames.Count > 0)
            {
                _adminView.ShowError("Verwijderen mislukt: Spel is al gespeeld");
                return;
            }

            _gameDataBridge.DeleteGame(game);

            UpdateGamesList(_gameDataBridge.GetAllGames());
        }

        private void OnRequestForceEndGame(object value)
        {
            Debug.Assert(value is GameDataView);

            var gdv = (GameDataView)value;

            var group = _groupDataBridge.GetGroup(gdv.OccupiedBy.Id);
            var game = _gameDataBridge.GetGame(gdv.Id);

            var playedGame = new PlayedGame
            {
                Player = group,
                Game = game,
                StartTime = game.StartTime ?? DateTime.MinValue,
                EndTime = DateTime.MinValue,
            };

            _playedGameDataBridge.InsertPlayedGame(playedGame);

            game.OccupiedBy = null;
            game.StartTime = null;

            _gameDataBridge.UpdateGame(game);
        }

        private void OnRequestPlayedGames(object value)
        {
            Debug.Assert(value is long);
            var id = (long)value;

            var group = new Group
            {
                Id = id,
            };

            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);

            _adminView.ShowPlayedGames(playedGames);
        }
    }
}
