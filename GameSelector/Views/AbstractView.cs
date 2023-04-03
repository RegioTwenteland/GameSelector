using System;
using System.Collections.Concurrent;
using System.Data.Entity.Core;
using System.Threading;
using System.Windows.Forms;

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

        protected static void WaitOnFormLoad(Form form)
        {
            var mre = new ManualResetEvent(false);

            form.Load += (s, e) =>
            {
                mre.Set();
            };

            mre.WaitOne();
        }
    }
}
