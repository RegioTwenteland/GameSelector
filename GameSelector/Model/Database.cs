using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace GameSelector.Model
{
    internal class Database
    {
        private const string DB_FILE_NAME = "data.sqlite";

        private SQLiteConnection _connection;

        private Database()
        {
            if (!File.Exists(DB_FILE_NAME))
            {
                throw new FileNotFoundException(DB_FILE_NAME);
            }

            _connection = new SQLiteConnection($"Data Source={DB_FILE_NAME};Version=3;");
            _connection.Open();
        }

        public static Database Instance { get; } = new Database();

        public List<GameData> GetGameData()
        {
            var sql = @"SELECT * FROM `games` ORDER BY `code` ASC;";
            var command = new SQLiteCommand(sql, _connection);
            var reader = command.ExecuteReader();

            var output = new List<GameData>();

            while(reader.Read())
            {
                var id = (long)reader["id"];
                var code = (string)reader["code"];
                var description = (string)reader["description"];
                var explanation = (string)reader["explanation"];
                var color = (string)reader["color"];

                output.Add(new GameData
                {
                    Id = (uint)id,
                    Code = code,
                    Description = description,
                    Explanation = explanation,
                    Color = color,
                });
            }

            return output;
        }

        public GameData GetGameData(string gameCode)
        {
            var sql = $"SELECT * FROM `games` WHERE `code` = @gameCode;";
            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@gameCode", gameCode);
            var reader = command.ExecuteReader();

            var output = new List<GameData>();

            while (reader.Read())
            {
                var id = (long)reader["id"];
                var code = (string)reader["code"];
                var description = (string)reader["description"];
                var explanation = (string)reader["explanation"];
                var color = (string)reader["color"];

                output.Add(new GameData
                {
                    Id = (uint)id,
                    Code = code,
                    Description = description,
                    Explanation = explanation,
                    Color = color,
                });
            }

            Debug.Assert(output.Count <= 1);

            return output.Count == 0 ? null : output[0];
        }

        public void InsertCard(CardData card)
        {
            var sql = @"INSERT INTO `cards` (card_id, group_id, scouting_name) VALUES (@UID, @groupId, @groupName)";
            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@UID", card.CardUID);
            command.Parameters.AddWithValue("@groupId", card.GroupId);
            command.Parameters.AddWithValue("@groupName", card.GroupName);
            command.ExecuteNonQuery();
        }
    }
}
