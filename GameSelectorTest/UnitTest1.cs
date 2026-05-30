using GameSelector.Controllers;
using GameSelector.Model;
using NSubstitute;

namespace GameSelectorTest
{
    public class Tests
    {
        GameSelectAlgorithm _subject;

        IGameDataBridge _gameDataBridge;
        IGroupDataBridge _groupDataBridge;
        IPlayedGameDataBridge _playedGameDataBridge;
        IRandomNumberGenerator _randomNumberGenerator;

        [SetUp]
        public void Setup()
        {
            _gameDataBridge = Substitute.For<IGameDataBridge>();
            _groupDataBridge = Substitute.For<IGroupDataBridge>();
            _playedGameDataBridge = Substitute.For<IPlayedGameDataBridge>();
            _randomNumberGenerator = Substitute.For<IRandomNumberGenerator>();

            _subject = new GameSelectAlgorithm(
                _gameDataBridge,
                _groupDataBridge,
                _playedGameDataBridge,
                _randomNumberGenerator
            );
        }

        private void SetNumberToGenerate(int number) =>
            _randomNumberGenerator.Next(Arg.Any<int>())
                .Returns(args => Math.Min((int)args[0], number) // Make sure the method adheres to the maximum given
            );

        private static long _dummyGroupId = 1;

        private static Group NewDummyGroup =>
            new()
            {
                Id = _dummyGroupId++,
                CardId = "blabla",
                Name = "Pietje",
                ScoutingName = "Klaas Vaak",
                IsAdmin = false,
                Remarks = "Testing"
            };

        private static Game NewGame(
            long id,
            string category,
            int priority,
            bool active = true,
            bool multiplePlayersRequired = false,
            int maxPlayerAmount = 1,
            string? code = null
        ) =>
            new()
            {
                Id = id,
                Code = code ?? $"Game {id}",
                Category = category,
                Description = "Test game",
                Active = active,
                Priority = priority,
                Remarks = "Test",
                Timeout = TimeSpan.FromSeconds(10),
                MultiplePlayersRequired = multiplePlayersRequired,
                MaxPlayerAmount = maxPlayerAmount,
            };

        private static PlayedGame PlayedGameFor(Game game, DateTime endTime) =>
            new()
            {
                Game = game,
                EndTime = endTime,
            };

        [Test]
        public void FirstGameIgnoresPriority()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Abc", priority: 3),
                NewGame(4, category: "Abc", priority: 5),
                NewGame(9, category: "AbcD", priority: 2),
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Return empty list, so the player has not played any games.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([]);

