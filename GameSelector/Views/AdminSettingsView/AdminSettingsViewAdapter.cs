using System.Windows.Forms;

namespace GameSelector.Views.AdminSettingsView
{
    internal class AdminSettingsViewAdapter : AbstractView
    {
        private readonly AdminSettingsView form;

        public AdminSettingsViewAdapter(MessageSender messageSender, IAdminViewScaffold adminScaffold)
            : base(messageSender)
        {
            form = new AdminSettingsView(SendMessage, adminScaffold);
        }

        public void ShowGamePaused()
        {
            form.Invoke(new MethodInvoker(() => form.ShowGamePaused()));
        }

        public void ShowGameRunning()
        {
            form.Invoke(new MethodInvoker(() => form.ShowGameRunning()));
        }

        public void ShowGameTimeout(int timeoutMinutes)
        {
            form.Invoke(new MethodInvoker(() => form.ShowGameTimeout(timeoutMinutes)));
        }

        public void ShowAnimationLength(int lengthMs)
        {
            form.Invoke(new MethodInvoker(() => form.ShowAnimationLength(lengthMs)));
        }
    }
}
