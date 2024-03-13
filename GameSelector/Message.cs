using System;
using System.Diagnostics;

namespace GameSelector
{
    internal class Message
    {
        public Message(ControllerId sender, ControllerId recipient, string key, object value = null)
        {
            CreatedAt = DateTime.Now;
            Sender = sender;
            Recipient = recipient;
            Key = key;
            Value = value;
        }

        public Guid Id { get; } = Guid.NewGuid();

        public DateTime CreatedAt { get; }
        public ControllerId Sender { get; private set; }

        public ControllerId Recipient { get; private set; }

        public string Key { get; }

        public object Value { get; }

        private bool _consumed = false;


        public void SetConsumed()
        {
            _consumed = true;
        }

        public void AssertConsumed()
        {
           Debug.Assert(_consumed, $"Message \"{Key}\" is not handled");
        }
    }
}
