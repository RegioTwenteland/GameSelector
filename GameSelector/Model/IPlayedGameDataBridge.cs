using System.Collections.Generic;

namespace GameSelector.Model
{
    internal interface IPlayedGameDataBridge
    {
        IEnumerable<PlayedGame> GetPlayedGamesByPlayer(Group group);

        IEnumerable<PlayedGame> GetPlayedGamesByGame(Game game);

        void InsertPlayedGame(PlayedGame playedGame);
    }
}
