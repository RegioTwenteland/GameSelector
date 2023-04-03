using GameSelector.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
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
            var task = Task.Run(() =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(form);
                onClose?.Invoke();
            });

            WaitOnFormLoad(form);
        }

        public void ShowCard(CardDataView card)
        {
            form.Invoke(new MethodInvoker(() => form.ShowCard(card)));
        }

        public void ShowGame(GameDataView game)
        {
            form.Invoke(new MethodInvoker(() => form.ShowGame(game)));
        }

        public void SetGamesList(IEnumerable<GameDataView> gameNames)
        {
            form.Invoke(new MethodInvoker(() => form.SetGamesList(gameNames)));
        }

        public void ShowError(string errorText)
        {
            form.Invoke(new MethodInvoker(() => form.ShowError(errorText)));
        }
    }
}
