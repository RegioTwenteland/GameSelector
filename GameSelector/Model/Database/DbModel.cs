using GameSelector.Database;

namespace GameSelector.Model.Database
{
    internal class DbModel : IModel
    {
        private readonly IDatabase _database;

        public DbModel()
        {
            _database = DatabaseFactory.GetDatabase();
        }

        private IGameDataBridge _gameDataBridge = null;

        public IGameDataBridge GameDataBridge { get => _gameDataBridge ?? (_gameDataBridge = new DbGameDataBridge(_database)); }

        private IGroupDataBridge _groupDataBridge = null;

        public IGroupDataBridge GroupDataBridge { get => _groupDataBridge ?? (_groupDataBridge = new DbGroupDataBridge(_database)); }

        private IPlayedGameDataBridge _playedGamesDataBridge = null;

        public IPlayedGameDataBridge PlayedGameDataBridge { get => _playedGamesDataBridge ?? (_playedGamesDataBridge = new DbPlayedGameDataBridge(_database)); }
    }
}
