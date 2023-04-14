using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.Database.SQLite
{
    internal class SQLiteGamesTable : IGamesTable
    {
        private SQLiteConnection _connection;

        public SQLiteGamesTable(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public List<IDbGame> GetAllGames()
        {
            var sql = @"SELECT
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
	                        `groups`.`scouting_name` AS group_scouting_name
                        FROM `games`
                        LEFT JOIN `groups`
                        ON `games`.`occupied_by` = `groups`.`id`;";

            var command = new SQLiteCommand(sql, _connection);

            var reader = command.ExecuteReader();

            var outputList = new List<IDbGame>();

            while (reader.Read())
            {
                outputList.Add(SQLiteDbGame.FromSqlReader(reader));
            }

            return outputList;
        }

        public List<IDbGame> GetAllGamesNotOccupied()
        {
            var sql = @"SELECT
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
	                        `groups`.`scouting_name` AS group_scouting_name
                        FROM `games`
                        LEFT JOIN `groups`
                        ON `games`.`occupied_by` = `groups`.`id`
                        WHERE `games`.`occupied_by` IS NULL;";

            var command = new SQLiteCommand(sql, _connection);

            var reader = command.ExecuteReader();

            var outputList = new List<IDbGame>();

            while (reader.Read())
            {
                outputList.Add(SQLiteDbGame.FromSqlReader(reader));
            }

            return outputList;
        }

        public IDbGame GetGameById(long id)
        {
            var sql = @"SELECT
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
	                        `groups`.`scouting_name` AS group_scouting_name
                        FROM `games`
                        LEFT JOIN `groups`
                        ON `games`.`occupied_by` = `groups`.`id`
                        WHERE `games`.`id` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", id);

            var reader = command.ExecuteReader();

            var outputList = new List<IDbGame>();

            while (reader.Read())
            {
                outputList.Add(SQLiteDbGame.FromSqlReader(reader));
            }

            return outputList.SingleOrDefault();
        }

        public IDbGame GetGameOccupiedBy(long playerId)
        {
            var sql = @"SELECT
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
	                        `groups`.`scouting_name` AS group_scouting_name
                        FROM `games`
                        LEFT JOIN `groups`
                        ON `games`.`occupied_by` = `groups`.`id`
                        WHERE `games`.`occupied_by` = @player_id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@player_id", playerId);

            var reader = command.ExecuteReader();

            var outputList = new List<IDbGame>();

            while (reader.Read())
            {
                outputList.Add(SQLiteDbGame.FromSqlReader(reader));
            }

            return outputList.SingleOrDefault();
        }

        public void UpdateGame(IDbGame game)
        {
            var sql = @"UPDATE `games` SET
	                        `code` = @code,
	                        `description` = @description,
	                        `explanation` = @explanation,
	                        `color` = @color,
	                        `priority` = @priority,
	                        `occupied_by` = @occupied_by,
                            `start_time` = @start_time
                        WHERE `games`.`id` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", game.Id);
            command.Parameters.AddWithValue("@code", game.Code);
            command.Parameters.AddWithValue("@description", game.Description);
            command.Parameters.AddWithValue("@explanation", game.Explanation);
            command.Parameters.AddWithValue("@color", game.Color);
            command.Parameters.AddWithValue("@priority", game.Priority);
            ////var theValue = SQLiteDatabase.ToDbNull(game.OccupiedById);
            if (game.OccupiedById == null)
                command.Parameters.AddWithValue("@occupied_by", DBNull.Value);
            else
                command.Parameters.AddWithValue("@occupied_by", game.OccupiedById.Value);

            if (game.StartTime == null)
                command.Parameters.AddWithValue("@start_time", DBNull.Value);
            else
                command.Parameters.AddWithValue("@start_time", game.StartTime.Value);

            var rowsUpdated = command.ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }
    }
}
