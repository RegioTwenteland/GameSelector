using GameSelector.Controllers;
using GameSelector.Model;
using NSubstitute;

namespace GameSelectorTest
{
    public class Tests
    {
        GameSelectAlgorithm _subject;

        IGameDataBridge _gameDataBridge;
        IPlayedGameDataBridge _playedGameDataBridge;
        IRandomNumberGenerator _randomNumberGenerator;

        [SetUp]
        public void Setup()
        {
            _gameDataBridge = Substitute.For<IGameDataBridge>();
            _playedGameDataBridge = Substitute.For<IPlayedGameDataBridge>();
            _randomNumberGenerator = Substitute.For<IRandomNumberGenerator>();

            _subject = new GameSelectAlgorithm(
                _gameDataBridge,
                _playedGameDataBridge,
                _randomNumberGenerator
            );
        }

        private void SetNumberToGenerate(int number) =>
            _randomNumberGenerator.Next(Arg.Any<int>())
                .Returns(args => Math.Min((int)args[0], number) // Make sure the method adheres to the maximum given
            );

        private static long _dummyGroupId = 1;

        private static Group DummyGroup =>
            new()
            {
                Id = _dummyGroupId++,
                CardId = "blabla",
                Name = "Pietje",
                ScoutingName = "Klaas Vaak",
                IsAdmin = false,
                Remarks = "Testing"
            };
        
        private static long HighestActivePriority(IEnumerable<Game> gameSet) =>
            gameSet
                .Where(g => g.Active)
                .Select(g => g.Priority)
                .Max();

