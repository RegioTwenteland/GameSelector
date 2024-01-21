using System.Data.SQLite;

namespace GameSelector.SQLite
{
    internal class SQLiteGame
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Explanation { get; set; }

        public long Active { get; set; }

        public string Color { get; set; }

        public long Priority { get; set; }

        public long? OccupiedById { get; set; }

        /// <summary>
        /// Warning: read-only. Don't use for modifying.
        /// </summary>
        public SQLiteGroup OccupiedBy { get; set; }

        public long? StartTime { get; set; }

        public string Remarks { get; set; }

        public long Timeout { get; set; }

        public const string SQLSelectFullGame = @"
                            `games`.`id` AS game_id,
                            `games`.`code` AS game_code,
                            `games`.`description` AS game_description,
                            `games`.`explanation` AS game_explanation,
                            `games`.`active` AS game_active,
                            `games`.`color` AS game_color,
                            `games`.`priority` AS game_priority,
                            `games`.`occupied_by` AS game_occupied_by,
                            `games`.`start_time` AS game_start_time,
                            `games`.`remarks` AS game_remarks,
                            `games`.`timeout` AS game_timeout";

        public const string SQLUpdateFullGame = @"
                            `code` = @code,
                            `description` = @description,
                            `explanation` = @explanation,
                            `active` = @active,
                            `color` = @color,
                            `priority` = @priority,
                            `occupied_by` = @occupied_by,
                            `start_time` = @start_time,
                            `remarks` = @remarks,
                            `timeout` = @timeout";

        public const string SQLInsertFullGame = @"
                        (
                            `code`,
                            `description`,
                            `explanation`,
                            `active`,
                            `color`,
                            `priority`,
                            `remarks`
                        )
                        VALUES
                        (
                            @code,
                            @description,
                            @explanation,
                            @active,
                            @color,
                            @priority,
                            @remarks
                        )";

        public static SQLiteGame FromSqlReader(SQLiteDataReader reader, SQLiteGroup occupiedBy = null)
        {
            var output = new SQLiteGame
            {
                Id = (long)reader["game_id"],
                Code = (string)reader["game_code"],
                Description = (string)reader["game_description"],
                Explanation = (string)reader["game_explanation"],
                Active = (long)reader["game_active"],
                Color = (string)reader["game_color"],
                Priority = (long)reader["game_priority"],
                OccupiedById = occupiedBy == null ? SQLiteDatabase.FromDbNull<long?>(reader["game_occupied_by"]) : occupiedBy.Id,
                StartTime = SQLiteDatabase.FromDbNull<long?>(reader["game_start_time"]),
                Remarks = (string)reader["game_remarks"],
                Timeout = (long)reader["game_timeout"],
            };

            if (occupiedBy != null)
            {
                output.OccupiedBy = occupiedBy;
            }
            else
            {
                if (output.OccupiedById.HasValue)
                    output.OccupiedBy = SQLiteGroup.FromSqlReader(reader, output);
            }

            return output;
        }
    }
}
