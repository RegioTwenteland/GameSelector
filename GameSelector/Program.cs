using GameSelector.Views;
using System.Collections.Concurrent;
using System;
using System.Threading;
using System.Collections.Generic;
using GameSelector.Controllers;
using GameSelector.Model;
using GameSelector.Database;

namespace GameSelector
{
    internal static class Program
    {
        private static BlockingCollection<Message> _messages = new BlockingCollection<Message>();
        private static CancellationTokenSource _messageCancellationTokenSource = new CancellationTokenSource();
        private static CancellationToken _messageCancellationToken;

        private static List<AbstractController> _controllers = new List<AbstractController>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var database = CreateDatabase();
            var cardDataBridge = new CardDataBridge(database);
            var gameDataBride = new GameDataBridge(database, cardDataBridge);

            var nfcDataBridge = new NfcDataBridge();
            var adminView = new AdminViewAdapter(_messages);
            var userInputView = new UserInputView(_messages);
            var userView = new UserViewAdapter(_messages);
            _controllers.Add(new AdminController(nfcDataBridge, adminView, userInputView, cardDataBridge, gameDataBride));
            _controllers.Add(new UserController(nfcDataBridge, userInputView, userView, cardDataBridge, gameDataBride));

            foreach (var controller in _controllers)
            {
                controller.Run(Stop);
            }

            _messageCancellationToken = _messageCancellationTokenSource.Token;

            while (!_messages.IsCompleted)
            {
                try
                {
                    var message = _messages.Take(_messageCancellationToken);

                    foreach (var controller in _controllers)
                    {
                        controller.HandleMessage(message);
                    }
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

        private const string DB_FILE_NAME = "..\\..\\data.sqlite";

        static IDatabase CreateDatabase()
        {
            return new SQLiteDatabase($"Data Source={DB_FILE_NAME}");
        }
    }

    

}
