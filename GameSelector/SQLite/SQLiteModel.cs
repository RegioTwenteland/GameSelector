using GameSelector.Model;
using System.Diagnostics;
using System.IO;

namespace GameSelector.SQLite
{
    internal class SQLiteModel : IModel
    {
        private string _dataSourceParam;

        private static SQLiteDatabase CreateSQLiteDatabase(string param)
        {
            Debug.Assert(param is not null);

            var freshDbFile = !File.Exists(param) || new FileInfo(param).Length == 0;

            var output = new SQLiteDatabase($"Data Source={param}");

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
