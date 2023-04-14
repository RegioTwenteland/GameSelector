using System.Collections.Generic;
using System.Linq;

namespace GameSelector.Model
{
    internal interface IGameDataBridge
    {
        List<Game> GetAllGames();

        List<Game> GetAllGamesNotOccupied();

        Game GetGame(long id);

        Game GetGameOccupiedBy(Group group);

        void UpdateGame(Game game);
    }
}
