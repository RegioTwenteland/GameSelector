using System.Windows.Forms;

namespace GameSelector.Views.AdminSettingsView
{
    internal class AdminSettingsViewAdapter : AbstractView
    {
        public AdminSettingsView Control { get; init; }

        public AdminSettingsViewAdapter(MessageSender messageSender)
            : base(messageSender)
        {
            Control = new AdminSettingsView(SendMessage);
        }

        public void ShowGamePaused()
        {
            Control.Invoke(new MethodInvoker(() => Control.ShowGamePaused()));
        }

        public void ShowGameRunning()
        {
            Control.Invoke(new MethodInvoker(() => Control.ShowGameRunning()));
        }

        public void ShowGameTimeout(int timeoutMinutes)
        {
            Control.Invoke(new MethodInvoker(() => Control.ShowGameTimeout(timeoutMinutes)));
        }

        public void ShowAnimationLength(int lengthMs)
        {
            Control.Invoke(new MethodInvoker(() => Control.ShowAnimationLength(lengthMs)));
        }
    }
}
