using GameSelector.Controllers;
using GameSelector.Model;
using GameSelector.NfcReader;
using GameSelector.SQLite;
using GameSelector.Views;
using GameSelector.Views.AdminScaffoldView;
using GameSelector.Views.AdminGroupView;
using System.Collections.Concurrent;
using GameSelector.Views.AdminSettingsView;

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
                (_nfcReader = NfcReaderFactory.CreateNfcReader(GlobalSettings.SimulateNfc));
        }

        private static AudioPlayer _audioPlayer;

        private static AudioPlayer AudioPlayer { get => _audioPlayer ?? (_audioPlayer = new AudioPlayer()); }

        private static AdminScaffoldViewAdapter _adminView;
        private static AdminScaffoldViewAdapter AdminScaffoldView { get => _adminView ?? (_adminView = new AdminScaffoldViewAdapter(MessageSender)); }

        private static AdminSettingsViewAdapter _adminSettingsView;
        private static AdminSettingsViewAdapter AdminSettingsView { get => _adminSettingsView ?? (_adminSettingsView = new AdminSettingsViewAdapter(MessageSender, AdminScaffoldView)); }

        private static AdminGroupViewAdapter _adminGroupView;
        private static AdminGroupViewAdapter AdminGroupView { get => _adminGroupView ?? (_adminGroupView = new AdminGroupViewAdapter(MessageSender, AdminScaffoldView)); }

        private static UserIdentificationView _userIdentificationView;
        private static UserIdentificationView UserIdentificationView { get => _userIdentificationView ?? (_userIdentificationView = new UserIdentificationView(MessageSender, NfcReader)); }

        private static UserViewAdapter _userView;
        private static UserViewAdapter UserView { get => _userView ?? (_userView = new UserViewAdapter(MessageSender, AudioPlayer)); }

        private static IModel _model;
        public static IModel Model { get => _model ?? (_model = new SQLiteModel()); }

        private static BlockingCollection<Message> _messageCollection;
        public static BlockingCollection<Message> MessageCollection { get => _messageCollection ?? (_messageCollection = new BlockingCollection<Message>()); }

        public static MessageSender _messageSender;
        public static MessageSender MessageSender { get => _messageSender ?? (_messageSender = new MessageSender(MessageCollection)); }

        private static AdminController _adminController;
        public static AdminController AdminController { get => _adminController ?? (_adminController = CreateAdminController()); }

        private static AdminSettingsController _adminSettingsController;
        public static AdminSettingsController AdminSettingsController { get => _adminSettingsController ?? (_adminSettingsController = CreateAdminSettingsController()); }

        private static AdminGroupController _adminGroupController;
        public static AdminGroupController AdminGroupController { get => _adminGroupController ?? (_adminGroupController = CreateAdminGroupController()); }

        private static AdminGameController _adminGameController;
        public static AdminGameController AdminGameController { get => _adminGameController ?? (_adminGameController = CreateAdminGameController()); }

        private static UserController _userController;
        public static UserController UserController { get => _userController ?? (_userController = CreateUserController()); }

        private static AdminController CreateAdminController()
        {
            return new AdminController(
                AdminScaffoldView,
                Model.GroupDataBridge,
                Model.GameDataBridge
            );
        }

        private static AdminSettingsController CreateAdminSettingsController()
        {
            return new AdminSettingsController(
                AdminSettingsView,
                GameState
            );
        }

        private static AdminGroupController CreateAdminGroupController()
        {
            return new AdminGroupController(
                AdminScaffoldView,
                AdminGroupView,
                UserIdentificationView,
                Model.GroupDataBridge,
                Model.GameDataBridge,
                Model.PlayedGameDataBridge
            );
        }

        private static AdminGameController CreateAdminGameController()
        {
            return new AdminGameController(
                AdminScaffoldView,
                Model.GameDataBridge,
                Model.GroupDataBridge,
                Model.PlayedGameDataBridge,
                MessageSender
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
