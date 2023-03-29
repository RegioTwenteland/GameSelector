using System;
using System.Collections.Concurrent;
using System.Data.Entity.Core;

namespace GameSelector.Views
{
    internal abstract class AbstractView
    {
        private BlockingCollection<Message> messages;

        protected AbstractView(BlockingCollection<Message> messages)
        {
            this.messages = messages;
        }

        protected void SendMessage(string key, object value = null)
        {
            messages.Add(new Message(key, value));
        }
    }
}
