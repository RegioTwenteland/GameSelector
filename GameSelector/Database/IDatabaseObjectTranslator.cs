using GameSelector.Model;

namespace GameSelector.Database
{
    internal interface IDatabaseObjectTranslator
    {
        IDbGame ToDbGame(Game game);

        IDbGroup ToDbGroup(Group group);

        IDbPlayedGame ToDbPlayedGame(PlayedGame playedGame);
    }
}
