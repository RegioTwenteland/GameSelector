using GameSelector.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace GameSelector.Views
{
    internal class AdminViewAdapter : AbstractView
    {
        private readonly AdminView form;


        public AdminViewAdapter(BlockingCollection<Message> messages)
            : base(messages)
        {
            form = new AdminView(SendMessage);
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

        public void ShowCard(CardData card)
        {
            AssertRightThread();
            form.Invoke(new MethodInvoker(() => form.ShowCard(card)));
        }

        public void ShowGame(GameData game)
        {
            AssertRightThread();
            form.Invoke(new MethodInvoker(() => form.ShowGame(game)));
        }

        public void SetGamesList(List<GameData> games)
        {
            AssertRightThread();
            form.Invoke(new MethodInvoker(() => form.SetGamesList(games)));
        }
    }
}
