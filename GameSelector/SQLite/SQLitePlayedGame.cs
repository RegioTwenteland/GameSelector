using GameSelector.SQLite.Common;
using System.Data.SQLite;

namespace GameSelector.SQLite
{
    [SQLiteTable(TableName = "played_games")]
    internal class SQLitePlayedGame : SQLiteObject
    {
        public SQLitePlayedGame()
            : base(SQLitePlayedGamesTable.TableName)
        {
        }

        public SQLitePlayedGame(SQLiteDataReader reader)
            : this()
        {
            FillPropsFromSqlReader(reader);
        }

        [SQLiteColumn(Name = "id")]
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

        public static string SQLSelectFullPlayedGame =>
            SQLiteHelper.SqlForSelectTableItems(typeof(SQLitePlayedGame));

        public const string SQLInsertFullPlayedGame = @"
                        (
                            `player`,
                            `game`,
                            `start_time`,
                            `end_time`
                        )
                        VALUES
                        (
                            @player_id,
                            @game_id,
                            @start_time,
                            @end_time
                        )";
    }
}
