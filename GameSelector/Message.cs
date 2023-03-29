using System;

namespace GameSelector
{
    internal class Message
    {
        public Message(string key, object value)
        {
            CreatedAt = DateTime.Now;
            Handled = false;
            Key = key;
            Value = value;
        }

        public DateTime CreatedAt { get; }

        public bool Handled { get; }

        public string Key { get; }

        public object Value { get; }
    }
}
