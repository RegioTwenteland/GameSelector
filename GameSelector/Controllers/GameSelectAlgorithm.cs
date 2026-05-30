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

        private delegate RuleResult RuleHandler(Group group, IReadOnlyList<Game> remainingGames);

        private sealed class RuleResult
        {
            private RuleResult(Game selectedGame, IEnumerable<Game> filteredGames)
            {
                SelectedGame = selectedGame;
                FilteredGames = filteredGames;
            }

            public Game SelectedGame { get; }
            public IEnumerable<Game> FilteredGames { get; }
            public bool HasSelectedGame => SelectedGame != null;
            public bool HasFilteredGames => FilteredGames != null;

            public static RuleResult NoChange() => new RuleResult(null, null);
            public static RuleResult Select(Game game) => new RuleResult(game, null);
            public static RuleResult Filter(IEnumerable<Game> filteredGames) => new RuleResult(null, filteredGames);
        }

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

        private long GetCurrentPlayerCountForGame(Game game)
        {
            var groupsPlaying = _groupDataBridge.GetAllGroupsPlaying(game);
            return groupsPlaying.Count();
        }

        public bool FindNewGameFor(Group group, out Game newGame)
        {
            newGame = null;

            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);
            var remainingGames = GetGamesNotPlayed(_gameDataBridge.GetAllGamesAvailable(), playedGames).ToArray();

            if (!remainingGames.Any())
            {
                return false;
            }

            var rules = new RuleHandler[]
            {
                SelectMultiplayerGameWithOpenSlot,
                SelectRandomFirstGame,
                ExcludeSameCategoryAsLastPlayedGame,
                FilterToHighestPriorityGames,
                SelectRandomRemainingGame
            };

            foreach (var rule in rules)
            {
                var result = rule(group, remainingGames);

                if (result.HasSelectedGame)
                {
                    newGame = result.SelectedGame;
                    return true;
                }

                if (result.HasFilteredGames)
                {
                    remainingGames = result.FilteredGames.ToArray();
                }
            }

            return false;
        }

        private RuleResult SelectMultiplayerGameWithOpenSlot(Group group, IReadOnlyList<Game> remainingGames)
        {
            var multiplayerGamesMissingPlayers = remainingGames
                .Where(g => g.MultiplePlayersRequired && GetCurrentPlayerCountForGame(g) < g.MaxPlayerAmount && GetCurrentPlayerCountForGame(g) > 0)
                .ToArray();

            if (!multiplayerGamesMissingPlayers.Any())
            {
                return RuleResult.NoChange();
            }

            var selectFrom = multiplayerGamesMissingPlayers
                .OrderBy(g => GetCurrentPlayerCountForGame(g))
                .ThenByDescending(g => g.Priority)
                .ToArray();

            return RuleResult.Select(selectFrom[_randomNumberGenerator.Next(selectFrom.Length)]);
        }

        private RuleResult SelectRandomFirstGame(Group group, IReadOnlyList<Game> remainingGames)
        {
            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);
            if (playedGames.Any())
            {
                return RuleResult.NoChange();
            }

            return RuleResult.Select(remainingGames[_randomNumberGenerator.Next(remainingGames.Count)]);
        }

        private RuleResult ExcludeSameCategoryAsLastPlayedGame(Group group, IReadOnlyList<Game> remainingGames)
        {
            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);
            if (!playedGames.Any())
            {
                return RuleResult.NoChange();
            }

            var lastPlayedCategory = playedGames
                .OrderByDescending(pg => pg.EndTime)
                .First()
                .Game
                .Category;

            var filteredGames = remainingGames
                .Where(g => g.Category != lastPlayedCategory)
                .ToArray();

            if (!filteredGames.Any())
            {
                // There are no games available in a different category. Accept that the user plays a similar game.
                return RuleResult.NoChange();
            }

            return RuleResult.Filter(filteredGames);
        }

        private RuleResult FilterToHighestPriorityGames(Group group, IReadOnlyList<Game> remainingGames)
        {
            var highestPriorityGames = remainingGames
                .GroupBy(game => game.Priority)
                .OrderByDescending(grouping => grouping.Key)
                .First()
                .ToArray();

            return RuleResult.Filter(highestPriorityGames);
        }

        private RuleResult SelectRandomRemainingGame(Group group, IReadOnlyList<Game> remainingGames)
        {
            if (!remainingGames.Any())
            {
                return RuleResult.NoChange();
            }

            return RuleResult.Select(remainingGames[_randomNumberGenerator.Next(remainingGames.Count)]);
        }
    }
}
