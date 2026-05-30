using GameSelector.Model;
using GameSelector.Views;
using GameSelector.Views.AdminGenericView;
using GameSelector.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace GameSelector.Controllers
{
    internal class AdminController : AbstractController
    {
        private AdminGenericViewAdapter _adminView;
        private IGroupDataBridge _groupDataBridge;
        private IGameDataBridge _gameDataBridge;
        private readonly WebEventDataBridge _webEventDataBridge;

        public AdminController(
            AdminGenericViewAdapter adminView,
            IGroupDataBridge groupDataBridge,
            IGameDataBridge gameDataBridge,
            WebEventDataBridge webEventDataBridge
        )
        {
            _adminView = adminView;
            _groupDataBridge = groupDataBridge;
            _gameDataBridge = gameDataBridge;
            _webEventDataBridge = webEventDataBridge;

            SetMessageHandlers(new Dictionary<string, Action<Message>>
            {
                { "CardInserted", OnUserLogin },
                { "CardEjected", m => { } },
                { "ShowAdminError", ShowAdminError },
                { "SaveGameTimeout", OnSaveGameTimeout },
                { "SaveAnimationLength", OnSaveAnimationLength },
                { "ImportGames", OnImportGames },
                { "Lock", OnLock },
            });
        }

        public override void Start(Action<object> stop)
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

        private static Game CombineWebAndLocalGame(Game localGame, Game webGame)
        {
            localGame.Description = webGame.Description;
            localGame.Active = webGame.Active;
            localGame.MaxPlayerAmount = webGame.MaxPlayerAmount;
            return localGame;
        }

        public void OnImportGames(Message message)
        {
            Debug.Assert(message.Value is string);

            var eventName = (string)message.Value;

            var gamesFromWeb = _webEventDataBridge
                .GetWebGameDataBridge(eventName)?
                .GetGames()
                .ToDictionary(g => g.Code, g => g);

            if (gamesFromWeb is null)
            {
                ShowAdminError($"Unable to retrieve games for event {eventName}");
                return;
            }

            var localGames = _gameDataBridge
                .GetAllGames()
                .ToDictionary(g => g.Code, g => g);


            foreach (var (code, webGame) in gamesFromWeb)
            {
                if (localGames.TryGetValue(code, out Game localGame))
                {
                    _gameDataBridge.UpdateGame(CombineWebAndLocalGame(localGame, webGame));
                }
                else
                {
                    _gameDataBridge.InsertGame(webGame);
                }
            }

            // I am not quite sure why, but if I don't run the call from another task it crashes in the view thread
            // even though we are using Invoke() before actually running any UI code. This works though, I guess...
            Task.Run(_adminView.ShowGamesTab);
        }
    }
}
