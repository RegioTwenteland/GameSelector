using System.Collections.Generic;
using System.Windows.Forms;

namespace GameSelector.Views.AdminGameView
{
    internal class AdminGameViewAdapter : AbstractView
    {
        public AdminGameView Control { get; init; }

        public AdminGameViewAdapter(MessageSender messageSender)
            : base(messageSender)
        {
            Control = new AdminGameView(SendMessage);
        }

        public void SetGamesList(IEnumerable<GameDataView> games)
        {
            Control.Invoke(new MethodInvoker(() => Control.SetGamesList(games)));
        }

        public void UpdateGame(GameDataView game)
        {
            Control.Invoke(new MethodInvoker(() => Control.UpdateGame(game)));
        }

        public void NewGame(GameDataView game)
        {
            Control.Invoke(new MethodInvoker(() => Control.NewGame(game)));
        }

        public void GameDeleted(GameDataView game)
        {
            Control.Invoke(new MethodInvoker(() => Control.GameDeleted(game)));
        }

        public void SetGameSelected(GameDataView group)
        {
            // TODO
        }
    }
}
