using GameSelector.Controllers;
using GameSelector.Model;
using GameSelector.NfcReader;
using GameSelector.SQLite;
using GameSelector.Views;
using GameSelector.Views.AdminGenericView;
using GameSelector.Views.AdminGroupView;
using System.Collections.Concurrent;
using GameSelector.Views.AdminSettingsView;
using GameSelector.Views.AdminGameView;

namespace GameSelector
{
    internal static class ObjectManager
    {
        private static GameState _gameState;
        private static GameState GameState => _gameState ??= new GameState();

        private static INfcReader _nfcReader;
        private static INfcReader NfcReader => _nfcReader ??= NfcReaderFactory.CreateNfcReader(GlobalSettings.SimulateNfc);

        private static AudioPlayer _audioPlayer;
        private static AudioPlayer AudioPlayer => _audioPlayer ??= new AudioPlayer();

        private static AdminGenericViewAdapter _adminGenericView;
        private static AdminGenericViewAdapter AdminGenericView => _adminGenericView ??= new AdminGenericViewAdapter(MessageSender);

        private static AdminSettingsViewAdapter _adminSettingsView;
        private static AdminSettingsViewAdapter AdminSettingsView => _adminSettingsView ??= new AdminSettingsViewAdapter(MessageSender, AdminGenericView);

        private static AdminGroupViewGridAdapter _adminGroupView;
        private static AdminGroupViewGridAdapter AdminGroupView => _adminGroupView ??= new AdminGroupViewGridAdapter(MessageSender, AdminGenericView);

        private static AdminGameViewAdapter _adminGameView;
        private static AdminGameViewAdapter AdminGameView => _adminGameView ??= new AdminGameViewAdapter(MessageSender, AdminGenericView);

        private static UserIdentificationView _userIdentificationView;
        private static UserIdentificationView UserIdentificationView => _userIdentificationView ??= new UserIdentificationView(MessageSender, NfcReader);

        private static UserViewAdapter _userView;
        private static UserViewAdapter UserView => _userView ??= new UserViewAdapter(MessageSender, AudioPlayer);

        private static IModel _model;
        public static IModel Model => _model ??= new SQLiteModel();

        private static BlockingCollection<Message> _messageCollection;
        public static BlockingCollection<Message> MessageCollection => _messageCollection ??= new BlockingCollection<Message>();

        public static MessageSender _messageSender;
        public static MessageSender MessageSender => _messageSender ??= new MessageSender(MessageCollection);

        private static AdminController _adminController;
        public static AdminController AdminController => _adminController ??= CreateAdminController();

        private static AdminSettingsController _adminSettingsController;
        public static AdminSettingsController AdminSettingsController => _adminSettingsController ??= CreateAdminSettingsController();

        private static AdminGroupController _adminGroupController;
        public static AdminGroupController AdminGroupController => _adminGroupController ??= CreateAdminGroupController();

        private static AdminGameController _adminGameController;
        public static AdminGameController AdminGameController => _adminGameController ??= CreateAdminGameController();

        private static UserController _userController;
        public static UserController UserController => _userController ??= CreateUserController();

        private static AdminController CreateAdminController()
        {
            return new AdminController(
                AdminGenericView,
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
                AdminGenericView,
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
                AdminGenericView,
                AdminGameView,
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
