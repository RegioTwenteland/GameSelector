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
using GameSelector.Views.AdminPlayedGameView;
using System.Collections.Generic;

namespace GameSelector
{
    internal class ApplicationManager
    {
        private readonly BlockingCollection<Message> _messageCollection;

        public ApplicationManager(BlockingCollection<Message> messageCollection)
        {
            _messageCollection = messageCollection;
        }

        private GameState _gameState;
        private GameState GameState => _gameState ??= new GameState();

        private INfcReader _nfcReader;
        private INfcReader NfcReader => _nfcReader ??= NfcReaderFactory.CreateNfcReader(GlobalSettings.SimulateNfc);

        private AudioPlayer _audioPlayer;
        private AudioPlayer AudioPlayer => _audioPlayer ??= new AudioPlayer();

        private AdminGenericViewAdapter _adminGenericView;
        private AdminGenericViewAdapter AdminGenericView => _adminGenericView ??= new AdminGenericViewAdapter(
            MessageSender,
            AdminSettingsView,
            AdminGroupView,
            AdminGameView,
            AdminPlayedGameView);

        private AdminSettingsViewAdapter _adminSettingsView;
        private AdminSettingsViewAdapter AdminSettingsView => _adminSettingsView ??= new AdminSettingsViewAdapter(MessageSender);

        private AdminGroupViewAdapter _adminGroupView;
        private AdminGroupViewAdapter AdminGroupView => _adminGroupView ??= new AdminGroupViewAdapter(MessageSender);

        private AdminGameViewAdapter _adminGameView;
        private AdminGameViewAdapter AdminGameView => _adminGameView ??= new AdminGameViewAdapter(MessageSender);

        private AdminPlayedGameViewAdapter _adminPlayedGameView;
        private AdminPlayedGameViewAdapter AdminPlayedGameView => _adminPlayedGameView ??= new AdminPlayedGameViewAdapter(MessageSender);

        private UserIdentificationView _userIdentificationView;
        private UserIdentificationView UserIdentificationView => _userIdentificationView ??= new UserIdentificationView(MessageSender, NfcReader);

        private UserViewAdapter _userView;
        private UserViewAdapter UserView => _userView ??= new UserViewAdapter(MessageSender, AudioPlayer);

        private IModel _model;
        public IModel Model => _model ??= new SQLiteModel();

        public MessageSender _messageSender;
        public MessageSender MessageSender => _messageSender ??= new MessageSender(_messageCollection);

        public IEnumerable<AbstractController> Controllers =>
        [
            new AdminController(
                AdminGenericView,
                Model.GroupDataBridge,
                Model.GameDataBridge),

            new AdminSettingsController(
                AdminSettingsView,
                GameState),

            new AdminGroupController(
                AdminGenericView,
                AdminGroupView,
                UserIdentificationView,
                Model.GroupDataBridge,
                Model.GameDataBridge,
                Model.PlayedGameDataBridge),

            new AdminGameController(
                AdminGenericView,
                AdminGameView,
                Model.GameDataBridge,
                Model.GroupDataBridge,
                Model.PlayedGameDataBridge,
                MessageSender),

            new AdminPlayedGameController(
                AdminPlayedGameView,
                Model.PlayedGameDataBridge),

            new UserController(
                GameState,
                UserIdentificationView,
                UserView,
                NfcReader,
                Model.GroupDataBridge,
                Model.GameDataBridge,
                Model.PlayedGameDataBridge)
        ];
    }
}
