using System;
using System.Data.SQLite;

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
                StartTime = SQLiteDatabase.FromDbNull<long?>(reader["game_start_time"])
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
