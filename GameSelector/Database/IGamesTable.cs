using System.Collections.Generic;

namespace GameSelector.Database
{
    internal interface IGamesTable
    {
        List<IDbGame> GetAllGames();

        List<IDbGame> GetAllGamesNotOccupied();

        IDbGame GetGameById(long id);

        IDbGame GetGameOccupiedBy(long playerId);

        void UpdateGame(IDbGame game);

        void InsertGame(IDbGame game);

        void DeleteGame(IDbGame game);
    }
}
