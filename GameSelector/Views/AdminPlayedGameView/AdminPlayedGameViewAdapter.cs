using GameSelector.Model;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameSelector.Views.AdminPlayedGameView
{
    internal class AdminPlayedGameViewAdapter : AbstractView
    {
        public AdminPlayedGameView Control { get; init; }

        public AdminPlayedGameViewAdapter(MessageSender messageSender)
            : base(messageSender)
        {
            Control = new AdminPlayedGameView();
        }

        public void SetPlayedGamesList(IEnumerable<PlayedGameDataView> playedGames)
        {
            Control.Invoke(new MethodInvoker(() => Control.SetPlayedGamesList(playedGames)));
        }

        public void NewPlayedGame(PlayedGameDataView playedGame)
        {
            Control.Invoke(new MethodInvoker(() => Control.NewPlayedGame(playedGame)));
        }
    }
}
