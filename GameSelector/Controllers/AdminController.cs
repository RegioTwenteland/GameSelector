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

            SetMessageHandlers(new Dictionary<string, Action<Message>>
            {
                { "CardInserted", OnUserLogin },
                { "CardEjected", m => { } },
                { "ShowAdminError", ShowAdminError },
                { "RequestStartStopGame", OnRequestStartStopGame },
                { "SaveGameTimeout", OnSaveGameTimeout },
                { "SaveAnimationLength", OnSaveAnimationLength },
            });
        }

        public override void Start(Action stop)
        {
            _adminView.Start(stop);

            _adminView.ShowGameTimeout(GlobalSettings.GameTimeoutMinutes);
            _adminView.ShowAnimationLength(GlobalSettings.AnimationLengthMilliseconds);

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

        private void OnUserLogin(Message message)
        {
            Debug.Assert(message.Value is string);

            var cardId = (string)message.Value;
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

        private void ShowAdminError(Message message)
        {
            Debug.Assert(message.Value is string);
            ShowAdminError((string)message.Value);
        }

        private void OnRequestStartStopGame(Message message)
        {
            Debug.Assert(message.Value is null);

            if (_gameState.CurrentState == GameState.State.Paused)
            {
                _gameState.CurrentState = GameState.State.Playing;
            }
            else
            {
                _gameState.CurrentState = GameState.State.Paused;
            }
        }

        private void OnSaveGameTimeout(Message message)
        {
            Debug.Assert(message.Value is int);

            GlobalSettings.GameTimeoutMinutes = (int)message.Value;
        }

        private void OnSaveAnimationLength(Message message)
        {
            Debug.Assert(message.Value is int);

            GlobalSettings.AnimationLengthMilliseconds = (int)message.Value;
        }
    }
}
