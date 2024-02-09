using System;
using System.Diagnostics;

namespace GameSelector
{
    internal class Message
    {
        public Message(string key, object value)
        {
            CreatedAt = DateTime.Now;
            Key = key;
            Value = value;
        }

        public Guid Id { get; } = Guid.NewGuid();

        public DateTime CreatedAt { get; }

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
