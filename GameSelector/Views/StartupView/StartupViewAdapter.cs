using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Threading;

namespace GameSelector.Views.StartupView
{
    internal class StartupViewAdapter : AbstractView
    {
        private StartupView _control;

        public StartupViewAdapter(MessageSender messageSender)
            : base(messageSender)
        {
            _control = new StartupView(SendMessage);
        }

        public void Start(Action onClose)
        {
            var thread = new Thread(() =>
            {
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(_control);
                onClose?.Invoke();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            WaitOnFormLoad(_control);
        }

        public void Close()
        {
            _control.Invoke(new MethodInvoker(() => _control.Close()));
        }
    }
}
