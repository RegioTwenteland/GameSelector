using GameSelector.Database.SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.Model
{
    internal class PlayedGameDataBridge
    {
        private PlayedGamesTable _playedGamesTable;

        public PlayedGameDataBridge(PlayedGamesTable playedGamesTable)
        {
            _playedGamesTable = playedGamesTable;
        }

        public static PlayedGame DbPlayedGameToPlayedGame(DbPlayedGame dbPlayedGame)
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

        public static DbPlayedGame PlayedGameToDbPlayedGame(PlayedGame playedGame)
        {
            return new DbPlayedGame
            {
                Id = playedGame.Id,
                PlayerId = playedGame.PlayerId,
                GameId = playedGame.GameId,
                StartTime = playedGame.StartTime.Ticks,
                EndTime = playedGame.EndTime.Ticks
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
            _playedGamesTable.InsertPlayedGame(PlayedGameToDbPlayedGame(playedGame));
        }
    }
}
