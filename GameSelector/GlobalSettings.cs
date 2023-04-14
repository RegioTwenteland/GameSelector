using System.Configuration;
using GameSelector.Model;
using System;
using GameSelector.Database;

namespace GameSelector
{
    internal static class GlobalSettings
    {
        public static ModelType ModelType {
            get
            {
                var str = ConfigurationManager.AppSettings.Get("ModelType");

                return (ModelType)Enum.Parse(typeof(ModelType), str);
            }
        }

        public static DatabaseType DatabaseType
        {
            get
            {
                var str = ConfigurationManager.AppSettings.Get("DatabaseType");

                return (DatabaseType)Enum.Parse(typeof(DatabaseType), str);
            }
        }
    }
}
