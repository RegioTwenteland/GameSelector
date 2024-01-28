using GameSelector.SQLite.Common;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.SQLite
{
    internal class SQLiteGroupsTable
    {
        private SQLiteConnection _connection;

        public const string TableName = "groups";

        public SQLiteGroupsTable(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public List<SQLiteGroup> GetAllGroups()
        {
            var gameOccupiedByColName = SQLiteHelper.GetDbName<SQLiteGame>(nameof(SQLiteGame.OccupiedById));
            var groupIdColName = SQLiteHelper.GetDbName<SQLiteGroup>(nameof(SQLiteGroup.Id));

            var sql = $@"SELECT
                            {SQLiteGroup.SQLSelectFull},
                            {SQLiteGame.SQLSelectFull}
                        FROM `{TableName}`
                        LEFT JOIN `{SQLiteGamesTable.TableName}`
                        ON `{TableName}`.`{groupIdColName}` = `{SQLiteGamesTable.TableName}`.`{gameOccupiedByColName}`;";

            var command = new SQLiteCommand(sql, _connection);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGroup>();

            while (reader.Read())
            {
                var toAdd = new SQLiteGroup(reader);
                var assocWith = new SQLiteGame(reader);
                if (assocWith.OccupiedById != null)
                    toAdd.CurrentlyPlaying = assocWith;
                outputList.Add(toAdd);
            }

            return outputList;
        }

        public SQLiteGroup GetGroupById(long id)
        {
            var gameOccupiedByColName = SQLiteHelper.GetDbName<SQLiteGame>(nameof(SQLiteGame.OccupiedById));
            var groupIdColName = SQLiteHelper.GetDbName<SQLiteGroup>(nameof(SQLiteGroup.Id));

            var sql = $@"SELECT
                            {SQLiteGroup.SQLSelectFull},
                            {SQLiteGame.SQLSelectFull}
                        FROM `{TableName}`
                        LEFT JOIN `{SQLiteGamesTable.TableName}`
                        ON `{TableName}`.`{groupIdColName}` = `{SQLiteGamesTable.TableName}`.`{gameOccupiedByColName}`
                        WHERE `{TableName}`.`{groupIdColName}` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", id);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGroup>();

            while (reader.Read())
            {
                var toAdd = new SQLiteGroup(reader);
                var assocWith = new SQLiteGame(reader);
                if (assocWith.OccupiedById != null)
                    toAdd.CurrentlyPlaying = assocWith;
                outputList.Add(toAdd);
            }

            return outputList.SingleOrDefault();
        }

        public SQLiteGroup GetGroupByCardId(string cardId)
        {
            var gameOccupiedByColName = SQLiteHelper.GetDbName<SQLiteGame>(nameof(SQLiteGame.OccupiedById));
            var groupIdColName = SQLiteHelper.GetDbName<SQLiteGroup>(nameof(SQLiteGroup.Id));
            var groupCardIdColName = SQLiteHelper.GetDbName<SQLiteGroup>(nameof(SQLiteGroup.CardId));

            var sql = $@"SELECT
                            {SQLiteGroup.SQLSelectFull},
                            {SQLiteGame.SQLSelectFull}
                        FROM `{TableName}`
                        LEFT JOIN `{SQLiteGamesTable.TableName}`
                        ON `{TableName}`.`{groupIdColName}` = `{SQLiteGamesTable.TableName}`.`{gameOccupiedByColName}`
                        WHERE `{TableName}`.`{groupCardIdColName}` = @cardId;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@cardId", cardId);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGroup>();

            while (reader.Read())
            {
                var toAdd = new SQLiteGroup(reader);
                var assocWith = new SQLiteGame(reader);
                if (assocWith.OccupiedById != null)
                    toAdd.CurrentlyPlaying = assocWith;
                outputList.Add(toAdd);
            }

            return outputList.SingleOrDefault();
        }

        public void UpdateGroup(SQLiteGroup group)
        {
            var sql = $@"UPDATE `groups` SET
	                        {SQLiteGroup.SQLUpdateFullGroup}
                        WHERE `id` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", group.Id);
            command.Parameters.AddWithValue("@card_id", group.CardId);
            command.Parameters.AddWithValue("@name", group.Name);
            command.Parameters.AddWithValue("@scouting_name", group.ScoutingName);
            command.Parameters.AddWithValue("@is_admin", group.IsAdmin);
            command.Parameters.AddWithValue("@remarks", group.Remarks);

            var rowsUpdated = command.ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }

        public void InsertGroup(SQLiteGroup group)
        {
            var sql = $@"INSERT INTO `groups` {SQLiteGroup.SQLInsertFullGroup};";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@card_id", group.CardId);
            command.Parameters.AddWithValue("@name", group.Name);
            command.Parameters.AddWithValue("@scouting_name", group.ScoutingName);
            command.Parameters.AddWithValue("@remarks", group.Remarks);

            var rowsUpdated = command.ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }

        public void DeleteGroup(SQLiteGroup group)
        {
            var sql = @"DELETE FROM `groups`
                        WHERE `id` = @id";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", group.Id);

            var rowsUpdated = command.ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }
    }
}
