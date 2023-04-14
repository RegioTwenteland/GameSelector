namespace GameSelector.Model
{
    internal interface IModel
    {
        IGameDataBridge GameDataBridge { get; }

        IGroupDataBridge GroupDataBridge { get; }

        IPlayedGameDataBridge PlayedGameDataBridge { get; }
    }
}
