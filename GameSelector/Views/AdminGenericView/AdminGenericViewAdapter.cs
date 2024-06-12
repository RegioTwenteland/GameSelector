using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace GameSelector.Views.AdminGenericView
{
    internal class AdminGenericViewAdapter : AbstractView, IAdminViewScaffold
    {
        private readonly AdminGenericView form;

        private readonly object _lock = new object();

        private readonly List<(string, Control, Action)> _tabsToAdd = new List<(string, Control, Action)>();

        public AdminGenericViewAdapter(MessageSender messageSender)
            : base(messageSender)
        {
            form = new AdminGenericView(SendMessage);
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

                thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
                thread.Start();

                WaitOnFormLoad(form);

                _tabsToAdd.ForEach(InternalAddTabPage);
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
