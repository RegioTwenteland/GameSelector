using System.Collections.Concurrent;
using System;
using System.Threading;
using System.Collections.Generic;
using GameSelector.Controllers;

namespace GameSelector
{
    internal static class Program
    {
        private static BlockingCollection<Message> _messages;
        private static CancellationTokenSource _messageCancellationTokenSource = new CancellationTokenSource();
        private static CancellationToken _messageCancellationToken;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var controllers = new List<AbstractController>
            {
                ControllerFactory.UserController,
                ControllerFactory.AdminController,
                ControllerFactory.AdminGroupController,
                ControllerFactory.AdminGameController,
            };

            foreach (var controller in controllers)
            {
                controller.Start(Stop);
            }

            _messages = ControllerFactory.MessageCollection;
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
        }

        static void Stop()
        {
            _messages.CompleteAdding();
            _messageCancellationTokenSource.Cancel();
        }
    }
}
