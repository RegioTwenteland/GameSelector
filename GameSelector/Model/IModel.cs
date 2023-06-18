namespace GameSelector.Model
{
    internal interface IModel
    {
        void SetDataSourceParam(string parameters);

        IGameDataBridge GameDataBridge { get; }

        IGroupDataBridge GroupDataBridge { get; }

        IPlayedGameDataBridge PlayedGameDataBridge { get; }
    }
}
