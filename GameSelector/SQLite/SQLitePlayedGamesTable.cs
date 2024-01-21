using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;

namespace GameSelector.SQLite
{
    internal class SQLitePlayedGamesTable
    {
        private SQLiteConnection _connection;

        public SQLitePlayedGamesTable(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public List<SQLitePlayedGame> GetPlayedGamesByPlayerId(long playerId)
        {
            var sql = $@"SELECT
                            {SQLitePlayedGame.SQLSelectFullPlayedGame},
                            {SQLiteGame.SQLSelectFullGame},
                            {SQLiteGroup.SQLSelectFullGroup}
                        FROM `played_games`
                        LEFT JOIN `games`
                        ON `games`.`id` = `played_games`.`game`
                        LEFT JOIN `groups`
                        ON `groups`.`id` = `played_games`.`player`
                        WHERE `player` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", playerId);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLitePlayedGame>();

            while (reader.Read())
            {
                outputList.Add(SQLitePlayedGame.FromSqlReader(reader));
            }

            return outputList;
        }

        public List<SQLitePlayedGame> GetPlayedGamesByGameId(long gameId)
        {
            var sql = $@"SELECT
	                        {SQLitePlayedGame.SQLSelectFullPlayedGame},
                            {SQLiteGame.SQLSelectFullGame},
                            {SQLiteGroup.SQLSelectFullGroup}
                        FROM `played_games`
                        LEFT JOIN `games`
                        ON `games`.`id` = `played_games`.`game`
                        LEFT JOIN `groups`
                        ON `groups`.`id` = `played_games`.`player`
                        WHERE `game` = @id;";

            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@id", gameId);

            var reader = command.ExecuteReader();

            var outputList = new List<SQLitePlayedGame>();

            while (reader.Read())
            {
                outputList.Add(SQLitePlayedGame.FromSqlReader(reader));
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
