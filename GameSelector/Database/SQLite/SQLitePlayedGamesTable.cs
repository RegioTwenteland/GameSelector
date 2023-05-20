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
	                        `played_games`.`end_time` AS played_game_end_time,
	
	                        `games`.`id` AS game_id,
	                        `games`.`code` AS game_code,
	                        `games`.`description` AS game_description,
	                        `games`.`explanation` AS game_explanation,
	                        `games`.`color` AS game_color,
	                        `games`.`priority` AS game_priority,
	                        `games`.`occupied_by` AS game_occupied_by,
	                        `games`.`start_time` AS game_start_time,
	
	                        `groups`.`id` AS group_id,
	                        `groups`.`card_id` AS group_card_id,
	                        `groups`.`group_name` AS group_name,
	                        `groups`.`scouting_name` AS group_scouting_name,
	                        `groups`.`is_admin` AS group_is_admin
                        FROM `played_games`
                        LEFT JOIN `games`
                        ON `games`.`id` = `played_games`.`game`
                        LEFT JOIN `groups`
                        ON `groups`.`id` = `played_games`.`player`
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
	                        `played_games`.`end_time` AS played_game_end_time,
	
	                        `games`.`id` AS game_id,
	                        `games`.`code` AS game_code,
	                        `games`.`description` AS game_description,
	                        `games`.`explanation` AS game_explanation,
	                        `games`.`color` AS game_color,
	                        `games`.`priority` AS game_priority,
	                        `games`.`occupied_by` AS game_occupied_by,
	                        `games`.`start_time` AS game_start_time,
	
	                        `groups`.`id` AS group_id,
	                        `groups`.`card_id` AS group_card_id,
	                        `groups`.`group_name` AS group_name,
	                        `groups`.`scouting_name` AS group_scouting_name,
	                        `groups`.`is_admin` AS group_is_admin
                        FROM `played_games`
                        LEFT JOIN `games`
                        ON `games`.`id` = `played_games`.`game`
                        LEFT JOIN `groups`
                        ON `groups`.`id` = `played_games`.`player`
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
