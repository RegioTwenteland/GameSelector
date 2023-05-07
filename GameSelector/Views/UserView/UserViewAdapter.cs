using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace GameSelector.Views
{
    internal class UserViewAdapter : AbstractView
    {
        private readonly UserView form;

        public event EventHandler Ready;

        public UserViewAdapter(BlockingCollection<Message> messages, AudioPlayer audioPlayer)
            : base(messages)
        {
            form = new UserView(SendMessage, audioPlayer);
        }

        public void Start(Action onClose)
        {
            Task.Run(() =>
            {
                ////Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(form);
                onClose?.Invoke();
            });

            form.Load += (s, e) => Ready?.Invoke(this, EventArgs.Empty);

            WaitOnFormLoad(form);
        }

        public void ShowGame(GameDataView game)
        {
            form.Invoke(new MethodInvoker(() => form.ShowGame(game)));
        }

        public void ShowAlreadyPlaying(GameDataView game)
        {
            form.Invoke(new MethodInvoker(() => form.ShowAlreadyPlaying(game)));
        }

        public void ShowNoGamesLeft()
        {
            form.Invoke(new MethodInvoker(() => form.ShowNoGamesLeft()));
        }

        public void ShowPaused()
        {
            form.Invoke(new MethodInvoker(() => form.ShowPaused()));
        }

        public void ShowReady()
        {
            form.Invoke(new MethodInvoker(() => form.ShowReady()));
        }

        public void ShowReadyAfter(TimeSpan delay)
        {
            form.Invoke(new MethodInvoker(() => form.ShowReadyAfter(delay)));
        }

        public void SetGameCodes(string[] names)
        {
            form.Invoke(new MethodInvoker(() => form.SetGameCodes(names)));
        }
    }
}
