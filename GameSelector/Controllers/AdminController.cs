using GameSelector.Model;
using GameSelector.Views;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameSelector.Controllers
{
    internal class AdminController : AbstractController
    {
        private AdminViewAdapter _adminView;
        private UserInputView _userInputView;
        private NfcDataBridge _nfcDataBridge;
        private Database _database;

        public AdminController(
            NfcDataBridge nfcDataBride,
            AdminViewAdapter adminView,
            UserInputView userInputView,
            Database database
        )
        {
            _adminView = adminView;
            _userInputView = userInputView;
            _nfcDataBridge = nfcDataBride;
            _database = database;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "WriteCardData", OnWriteCardData },
                { "CardInserted", OnCardInserted },
                { "CardEjected", OnCardEjected },
                { "RequestGames", OnRequestGames },
                { "RequestGame", OnRequestGame }
            });
        }

        public override void Run(Action stop)
        {
            _adminView.Start(stop);
        }

        private void OnWriteCardData(object value)
        {
            Debug.Assert(value is CardData);

            //_nfcDataBridge.CardData = (CardData)value;
            _database.InsertCard((CardData)value);
        }

        private void OnCardInserted(object value)
        {
            //var cardData = _nfcDataBridge.CardData;
            var card = new CardData
            {
                CardUID = _nfcDataBridge.GetCardUID()
            };

            _database.GetCard(card);
            _adminView.ShowCard(card);
        }

        private void OnCardEjected(object value)
        {
            _adminView.ShowCard(null);
        }

        private void OnRequestGames(object value)
        {
            var games = _database.GetGameData();
            _adminView.SetGamesList(games);
        }

        private void OnRequestGame(object value)
        {
            var code = (string)value;
            var games = _database.GetGameData(code);
            _adminView.ShowGame(games);
        }
    }
}
