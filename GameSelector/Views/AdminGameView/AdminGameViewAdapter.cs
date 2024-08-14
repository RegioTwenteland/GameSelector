using GameSelector.Model;
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

        public void SetGamesList(IEnumerable<GameDataView> gameNames)
        {
            Control.Invoke(new MethodInvoker(() => Control.SetGamesList(gameNames)));
        }

        public void ShowPlayedGames(IEnumerable<PlayedGame> playedGames)
        {
            Control.Invoke(new MethodInvoker(() => Control.ShowPlayedGames(playedGames)));
        }

        public void UpdateGame(GameDataView game)
        {
            Control.Invoke(new MethodInvoker(() => Control.UpdateGame(game)));
        }

        public void SetGameSelected(GameDataView game)
        {
            Control.Invoke(new MethodInvoker(() => Control.SetGameSelected(game)));
        }
    }
}
