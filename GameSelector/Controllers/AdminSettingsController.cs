using GameSelector.Views.AdminSettingsView;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameSelector.Controllers
{
    internal class AdminSettingsController : AbstractController
    {
        private GameState _gameState;
        private AdminSettingsViewAdapter _adminSettingsView;

        public AdminSettingsController(
            AdminSettingsViewAdapter adminSettingsView,
            GameState gameState)
        {
            _adminSettingsView = adminSettingsView;
            _gameState = gameState;

            _gameState.StateChanged += OnGameStateChanged;

            SetMessageHandlers(new Dictionary<string, Action<Message>>
            {
                { "RequestStartStopGame", OnRequestStartStopGame },
            });
        }

        public override void Start(Action stop)
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
    }
}
