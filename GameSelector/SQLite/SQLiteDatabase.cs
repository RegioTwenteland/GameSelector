using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace GameSelector.SQLite
{
    internal class SQLiteDatabase
    {
        private SQLiteConnection _connection;

        public SQLiteGamesTable GamesTable { get; }

        public SQLiteGroupsTable GroupsTable { get; }

        public SQLitePlayedGamesTable PlayedGamesTable { get; }

        public SQLiteDatabaseObjectTranslator ObjectTranslator { get; }

        public SQLiteDatabase(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();

            GamesTable = new SQLiteGamesTable(_connection);
            GroupsTable = new SQLiteGroupsTable(_connection);
            PlayedGamesTable = new SQLitePlayedGamesTable(_connection);
            ObjectTranslator = new SQLiteDatabaseObjectTranslator();
        }

        /// <summary>
        /// Must only be called if the database file is newly created.
        /// </summary>
        public void InitSchema(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string sql = reader.ReadToEnd();

                var command = new SQLiteCommand(sql, _connection);
                command.ExecuteNonQuery();
            }
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
