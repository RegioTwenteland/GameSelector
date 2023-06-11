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

        public const string SQLSelectFullPlayedGame = @"
                            `played_games`.`id` AS played_game_id,
                            `played_games`.`player` AS played_game_player,
                            `played_games`.`game` AS played_game_game,
                            `played_games`.`start_time` AS played_game_start_time,
                            `played_games`.`end_time` AS played_game_end_time";

        public const string SQLInsertFullPlayedGame = @"
                        (
                            `player`,
                            `game`,
                            `start_time`,
                            `end_time`
                        )
                        VALUES
                        (
                            @player_id,
                            @game_id,
                            @start_time,
                            @end_time
                        )";

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
