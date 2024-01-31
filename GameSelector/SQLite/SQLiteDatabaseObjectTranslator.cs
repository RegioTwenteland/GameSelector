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
                Color = game.Color,
                Priority = game.HasPriority ? 1 : 0,
                OccupiedById = game.OccupiedBy?.Id,
                StartTime = game.StartTime.HasValue ? (long?)game.StartTime.Value.Ticks : null,
                Remarks = game.Remarks ?? "",
                Timeout = game.Timeout.Ticks,
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

        public Game ToGame(SQLiteGame dbGame, Group group = null)
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
                Color = dbGame.Color,
                HasPriority = dbGame.Priority == 0 ? false : true,
                StartTime = dbGame.StartTime.HasValue ? (DateTime?)new DateTime(dbGame.StartTime.Value) : null,
                Remarks = dbGame.Remarks,
                Timeout = new TimeSpan(dbGame.Timeout),
            };

            output.OccupiedBy = group ?? ToGroup(dbGame.OccupiedBy, output);

            return output;
        }

        public Group ToGroup(SQLiteGroup dbGroup, Game game = null)
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
                Remarks = dbGroup.Remarks,
            };

            output.CurrentlyPlaying = game ?? ToGame(dbGroup.CurrentlyPlaying, output);

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
