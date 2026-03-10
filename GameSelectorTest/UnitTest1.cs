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

        private Group DummyGroup =>
            new Group
            {
                Id = 1,
                CardId = "blabla",
                Name = "Pietje",
                ScoutingName = "Klaas Vaak",
                IsAdmin = false,
                Remarks = "Testing"
            };

        [Test]
        public void FirstGameIgnoresPriority()
        {
            _gameDataBridge.GetAllGamesAvailable().Returns(
            [
                new Game
                {
                    Id = 1,
                    Code = "Game 1",
                    Category = "Abc",
                    Description = "Do something",
                    Active = true,
                    Priority = 1,
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
            ]);

            // Return empty list, so the player has not played any games.
            _playedGameDataBridge.GetPlayedGamesByPlayer(Arg.Any<Group>()).Returns([]);

            SetNumberToGenerate(1);

            var success = _subject.FindNewGameFor(DummyGroup, out var game);

            Assert.IsTrue(success);
            Assert.That(game.Id, Is.EqualTo(4));
        }
    }
}