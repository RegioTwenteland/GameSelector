using GameSelector.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace GameSelector.Views.AdminScaffoldView
{
    internal class AdminScaffoldViewAdapter : AbstractView, IAdminViewScaffold
    {
        private readonly AdminScaffoldView form;

        private readonly object _lock = new object();

        private readonly List<(string, Control, Action)> _tabsToAdd = new List<(string, Control, Action)>();

        public AdminScaffoldViewAdapter(MessageSender messageSender)
            : base(messageSender)
        {
            form = new AdminScaffoldView(SendMessage);
        }

        public void Start(Action onClose)
        {
            lock (_lock)
            {
                var task = Task.Run(() =>
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(form);
                    onClose?.Invoke();
                });

                WaitOnFormLoad(form);

                _tabsToAdd.ForEach(InternalAddTabPage);
            }
        }

        public void SetGamesList(IEnumerable<GameDataView> gameNames)
        {
            form.Invoke(new MethodInvoker(() => form.SetGamesList(gameNames)));
        }

        public void ShowPlayedGames(IEnumerable<PlayedGame> playedGames)
        {
            form.Invoke(new MethodInvoker(() => form.ShowPlayedGames(playedGames)));
        }

        public void ShowError(string errorText)
        {
            form.Invoke(new MethodInvoker(() => form.ShowError(errorText)));
        }

        public void UpdateGame(GameDataView game)
        {
            form.Invoke(new MethodInvoker(() => form.UpdateGame(game)));
        }

        public void SetGameSelected(GameDataView game)
        {
            form.Invoke(new MethodInvoker(() => form.SetGameSelected(game)));
        }

        public void ShowView()
        {
            form.Invoke(new MethodInvoker(() => form.ShowView()));
        }

        public void HideView()
        {
            form.Invoke(new MethodInvoker(() => form.HideView()));
        }

        private void InternalAddTabPage((string, Control, Action) tabPage)
        {
            form.Invoke(new MethodInvoker(() => form.AddTabPage(tabPage.Item1, tabPage.Item2, tabPage.Item3)));
        }

        public void AddTabPage(string name, Control control, Action loadedCallback)
        {
            var page = (name, control, loadedCallback);

            lock (_lock)
            {
                if (form.IsHandleCreated)
                {
                    InternalAddTabPage(page);
                }
                else
                {
                    _tabsToAdd.Add(page);
                }
            }
        }
    }
}
