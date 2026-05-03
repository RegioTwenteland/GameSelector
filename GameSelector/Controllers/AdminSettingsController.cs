using GameSelector.Model;
using GameSelector.Views.AdminSettingsView;
using GameSelector.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameSelector.Controllers
{
    internal class AdminSettingsController : AbstractController
    {
        private GameState _gameState;
        private AdminSettingsViewAdapter _adminSettingsView;
        private readonly WebEventDataBridge _webEventDataBridge;

        public AdminSettingsController(
            AdminSettingsViewAdapter adminSettingsView,
            WebEventDataBridge webEventDataBridge,
            IGameDataBridge gameDataBridge,
            GameState gameState)
        {
            _adminSettingsView = adminSettingsView;
            _webEventDataBridge = webEventDataBridge;
            _gameState = gameState;

            _gameState.StateChanged += OnGameStateChanged;

            SetMessageHandlers(new Dictionary<string, Action<Message>>
            {
                { "RequestStartStopGame", OnRequestStartStopGame },
                { "RequestImportGames", OnRequestImportGames },
            });
        }

        public override void Start(Action<object> stop)
        {
            _adminSettingsView.ShowGameTimeout(GlobalSettings.GameTimeoutMinutes);
            _adminSettingsView.ShowAnimationLength(GlobalSettings.AnimationLengthMilliseconds);

            _gameState.CurrentState = GameState.State.Paused;
        }

        private void OnGameStateChanged(object sender, EventArgs e)
        {
            if (_gameState.CurrentState == GameState.State.Paused)
            {
                _adminSettingsView.ShowGamePaused();
            }
            else
            {
                _adminSettingsView.ShowGameRunning();
            }
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

        public void OnRequestImportGames(Message message)
        {
            Debug.Assert(message.Value is null);

            var possibleEvents = _webEventDataBridge.GetEvents();

            _adminSettingsView.SelectEventFromList(possibleEvents);
        }
    }
}
