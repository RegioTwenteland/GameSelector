using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.SQLite
{
    internal class SQLiteGamesTable
    {
        private SQLiteConnection _connection;

        public SQLiteGamesTable(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public List<SQLiteGame> GetAllGames()
        {
            var sql = $@"SELECT
	                        {SQLiteGame.SQLSelectFullGame},
	                        {SQLiteGroup.SQLSelectFullGroup}
                        FROM `games`
                        LEFT JOIN `groups`
                        ON `games`.`occupied_by` = `groups`.`id`;";

            var command = new SQLiteCommand(sql, _connection);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGame>();

            while (reader.Read())
            {
                outputList.Add(SQLiteGame.FromSqlReader(reader));
            }

            return outputList;
        }

        public List<SQLiteGame> GetAllGamesNotOccupied()
        {
            var sql = $@"SELECT
	                        {SQLiteGame.SQLSelectFullGame},
	                        {SQLiteGroup.SQLSelectFullGroup}
                        FROM `games`
                        LEFT JOIN `groups`
                        ON `games`.`occupied_by` = `groups`.`id`
                        WHERE `games`.`occupied_by` IS NULL;";

            var command = new SQLiteCommand(sql, _connection);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGame>();

            while (reader.Read())
            {
                outputList.Add(SQLiteGame.FromSqlReader(reader));
            }

            return outputList;
        }

        public SQLiteGame GetGameById(long id)
        {
            var sql = $@"SELECT
	                        {SQLiteGame.SQLSelectFullGame},
	                        {SQLiteGroup.SQLSelectFullGroup}
                        FROM `games`
                        LEFT JOIN `groups`
                        ON `games`.`occupied_by` = `groups`.`id`
                        WHERE `games`.`id` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", id);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGame>();

            while (reader.Read())
            {
                outputList.Add(SQLiteGame.FromSqlReader(reader));
            }

            return outputList.SingleOrDefault();
        }

        public SQLiteGame GetGameOccupiedBy(long playerId)
        {
            var sql = $@"SELECT
	                        {SQLiteGame.SQLSelectFullGame},
	                        {SQLiteGroup.SQLSelectFullGroup}
                        FROM `games`
                        LEFT JOIN `groups`
                        ON `games`.`occupied_by` = `groups`.`id`
                        WHERE `games`.`occupied_by` = @player_id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@player_id", playerId);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGame>();

            while (reader.Read())
            {
                outputList.Add(SQLiteGame.FromSqlReader(reader));
            }

            return outputList.SingleOrDefault();
        }

        public void UpdateGame(SQLiteGame game)
        {
            var sql = $@"UPDATE `games` SET
	                        {SQLiteGame.SQLUpdateFullGame}
                        WHERE `games`.`id` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", game.Id);
            command.Parameters.AddWithValue("@code", game.Code);
            command.Parameters.AddWithValue("@description", game.Description);
            command.Parameters.AddWithValue("@explanation", game.Explanation);
            command.Parameters.AddWithValue("@active", game.Active);
            command.Parameters.AddWithValue("@color", game.Color);
            command.Parameters.AddWithValue("@priority", game.Priority);
            command.Parameters.AddWithValue("@remarks", game.Remarks);
            command.Parameters.AddWithValue("@timeout", game.Timeout);
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

        public void InsertGame(SQLiteGame game)
        {
            var sql = $@"INSERT INTO `games` {SQLiteGame.SQLInsertFullGame};";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@code", game.Code);
            command.Parameters.AddWithValue("@description", game.Description);
            command.Parameters.AddWithValue("@explanation", game.Explanation);
            command.Parameters.AddWithValue("@active", game.Active);
            command.Parameters.AddWithValue("@color", game.Color);
            command.Parameters.AddWithValue("@priority", game.Priority);
            command.Parameters.AddWithValue("@remarks", game.Remarks);

            var rowsUpdated = command.ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }

        public void DeleteGame(SQLiteGame game)
        {
            var sql = @"DELETE FROM `games`
                        WHERE `id` = @id";
            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", game.Id);

            var rowsUpdated = command.ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }
    }
}
