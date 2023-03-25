using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameSelector.Controllers
{
    internal abstract class AbstractController
    {
        private BlockingCollection<Tuple<string, object>> _messages;
        private Dictionary<string, Action<object>> _messageHandlers;

        private static CancellationTokenSource _messageCancellationTokenSource = new CancellationTokenSource();
        private static CancellationToken _messageCancellationToken;

        protected AbstractController(BlockingCollection<Tuple<string, object>> messages)
        {
            _messages = messages;
        }

        protected void SetMessageHandlers(Dictionary<string, Action<object>> messageHandlers)
        {
            _messageHandlers = messageHandlers;
        }

        private void HandleMessage(string key, object value)
        {
            if (_messageHandlers is null || !_messageHandlers.ContainsKey(key))
            {
                throw new InvalidOperationException($"Invalid message key: {key}");
            }

            _messageHandlers[key](value);
        }

        protected void Run()
        {
            _messageCancellationToken = _messageCancellationTokenSource.Token;

            while (!_messages.IsCompleted)
            {
                try
                {
                    var message = _messages.Take(_messageCancellationToken);
                    HandleMessage(message.Item1, message.Item2);
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
            }
        }

        public void Stop()
        {
            _messages.CompleteAdding();
            _messageCancellationTokenSource.Cancel();
        }
    }
}
