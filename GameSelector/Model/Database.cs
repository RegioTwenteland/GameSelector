using External;
using System;
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

        private GameData ReaderToGameData(SQLiteDataReader reader)
        {
            var id = (long)reader["id"];
            var code = (string)reader["code"];
            var description = (string)reader["description"];
            var explanation = (string)reader["explanation"];
            var color = (string)reader["color"];

            return new GameData
            {
                Id = (uint)id,
                Code = code,
                Description = description,
                Explanation = explanation,
                Color = color,
            };
        }

        public static Database Instance { get; } = new Database();

        public List<GameData> GetGameData()
        {
            var sql = @"SELECT * FROM `games` ORDER BY `priority` ASC;";
            var command = new SQLiteCommand(sql, _connection);
            var reader = command.ExecuteReader();

            var output = new List<GameData>();

            while(reader.Read())
            {
                output.Add(ReaderToGameData(reader));
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
                output.Add(ReaderToGameData(reader));
            }

            Debug.Assert(output.Count <= 1);

            return output.Count == 0 ? null : output[0];
        }

        public bool GetCard(CardData card)
        {
            var sql = @"SELECT * FROM `cards`
                        LEFT JOIN `games`
                        ON `cards`.`current_game` = `games`.`id`
                        WHERE card_id = @UID;";
            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@UID", card.CardUID);
            var reader = command.ExecuteReader();

            var foundOne = reader.Read();

            if (!foundOne)
            {
                return false;
            }

            var group_id = (long)reader["group_id"];
            var scouting_name = (string)reader["scouting_name"];
            var current_game = reader["current_game"].GetType() == typeof(DBNull) ? null : (long?)reader["current_game"];

            card.GroupId = (uint)group_id;
            card.GroupName = scouting_name;
            card.CurrentGame = current_game == null ? null : ReaderToGameData(reader);

            return true;
        }

        public void UpdateCard(CardData card)
        {
            var sql = @"UPDATE `cards` SET group_id = @groupId, scouting_name = @scoutingName, current_game = @currentGame WHERE `card_id` = @UID";
            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@groupId", card.GroupId);
            command.Parameters.AddWithValue("@scoutingName", card.GroupName);
            command.Parameters.AddWithValue("@currentGame", card.CurrentGame.Id);
            command.Parameters.AddWithValue("@UID", card.CardUID);
            command.ExecuteNonQuery();
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

        public List<PlayedGame> GetGamesPlayed(CardData card)
        {
            var sql = @"SELECT * FROM `games_played` WHERE `player` = @UID;";
            var command = new SQLiteCommand(sql, _connection);
            
            command.Parameters.AddWithValue("@UID", card.CardUID);

            var reader = command.ExecuteReader();
            
            var output = new List<PlayedGame>();
            while (reader.Read())
            {
                var id = (long)reader["id"];
                var player = (string)reader["player"];
                var game = (long)reader["game"];
                var start_time_ticks = reader["start_time"].GetType() == typeof(DBNull) ? null : (long?)reader["start_time"];
                var end_time_ticks = reader["end_time"].GetType() == typeof(DBNull) ? null : (long?)reader["end_time"];

                DateTime? start_time = null;
                if (start_time_ticks != null) start_time = new DateTime((long)start_time_ticks);

                DateTime? end_time = null;
                if (end_time_ticks != null) end_time = new DateTime((long)end_time_ticks);

                output.Add(new PlayedGame
                {
                    Id = (uint)id,
                    Player = player,
                    Game = (uint)game,
                    StartTime = start_time,
                    EndTime = end_time
                });
            }

            return output;
        }
    }
}
