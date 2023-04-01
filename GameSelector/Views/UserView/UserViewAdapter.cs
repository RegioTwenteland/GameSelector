using External;
using GameSelector.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace GameSelector.Views
{
    internal class UserViewAdapter : AbstractView
    {
        private readonly UserView form;


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
        }

        private void AssertRightThread()
        {
            Debug.Assert(form.InvokeRequired);
        }

        public void ShowGame(GameDataView game)
        {
            AssertRightThread();
            form.Invoke(new MethodInvoker(() => form.ShowGame(game)));
        }
    }
}
