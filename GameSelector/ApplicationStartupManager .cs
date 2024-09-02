using GameSelector.Controllers;
using System.Collections.Concurrent;
using GameSelector.Views.StartupView;
using System.Collections.Generic;

namespace GameSelector
{
    internal class ApplicationStartupManager
    {
        private readonly BlockingCollection<Message> _messageCollection;

        public ApplicationStartupManager(BlockingCollection<Message> messageCollection)
        {
            _messageCollection = messageCollection;
        }

        public MessageSender _messageSender;
        public MessageSender MessageSender => _messageSender ??= new MessageSender(_messageCollection);

        private StartupViewAdapter _startupView;
        private StartupViewAdapter StartupViewAdapter => _startupView ??= new StartupViewAdapter(MessageSender);

        private StartupController _startupController;

        public StartupController StartupController => _startupController ??= new StartupController(StartupViewAdapter);

        public IEnumerable<AbstractController> Controllers =>
        [
            StartupController
        ];
    }
}
