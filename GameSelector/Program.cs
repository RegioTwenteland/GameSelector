using GameSelector.Views;
using System.Threading;

namespace GameSelector
{
    internal static class Program
    {
        private static ManualResetEvent stopEvent = new ManualResetEvent(false);
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var testView = new TestView();
            testView.Start(Quit);

            stopEvent.WaitOne();
        }

        private static void Quit()
        {
            stopEvent.Set();
        }

       
    }

    

}
