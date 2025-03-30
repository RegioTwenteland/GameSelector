using GameSelector.Controllers;
using GameSelector.Model;
using System;
using System.Diagnostics;

namespace Test
{
    internal class TestController : AbstractController
    {
        private Action<object> _stop;

        private List<Func<bool>> _tests;
        private readonly IGameDataBridge _gameDataBridge;

        public TestController(IGameDataBridge gameDataBridge)
        {
            _gameDataBridge = gameDataBridge;

            _tests =
            [
                TestAddAndReadBackGame
            ];
        }

        private bool TestAddAndReadBackGame()
        {
            var newGame = new Game
            {
                Code = "abc",
                Description = "DEF",
                Category = string.Empty,
                Active = true,
                Priority = 0,
                Remarks = string.Empty,
                Timeout = TimeSpan.FromMinutes(15),
                MaxPlayerAmount = 3,
            };

            _gameDataBridge.InsertGame(newGame);

            var retrievedGame = _gameDataBridge.GetGame(newGame.Id);

            return newGame.Code == "abc";
        }

        public override void Start(Action<object> stop)
        {
            _stop = stop;

            foreach (var test in _tests)
            {
                Console.WriteLine($"Executing test {test.Method.Name}");
                if (test())
                {
                    Console.WriteLine("TEST PASSED");
                }
                else
                {
                    Console.WriteLine("TEST FAILED");
                }
            }

            Console.ReadKey();
            stop(null);
        }
    }
}
