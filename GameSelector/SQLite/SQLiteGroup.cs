using GameSelector.SQLite.Common;
using System.Data.SQLite;

namespace GameSelector.SQLite
{
    [SQLiteTable(TableName = "groups")]
    internal class SQLiteGroup : SQLiteObject
    {
        public SQLiteGroup()
        {
        }

        public SQLiteGroup(SQLiteDataReader reader)
            : base(reader)
        {
        }

        [SQLiteColumn(Name = "id", IsPK = true)]
        public long Id { get; set; }

        [SQLiteColumn(Name = "card_id")]
        public string CardId { get; set; }

        [SQLiteColumn(Name = "group_name")]
        public string Name { get; set; }

        [SQLiteColumn(Name = "scouting_name")]
        public string ScoutingName { get; set; }

        [SQLiteColumn(Name = "is_admin")]
        public long IsAdmin { get; set; }

        /// <summary>
        /// Warning: read-only. Dont use for modifying.
        /// </summary>
        [SQLiteForeignKey]
        public SQLiteGame CurrentlyPlaying { get; set; }

        [SQLiteColumn(Name = "remarks")]
        public string Remarks { get; set; }
    }
}
