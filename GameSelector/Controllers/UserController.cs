using External;
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
        private UserInputView _userInputView;
        private NfcDataBridge _nfcDataBridge;
        private Database _database;

        private bool _gameStarted = false;

        private Random _random = new Random();

        public UserController(
            NfcDataBridge nfcDataBride,
            UserInputView userInputView,
            UserViewAdapter userView,
            Database database
        )
        {
            _nfcDataBridge = nfcDataBride;
            _userInputView = userInputView;
            _userView = userView;
            _database = database;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "CardInserted", OnCardInserted }
            });
        }

        public override void Run(Action stop)
        {
            _userView.Start(stop);
        }

        private List<GameData> GetPossibleGames(CardData card)
        {
            var games = _database.GetGameData();
            var playedGames = _database.GetGamesPlayed(card);

            var playedGameIds = new HashSet<uint>();
            foreach (var game in playedGames)
            {
                playedGameIds.Add(game.Id);
            }

            var possibleGames = new List<GameData>(games.Count);
            foreach (var game in games)
            {
                if (!playedGameIds.Contains(game.Id))
                {
                    possibleGames.Add(game);
                }
            }

            return possibleGames;
        }

        private void OnCardInserted(object value)
        {
            var card = new CardData
            {
                CardUID = _nfcDataBridge.GetCardUID()
            };

            var cardIsKnown = _database.GetCard(card);

            if (!cardIsKnown)
            {
                return;
            }

            var possibleGames = GetPossibleGames(card);

            GameData selectedGame = null;
            if (possibleGames.Count > 0)
            {
                // list is already sorted by priority by data source
                selectedGame = possibleGames[0];
                selectedGame.OccupiedBy = card;
                _database.UpdateGame(selectedGame);
            }

            _userView.ShowGame(selectedGame);
        }
    }
}
