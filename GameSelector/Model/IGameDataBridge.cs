using System;
using System.Collections.Generic;

namespace GameSelector.Model
{
    internal interface IGameDataBridge
    {
        event EventHandler<GameUpdatedEventArgs> GameUpdated;

        IEnumerable<Game> GetAllGames();

        IEnumerable<Game> GetAllGamesNotOccupied();

        Game GetGameBeingPlayedBy(Group group);

        Game GetGame(long id);

        void UpdateGame(Game game);

        void InsertGame(Game game);

        void DeleteGame(Game game);
    }
}
