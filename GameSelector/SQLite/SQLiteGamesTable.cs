﻿using GameSelector.SQLite.SQLSyntax;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.SQLite
{
    internal class SQLiteGamesTable
    {
        private SQLiteConnection _connection;

        public SQLiteGamesTable(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<SQLiteGame> GetAllGames()
        {
            return new SQLQuery(_connection)
                .Select<SQLiteGame>()
                .From<SQLiteGame>()
                .Execute()
                .Get<SQLiteGame>();
        }

        public IEnumerable<SQLiteGame> GetAllGamesAvailable()
        {
            return new SQLQuery(_connection)
                .Select<SQLiteGame>().From<SQLiteGame>()
                .LeftJoin<SQLiteGroup>()
                .On<SQLiteGame, SQLiteGroup>(nameof(SQLiteGame.Id), nameof(SQLiteGroup.CurrentlyPlayingId))
                .GroupBy<SQLiteGame>(nameof(SQLiteGame.Id))
                .Having()
                .Count<SQLiteGame>(nameof(SQLiteGame.Id))
                .LessThan<SQLiteGame>(nameof(SQLiteGame.MaxPlayerAmount))
                .Or<SQLiteGroup>(nameof(SQLiteGroup.Id)).EqualsNull()
                .Execute()
                .Get<SQLiteGame>();
        }

        public SQLiteGame GetGameBeingPlayedBy(long groupId)
        {
            return new SQLQuery(_connection)
                .Select<SQLiteGame>().From<SQLiteGame>()
                .LeftJoin<SQLiteGroup>()
                .On<SQLiteGame, SQLiteGroup>(nameof(SQLiteGame.Id), nameof(SQLiteGroup.CurrentlyPlayingId))
                .Where<SQLiteGroup>(nameof(SQLiteGroup.Id)).Equals(groupId)
                .Execute()
                .Get<SQLiteGame>()
                .SingleOrDefault();
        }

        public SQLiteGame GetGameById(long id)
        {
            return new SQLQuery(_connection)
                .Select<SQLiteGame>()
                .From<SQLiteGame>()
                .Where<SQLiteGame>(nameof(SQLiteGame.Id)).Equals(id)
                .Execute()
                .Get<SQLiteGame>()
                .SingleOrDefault();
        }

        public void UpdateGame(SQLiteGame game)
        {
            var rowsUpdated = new SQLQuery(_connection)
                .Update<SQLiteGame>()
                .Set(game)
                .Where<SQLiteGame>(nameof(SQLiteGame.Id)).Equals(game.Id)
                .ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }

        public void InsertGame(SQLiteGame game)
        {
            var rowsUpdated = new SQLQuery(_connection)
                .Insert<SQLiteGame>(game)
                .ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }

        public void DeleteGame(SQLiteGame game)
        {
            var rowsUpdated = new SQLQuery(_connection)
                .Delete<SQLiteGame>()
                .Where<SQLiteGame>(nameof(SQLiteGame.Id)).Equals(game.Id)
                .ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }

        internal SQLiteGame GetNewestGame()
        {
            return new SQLQuery(_connection)
                .Select<SQLiteGame>().Select<SQLiteGroup>()
                .From<SQLiteGame>().LeftJoin<SQLiteGroup>()
                .On<SQLiteGame, SQLiteGroup>(nameof(SQLiteGame.Id), nameof(SQLiteGroup.CurrentlyPlayingId))
                .OrderByDesc<SQLiteGame>(nameof(SQLiteGame.Id))
                .Limit(1)
                .Execute()
                .Get<SQLiteGame>()
                .SingleOrDefault();
        }
    }
}
