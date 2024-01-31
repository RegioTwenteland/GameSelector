using GameSelector.SQLite.Common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace GameSelector.SQLite
{
    [SQLiteTable(TableName = "groups")]
    internal class SQLiteGroup : SQLiteObject
    {
        public SQLiteGroup()
            : base(SQLiteGroupsTable.TableName)
        {
        }

        public SQLiteGroup(SQLiteDataReader reader)
            : this()
        {
            FillPropsFromSqlReader(reader);
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

        public static string SQLSelectFull =>
            SQLiteHelper.SqlForSelectTableItems(typeof(SQLiteGroup));

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
    }
}
