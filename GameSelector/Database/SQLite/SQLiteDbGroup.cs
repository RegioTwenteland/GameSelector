using System.Data.SQLite;

namespace GameSelector.Database.SQLite
{
    internal class SQLiteDbGroup : IDbGroup
    {
        public long Id { get; set; }

        public string CardId { get; set; }

        public string Name { get; set; }

        public string ScoutingName { get; set; }

        /// <summary>
        /// Warning: read-only. Dont use for modifying.
        /// </summary>
        public IDbGame CurrentlyPlaying { get; set; }

        public static IDbGroup FromSqlReader(SQLiteDataReader reader, IDbGame CurrentlyPlaying = null)
        {
            var output = new SQLiteDbGroup
            {
                Id = (long)reader["group_id"],
                CardId = (string)reader["group_card_id"],
                Name = (string)reader["group_name"],
                ScoutingName = (string)reader["group_scouting_name"],
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
