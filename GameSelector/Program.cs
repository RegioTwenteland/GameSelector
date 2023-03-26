using GameSelector.Views;
using System.Collections.Concurrent;
using System;
using System.Threading;
using System.Collections.Generic;
using GameSelector.Controllers;
using GameSelector.Model;

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
            var nfcDataBridge = new NfcDataBridge();
            TestController testController = new TestController(_testMessages, nfcDataBridge);
            testController.Run();
        }       
    }

    

}
