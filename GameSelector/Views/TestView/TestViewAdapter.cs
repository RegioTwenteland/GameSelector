using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace GameSelector.Views
{
    internal class TestViewAdapter : AbstractView
    {
        private readonly TestView form;


        public TestViewAdapter(BlockingCollection<Tuple<string, object>> messages)
            : base(messages)
        {
            form = new TestView(SendMessage);
        }

        public void Start(Action onClose)
        {
            Task.Run(() =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(form);
                onClose?.Invoke();
            });
        }

        private void AssertRightThread()
        {
            Debug.Assert(form.InvokeRequired);
        }

        public void ShowData(string data)
        {
            AssertRightThread();
            form.Invoke(new MethodInvoker(() => form.ShowData(data)));
        }
    }
}
