using System.Collections.Concurrent;
using GameSelector;
using GameSelector.Controllers;

namespace Test
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var tempPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location);
            var tempFile = Path.Join(tempPath, "data.sqlite");

            Directory.CreateDirectory(tempPath ?? throw new Exception());
            // Create file, but don't keep it open
            using (File.Create(tempFile)) { }

            var messageCollection = new BlockingCollection<Message>();
            var appManager = new TestApplicationManager(messageCollection);
            appManager.Model.SetDataSource(tempFile);

            StartControllers(appManager.Controllers, messageCollection);
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
