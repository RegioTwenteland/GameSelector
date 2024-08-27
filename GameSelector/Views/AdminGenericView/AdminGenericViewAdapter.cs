using GameSelector.Controllers;
using GameSelector.Views.AdminGameView;
using GameSelector.Views.AdminGroupView;
using GameSelector.Views.AdminPlayedGameView;
using GameSelector.Views.AdminSettingsView;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace GameSelector.Views.AdminGenericView
{
    internal class AdminGenericViewAdapter : AbstractView
    {
        private readonly AdminGenericView form;

        private readonly object _lock = new object();
        private readonly List<(string, Control, Action)> _tabsToAdd = new List<(string, Control, Action)>();

        private AdminSettingsViewAdapter _adminSettingsView;

        private AdminGroupViewAdapter _adminGroupView;

        private AdminGameViewAdapter _adminGameView;
        private readonly AdminPlayedGameViewAdapter _adminPlayedGameView;

        public AdminGenericViewAdapter(
            MessageSender messageSender,
            AdminSettingsViewAdapter adminSettingsView,
            AdminGroupViewAdapter adminGroupView,
            AdminGameViewAdapter adminGameView,
            AdminPlayedGameViewAdapter adminPlayedGameView
            )
            : base(messageSender)
        {
            form = new AdminGenericView(SendMessage);

            _adminSettingsView = adminSettingsView;
            _adminGroupView = adminGroupView;
            _adminGameView = adminGameView;
            _adminPlayedGameView = adminPlayedGameView;
        }

        public void Start(Action onClose)
        {
            lock (_lock)
            {
                var thread = new Thread(() =>
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(form);
                    onClose?.Invoke();
                });

                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

                WaitOnFormLoad(form);

                form.Invoke(new MethodInvoker(() =>
                {
                    form.AddTabPage("Spellen", _adminGameView.Control);
                    form.AddTabPage("Groepen", _adminGroupView.Control);
                    form.AddTabPage("Gespeeld", _adminPlayedGameView.Control);
                    form.AddTabPage("Admin", _adminSettingsView.Control);
                }));
            }
        }

        public void ShowError(string errorText)
        {
            form.Invoke(new MethodInvoker(() => form.ShowError(errorText)));
        }

        public void ShowView()
        {
            form.Invoke(new MethodInvoker(() => form.ShowView()));
        }

        public void HideView()
        {
            form.Invoke(new MethodInvoker(() => form.HideView()));
        }
    }
}
