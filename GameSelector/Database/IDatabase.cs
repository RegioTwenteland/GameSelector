namespace GameSelector.Database
{
    internal interface IDatabase
    {
        IGamesTable GamesTable { get; }

        IGroupsTable GroupsTable { get; }

        IPlayedGamesTable PlayedGamesTable { get; }

        IDatabaseObjectTranslator ObjectTranslator { get; }
    }
}
