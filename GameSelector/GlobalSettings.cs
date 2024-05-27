using System.Configuration;
using GameSelector.Model;
using System;
namespace GameSelector
{
    internal static class GlobalSettings
    {
        public static bool SimulateNfc
        {
            get
            {
                var str = ConfigurationManager.AppSettings.Get("SimulateNfc");

                return bool.Parse(str);
            }
        }

        public static int GameTimeoutMinutes { get; set; } = 5;

        public static int AnimationLengthMilliseconds { get; set; } = 4000;
    }
}
