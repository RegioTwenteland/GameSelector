using System.Collections.Concurrent;
using System;
using System.Threading;
using System.Collections.Generic;
using GameSelector.Controllers;
using System.Windows.Forms;

namespace GameSelector
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            BlockingCollection<Message> messageCollection;

            string dataSource;
            if (args.Length > 0)
            {
                dataSource = args[0];
            }
            else
            {
                messageCollection = new BlockingCollection<Message>();
                var appStartManager = new ApplicationStartupManager(messageCollection);

                var returnValue = StartControllers(appStartManager.Controllers, messageCollection);

                dataSource = (string)returnValue;
            }

            if (dataSource is not null)
            {
                messageCollection = new BlockingCollection<Message>();
                var appManager = new ApplicationManager(messageCollection);
                appManager.Model.SetDataSource(dataSource);

                StartControllers(appManager.Controllers, messageCollection);
            }
        }

        private static object _returnedParam = null;

        private static object StartControllers(IEnumerable<AbstractController> controllers, BlockingCollection<Message> messages)
        {
            CancellationTokenSource messageCancellationTokenSource = new CancellationTokenSource();
            CancellationToken messageCancellationToken = messageCancellationTokenSource.Token;

            Action<object> stopFunction = (object param) =>
            {
                if (param is not null)
                {
                    _returnedParam = param;
                }

                messages.CompleteAdding();
                messageCancellationTokenSource.Cancel();
            };

            foreach (var controller in controllers)
            {
                controller.Start(stopFunction);
            }

            while (!messages.IsCompleted)
            {
                try
                {
                    var message = messages.Take(messageCancellationToken);

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

            return _returnedParam;
        }
    }
}
