using GameSelector.Model;
using GameSelector.Views;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GameSelector.Controllers
{
    internal class AdminController : AbstractController
    {
        private GameState _gameState;
        private AdminViewAdapter _adminView;
        private IGroupDataBridge _groupDataBridge;
        private IGameDataBridge _gameDataBridge;

        public AdminController(
            GameState gameState,
            AdminViewAdapter adminView,
            IGroupDataBridge groupDataBridge,
            IGameDataBridge gameDataBridge
        )
        {
            _gameState = gameState;
            _adminView = adminView;
            _groupDataBridge = groupDataBridge;
            _gameDataBridge = gameDataBridge;

            _gameState.StateChanged += OnGameStateChanged;
            _gameDataBridge.GameUpdated += OnGameUpdated;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "CardInserted", OnUserLogin },
                { "CardEjected", o => { } },
                { "ShowAdminError", ShowAdminError },
                { "RequestStartStopGame", OnRequestStartStopGame },
                { "SaveGameTimeout", OnSaveGameTimeout }
            });
        }

        private void OnGameUpdated(object sender, GameUpdatedEventArgs e)
        {
            _adminView.UpdateGroup(GroupDataView.FromGroup(e.Game.OccupiedBy));
        }

        public override void Start(Action stop)
        {
            _adminView.Start(stop);

            _adminView.ShowGameTimeout(GlobalSettings.GameTimeoutMinutes);

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

        private void OnUserLogin(object value)
        {
            Debug.Assert(value is string);

            var cardId = (string)value;
            var group = _groupDataBridge.GetGroup(cardId);


            GroupDataView groupView = null;

            if (group != null)
            {
                groupView = GroupDataView.FromGroup(group);

                if (group.IsAdmin)
                {
                    _adminView.ShowView();
                }
            }

            _adminView.SetGroupSelected(groupView);
            _adminView.ShowLastScannedCardId(cardId);
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

        private void OnSaveGameTimeout(object value)
        {
            Debug.Assert(value is int);

            GlobalSettings.GameTimeoutMinutes = (int)value;
        }
    }
}
