using GameSelector.Database.SQLite;
using System;
using System.IO;

namespace GameSelector.Database
{
    internal static class DatabaseFactory
    {
        // TODO: Read this from some sort of configuration file
        private const string SQLITE_DB_DEFAULT_FILE_NAME = "data.sqlite";
        private static IDatabase _db = null;

        private static SQLiteDatabase CreateSQLiteDatabase(string param)
        {
            var filename = string.IsNullOrEmpty(param) ? SQLITE_DB_DEFAULT_FILE_NAME : param;

            var freshDbFile = !File.Exists(filename);

            var output = new SQLiteDatabase($"Data Source={filename}");

            if (freshDbFile)
            {
                output.InitSchema("GameSelector.schema.sql");
            }

            return output;
        }

        public static IDatabase GetDatabase(string param)
        {
            if (_db == null)
            {
                switch (GlobalSettings.DatabaseType)
                {
                    case DatabaseType.SQLite:
                        _db = CreateSQLiteDatabase(param);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return _db;
        }
    }
}
