using GameSelector.Database;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameSelector.Model
{
    internal interface IPlayedGameDataBridge
    {
        List<PlayedGame> GetPlayedGamesByPlayer(Group group);

        void InsertPlayedGame(PlayedGame playedGame);
    }
}
