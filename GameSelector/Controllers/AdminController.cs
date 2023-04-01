using GameSelector.Model;
using GameSelector.Views;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.Controllers
{
    internal class AdminController : AbstractController
    {
        private AdminViewAdapter _adminView;
        private UserInputView _userInputView;
        private NfcDataBridge _nfcDataBridge;
        private CardDataBridge _cardDataSource;
        private GameDataBridge _gameDataBridge;

        public AdminController(
            NfcDataBridge nfcDataBride,
            AdminViewAdapter adminView,
            UserInputView userInputView,
            CardDataBridge cardDataSource,
            GameDataBridge gameDataBridge
        )
        {
            _adminView = adminView;
            _userInputView = userInputView;
            _nfcDataBridge = nfcDataBride;
            _cardDataSource = cardDataSource;
            _gameDataBridge = gameDataBridge;

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
            Debug.Assert(value is CardDataView);

            var cardView = (CardDataView)value;

            var card = _cardDataSource.GetCard(cardView.Id);

            if (card == null)
            {
                // new card
                card = new Card
                {
                    Id = cardView.Id,
                    GroupId = cardView.GroupId,
                    ScoutingName = cardView.ScoutingName,
                };

                _cardDataSource.InsertCard(card);
            }
            else
            {
                // existing card
                card.GroupId = cardView.GroupId;
                card.ScoutingName = cardView.ScoutingName;

                _cardDataSource.UpdateCard(card);
            }
        }

        private void OnCardInserted(object value)
        {
            //var cardData = _nfcDataBridge.CardData;
            var cardUid = _nfcDataBridge.GetCardUID();
            var card = _cardDataSource.GetCard(cardUid);


            CardDataView cardView;

            if (card == null)
            {
                cardView = new CardDataView
                {
                    Id = cardUid,
                    StartTime = DateTime.Now,
                };
            }
            else
            {
                cardView = CardDataView.FromCard(card);
            }

            _adminView.ShowCard(cardView);
        }

        private void OnCardEjected(object value)
        {
            _adminView.ShowCard(null);
        }

        private void OnRequestGames(object value)
        {
            var games = _gameDataBridge.GetAllGames().Select(g => GameDataView.FromGame(g));
            _adminView.SetGamesList(games);
        }

        private void OnRequestGame(object value)
        {
            Debug.Assert(value is long);
            var id = (long)value;

            var game = GameDataView.FromGame(_gameDataBridge.GetGame(id));

            _adminView.ShowGame(game);
        }
    }
}
