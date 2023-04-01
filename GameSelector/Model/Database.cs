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
        private const string DB_FILE_NAME = "..\\..\\data.sqlite";

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
            var priority = (long)reader["priority"];
            var occupied_by = reader["occupied_by"].GetType() == typeof(DBNull) ? null : (string)reader["occupied_by"];

            return new GameData
            {
                Id = (uint)id,
                Code = code,
                Description = description,
                Explanation = explanation,
                Color = color,
                Priority = (uint)priority,
                OccupiedBy = occupied_by == null ? null : ReaderToCardData(reader),
            };
        }

        private CardData ReaderToCardData(SQLiteDataReader reader)
        {
            var card_id = (string)reader["card_id"];
            var group_id = (long)reader["group_id"];
            var scouting_name = (string)reader["scouting_name"];
            var last_inserted = reader["last_inserted"].GetType() == typeof(DBNull) ? null : (long?)reader["last_inserted"];

            return new CardData
            {
                CardUID = card_id,
                GroupId = (uint)group_id,
                GroupName = scouting_name,
                LastInserted = last_inserted == null ? null : (DateTime?)new DateTime((long)last_inserted),
            };
        }

        public static Database Instance { get; } = new Database();

        public List<GameData> GetGameData()
        {
            var sql = @"SELECT * FROM `games`
                        LEFT JOIN `cards`
                        ON `cards`.`card_id` = `games`.`occupied_by`
                        ORDER BY `games`.`priority` ASC;";
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

        public void UpdateGame(GameData game)
        {
            var sql = @"UPDATE `games` SET
                            code = @gameCode,
                            description = @gameDesc,
                            explanation = @gameExpl,
                            color = @gameColor,
                            priority = @gamePrio,
                            occupied_by = @gameOccupant
                        WHERE `games`.`id` = @gameId";
            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@gameCode", game.Code);
            command.Parameters.AddWithValue("@gameDesc", game.Description);
            command.Parameters.AddWithValue("@gameExpl", game.Explanation);
            command.Parameters.AddWithValue("@gameColor", game.Color);
            command.Parameters.AddWithValue("@gamePrio", game.Priority);
            command.Parameters.AddWithValue("@gameOccupant", game.OccupiedBy.CardUID);
            command.Parameters.AddWithValue("@gameId", game.Id);
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

        public bool GetCard(CardData card)
        {
            var sql = @"SELECT * FROM `cards` WHERE card_id = @UID;";
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
            var last_inserted = reader["last_inserted"].GetType() == typeof(DBNull) ? null : (long?)reader["last_inserted"];

            card.GroupId = (uint)group_id;
            card.GroupName = scouting_name;
            card.LastInserted = last_inserted == null ? null : (DateTime?)new DateTime((long)last_inserted);

            return true;
        }

        public void UpdateCard(CardData card)
        {
            var sql = @"UPDATE `cards` SET group_id = @groupId, scouting_name = @scoutingName, last_inserted = @lastInserted WHERE `card_id` = @UID";
            var command = new SQLiteCommand(sql, _connection);
            command.Parameters.AddWithValue("@groupId", card.GroupId);
            command.Parameters.AddWithValue("@scoutingName", card.GroupName);
            if (card.LastInserted == null)
            {
                command.Parameters.AddWithValue("@lastInserted", null);
            }
            else
            {
                command.Parameters.AddWithValue("@lastInserted", card.LastInserted.Value.Ticks);
            }
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
    }
}
