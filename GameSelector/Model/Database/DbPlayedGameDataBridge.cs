using GameSelector.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.Model.Database
{
    internal class DbPlayedGameDataBridge : IPlayedGameDataBridge
    {
        private readonly IPlayedGamesTable _playedGamesTable;
        private readonly IDatabaseObjectTranslator _objectTranslator;

        public DbPlayedGameDataBridge(IDatabase database)
        {
            _playedGamesTable = database.PlayedGamesTable;
            _objectTranslator = database.ObjectTranslator;
        }

        public static PlayedGame DbPlayedGameToPlayedGame(IDbPlayedGame dbPlayedGame)
        {
            return new PlayedGame
            {
                Id = dbPlayedGame.Id,
                PlayerId = dbPlayedGame.PlayerId,
                GameId = dbPlayedGame.GameId,
                StartTime = new DateTime(dbPlayedGame.StartTime),
                EndTime = new DateTime(dbPlayedGame.EndTime)
            };
        }

        public List<PlayedGame> GetPlayedGamesByPlayer(Group group)
        {
            return
                _playedGamesTable.GetPlayedGamesByPlayerId(group.Id)
                .Select(dbG => DbPlayedGameToPlayedGame(dbG))
                .ToList();
        }

        public void InsertPlayedGame(PlayedGame playedGame)
        {
            Debug.Assert(playedGame.Id == 0);
            _playedGamesTable.InsertPlayedGame(_objectTranslator.ToDbPlayedGame(playedGame));
        }
    }
}