            SetNumberToGenerate(0);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.True);
            // We set the "random" game to be index 0, which is the one to select because this group
            // has not yet played any games.
            Assert.That(game.Id, Is.EqualTo(gameSet[0].Id));
        }

        [Test]
        public void HighestPriorityGameIsSelected()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Abc", priority: 3),
                NewGame(4, category: "Abc", priority: 5),
                NewGame(9, category: "AbcD", priority: 2),
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Player has played one game.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                PlayedGameFor(gameSet[2], new DateTime(2026, 1, 4, 10, 10, 10))
            ]);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.True);
            // We expect the game with the highest priority to be selected which is index 1
            Assert.That(game.Id, Is.EqualTo(gameSet[1].Id));
        }

        [Test]
        public void InactiveGamesAreNotSelected()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Abc", priority: 3),
                NewGame(4, category: "Abc", priority: 5, active: false),
                NewGame(9, category: "AbcD", priority: 2),
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Player has played one game.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                PlayedGameFor(gameSet[0], new DateTime(2026, 1, 4, 10, 10, 10))
            ]);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.True);
            // Even though the game at index 1 has a higher priority, it must not be selected because
            // it is inactive. Game at index 2 is the next highest priority and is active.
            Assert.That(game.Id, Is.EqualTo(gameSet[2].Id));
        }

        [Test]
        public void AlreadyPlayedGamesAreNotRepeated()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Running", priority: 3),
                NewGame(4, category: "Abc", priority: 5),
                NewGame(9, category: "Running", priority: 2),
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Player has played one game.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                PlayedGameFor(gameSet[1], new DateTime(2026, 1, 4, 10, 10, 10))
            ]);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.True);
            // Game 1 has a higher priority than game 0, but the group already played game 1.
            // Therefore game 0 must be selected.
            Assert.That(game.Id, Is.EqualTo(gameSet[0].Id));
        }

        [Test]
        public void ReturnsFalseWhenNoEligibleGamesRemain()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Abc", priority: 3)
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                PlayedGameFor(gameSet[0], new DateTime(2026, 1, 4, 10, 10, 10))
            ]);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.False);
            Assert.That(game, Is.Null);
        }

        [Test]
        public void SameCategoryIsAvoided()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Abc", priority: 3),
                NewGame(4, category: "Abc", priority: 5),
                NewGame(9, category: "AbcD", priority: 2),
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Player has played one game.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                PlayedGameFor(gameSet[0], new DateTime(2026, 1, 4, 10, 10, 10))
            ]);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.True);
            // Game 2 must be selected, even though it has the lower priority than game 1,
            // because the category of game 1 is the same as the game the group has just played
            Assert.That(game.Id, Is.EqualTo(gameSet[2].Id));
        }

        [Test]
        public void SameCategoryIsAcceptedIfNoOtherChoice()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Abc", priority: 3),
                NewGame(4, category: "Abc", priority: 5),
                NewGame(9, category: "AbcD", priority: 2),
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Player has played one game.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                PlayedGameFor(gameSet[0], new DateTime(2026, 1, 4, 10, 10, 10)),
                PlayedGameFor(gameSet[2], new DateTime(2026, 1, 4, 10, 5, 10))
            ]);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.True);
            // Game 1 must be selected, even though it has the same category as the latest played game.
            // This is because we already played all other games.
            Assert.That(game.Id, Is.EqualTo(gameSet[1].Id));
        }

        [Test]
        public void MultiplayerGameWithFewerPlayersThanMaxHasPriority()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Abc", priority: 3),
                NewGame(4, category: "Abc", priority: 5),
                NewGame(9, category: "AbcD", priority: 2, multiplePlayersRequired: true, maxPlayerAmount: 10),
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // The "randomly" selected game rule must not be used. If game index 0 is chosen the test must fail
            SetNumberToGenerate(0);

            // No-one has played anything, so all games are available. Also ensures this rule takes precedence over the
            // "first played game is always random" rule:
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([]);
            // Game with MultiplePlayersRequired has 2 players, max is 10, so it has room for more:
            _groupDataBridge.GetAllGroupsPlaying(gameSet[2]).Returns([NewDummyGroup, NewDummyGroup]);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.True);
            // Game 2 must be selected because it has MultiplePlayersRequired=true and has fewer players than max.
            Assert.That(game.Id, Is.EqualTo(gameSet[2].Id));
        }

        [Test]
        public void MultiplayerGameAtMaxCapacityHasNoPriority()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Abc", priority: 3),
                NewGame(4, category: "Abc", priority: 5),
                NewGame(9, category: "AbcD", priority: 2, multiplePlayersRequired: true, maxPlayerAmount: 10),
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // The "randomly" selected game rule must be used. If another game than index 0 is chosen the test must fail
            SetNumberToGenerate(0);

            // No-one has played anything, so all games are available.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([]);
            // Game with MultiplePlayersRequired is at max capacity (10 players, max is 10)
            _groupDataBridge.GetAllGroupsPlaying(gameSet[2]).Returns(
                [NewDummyGroup, NewDummyGroup, NewDummyGroup, NewDummyGroup, NewDummyGroup,
                 NewDummyGroup, NewDummyGroup, NewDummyGroup, NewDummyGroup, NewDummyGroup]);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.True);
            // The multiplayer game does not take precedence because it is at max capacity.
            // We instead expect the "randomly" generated game to be selected.
            Assert.That(game.Id, Is.EqualTo(gameSet[0].Id));
        }

        [Test]
        public void MultiplayerGameWithNoPlayersButSpaceAvailableHasNoPriority()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Abc", priority: 3),
                NewGame(4, category: "Abc", priority: 5),
                NewGame(9, category: "AbcD", priority: 2, multiplePlayersRequired: true, maxPlayerAmount: 10),
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // The "randomly" selected game rule must not be used. If game index 0 is chosen the test must fail
            SetNumberToGenerate(0);

            // No-one has played anything, so all games are available.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([]);
            // No one is currently playing the multiplayer game, but there is space for multiple players
            _groupDataBridge.GetAllGroupsPlaying(gameSet[2]).Returns([]);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.True);

            // The multiplayer game does not take precedence because it has 0 players and must be treated as any other game
            // We instead expect the "randomly" generated game to be selected.
            Assert.That(game.Id, Is.EqualTo(gameSet[0].Id));
        }

        [Test]
        public void AlreadyPlayedMultiplayerGameIsNotPrioritized()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Abc", priority: 2),
                NewGame(4, category: "Abc", priority: 1),
                NewGame(9, category: "AbcD", priority: 3, multiplePlayersRequired: true, maxPlayerAmount: 10),
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // The player has already played the multiplayer game
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                PlayedGameFor(gameSet[2], new DateTime(2026, 1, 4, 10, 10, 10))
            ]);
            
            // The multiplayer game has space available, but should not be prioritized because player already played it
            _groupDataBridge.GetAllGroupsPlaying(gameSet[2]).Returns([NewDummyGroup]);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.True);
            // Game 0 (highest priority among available games) must be selected, not the multiplayer game
            Assert.That(game.Id, Is.EqualTo(gameSet[0].Id));
        }

        [Test]
        public void SinglePlayerGameIsNotPrioritizedByMultiplayerRule()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Abc", priority: 3),
                NewGame(4, category: "Abc", priority: 5),
                NewGame(9, category: "AbcD", priority: 2),
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Player has already played one game, so normal priority rules apply
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                PlayedGameFor(gameSet[2], new DateTime(2026, 1, 4, 10, 10, 10))
            ]);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.True);
            // No multiplayer games exist, so normal priority rules apply. Game with highest priority is selected.
            Assert.That(game.Id, Is.EqualTo(gameSet[1].Id));
        }

        [Test]
        public void GameWithZeroMaxPlayersIsNotSelected()
        {
            List<Game> gameSet =
            [
                NewGame(1, category: "Abc", priority: 3),
                NewGame(4, category: "Abc", priority: 5, maxPlayerAmount: 0),
                NewGame(9, category: "AbcD", priority: 2),
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Player has played one game
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                PlayedGameFor(gameSet[0], new DateTime(2026, 1, 4, 10, 10, 10))
            ]);

            var success = _subject.FindNewGameFor(NewDummyGroup, out var game);

            Assert.That(success, Is.True);
            // Even though game at index 1 has higher priority, it must not be selected because
            // it has MaxPlayerAmount = 0. Game at index 2 is the next highest priority and is valid.
            Assert.That(game.Id, Is.EqualTo(gameSet[2].Id));
        }
    }
}