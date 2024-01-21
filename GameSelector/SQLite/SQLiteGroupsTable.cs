using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.SQLite
{
    internal class SQLiteGroupsTable
    {
        private SQLiteConnection _connection;

        public SQLiteGroupsTable(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public List<SQLiteGroup> GetAllGroups()
        {
            var sql = $@"SELECT
                            {SQLiteGroup.SQLSelectFullGroup},
                            {SQLiteGame.SQLSelectFullGame}
                        FROM `groups`
                        LEFT JOIN `games`
                        ON `groups`.`id` = `games`.`occupied_by`;";

            var command = new SQLiteCommand(sql, _connection);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGroup>();

            while (reader.Read())
            {
                outputList.Add(SQLiteGroup.FromSqlReader(reader));
            }

            return outputList;
        }

        public SQLiteGroup GetGroupById(long id)
        {
            var sql = $@"SELECT
                            {SQLiteGroup.SQLSelectFullGroup},
                            {SQLiteGame.SQLSelectFullGame}
                        FROM `groups`
                        LEFT JOIN `games`
                        ON `groups`.`id` = `games`.`occupied_by`
                        WHERE `groups`.`id` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", id);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGroup>();

            while (reader.Read())
            {
                outputList.Add(SQLiteGroup.FromSqlReader(reader));
            }

            return outputList.SingleOrDefault();
        }

        public SQLiteGroup GetGroupByCardId(string cardId)
        {
            var sql = $@"SELECT
                            {SQLiteGroup.SQLSelectFullGroup},
                            {SQLiteGame.SQLSelectFullGame}
                        FROM `groups`
                        LEFT JOIN `games`
                        ON `groups`.`id` = `games`.`occupied_by`
                        WHERE `groups`.`card_id` = @cardId;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@cardId", cardId);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLiteGroup>();

            while (reader.Read())
            {
                outputList.Add(SQLiteGroup.FromSqlReader(reader));
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
