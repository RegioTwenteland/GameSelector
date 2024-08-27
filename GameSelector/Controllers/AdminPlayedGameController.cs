using GameSelector.Model;
using GameSelector.Views;
using GameSelector.Views.AdminPlayedGameView;
using System;
using System.Linq;

namespace GameSelector.Controllers
{
    internal class AdminPlayedGameController : AbstractController
    {
        private readonly AdminPlayedGameViewAdapter _view;
        private readonly IPlayedGameDataBridge _playedGameDataBridge;

        public AdminPlayedGameController(AdminPlayedGameViewAdapter view, IPlayedGameDataBridge playedGameDataBridge)
        {
            _view = view;
            _playedGameDataBridge = playedGameDataBridge;

            _playedGameDataBridge.PlayedGameAdded += OnPlayedGameAdded;
        }

        private void OnPlayedGameAdded(object sender, PlayedGameAddedEventArgs e)
        {
            _view.NewPlayedGame(PlayedGameDataView.FromPlayedGame(e.PlayedGame));
        }

        public override void Start(Action stop)
        {
            _view.SetPlayedGamesList(
                _playedGameDataBridge.GetAllPlayedGames()
                .Select(PlayedGameDataView.FromPlayedGame));
        }
    }
}
