﻿using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace GameSelector.Views
{
    internal class UserViewAdapter : AbstractView
    {
        private readonly UserView form;

        public event EventHandler Ready;

        public UserViewAdapter(MessageSender messageSender, AudioPlayer audioPlayer)
            : base(messageSender)
        {
            form = new UserView(SendMessage, audioPlayer);
        }

        public void Start(Action<object> onClose)
        {
            Task.Run(() =>
            {
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(form);
                onClose?.Invoke(null);
            });

            form.Load += (s, e) => Ready?.Invoke(this, EventArgs.Empty);

            WaitOnFormLoad(form);
        }

        public void ShowGameImmediate(GameDataView game, GroupDataView group)
        {
            form.Invoke(new MethodInvoker(() => form.ShowGameImmediate(game, group)));
        }

        public void ShowGame(GameDataView game, GroupDataView group)
        {
            form.Invoke(new MethodInvoker(() => form.ShowGame(game, group)));
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
