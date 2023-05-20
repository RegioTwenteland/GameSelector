using GameSelector.Model;

namespace GameSelector.Database.SQLite
{
    internal class SQLiteDatabaseObjectTranslator : IDatabaseObjectTranslator
    {
        public IDbGame ToDbGame(Game game)
        {
            return new SQLiteDbGame
            {
                Id = game.Id,
                Code = game.Code,
                Description = game.Description,
                Explanation = game.Explanation,
                Color = game.Color,
                Priority = game.HasPriority ? 1 : 0,
                OccupiedById = game.OccupiedBy?.Id,
                StartTime = game.StartTime.HasValue ? (long?)game.StartTime.Value.Ticks : null
            };
        }

        public IDbGroup ToDbGroup(Group group)
        {
            if (group == null) return null;

            return new SQLiteDbGroup
            {
                Id = group.Id,
                CardId = group.CardId,
                Name = group.Name,
                ScoutingName = group.ScoutingName,
                IsAdmin = group.IsAdmin ? 1 : 0,
            };
        }

        public IDbPlayedGame ToDbPlayedGame(PlayedGame playedGame)
        {
            return new SQLiteDbPlayedGame
            {
                Id = playedGame.Id,
                PlayerId = playedGame.Player.Id,
                GameId = playedGame.Game.Id,
                StartTime = playedGame.StartTime.Ticks,
                EndTime = playedGame.EndTime.Ticks
            };
        }
    }
}
