using System;
using System.Diagnostics;

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
