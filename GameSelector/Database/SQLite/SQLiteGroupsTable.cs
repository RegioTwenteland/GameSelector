using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.Database.SQLite
{
    internal class SQLiteGroupsTable : IGroupsTable
    {
        private SQLiteConnection _connection;

        public SQLiteGroupsTable(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public List<IDbGroup> GetAllGroups()
        {
            var sql = @"SELECT
	                        `groups`.`id` AS group_id,
	                        `groups`.`card_id` AS group_card_id,
	                        `groups`.`group_name` AS group_name,
	                        `groups`.`scouting_name` AS group_scouting_name,
	
	                        `games`.`id` AS game_id,
	                        `games`.`code` AS game_code,
	                        `games`.`description` AS game_description,
	                        `games`.`explanation` AS game_explanation,
	                        `games`.`color` AS game_color,
	                        `games`.`priority` AS game_priority,
	                        `games`.`occupied_by` AS game_occupied_by,
	                        `games`.`start_time` AS game_start_time
                        FROM `groups`
                        LEFT JOIN `games`
                        ON `groups`.`id` = `games`.`occupied_by`;";

            var command = new SQLiteCommand(sql, _connection);

            var reader = command.ExecuteReader();

            var outputList = new List<IDbGroup>();

            while (reader.Read())
            {
                outputList.Add(SQLiteDbGroup.FromSqlReader(reader));
            }

            return outputList;
        }

        public IDbGroup GetGroupById(long id)
        {
            var sql = @"SELECT
	                        `groups`.`id` AS group_id,
	                        `groups`.`card_id` AS group_card_id,
	                        `groups`.`group_name` AS group_name,
	                        `groups`.`scouting_name` AS group_scouting_name,
	
	                        `games`.`id` AS game_id,
	                        `games`.`code` AS game_code,
	                        `games`.`description` AS game_description,
	                        `games`.`explanation` AS game_explanation,
	                        `games`.`color` AS game_color,
	                        `games`.`priority` AS game_priority,
	                        `games`.`occupied_by` AS game_occupied_by,
	                        `games`.`start_time` AS game_start_time
                        FROM `groups`
                        LEFT JOIN `games`
                        ON `groups`.`id` = `games`.`occupied_by`
                        WHERE `groups`.`id` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", id);

            var reader = command.ExecuteReader();

            var outputList = new List<IDbGroup>();

            while (reader.Read())
            {
                outputList.Add(SQLiteDbGroup.FromSqlReader(reader));
            }

            return outputList.SingleOrDefault();
        }

        public IDbGroup GetGroupByCardId(string cardId)
        {
            var sql = @"SELECT
	                        `groups`.`id` AS group_id,
	                        `groups`.`card_id` AS group_card_id,
	                        `groups`.`group_name` AS group_name,
	                        `groups`.`scouting_name` AS group_scouting_name,
	
	                        `games`.`id` AS game_id,
	                        `games`.`code` AS game_code,
	                        `games`.`description` AS game_description,
	                        `games`.`explanation` AS game_explanation,
	                        `games`.`color` AS game_color,
	                        `games`.`priority` AS game_priority,
	                        `games`.`occupied_by` AS game_occupied_by,
	                        `games`.`start_time` AS game_start_time
                        FROM `groups`
                        LEFT JOIN `games`
                        ON `groups`.`id` = `games`.`occupied_by`
                        WHERE `groups`.`card_id` = @cardId;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@cardId", cardId);

            var reader = command.ExecuteReader();

            var outputList = new List<IDbGroup>();

            while (reader.Read())
            {
                outputList.Add(SQLiteDbGroup.FromSqlReader(reader));
            }

            return outputList.SingleOrDefault();
        }

        public void UpdateGroup(IDbGroup group)
        {
            var sql = @"UPDATE `groups` SET
	                        `card_id` = @card_id,
	                        `group_name` = @name,
	                        `scouting_name` = @scouting_name
                        WHERE `id` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", group.Id);
            command.Parameters.AddWithValue("@card_id", group.CardId);
            command.Parameters.AddWithValue("@name", group.Name);
            command.Parameters.AddWithValue("@scouting_name", group.ScoutingName);

            var rowsUpdated = command.ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }

        public void InsertGroup(IDbGroup group)
        {
            var sql = @"INSERT INTO `groups`
                        (
	                        `id`,
	                        `card_id`,
	                        `group_name`,
	                        `scouting_name`
                        )
                        VALUES
                        (
	                        @id,
	                        @card_id,
	                        @name,
	                        @scouting_name
                        );";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", group.Id);
            command.Parameters.AddWithValue("@card_id", group.CardId);
            command.Parameters.AddWithValue("@name", group.Name);
            command.Parameters.AddWithValue("@scouting_name", group.ScoutingName);

            var rowsUpdated = command.ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }

        public void DeleteGroup(IDbGroup group)
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
