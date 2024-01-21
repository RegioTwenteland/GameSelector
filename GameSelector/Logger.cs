using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSelector
{
    internal static class Logger
    {
        static Logger()
        {
        }

        public static void LogError(object who, string message)
        {
            Log("ERROR", ConsoleColor.DarkRed, who, message);
        }

        public static void LogInfo(object who, string message)
        {
            Log("INFO", ConsoleColor.White, who, message);
        }

        private static void Log(string logType, ConsoleColor color, object who, string message)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"{DateTime.Now}: [{logType}] {who.GetType().Name}: {message}");
            Console.ForegroundColor = original;
        }
    }
}
