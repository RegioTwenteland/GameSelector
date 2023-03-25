using GameSelector.Views;
using System.Collections.Concurrent;
using System;
using System.Threading;
using System.Collections.Generic;
using GameSelector.Controllers;

namespace GameSelector
{
    internal static class Program
    {
        private static BlockingCollection<Tuple<string, object>> _testMessages = new BlockingCollection<Tuple<string, object>>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var uiView = new UserInputView();
            var testView = new TestViewAdapter(_testMessages);
            TestController testController = new TestController(_testMessages, testView, uiView);
            testController.Run();
        }       
    }

    

}
