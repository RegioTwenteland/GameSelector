using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace GameSelector.Views
{
    internal class UserViewAdapter : AbstractView
    {
        private readonly UserView form;

        public event EventHandler Ready;

        public UserViewAdapter(BlockingCollection<Message> messages)
            : base(messages)
        {
            form = new UserView(SendMessage);
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

            form.Load += (s, e) => Ready?.Invoke(this, EventArgs.Empty);

            WaitOnFormLoad(form);
        }

        public void ShowGame(GameDataView game)
        {
            form.Invoke(new MethodInvoker(() => form.ShowGame(game)));
        }

        public void ShowPaused()
        {
            form.Invoke(new MethodInvoker(() => form.ShowPaused()));
        }

        public void ShowUnpaused()
        {
            form.Invoke(new MethodInvoker(() => form.ShowUnpaused()));
        }
    }
}
