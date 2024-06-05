using GameSelector.Model;
using System;

namespace GameSelector.SQLite
{
    internal class SQLiteDatabaseObjectTranslator
    {
        public SQLiteGame ToSQLiteGame(Game game)
        {
            if (game == null) return null;

            return new SQLiteGame
            {
                Id = game.Id,
                Code = game.Code,
                Description = game.Description,
                Explanation = game.Explanation,
                Active = game.Active ? 1 : 0,
                Priority = game.HasPriority ? 1 : 0,
                Remarks = game.Remarks ?? "",
                Timeout = game.Timeout.Ticks,
                MaxPlayerAmount = game.MaxPlayerAmount,
            };
        }

        public SQLiteGroup ToSQLiteGroup(Group group)
        {
            if (group == null) return null;

            return new SQLiteGroup
            {
                Id = group.Id,
                CardId = group.CardId,
                Name = group.Name,
                ScoutingName = group.ScoutingName,
                IsAdmin = group.IsAdmin ? 1 : 0,
                CurrentlyPlayingId = group.CurrentlyPlaying?.Id,
                StartTime = group.StartTime.HasValue ? (long?)group.StartTime.Value.Ticks : null,
                Remarks = group.Remarks ?? "",
            };
        }

        public SQLitePlayedGame ToSQLitePlayedGame(PlayedGame playedGame)
        {
            if (playedGame == null) return null;

            return new SQLitePlayedGame
            {
                Id = playedGame.Id,
                PlayerId = playedGame.Player.Id,
                GameId = playedGame.Game.Id,
                StartTime = playedGame.StartTime.Ticks,
                EndTime = playedGame.EndTime.Ticks
            };
        }

        public Game ToGame(SQLiteGame dbGame)
        {
            if (dbGame == null) return null;
            if (dbGame.Id == 0) return null;

            var output = new Game
            {
                Id = dbGame.Id,
                Code = dbGame.Code,
                Description = dbGame.Description,
                Explanation = dbGame.Explanation,
                Active = dbGame.Active == 0 ? false : true,
                HasPriority = dbGame.Priority == 0 ? false : true,
                Remarks = dbGame.Remarks,
                Timeout = new TimeSpan(dbGame.Timeout),
                MaxPlayerAmount = dbGame.MaxPlayerAmount,
            };

            return output;
        }

        public Group ToGroup(SQLiteGroup dbGroup)
        {
            if (dbGroup == null) return null;
            if (dbGroup.Id == 0) return null;

            var output = new Group
            {
                Id = dbGroup.Id,
                CardId = dbGroup.CardId,
                Name = dbGroup.Name,
                ScoutingName = dbGroup.ScoutingName,
                IsAdmin = dbGroup.IsAdmin != 0,
                CurrentlyPlaying = ToGame(dbGroup.CurrentlyPlaying),
                StartTime = dbGroup.StartTime.HasValue ? (DateTime?)new DateTime(dbGroup.StartTime.Value) : null,
                Remarks = dbGroup.Remarks,
            };

            return output;
        }

        public PlayedGame ToPlayedGame(SQLitePlayedGame dbPlayedGame)
        {
            if (dbPlayedGame == null) return null;
            if (dbPlayedGame.Id == 0) return null;

            return new PlayedGame
            {
                Id = dbPlayedGame.Id,
                Player = ToGroup(dbPlayedGame.Player),
                Game = ToGame(dbPlayedGame.Game),
                StartTime = new DateTime(dbPlayedGame.StartTime),
                EndTime = new DateTime(dbPlayedGame.EndTime)
            };
        }
    }
}
