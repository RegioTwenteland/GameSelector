using GameSelector.Database.SQLite;
using System;

namespace GameSelector.Database
{
    internal class DatabaseFactory
    {
        private DatabaseType _databaseType;

        // TODO: Read this from some sort of configuration file
        private const string SQLITE_DB_FILE_NAME = "..\\..\\data.sqlite";

        public DatabaseFactory(DatabaseType type)
        {
            _databaseType = type;
        }

        private IDatabase _db = null;

        public IDatabase GetDatabase()
        {
            if (_db == null)
            {
                switch (_databaseType)
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
