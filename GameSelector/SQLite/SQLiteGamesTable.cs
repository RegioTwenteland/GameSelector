using GameSelector.SQLite.Common;
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

        public const string TableName = "games";

        public SQLiteGamesTable(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public List<SQLiteGame> GetAllGames()
        {
            var gameOccupiedByColName = SQLiteHelper.GetDbName<SQLiteGame>(nameof(SQLiteGame.OccupiedById));
            var groupIdColName = SQLiteHelper.GetDbName<SQLiteGroup>(nameof(SQLiteGroup.Id));

            var sql = $@"SELECT
	                        {SQLiteGame.SQLSelectFull},
	                        {SQLiteGroup.SQLSelectFull}
                        FROM `{TableName}`
                        LEFT JOIN `{SQLiteGroupsTable.TableName}`
                        ON `{TableName}`.`{gameOccupiedByColName}` = `{SQLiteGroupsTable.TableName}`.`{groupIdColName}`;";

            var command = new SQLiteCommand(sql, _connection);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGame>();

            while (reader.Read())
            {
                var toAdd = new SQLiteGame(reader);
                if (toAdd.OccupiedById.HasValue)
                {
                    var assocGroup = new SQLiteGroup(reader);
                    toAdd.OccupiedBy = assocGroup;
                }
                outputList.Add(toAdd);
            }

            return outputList;
        }

        public List<SQLiteGame> GetAllGamesNotOccupied()
        {
            var gameOccupiedByColName = SQLiteHelper.GetDbName<SQLiteGame>(nameof(SQLiteGame.OccupiedById));

            var sql = $@"SELECT
	                        {SQLiteGame.SQLSelectFull}
                        FROM `{TableName}`
                        WHERE `{TableName}`.`{gameOccupiedByColName}` IS NULL;";

            var command = new SQLiteCommand(sql, _connection);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGame>();

            while (reader.Read())
            {
                outputList.Add(new SQLiteGame(reader));
            }

            return outputList;
        }

        public SQLiteGame GetGameById(long id)
        {
            var gameOccupiedByColName = SQLiteHelper.GetDbName<SQLiteGame>(nameof(SQLiteGame.OccupiedById));
            var gameIdColName = SQLiteHelper.GetDbName<SQLiteGame>(nameof(SQLiteGame.Id));
            var groupIdColName = SQLiteHelper.GetDbName<SQLiteGroup>(nameof(SQLiteGroup.Id));

            var sql = $@"SELECT
	                        {SQLiteGame.SQLSelectFull},
	                        {SQLiteGroup.SQLSelectFull}
                        FROM `{TableName}`
                        LEFT JOIN `{SQLiteGroupsTable.TableName}`
                        ON `{TableName}`.`{gameOccupiedByColName}` = `{SQLiteGroupsTable.TableName}`.`{groupIdColName}`
                        WHERE `{TableName}`.`{gameIdColName}` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", id);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGame>();

            while (reader.Read())
            {
                var toAdd = new SQLiteGame(reader);
                if (toAdd.OccupiedById.HasValue)
                {
                    var assocGroup = new SQLiteGroup(reader);
                    toAdd.OccupiedBy = assocGroup;
                }
                outputList.Add(toAdd);
            }

            return outputList.SingleOrDefault();
        }

        public SQLiteGame GetGameOccupiedBy(long playerId)
        {
            var gameOccupiedByColName = SQLiteHelper.GetDbName<SQLiteGame>(nameof(SQLiteGame.OccupiedById));
            var groupIdColName = SQLiteHelper.GetDbName<SQLiteGroup>(nameof(SQLiteGroup.Id));

            var sql = $@"SELECT
	                        {SQLiteGame.SQLSelectFull},
	                        {SQLiteGroup.SQLSelectFull}
                        FROM `{TableName}`
                        LEFT JOIN `{SQLiteGroupsTable.TableName}`
                        ON `{TableName}`.`{gameOccupiedByColName}` = `{SQLiteGroupsTable.TableName}`.`{groupIdColName}`
                        WHERE `{TableName}`.`{gameOccupiedByColName}` = @player_id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@player_id", playerId);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGame>();

            while (reader.Read())
            {
                var toAdd = new SQLiteGame(reader);
                if (toAdd.OccupiedById.HasValue)
                {
                    var assocGroup = new SQLiteGroup(reader);
                    toAdd.OccupiedBy = assocGroup;
                }
                outputList.Add(toAdd);
            }

            return outputList.SingleOrDefault();
        }

        public void UpdateGame(SQLiteGame game)
        {
            var gameIdColName = SQLiteHelper.GetDbName<SQLiteGame>(nameof(SQLiteGame.Id));

            var sql = $@"UPDATE `games` SET
	                        {game.SQLUpdateFull}
                        WHERE `{TableName}`.`{gameIdColName}` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            game.AddAllParametersForPreparedStatement(command);
            command.Parameters.AddWithValue("@id", game.Id);

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
