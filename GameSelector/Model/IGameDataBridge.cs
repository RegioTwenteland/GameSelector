using System;
using System.Collections.Generic;

namespace GameSelector.Model
{
    internal interface IGameDataBridge
    {
        event EventHandler<GameUpdatedEventArgs> GameUpdated;

        List<Game> GetAllGames();

        List<Game> GetAllGamesNotOccupied();

        Game GetGame(long id);

        Game GetGameOccupiedBy(Group group);

        void UpdateGame(Game game);

        void InsertGame(Game game);

        void DeleteGame(Game game);
    }
}
