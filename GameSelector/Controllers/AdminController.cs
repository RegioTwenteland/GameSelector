using GameSelector.Model;
using GameSelector.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameSelector.Controllers
{
    internal class AdminController : AbstractController
    {
        private AdminViewAdapter _adminView;
        private IGroupDataBridge _groupDataBridge;
        private IGameDataBridge _gameDataBridge;

        public AdminController(
            AdminViewAdapter adminView,
            IGroupDataBridge groupDataBridge,
            IGameDataBridge gameDataBridge
        )
        {
            _adminView = adminView;
            _groupDataBridge = groupDataBridge;
            _gameDataBridge = gameDataBridge;

            SetMessageHandlers(new Dictionary<string, Action<Message>>
            {
                { "CardInserted", OnUserLogin },
                { "CardEjected", m => { } },
                { "ShowAdminError", ShowAdminError },
                { "SaveGameTimeout", OnSaveGameTimeout },
                { "SaveAnimationLength", OnSaveAnimationLength },
                { "Lock", OnLock },
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

        private void OnLock(Message message)
        {
            Debug.Assert(message.Value is null);

            _adminView.HideView();
        }

        private void ShowAdminError(Message message)
        {
            Debug.Assert(message.Value is string);
            ShowAdminError((string)message.Value);
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
