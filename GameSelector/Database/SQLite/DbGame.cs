using System;
using System.Data.SQLite;

namespace GameSelector.Database.SQLite
{
    internal class DbGame
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
        public DbGroup OccupiedBy { get; set; }

        public long? StartTime { get; set; }

        public static DbGame FromSqlReader(SQLiteDataReader reader, DbGroup OccupiedBy = null)
        {
            var output = new DbGame
            {
                Id = (long)reader["game_id"],
                Code = (string)reader["game_code"],
                Description = (string)reader["game_description"],
                Explanation = (string)reader["game_explanation"],
                Color = (string)reader["game_color"],
                Priority = (long)reader["game_priority"],
                OccupiedById = SQLiteDatabase.FromDbNull<long?>(reader["game_occupied_by"]),
                StartTime = SQLiteDatabase.FromDbNull<long?>(reader["game_start_time"])
            };

            if (output.OccupiedById.HasValue)
                output.OccupiedBy = DbGroup.FromSqlReader(reader, output);

            return output;
        }
    }
}
