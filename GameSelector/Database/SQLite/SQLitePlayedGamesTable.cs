using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;

namespace GameSelector.Database.SQLite
{
    internal class SQLitePlayedGamesTable : IPlayedGamesTable
    {
        private SQLiteConnection _connection;

        public SQLitePlayedGamesTable(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public List<IDbPlayedGame> GetPlayedGamesByPlayerId(long playerId)
        {
            var sql = @"SELECT
                            `played_games`.`id` AS played_game_id,
	                        `played_games`.`player` AS played_game_player,
	                        `played_games`.`game` AS played_game_game,
	                        `played_games`.`start_time` AS played_game_start_time,
	                        `played_games`.`end_time` AS played_game_end_time
                        FROM `played_games`
                        WHERE `player` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", playerId);

            var reader = command.ExecuteReader();

            var outputList = new List<IDbPlayedGame>();

            while (reader.Read())
            {
                outputList.Add(SQLiteDbPlayedGame.FromSqlReader(reader));
            }

            return outputList;
        }

        public List<IDbPlayedGame> GetPlayedGamesByGameId(long gameId)
        {
            var sql = @"SELECT
                            `played_games`.`id` AS played_game_id,
	                        `played_games`.`player` AS played_game_player,
	                        `played_games`.`game` AS played_game_game,
	                        `played_games`.`start_time` AS played_game_start_time,
	                        `played_games`.`end_time` AS played_game_end_time
                        FROM `played_games`
                        WHERE `game` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", gameId);

            var reader = command.ExecuteReader();

            var outputList = new List<IDbPlayedGame>();

            while (reader.Read())
            {
                outputList.Add(SQLiteDbPlayedGame.FromSqlReader(reader));
            }

            return outputList;
        }

        public void InsertPlayedGame(IDbPlayedGame game)
        {
            var sql = @"INSERT INTO `played_games`
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
                        );";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@player_id", game.PlayerId);
            command.Parameters.AddWithValue("@game_id", game.GameId);
            command.Parameters.AddWithValue("@start_time", game.StartTime);
            command.Parameters.AddWithValue("@end_time", game.EndTime);

            var rowsUpdated = command.ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }
    }
}
