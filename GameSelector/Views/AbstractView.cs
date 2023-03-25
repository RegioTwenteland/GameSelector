using System;
using System.Collections.Concurrent;

namespace GameSelector.Views
{
    internal abstract class AbstractView
    {
        private BlockingCollection<Tuple<string, object>> messages;

        protected AbstractView(BlockingCollection<Tuple<string, object>> messages)
        {
            this.messages = messages;
        }

        protected void SendMessage(string key, object value)
        {
            messages.Add(new Tuple<string, object>($"{key}", value));
        }
    }
}
