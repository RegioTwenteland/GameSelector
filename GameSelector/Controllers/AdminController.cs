using GameSelector.Model;
using GameSelector.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.Controllers
{
    internal class AdminController : AbstractController
    {
        private GameState _gameState;
        private AdminViewAdapter _adminView;
        private UserIdentificationView _userIdentificationView;
        private IGroupDataBridge _groupDataBridge;
        private IGameDataBridge _gameDataBridge;

        public AdminController(
            GameState gameState,
            AdminViewAdapter adminView,
            UserIdentificationView userIdentificationView,
            IGroupDataBridge groupDataBridge,
            IGameDataBridge gameDataBridge
        )
        {
            _gameState = gameState;
            _adminView = adminView;
            _userIdentificationView = userIdentificationView;
            _groupDataBridge = groupDataBridge;
            _gameDataBridge = gameDataBridge;

            _gameState.StateChanged += OnGameStateChanged;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "WriteCardData", OnWriteCardData },
                { "UserLogin", OnUserLogin },
                { "RequestGames", OnRequestGames },
                { "RequestGame", OnRequestGame },
                { "ShowAdminError", ShowAdminError },
                { "RequestStartStopGame", OnRequestStartStopGame }
            });
        }

        public override void Start(Action stop)
        {
            _adminView.Start(stop);

            _gameState.CurrentState = GameState.State.Paused;
        }

        public void ShowAdminError(string message)
        {
            _adminView.ShowError(message);
        }

        private void OnGameStateChanged(object sender, EventArgs e)
        {
            if (_gameState.CurrentState == GameState.State.Paused)
            {
                _adminView.ShowGamePaused();
            }
            else
            {
                _adminView.ShowGameRunning();
            }
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

        private void OnRequestStartStopGame(object value)
        {
            Debug.Assert(value is null);

            if (_gameState.CurrentState == GameState.State.Paused)
            {
                _gameState.CurrentState = GameState.State.Playing;
            }
            else
            {
                _gameState.CurrentState = GameState.State.Paused;
            }
        }
    }
}
