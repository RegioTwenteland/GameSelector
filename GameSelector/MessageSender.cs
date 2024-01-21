using System.Collections.Concurrent;

namespace GameSelector
{
    internal class MessageSender
    {
        private BlockingCollection<Message> _messageCollection;

        public MessageSender(BlockingCollection<Message> messageCollection)
        {
            _messageCollection = messageCollection;
        }

        public void Send(Message message)
        {
            _messageCollection.Add(message);
        }
    }
}
