using System.Collections.Concurrent;
using System;
using System.Threading;
using System.Collections.Generic;
using GameSelector.Controllers;

namespace GameSelector
{
    public static class Program
    {
        public static event EventHandler ProgramTerminate;

        private static List<Action> _terminateActions = new List<Action>();

        private static BlockingCollection<Message> _messages;
        private static CancellationTokenSource _messageCancellationTokenSource = new CancellationTokenSource();
        private static CancellationToken _messageCancellationToken;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                ObjectManager.Model.SetDataSource(args[0]);
            }

            var controllers = new List<AbstractController>
            {
                ObjectManager.UserController,
                ObjectManager.AdminController,
                ObjectManager.AdminGroupController,
                ObjectManager.AdminGameController,
            };

            foreach (var controller in controllers)
            {
                controller.Start(Stop);
            }

            _messages = ObjectManager.MessageCollection;
            _messageCancellationToken = _messageCancellationTokenSource.Token;

            while (!_messages.IsCompleted)
            {
                try
                {
                    var message = _messages.Take(_messageCancellationToken);

                    foreach (var controller in controllers)
                    {
                        controller.HandleMessage(message);
                    }

                    message.AssertConsumed();
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
            }

            foreach (var terminateAction in _terminateActions)
            {
                terminateAction();
            }
        }

        public static void RegisterTerminateAction(Action action)
        {
            _terminateActions.Add(action);
        }

        static void Stop()
        {
            _messages.CompleteAdding();
            _messageCancellationTokenSource.Cancel();
        }
    }
}
