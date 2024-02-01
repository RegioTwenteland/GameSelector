using GameSelector.SQLite.SQLSyntax;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.SQLite
{
    internal class SQLitePlayedGamesTable
    {
        public static string TableName = "played_games";

        private SQLiteConnection _connection;

        public SQLitePlayedGamesTable(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public List<SQLitePlayedGame> GetPlayedGamesByPlayerId(long playerId)
        {
            return new SQLQuery(_connection)
                .Select<SQLitePlayedGame>()
                .Select<SQLiteGame>()
                .Select<SQLiteGroup>()
                .From<SQLitePlayedGame>()
                .LeftJoin<SQLiteGame>()
                .On<SQLiteGame, SQLitePlayedGame>(nameof(SQLiteGame.Id), nameof(SQLitePlayedGame.GameId))
                .LeftJoin<SQLiteGroup>()
                .On<SQLiteGroup, SQLitePlayedGame>(nameof(SQLiteGroup.Id), nameof(SQLitePlayedGame.PlayerId))
                .Where<SQLitePlayedGame>(nameof(SQLitePlayedGame.PlayerId)).Equals(playerId)
                .Execute()
                .Get<SQLitePlayedGame>()
                .ToList();
        }

        public List<SQLitePlayedGame> GetPlayedGamesByGameId(long gameId)
        {
            return new SQLQuery(_connection)
                .Select<SQLitePlayedGame>()
                .Select<SQLiteGame>()
                .Select<SQLiteGroup>()
                .From<SQLitePlayedGame>()
                .LeftJoin<SQLiteGame>()
                .On<SQLiteGame, SQLitePlayedGame>(nameof(SQLiteGame.Id), nameof(SQLitePlayedGame.GameId))
                .LeftJoin<SQLiteGroup>()
                .On<SQLiteGroup, SQLitePlayedGame>(nameof(SQLiteGroup.Id), nameof(SQLitePlayedGame.PlayerId))
                .Where<SQLitePlayedGame>(nameof(SQLitePlayedGame.GameId)).Equals(gameId)
                .Execute()
                .Get<SQLitePlayedGame>()
                .ToList();
        }

        public void InsertPlayedGame(SQLitePlayedGame game)
        {
            var rowsUpdated = new SQLQuery(_connection)
                .Insert<SQLitePlayedGame>(game)
                .ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }
    }
}
