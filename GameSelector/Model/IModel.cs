namespace GameSelector.Model
{
    internal interface IModel
    {
        void SetDataSource(string dataSource);

        IGameDataBridge GameDataBridge { get; }

        IGroupDataBridge GroupDataBridge { get; }

        IPlayedGameDataBridge PlayedGameDataBridge { get; }
    }
}
