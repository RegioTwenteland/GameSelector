using System;
using System.Collections.Generic;
using WashLine;

namespace GameSelector.Controllers
{
    internal abstract class AbstractController
    {
        private Dictionary<string, Action<Message>> _messageHandlers;

        public AbstractController(ControllerId id)
        {
            Id = id;
        }

        public ControllerId Id { get; private set; }

        public abstract void Start(Action stop);

        protected void SetMessageHandlers(Dictionary<string, Action<Message>> messageHandlers)
        {
            _messageHandlers = messageHandlers;
        }

        public void HandleMessage(Message message)
        {
            if (_messageHandlers.ContainsKey(message.Command))
            {
                message.SetConsumed();
                _messageHandlers[message.Command](message);
            }
        }
    }
}
