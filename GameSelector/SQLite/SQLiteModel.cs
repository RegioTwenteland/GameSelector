using GameSelector.Model;
using System.IO;

namespace GameSelector.SQLite
{
    internal class SQLiteModel : IModel
    {
        private string _dataSourceParam = string.Empty;

        private const string SQLITE_DB_DEFAULT_FILE_NAME = "data.sqlite";

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

        public void SetDataSource(string param)
        {
            _dataSourceParam = param;
        }

        private SQLiteDatabase _database;

        private SQLiteDatabase Database { get => _database ?? (_database = CreateSQLiteDatabase(_dataSourceParam)); }

        private IGameDataBridge _gameDataBridge = null;

        public IGameDataBridge GameDataBridge { get => _gameDataBridge ?? (_gameDataBridge = new SQLiteGameDataBridge(Database)); }

        private IGroupDataBridge _groupDataBridge = null;

        public IGroupDataBridge GroupDataBridge { get => _groupDataBridge ?? (_groupDataBridge = new SQLiteGroupDataBridge(Database)); }

        private IPlayedGameDataBridge _playedGamesDataBridge = null;

        public IPlayedGameDataBridge PlayedGameDataBridge { get => _playedGamesDataBridge ?? (_playedGamesDataBridge = new SQLitePlayedGameDataBridge(Database)); }
    }
}
