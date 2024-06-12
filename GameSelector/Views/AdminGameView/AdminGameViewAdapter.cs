using GameSelector.Model;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameSelector.Views.AdminGameView
{
    internal class AdminGameViewAdapter : AbstractView
    {
        private readonly AdminGameView form;

        public AdminGameViewAdapter(MessageSender messageSender, IAdminViewScaffold adminScaffold)
            : base(messageSender)
        {
            form = new AdminGameView(SendMessage, adminScaffold);
        }

        public void SetGamesList(IEnumerable<GameDataView> gameNames)
        {
            form.Invoke(new MethodInvoker(() => form.SetGamesList(gameNames)));
        }

        public void ShowPlayedGames(IEnumerable<PlayedGame> playedGames)
        {
            form.Invoke(new MethodInvoker(() => form.ShowPlayedGames(playedGames)));
        }

        public void UpdateGame(GameDataView game)
        {
            form.Invoke(new MethodInvoker(() => form.UpdateGame(game)));
        }

        public void SetGameSelected(GameDataView game)
        {
            form.Invoke(new MethodInvoker(() => form.SetGameSelected(game)));
        }
    }
}
