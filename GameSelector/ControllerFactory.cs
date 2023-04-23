using GameSelector.Controllers;
using GameSelector.Model;
using GameSelector.Views;
using NFC;
using System.Collections.Concurrent;

namespace GameSelector
{
    internal static class ControllerFactory
    {
        private static GameState _gameState;
        private static GameState GameState { get => _gameState ?? (_gameState = new GameState()); }

        private static IModel _model;
        private static IModel Model { get => _model ?? (_model = ModelFactory.GetModel()); }

        private static NfcReader _nfcReader;
        private static NfcReader NfcReader { get => _nfcReader ?? (_nfcReader = new NfcReader()); }

        private static AdminViewAdapter _adminView;
        private static AdminViewAdapter AdminView { get => _adminView ?? (_adminView = new AdminViewAdapter(MessageCollection)); }

        private static UserIdentificationView _userIdentificationView;
        private static UserIdentificationView UserIdentificationView { get => _userIdentificationView ?? (_userIdentificationView = new UserIdentificationView(MessageCollection, NfcReader)); }

        private static UserViewAdapter _userView;
        private static UserViewAdapter UserView { get => _userView ?? (_userView = new UserViewAdapter(MessageCollection)); }

        private static BlockingCollection<Message> _messageCollection;
        public static BlockingCollection<Message> MessageCollection { get => _messageCollection ?? (_messageCollection = new BlockingCollection<Message>()); }

        private static AdminController CreateAdminController()
        {
            return new AdminController(
                GameState,
                AdminView,
                UserIdentificationView,
                Model.GroupDataBridge,
                Model.GameDataBridge,
                Model.PlayedGameDataBridge
            );
        }

        private static AdminController _adminController;

        public static AdminController AdminController { get => _adminController ?? (_adminController = CreateAdminController()); }

        private static UserController CreateUserController()
        {
            return new UserController(
                GameState,
                UserIdentificationView,
                UserView,
                Model.GroupDataBridge,
                Model.GameDataBridge,
                Model.PlayedGameDataBridge
            );
        }

        private static UserController _userController;

        public static UserController UserController { get => _userController ?? (_userController = CreateUserController()); }
    }
}
