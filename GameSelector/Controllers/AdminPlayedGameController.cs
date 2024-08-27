using GameSelector.Model;
using GameSelector.Views;
using GameSelector.Views.AdminPlayedGameView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public override void Start(Action stop)
        {
            UpdatePlayedGamesList(_playedGameDataBridge.GetAllPlayedGames());
        }

        private void UpdatePlayedGamesList(IEnumerable<PlayedGame> playedGames)
        {
            _view.SetPlayedGamesList(playedGames.Select(PlayedGameDataView.FromPlayedGame));
        }
    }
}
