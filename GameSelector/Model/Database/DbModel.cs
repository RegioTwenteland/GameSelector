using GameSelector.Database;

namespace GameSelector.Model.Database
{
    internal class DbModel : IModel
    {
        private string _dataSourceParam = string.Empty;

        public void SetDataSourceParam(string param)
        {
            _dataSourceParam = param;
        }

        private IDatabase _database;

        private IDatabase Database { get => _database ?? (_database = DatabaseFactory.GetDatabase(_dataSourceParam)); }

        private IGameDataBridge _gameDataBridge = null;

        public IGameDataBridge GameDataBridge { get => _gameDataBridge ?? (_gameDataBridge = new DbGameDataBridge(Database)); }

        private IGroupDataBridge _groupDataBridge = null;

        public IGroupDataBridge GroupDataBridge { get => _groupDataBridge ?? (_groupDataBridge = new DbGroupDataBridge(Database)); }

        private IPlayedGameDataBridge _playedGamesDataBridge = null;

        public IPlayedGameDataBridge PlayedGameDataBridge { get => _playedGamesDataBridge ?? (_playedGamesDataBridge = new DbPlayedGameDataBridge(Database)); }
    }
}
