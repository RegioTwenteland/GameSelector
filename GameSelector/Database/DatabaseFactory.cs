using GameSelector.Database.SQLite;
using System;

namespace GameSelector.Database
{
    internal static class DatabaseFactory
    {
        // TODO: Read this from some sort of configuration file
        private const string SQLITE_DB_FILE_NAME = "..\\..\\data.sqlite";
        private static IDatabase _db = null;

        public static IDatabase GetDatabase()
        {
            if (_db == null)
            {
                switch (GlobalSettings.DatabaseType)
                {
                    case DatabaseType.SQLite:
                        _db = new SQLiteDatabase($"Data Source={SQLITE_DB_FILE_NAME}");
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return _db;
        }
    }
}
