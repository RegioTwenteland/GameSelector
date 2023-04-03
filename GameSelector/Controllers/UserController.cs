using GameSelector.Model;
using GameSelector.Views;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameSelector.Controllers
{
    internal class UserController : AbstractController
    {
        private UserViewAdapter _userView;
        private UserIdentificationView _userIdentificationView;
        private CardDataBridge _cardDataBridge;
        private GameDataBridge _gameDataBridge;

        private Random _random = new Random();

        public UserController(
            UserIdentificationView userIdentificationView,
            UserViewAdapter userView,
            CardDataBridge cardDataBridge,
            GameDataBridge gameDataBridge
        )
        {
            _userIdentificationView = userIdentificationView;
            _userView = userView;
            _cardDataBridge = cardDataBridge;
            _gameDataBridge = gameDataBridge;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "UserLogin", OnUserLogin }
            });
        }

        public override void Start(Action stop)
        {
            _userView.Start(stop);
        }

        private List<Game> GetPossibleGames(Card card)
        {
            var games = _gameDataBridge.GetAllGames();
            var playedGames = _gameDataBridge.GetGamesPlayedBy(card);

            var playedGameIds = new HashSet<long>();
            foreach (var game in playedGames)
            {
                playedGameIds.Add(game.Id);
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

        private void OnUserLogin(object value)
        {
            Debug.Assert(value is string);

            var cardId = (string)value;

            var card = _cardDataBridge.GetCard(cardId);

            if (card == null)
            {
                return;
            }

            var possibleGames = GetPossibleGames(card);

            Game selectedGame = null;
            if (possibleGames.Count > 0)
            {
                // list is already sorted by priority by data bridge
                selectedGame = possibleGames[0];

                selectedGame.StartTime = DateTime.Now;
                selectedGame.OccupiedBy = card;

                card.CurrentGame = selectedGame;

                _cardDataBridge.UpdateCard(card);
            }

            _userView.ShowGame(GameDataView.FromGame(selectedGame));
        }
    }
}
