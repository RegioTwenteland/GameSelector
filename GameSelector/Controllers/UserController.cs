using GameSelector.Model;
using GameSelector.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.Controllers
{
    internal class UserController : AbstractController
    {
        private UserViewAdapter _userView;
        private UserIdentificationView _userIdentificationView;
        private GroupDataBridge _groupDataBridge;
        private GameDataBridge _gameDataBridge;
        private PlayedGameDataBridge _playedGameDataBridge;

        private Random _random = new Random();

        public UserController(
            UserIdentificationView userIdentificationView,
            UserViewAdapter userView,
            GroupDataBridge groupDataBridge,
            GameDataBridge gameDataBridge,
            PlayedGameDataBridge playedGameDataBridge
        )
        {
            _userIdentificationView = userIdentificationView;
            _userView = userView;
            _groupDataBridge = groupDataBridge;
            _gameDataBridge = gameDataBridge;
            _playedGameDataBridge = playedGameDataBridge;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "UserLogin", OnUserLogin }
            });
        }

        public override void Start(Action stop)
        {
            _userView.Start(stop);
        }

        private List<Game> GetPossibleGames(Group group)
        {
            var games = _gameDataBridge.GetAllGamesNotOccupied();
            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);

            var playedGameIds = new HashSet<long>();
            foreach (var game in playedGames)
            {
                playedGameIds.Add(game.GameId);
            }

            var possibleGames = new List<Game>(games.Count);
            foreach (var game in games)
            {
                if (!playedGameIds.Contains(game.Id))
                {
                    possibleGames.Add(game);
                }
            }

            return possibleGames;
        }

        private Game SelectNewGameFor(Group group)
        {
            var possibleGames = GetPossibleGames(group);

            Game selectedGame = null;
            
            if (possibleGames.Count <= 0) return null;

            var recordPriority = -1L;
            foreach (var game in possibleGames)
            {
                if (game.Priority > recordPriority)
                {
                    recordPriority = game.Priority;
                    selectedGame = game;
                }
            }

            if (selectedGame == null) return null;

            selectedGame.StartTime = DateTime.Now;
            selectedGame.OccupiedBy = group;

            _gameDataBridge.UpdateGame(selectedGame);

            return selectedGame;
        }

        private void EndGameFor(Group group)
        {
            var currentGame = _gameDataBridge.GetGameOccupiedBy(group);
            if (currentGame == null) return;

            Debug.Assert(currentGame.StartTime.HasValue);

            var playedGame = new PlayedGame
            {
                PlayerId = group.Id,
                GameId = currentGame.Id,
                StartTime = currentGame.StartTime.HasValue ? currentGame.StartTime.Value : DateTime.MinValue,
                EndTime = DateTime.Now,
            };

            _playedGameDataBridge.InsertPlayedGame(playedGame);

            currentGame.OccupiedBy = null;
            currentGame.StartTime = null;

            _gameDataBridge.UpdateGame(currentGame);
        }

        private void OnUserLogin(object value)
        {
            Debug.Assert(value is string);

            var cardId = (string)value;
            var group = _groupDataBridge.GetGroup(cardId);

            if (group == null) return;

            EndGameFor(group);
            var newGame = SelectNewGameFor(group);
            _userView.ShowGame(GameDataView.FromGame(newGame));
        }
    }
}
