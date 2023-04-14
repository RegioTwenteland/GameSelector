using System;
using System.Data.SQLite;

namespace GameSelector.Database.SQLite
{
    internal class SQLiteDatabase : IDatabase
    {
        private SQLiteConnection _connection;

        public IGamesTable GamesTable { get; }

        public IGroupsTable GroupsTable { get; }

        public IPlayedGamesTable PlayedGamesTable { get; }

        public IDatabaseObjectTranslator ObjectTranslator { get; }

        public SQLiteDatabase(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();

            GamesTable = new SQLiteGamesTable(_connection);
            GroupsTable = new SQLiteGroupsTable(_connection);
            PlayedGamesTable = new SQLitePlayedGamesTable(_connection);
            ObjectTranslator = new SQLiteDatabaseObjectTranslator();
        }

        public static T FromDbNull<T>(object dbValue)
        {
            var val = dbValue.GetType() == typeof(DBNull) ? default : (T)dbValue;
            return val;
        }

        public static object ToDbNull<T>(T? value)
            where T : struct
        {
            return value.HasValue ? (object)value.Value : DBNull.Value;
        }
    }
}
