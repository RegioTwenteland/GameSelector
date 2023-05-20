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

        public void ShowGameTimeout(int timeout)
        {
            form.Invoke(new MethodInvoker(() => form.ShowGameTimeout(timeout)));
        }

        public void UpdateGroup(GroupDataView group)
        {
            form.Invoke(new MethodInvoker(() => form.UpdateGroup(group)));
        }

        public void SetGroupSelected(GroupDataView group)
        {
            form.Invoke(new MethodInvoker(() => form.SetGroupSelected(group)));
        }

        public void ShowLastScannedCardId(string cardId)
        {
            form.Invoke(new MethodInvoker(() => form.ShowLastScannedCardId(cardId)));
        }

        public void SetGroupsList(IEnumerable<GroupDataView> groups)
        {
            form.Invoke(new MethodInvoker(() => form.SetGroupsList(groups)));
        }

        public void SetGamesList(IEnumerable<GameDataView> gameNames)
        {
            form.Invoke(new MethodInvoker(() => form.SetGamesList(gameNames)));
        }

        public void ShowPlayedGames(List<PlayedGame> playedGames)
        {
            form.Invoke(new MethodInvoker(() => form.ShowPlayedGames(playedGames)));
        }

        public void ShowError(string errorText)
        {
            form.Invoke(new MethodInvoker(() => form.ShowError(errorText)));
        }

        public void ShowGamePaused()
        {
            form.Invoke(new MethodInvoker(() => form.ShowGamePaused()));
        }

        public void ShowGameRunning()
        {
            form.Invoke(new MethodInvoker(() => form.ShowGameRunning()));
        }

        public void UpdateGame(GameDataView game)
        {
            form.Invoke(new MethodInvoker(() => form.UpdateGame(game)));
        }

        public void ShowView()
        {
            form.Invoke(new MethodInvoker(() => form.ShowView()));
        }
    }
}
