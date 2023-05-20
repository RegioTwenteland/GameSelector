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
        private IPlayedGameDataBridge _playedGameDataBridge;

        public AdminGameController(
            AdminViewAdapter adminView,
            IGameDataBridge gameDataBridge,
            IPlayedGameDataBridge playedGameDataBridge
        )
        {
            _adminView = adminView;
            _gameDataBridge = gameDataBridge;
            _playedGameDataBridge = playedGameDataBridge;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "RequestSaveGame", OnRequestSaveGame },
                { "RequestNewGame", OnRequestNewGame },
                { "RequestDeleteGame", OnRequestDeleteGame },
                { "RequestPlayedGames", OnRequestPlayedGames }
            });
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
            game.Color = gameDataView.Color;
            game.HasPriority = gameDataView.HasPriority;

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
                Color = string.Empty
            };

            _gameDataBridge.InsertGame(newGame);

            var games = _gameDataBridge.GetAllGames().Select(g => GameDataView.FromGame(g));
            _adminView.SetGamesList(games);
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
