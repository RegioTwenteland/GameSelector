using System;
using System.Data.SQLite;
using System.Security.Policy;

namespace GameSelector.Database.SQLite
{
    internal class SQLiteDbGame : IDbGame
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Explanation { get; set; }

        public string Color { get; set; }

        public long Priority { get; set; }

        public long? OccupiedById { get; set; }

        /// <summary>
        /// Warning: read-only. Dont use for modifying.
        /// </summary>
        public IDbGroup OccupiedBy { get; set; }

        public long? StartTime { get; set; }

        public string Remarks { get; set; }

        public const string SQLSelectFullGame = @"
                            `games`.`id` AS game_id,
                            `games`.`code` AS game_code,
                            `games`.`description` AS game_description,
                            `games`.`explanation` AS game_explanation,
                            `games`.`color` AS game_color,
                            `games`.`priority` AS game_priority,
                            `games`.`occupied_by` AS game_occupied_by,
                            `games`.`start_time` AS game_start_time,
                            `games`.`remarks` AS game_remarks";

        public const string SQLUpdateFullGame = @"
                            `code` = @code,
                            `description` = @description,
                            `explanation` = @explanation,
                            `color` = @color,
                            `priority` = @priority,
                            `occupied_by` = @occupied_by,
                            `start_time` = @start_time,
                            `remarks` = @remarks";

        public const string SQLInsertFullGame = @"
                        (
                            `code`,
                            `description`,
                            `explanation`,
                            `color`,
                            `priority`,
                            `remarks`
                        )
                        VALUES
                        (
                            @code,
                            @description,
                            @explanation,
                            @color,
                            @priority,
                            @remarks
                        )";

        public static IDbGame FromSqlReader(SQLiteDataReader reader, IDbGroup occupiedBy = null)
        {
            var output = new SQLiteDbGame
            {
                Id = (long)reader["game_id"],
                Code = (string)reader["game_code"],
                Description = (string)reader["game_description"],
                Explanation = (string)reader["game_explanation"],
                Color = (string)reader["game_color"],
                Priority = (long)reader["game_priority"],
                OccupiedById = occupiedBy == null ? SQLiteDatabase.FromDbNull<long?>(reader["game_occupied_by"]) : occupiedBy.Id,
                StartTime = SQLiteDatabase.FromDbNull<long?>(reader["game_start_time"]),
                Remarks = (string)reader["game_remarks"]
            };

            if (occupiedBy != null)
            {
                output.OccupiedBy = occupiedBy;
            }
            else
            {
                if (output.OccupiedById.HasValue)
                    output.OccupiedBy = SQLiteDbGroup.FromSqlReader(reader, output);
            }

            return output;
        }
    }
}
