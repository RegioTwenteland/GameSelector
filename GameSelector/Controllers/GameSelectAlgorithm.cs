using GameSelector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSelector.Controllers
{
    internal class GameSelectAlgorithm
    {
        private IGameDataBridge _gameDataBridge;
        private IGroupDataBridge _groupDataBridge;
        private IPlayedGameDataBridge _playedGameDataBridge;
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public GameSelectAlgorithm(
            IGameDataBridge gameDataBridge,
            IGroupDataBridge groupDataBridge,
            IPlayedGameDataBridge playedGameDataBridge,
            IRandomNumberGenerator randomNumberGenerator
        )
        {
            _gameDataBridge = gameDataBridge;
            _groupDataBridge = groupDataBridge;
            _playedGameDataBridge = playedGameDataBridge;
            _randomNumberGenerator = randomNumberGenerator;
        }

        private IEnumerable<Game> GetGamesNotPlayed(IEnumerable<Game> gamesAvailable, IEnumerable<PlayedGame> playedGames)
        {
            var playedGameIds = playedGames.Select(pg => pg.Game.Id).ToHashSet();
            return gamesAvailable.Where(game => (!playedGameIds.Contains(game.Id)) && game.Active);
        }

        public bool FindNewGameFor(Group group, out Game newGame)
        {
            newGame = null;

            var gamesAvailable = _gameDataBridge.GetAllGamesAvailable();
            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);

            // Make sure no game is played twice by the same group
            var remainingGames = GetGamesNotPlayed(gamesAvailable, playedGames);

            // If this leaves no games to be played, the group has played all games.
            if (!remainingGames.Any())
            {
                return false;
            }

            // If a game has a minimum amount of players which is not yet fulfilled, AND it already has at least one player we will select that game.
            var gamesWithMinimumPlayerCountNotFulfilled = remainingGames
                .Where(g => g.MinPlayerAmount > 0)
                .Where(g => _groupDataBridge.GetAllGroupsPlaying(g).Any())
                .Where(g => _groupDataBridge.GetAllGroupsPlaying(g).Count() < g.MinPlayerAmount);

            if (gamesWithMinimumPlayerCountNotFulfilled.Any())
            {
                newGame = gamesWithMinimumPlayerCountNotFulfilled.ToArray()[_randomNumberGenerator.Next(gamesWithMinimumPlayerCountNotFulfilled.Count())];
                return true;
            }

            // First game is completely random
            if (!playedGames.Any())
            {
                newGame = gamesAvailable.ToArray()[_randomNumberGenerator.Next(gamesAvailable.Count())];
                return true;
            }

            var lastPlayedCategory = playedGames.OrderByDescending(g => g.EndTime)
                .First()
                .Game
                .Category;

            // Remove all games which are in the same category as the last played game, but only if that leaves games to be played
            // Otherwise we just accept that the group might play a similar game to the last one
            if (remainingGames.Where(g => g.Category != lastPlayedCategory).Any())
            {
                remainingGames = remainingGames.Where(g => g.Category != lastPlayedCategory);
            }

            // Remove all games which do not have the highest priority.
            remainingGames = remainingGames
                .GroupBy(game => game.Priority)
                .OrderByDescending(grouping => grouping.Key)
                .First();

            // These are all of the possible games to select from.
            var selectFrom = remainingGames.ToArray();

            newGame = selectFrom[_randomNumberGenerator.Next(selectFrom.Length)];
            return true;
        }
    }
}
