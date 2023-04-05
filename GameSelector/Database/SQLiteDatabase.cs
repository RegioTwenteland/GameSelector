using System;
using System.Data.Linq;
using System.Data.SQLite;

namespace GameSelector.Database
{
    internal class SQLiteDatabase : IDatabase
    {
        private Table<DbGroup> _cardTable;
        private Table<DbGame> _gameTable;
        private Table<DbPlayedGame> _playedGameTable;

        private DataContext _dataContext;

        public SQLiteDatabase(string connectionString)
        {
            _dataContext = new DataContext(new SQLiteConnection(connectionString));

            _cardTable = _dataContext.GetTable<DbGroup>();
            _gameTable = _dataContext.GetTable<DbGame>();
            _playedGameTable = _dataContext.GetTable<DbPlayedGame>();
        }

        public Table<DbGroup> CardTable => _cardTable;

        public Table<DbGame> GameTable => _gameTable;

        public Table<DbPlayedGame> PlayedGameTable => _playedGameTable;

        public DataContext DataContext => _dataContext;

        ~SQLiteDatabase()
        {
            _dataContext.Connection.Close();
        }
    }
}
