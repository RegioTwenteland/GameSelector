using GameSelector.Views.StartupView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GameSelector.Controllers
{
    internal class StartupController : AbstractController
    {
        private readonly StartupViewAdapter _view;

        private Action<object> _stopCallback;

        private string _pathToOpen;

        public StartupController(StartupViewAdapter view)
        {
            _view = view;

            SetMessageHandlers(new Dictionary<string, Action<Message>>
            {
                { "RequestOpenFile", OnRequestOpenFile },
                { "RequestNewFile", OnRequestNewFile },
            });
        }

        private void OnRequestNewFile(Message message)
        {
            Debug.Assert(message.Value is string);
            _pathToOpen = (string)message.Value;

            if (File.Exists(_pathToOpen))
            {
                File.Delete(_pathToOpen);
            }

            // Create file, but don't keep it open
            using (File.Create(_pathToOpen)) { }

            _view.Close();
        }

        private void OnRequestOpenFile(Message message)
        {
            Debug.Assert(message.Value is string);
            _pathToOpen = (string)message.Value;
            _view.Close();
        }

        private void OnViewClosed()
        {
            _stopCallback?.Invoke(_pathToOpen);
        }

        public override void Start(Action<object> stop)
        {
            _stopCallback = stop;

            _view.Start(OnViewClosed);
        }
    }
}
