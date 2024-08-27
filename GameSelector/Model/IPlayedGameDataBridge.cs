using System;
using System.Collections.Generic;

namespace GameSelector.Model
{
    internal interface IPlayedGameDataBridge
    {
        public event EventHandler<PlayedGameAddedEventArgs> PlayedGameAdded;

        IEnumerable<PlayedGame> GetPlayedGamesByPlayer(Group group);

        IEnumerable<PlayedGame> GetPlayedGamesByGame(Game game);

        IEnumerable<PlayedGame> GetAllPlayedGames();

        void InsertPlayedGame(PlayedGame playedGame);
    }
}
