using System.Collections.Generic;

namespace GameSelector.Model
{
    internal interface IPlayedGameDataBridge
    {
        List<PlayedGame> GetPlayedGamesByPlayer(Group group);

        List<PlayedGame> GetPlayedGamesByGame(Game game);

        void InsertPlayedGame(PlayedGame playedGame);
    }
}
