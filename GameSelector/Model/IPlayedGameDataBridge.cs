using GameSelector.Database;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameSelector.Model
{
    internal interface IPlayedGameDataBridge
    {
        List<PlayedGame> GetPlayedGamesByPlayer(Group group);

        List<PlayedGame> GetPlayedGamesByGame(Game game);

        void InsertPlayedGame(PlayedGame playedGame);
    }
}
