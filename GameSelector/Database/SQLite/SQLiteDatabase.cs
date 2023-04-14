using System;
using System.Data.SQLite;

namespace GameSelector.Database.SQLite
{
    internal class SQLiteDatabase
    {
        private SQLiteConnection _connection;

        public SQLiteConnection Connection => _connection;

        public SQLiteDatabase(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);

            _connection.Open();
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
