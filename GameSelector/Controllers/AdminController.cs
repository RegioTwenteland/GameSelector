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
        private UserIdentificationView _userIdentificationView;
        private GroupDataBridge _groupDataBridge;
        private GameDataBridge _gameDataBridge;

        public AdminController(
            AdminViewAdapter adminView,
            UserIdentificationView userIdentificationView,
            GroupDataBridge groupDataBridge,
            GameDataBridge gameDataBridge
        )
        {
            _adminView = adminView;
            _userIdentificationView = userIdentificationView;
            _groupDataBridge = groupDataBridge;
            _gameDataBridge = gameDataBridge;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "WriteCardData", OnWriteCardData },
                { "UserLogin", OnUserLogin },
                { "RequestGames", OnRequestGames },
                { "RequestGame", OnRequestGame },
                { "ShowAdminError", ShowAdminError },
            });
        }

        public override void Start(Action stop)
        {
            _adminView.Start(stop);
        }

        public void ShowAdminError(string message)
        {
            _adminView.ShowError(message);
        }

        private void OnWriteCardData(object value)
        {
            Debug.Assert(value is GroupDataView);

            var cardView = (GroupDataView)value;

            var group = _groupDataBridge.GetGroup(cardView.Id);

            if (group == null)
            {
                // new card
                group = new Group
                {
                    Id = cardView.Id,
                    CardId = cardView.CardId,
                    Name = cardView.GroupName,
                    ScoutingName = cardView.ScoutingName,
                };

                _groupDataBridge.InsertGroup(group);
            }
            else
            {
                // existing card
                group.CardId = cardView.CardId;
                group.Name = cardView.GroupName;
                group.ScoutingName = cardView.ScoutingName;

                _groupDataBridge.UpdateGroup(group);
            }
        }

        private void OnUserLogin(object value)
        {
            Debug.Assert(value is string);

            var cardId = (string)value;
            var group = _groupDataBridge.GetGroup(cardId);


            GroupDataView groupView = null;

            if (group != null)
            {
                groupView = GroupDataView.FromGroup(group);
            }

            _adminView.ShowGroup(groupView);
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

        private void ShowAdminError(object value)
        {
            Debug.Assert(value is string);
            ShowAdminError((string)value);
        }
    }
}
