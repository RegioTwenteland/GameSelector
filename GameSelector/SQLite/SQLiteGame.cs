﻿using GameSelector.SQLite.Common;
using System.Data.SQLite;

namespace GameSelector.SQLite
{
    [SQLiteTable(TableName = "games")]
    internal class SQLiteGame : SQLiteObject
    {
        public SQLiteGame()
        {
        }

        public SQLiteGame(SQLiteDataReader reader)
            : base(reader)
        {
        }

        [SQLiteColumn(Name = "id", IsPK = true)]
        public long Id { get; set; }

        [SQLiteColumn(Name = "code")]
        public string Code { get; set; }

        [SQLiteColumn(Name = "category")]
        public string Category { get; set; }

        [SQLiteColumn(Name = "description")]
        public string Description { get; set; }

        [SQLiteColumn(Name = "active")]
        public long Active { get; set; }

        [SQLiteColumn(Name = "priority")]
        public long Priority { get; set; }

        [SQLiteColumn(Name = "remarks")]
        public string Remarks { get; set; }

        [SQLiteColumn(Name = "timeout")]
        public long Timeout { get; set; }

        [SQLiteColumn(Name = "max_player_amount")]
        public long MaxPlayerAmount { get; set; }
    }
}
