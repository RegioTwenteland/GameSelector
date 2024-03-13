using System.Collections.Concurrent;
using System.Diagnostics;

namespace GameSelector
{
    internal class MessageSender
    {
        private BlockingCollection<Message> _messageCollection;

        public ControllerId _sender;


        public MessageSender(BlockingCollection<Message> messageCollection)
        {
            _messageCollection = messageCollection;
        }

        public void Bind(ControllerId sender)
        {
            _sender = sender;
        }

        public void Send(ControllerId recipient, string key, object value = null)
        {
            var message = new Message(_sender, recipient, key, value);
            _messageCollection.Add(message);
        }
    }
}
