using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Forms;

namespace GameSelector.Views
{
    internal abstract class AbstractView
    {
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
