using System.Data.SQLite;

namespace GameSelector.Database.SQLite
{
    internal class SQLiteDbPlayedGame : IDbPlayedGame
    {
        public long Id { get; set; }

        public long PlayerId { get; set; }

        /// <summary>
        /// Warning: read-only. Dont use for modifying.
        /// </summary>
        public IDbGroup Player { get; set; }

        public long GameId { get; set; }

        /// <summary>
        /// Warning: read-only. Dont use for modifying.
        /// </summary>
        public IDbGame Game { get; set; }

        public long StartTime { get; set; }

        public long EndTime { get; set; }

        public static IDbPlayedGame FromSqlReader(SQLiteDataReader reader)
        {
            return new SQLiteDbPlayedGame
            {
                Id = (long)reader["played_game_id"],
                PlayerId = (long)reader["played_game_player"],
                Player = SQLiteDbGroup.FromSqlReader(reader),
                GameId = (long)reader["played_game_game"],
                Game = SQLiteDbGame.FromSqlReader(reader),
                StartTime = (long)reader["played_game_start_time"],
                EndTime = (long)reader["played_game_end_time"],
            };
        }
    }
}
