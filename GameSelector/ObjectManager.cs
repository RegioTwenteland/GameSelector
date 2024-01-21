using GameSelector.Controllers;
using GameSelector.Model;
using GameSelector.SQLite;
using GameSelector.Views;
using NfcReader;
using System.Collections.Concurrent;

namespace GameSelector
{
    internal static class ObjectManager
    {
        private static GameState _gameState;
        private static GameState GameState { get => _gameState ?? (_gameState = new GameState()); }

        private static INfcReader _nfcReader;
        private static INfcReader NfcReader
        {
            get => _nfcReader ??
                (_nfcReader = NfcReaderFactory.CreateNfcReader(GlobalSettings.SimulateNfc, Program.RegisterTerminateAction));
        }

        private static AudioPlayer _audioPlayer;

        private static AudioPlayer AudioPlayer { get => _audioPlayer ?? (_audioPlayer = new AudioPlayer()); }

        private static AdminViewAdapter _adminView;
        private static AdminViewAdapter AdminView { get => _adminView ?? (_adminView = new AdminViewAdapter(MessageCollection)); }

        private static UserIdentificationView _userIdentificationView;
        private static UserIdentificationView UserIdentificationView { get => _userIdentificationView ?? (_userIdentificationView = new UserIdentificationView(MessageCollection, NfcReader)); }

        private static UserViewAdapter _userView;
        private static UserViewAdapter UserView { get => _userView ?? (_userView = new UserViewAdapter(MessageCollection, AudioPlayer)); }

        private static IModel _model;
        public static IModel Model { get => _model ?? (_model = new SQLiteModel()); }

        private static BlockingCollection<Message> _messageCollection;
        public static BlockingCollection<Message> MessageCollection { get => _messageCollection ?? (_messageCollection = new BlockingCollection<Message>()); }

        private static AdminController _adminController;
        public static AdminController AdminController { get => _adminController ?? (_adminController = CreateAdminController()); }

        private static AdminGroupController _adminGroupController;
        public static AdminGroupController AdminGroupController { get => _adminGroupController ?? (_adminGroupController = CreateAdminGroupController()); }

        private static AdminGameController _adminGameController;
        public static AdminGameController AdminGameController { get => _adminGameController ?? (_adminGameController = CreateAdminGameController()); }

        private static UserController _userController;
        public static UserController UserController { get => _userController ?? (_userController = CreateUserController()); }

        private static AdminController CreateAdminController()
        {
            return new AdminController(
                GameState,
                AdminView,
                Model.GroupDataBridge,
                Model.GameDataBridge
            );
        }

        private static AdminGroupController CreateAdminGroupController()
        {
            return new AdminGroupController(
                AdminView,
                UserIdentificationView,
                Model.GroupDataBridge,
                Model.GameDataBridge,
                Model.PlayedGameDataBridge
            );
        }

        private static AdminGameController CreateAdminGameController()
        {
            return new AdminGameController(
                AdminView,
                Model.GameDataBridge,
                Model.GroupDataBridge,
                Model.PlayedGameDataBridge
            );
        }

        private static UserController CreateUserController()
        {
            return new UserController(
                GameState,
                UserIdentificationView,
                UserView,
                NfcReader,
                Model.GroupDataBridge,
                Model.GameDataBridge,
                Model.PlayedGameDataBridge
            );
        }
    }
}
