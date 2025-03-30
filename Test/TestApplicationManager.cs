using GameSelector.Controllers;
using System.Collections.Concurrent;
using GameSelector;

namespace Test
{
    internal class TestApplicationManager : ApplicationManager
    {
        public TestApplicationManager(BlockingCollection<Message> messageCollection)
            : base(messageCollection)
        {
        }



        public new IEnumerable<AbstractController> Controllers =>
            base.Controllers.Append(new TestController(
                Model.GameDataBridge
            ));
    }
}
