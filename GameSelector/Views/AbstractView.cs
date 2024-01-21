using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Forms;

namespace GameSelector.Views
{
    internal abstract class AbstractView
    {
        private MessageSender _messageSender;

        protected AbstractView(MessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        protected void SendMessage(string key, object value = null)
        {
            _messageSender.Send(new Message(key, value));
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
