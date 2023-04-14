using System.Collections.Generic;

namespace GameSelector.Database
{
    internal interface IPlayedGamesTable
    {
        List<IDbPlayedGame> GetPlayedGamesByPlayerId(long playerId);

        List<IDbPlayedGame> GetPlayedGamesByGameId(long gameId);

        void InsertPlayedGame(IDbPlayedGame game);
    }
}
