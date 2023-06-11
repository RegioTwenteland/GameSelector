using System.Data.SQLite;

namespace GameSelector.Database.SQLite
{
    internal class SQLiteDbGroup : IDbGroup
    {
        public long Id { get; set; }

        public string CardId { get; set; }

        public string Name { get; set; }

        public string ScoutingName { get; set; }

        public long IsAdmin { get; set; }

        /// <summary>
        /// Warning: read-only. Dont use for modifying.
        /// </summary>
        public IDbGame CurrentlyPlaying { get; set; }

        public string Remarks { get; set; }

        public const string SQLSelectFullGroup = @"
                            `groups`.`id` AS group_id,
                            `groups`.`card_id` AS group_card_id,
                            `groups`.`group_name` AS group_name,
                            `groups`.`scouting_name` AS group_scouting_name,
                            `groups`.`is_admin` AS group_is_admin,
                            `groups`.`remarks` AS group_remarks";

        public const string SQLUpdateFullGroup = @"
                            `card_id` = @card_id,
                            `group_name` = @name,
                            `scouting_name` = @scouting_name,
                            `is_admin` = @is_admin,
                            `remarks` = @remarks";

        public const string SQLInsertFullGroup = @"
                        (
                            `card_id`,
                            `group_name`,
                            `scouting_name`,
                            `remarks`
                        )
                        VALUES
                        (
                            @card_id,
                            @name,
                            @scouting_name,
                            @remarks
                        )";

        public static IDbGroup FromSqlReader(SQLiteDataReader reader, IDbGame CurrentlyPlaying = null)
        {
            
            var output = new SQLiteDbGroup
            {
                Id = (long)reader["group_id"],
                CardId = SQLiteDatabase.FromDbNull<string>(reader["group_card_id"]),
                Name = (string)reader["group_name"],
                ScoutingName = (string)reader["group_scouting_name"],
                IsAdmin = (long)reader["group_is_admin"],
                Remarks = (string)reader["group_remarks"]
            };

            try
            {
                output.CurrentlyPlaying = CurrentlyPlaying ?? SQLiteDbGame.FromSqlReader(reader, output);
            }
            catch
            {
                output.CurrentlyPlaying = null;
            }

            return output;
        }
    }
}
