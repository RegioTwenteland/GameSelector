﻿using System.Data.SQLite;

namespace GameSelector.Database.SQLite
{
    internal class SQLiteDbPlayedGame : IDbPlayedGame
    {
        public long Id { get; set; }

        public long PlayerId { get; set; }

        public long GameId { get; set; }

        public long StartTime { get; set; }

        public long EndTime { get; set; }

        public static IDbPlayedGame FromSqlReader(SQLiteDataReader reader)
        {
            return new SQLiteDbPlayedGame
            {
                Id = (long)reader["played_game_id"],
                PlayerId = (long)reader["played_game_player"],
                GameId = (long)reader["played_game_game"],
                StartTime = (long)reader["played_game_start_time"],
                EndTime = (long)reader["played_game_end_time"],
            };
        }
    }
}