using GameSelector.SQLite.Common;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;

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
            var gameIdColName = SQLiteHelper.GetDbName<SQLiteGame>(nameof(SQLiteGame.Id));
            var playedGameGameIdColName = SQLiteHelper.GetDbName<SQLitePlayedGame>(nameof(SQLitePlayedGame.GameId));

            var groupIdColumnName = SQLiteHelper.GetDbName<SQLiteGroup>(nameof(SQLiteGroup.Id));
            var playedGamesPlayerIdColName = SQLiteHelper.GetDbName<SQLitePlayedGame>(nameof(SQLitePlayedGame.PlayerId));

            var sql = $@"SELECT
                            {SQLitePlayedGame.SQLSelectFullPlayedGame},
                            {SQLiteGame.SQLSelectFull},
                            {SQLiteGroup.SQLSelectFull}
                        FROM `{TableName}`
                        LEFT JOIN `{SQLiteGamesTable.TableName}`
                        ON `{SQLiteGamesTable.TableName}`.`{gameIdColName}` = `{TableName}`.`{playedGameGameIdColName}`
                        LEFT JOIN `{SQLiteGroupsTable.TableName}`
                        ON `{SQLiteGroupsTable.TableName}`.`{groupIdColumnName}` = `{TableName}`.`{playedGamesPlayerIdColName}`
                        WHERE `{playedGamesPlayerIdColName}` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", playerId);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLitePlayedGame>();

            while (reader.Read())
            {
                var game = new SQLiteGame(reader);
                var player = new SQLiteGroup(reader);

                var toAdd = new SQLitePlayedGame(reader);
                toAdd.Game = game;
                toAdd.Player = player;

                outputList.Add(toAdd);
            }

            return outputList;
        }

        public List<SQLitePlayedGame> GetPlayedGamesByGameId(long gameId)
        {
            var gameIdColName = SQLiteHelper.GetDbName<SQLiteGame>(nameof(SQLiteGame.Id));
            var playedGameGameIdColName = SQLiteHelper.GetDbName<SQLitePlayedGame>(nameof(SQLitePlayedGame.GameId));

            var groupIdColumnName = SQLiteHelper.GetDbName<SQLiteGroup>(nameof(SQLiteGroup.Id));
            var playedGamesPlayerIdColName = SQLiteHelper.GetDbName<SQLitePlayedGame>(nameof(SQLitePlayedGame.PlayerId));

            var sql = $@"SELECT
	                        {SQLitePlayedGame.SQLSelectFullPlayedGame},
                            {SQLiteGame.SQLSelectFull},
                            {SQLiteGroup.SQLSelectFull}
                        FROM `{TableName}`
                        LEFT JOIN `{SQLiteGamesTable.TableName}`
                        ON `{SQLiteGamesTable.TableName}`.`{gameIdColName}` = `{TableName}`.`{playedGameGameIdColName}`
                        LEFT JOIN `{SQLiteGroupsTable.TableName}`
                        ON `{SQLiteGroupsTable.TableName}`.`{groupIdColumnName}` = `{TableName}`.`{playedGamesPlayerIdColName}`
                        WHERE `{playedGameGameIdColName}` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", gameId);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLitePlayedGame>();

            while (reader.Read())
            {
                var game = new SQLiteGame(reader);
                var player = new SQLiteGroup(reader);

                var toAdd = new SQLitePlayedGame(reader);
                toAdd.Game = game;
                toAdd.Player = player;

                outputList.Add(toAdd);
            }

            return outputList;
        }

        public void InsertPlayedGame(SQLitePlayedGame game)
        {
            var sql = $@"INSERT INTO `played_games` {SQLitePlayedGame.SQLInsertFullPlayedGame};";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@player_id", game.PlayerId);
            command.Parameters.AddWithValue("@game_id", game.GameId);
            command.Parameters.AddWithValue("@start_time", game.StartTime);
            command.Parameters.AddWithValue("@end_time", game.EndTime);

            var rowsUpdated = command.ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }
    }
}
