using System;
using System.Collections.Generic;

namespace GameSelector.Controllers
{
    internal abstract class AbstractController
    {
        private Dictionary<string, Action<Message>> _messageHandlers;

        public abstract void Start(Action stop);

        protected void SetMessageHandlers(Dictionary<string, Action<Message>> messageHandlers)
        {
            _messageHandlers = messageHandlers;
        }

        public void HandleMessage(Message message)
        {
            if (_messageHandlers.ContainsKey(message.Key))
            {
                message.SetConsumed();
                _messageHandlers[message.Key](message);
            }
        }
    }
}