        [Test]
        public void FirstGameIgnoresPriority()
        {
            List<Game> gameSet =
            [
                new Game
                {
                    Id = 1,
                    Code = "Game 1",
                    Category = "Abc",
                    Description = "Do something",
                    Active = true,
                    Priority = 3,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
                new Game
                {
                    Id = 4,
                    Code = "Game 2",
                    Category = "Abc",
                    Description = "Do something",
                    Active = true,
                    Priority = 5,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
                new Game
                {
                    Id = 9,
                    Code = "Game 5",
                    Category = "AbcD",
                    Description = "Do something else",
                    Active = true,
                    Priority = 2,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Return empty list, so the player has not played any games.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([]);

            SetNumberToGenerate(0);

            var success = _subject.FindNewGameFor(DummyGroup, out var game);

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
                new Game
                {
                    Id = 1,
                    Code = "Game 1",
                    Category = "Abc",
                    Description = "Do something",
                    Active = true,
                    Priority = 3,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
                new Game
                {
                    Id = 4,
                    Code = "Game 2",
                    Category = "Abc",
                    Description = "Do something",
                    Active = true,
                    Priority = 5,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
                new Game
                {
                    Id = 9,
                    Code = "Game 5",
                    Category = "AbcD",
                    Description = "Do something else",
                    Active = true,
                    Priority = 2,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Player has played one game.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                new PlayedGame()
                {
                    Game = gameSet[2],
                    EndTime = new DateTime(2026, 1, 4, 10, 10, 10)
                }
            ]);

            var success = _subject.FindNewGameFor(DummyGroup, out var game);

            Assert.That(success, Is.True);
            // We expect the game with the highest priority to be selected which is index 1
            Assert.That(game.Id, Is.EqualTo(gameSet[1].Id));
        }

        [Test]
        public void InactiveGamesAreNotSelected()
        {
            List<Game> gameSet =
            [
                new Game
                {
                    Id = 1,
                    Code = "Game 1",
                    Category = "Abc",
                    Description = "Do something",
                    Active = true,
                    Priority = 3,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
                new Game
                {
                    Id = 4,
                    Code = "Game 2",
                    Category = "Abc",
                    Description = "Do something",
                    Active = false,
                    Priority = 5,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
                new Game
                {
                    Id = 9,
                    Code = "Game 5",
                    Category = "AbcD",
                    Description = "Do something else",
                    Active = true,
                    Priority = 2,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Player has played one game.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                new PlayedGame()
                {
                    Game = gameSet[0],
                    EndTime = new DateTime(2026, 1, 4, 10, 10, 10)
                }
            ]);

            var success = _subject.FindNewGameFor(DummyGroup, out var game);

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
                new Game
                {
                    Id = 1,
                    Code = "Game 1",
                    Category = "Running",
                    Description = "Do something",
                    Active = true,
                    Priority = 3,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
                new Game
                {
                    Id = 4,
                    Code = "Game 2",
                    Category = "Abc",
                    Description = "Do something",
                    Active = true,
                    Priority = 5,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
                new Game
                {
                    Id = 9,
                    Code = "Game 5",
                    Category = "Running",
                    Description = "Do something else",
                    Active = true,
                    Priority = 2,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Player has played one game.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                new PlayedGame()
                {
                    Game = gameSet[1],
                    EndTime = new DateTime(2026, 1, 4, 10, 10, 10)
                }
            ]);

            var success = _subject.FindNewGameFor(DummyGroup, out var game);

            Assert.That(success, Is.True);
            // Game 1 has a higher priority than game 0, but the group already played game 1.
            // Therefore game 0 must be selected.
            Assert.That(game.Priority, Is.EqualTo(gameSet[0].Priority));
        }

        [Test]
        public void SameCategoryIsAvoided()
        {
            List<Game> gameSet =
            [
                new Game
                {
                    Id = 1,
                    Code = "Game 1",
                    Category = "Abc",
                    Description = "Do something",
                    Active = true,
                    Priority = 3,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
                new Game
                {
                    Id = 4,
                    Code = "Game 2",
                    Category = "Abc",
                    Description = "Do something",
                    Active = true,
                    Priority = 5,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
                new Game
                {
                    Id = 9,
                    Code = "Game 5",
                    Category = "AbcD",
                    Description = "Do something else",
                    Active = true,
                    Priority = 2,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Player has played one game.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                new PlayedGame()
                {
                    Game = gameSet[0],
                    EndTime = new DateTime(2026, 1, 4, 10, 10, 10)
                }
            ]);

            var success = _subject.FindNewGameFor(DummyGroup, out var game);

            Assert.That(success, Is.True);
            // Game 2 must be selected, even though it has the lower priority than game 1,
            // because the category of game 1 is the same as the game the group has just played
            Assert.That(game.Priority, Is.EqualTo(gameSet[2].Priority));
        }

        [Test]
        public void SameCategoryIsAcceptedIfNoOtherChoice()
        {
            List<Game> gameSet =
            [
                new Game
                {
                    Id = 1,
                    Code = "Game 1",
                    Category = "Abc",
                    Description = "Do something",
                    Active = true,
                    Priority = 3,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
                new Game
                {
                    Id = 4,
                    Code = "Game 2",
                    Category = "Abc",
                    Description = "Do something",
                    Active = true,
                    Priority = 5,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
                new Game
                {
                    Id = 9,
                    Code = "Game 5",
                    Category = "AbcD",
                    Description = "Do something else",
                    Active = true,
                    Priority = 2,
                    Remarks = "asdfasd",
                    Timeout = TimeSpan.FromSeconds(10),
                    MaxPlayerAmount = 1,
                },
            ];

            _gameDataBridge.GetAllGamesAvailable().Returns(gameSet);

            // Player has played one game.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([
                new PlayedGame()
                {
                    Game = gameSet[0],
                    EndTime = new DateTime(2026, 1, 4, 10, 10, 10)
                },
                new PlayedGame()
                {
                    Game = gameSet[2],
                    EndTime = new DateTime(2026, 1, 4, 10, 5, 10)
                }
            ]);

            var success = _subject.FindNewGameFor(DummyGroup, out var game);

            Assert.That(success, Is.True);
            // Game 1 must be selected, even though it has the same category as the latest played game.
            // This is because we already played all other games.
            Assert.That(game.Priority, Is.EqualTo(gameSet[1].Priority));
        }
    }
}