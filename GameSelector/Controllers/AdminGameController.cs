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
                { "RequestGames", OnRequestGames },
                { "RequestSaveGame", OnRequestSaveGame },
                { "RequestNewGame", OnRequestNewGame },
                { "RequestIncreasePrio", OnRequestIncreasePrio },
                { "RequestDecreasePrio", OnRequestDecreasePrio },
                { "RequestDeleteGame", OnRequestDeleteGame },
            });
        }

        public override void Start(Action stop)
        {
        }

        private void UpdateGamesList(List<Game> games)
        {
            var gdv = games.Select(g => GameDataView.FromGame(g));
            _adminView.SetGamesList(gdv);
        }

        private void OnRequestGames(object value)
        {
            Debug.Assert(value is null);
            UpdateGamesList(_gameDataBridge.GetAllGames());
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
            game.Priority = gameDataView.Priority;

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

        private List<Game> ChangePrio(GameDataView gdv, int offset)
        {
            var games = _gameDataBridge.GetAllGames().OrderBy(g => g.Priority).ToList();
            var idx = games
                .Select((g, i) => new { game = g, index = i })
                .Single(o => o.game.Id == gdv.Id)
                .index;

            if (idx + offset >= games.Count || idx + offset < 0) return games;

            (games[idx], games[idx + offset]) = (games[idx + offset], games[idx]);

            var prioCounter = 0;
            foreach (var game in games)
            {
                game.Priority = prioCounter++;
                _gameDataBridge.UpdateGame(game);
            }

            return games;
        }

        private void OnRequestIncreasePrio(object value)
        {
            Debug.Assert(value is GameDataView);
            var gameDataView = (GameDataView)value;

            var games = ChangePrio(gameDataView, 1);

            UpdateGamesList(games);

            // Select game in the view:
            var updatedGame = _gameDataBridge.GetGame(gameDataView.Id);
            _adminView.UpdateGame(GameDataView.FromGame(updatedGame));
        }

        private void OnRequestDecreasePrio(object value)
        {
            Debug.Assert(value is GameDataView);
            var gameDataView = (GameDataView)value;

            var games = ChangePrio(gameDataView, -1);

            UpdateGamesList(games);

            // Select game in the view:
            var updatedGame = _gameDataBridge.GetGame(gameDataView.Id);
            _adminView.UpdateGame(GameDataView.FromGame(updatedGame));
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
    }
}
