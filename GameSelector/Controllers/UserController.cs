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

        private void OnCardInserted(object value)
        {
            var games = _database.GetGameData();
            var selectedGame = games[_random.Next(games.Count)];

            _userView.ShowGame(selectedGame);
        }
    }
}
