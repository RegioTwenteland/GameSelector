using GameSelector.SQLite.Common;
using System.Data.SQLite;

namespace GameSelector.SQLite
{
    [SQLiteTable(TableName = "played_games")]
    internal class SQLitePlayedGame : SQLiteObject
    {
        public SQLitePlayedGame()
        {
        }

        public SQLitePlayedGame(SQLiteDataReader reader)
            : base(reader)
        {
        }

        [SQLiteColumn(Name = "id", IsPK = true)]
        public long Id { get; set; }

        [SQLiteColumn(Name = "player")]
        public long PlayerId { get; set; }

        /// <summary>
        /// Warning: read-only. Don't use for modifying.
        /// </summary>
        [SQLiteForeignKey]
        public SQLiteGroup Player { get; set; }

        [SQLiteColumn(Name = "game")]
        public long GameId { get; set; }

        /// <summary>
        /// Warning: read-only. Don't use for modifying.
        /// </summary>
        [SQLiteForeignKey]
        public SQLiteGame Game { get; set; }

        [SQLiteColumn(Name = "start_time")]
        public long StartTime { get; set; }

        [SQLiteColumn(Name = "end_time")]
        public long EndTime { get; set; }
    }
}
