using GameSelector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSelector.SQLite
{
    internal class SQLiteGameDataBridge : IGameDataBridge
    {
        private readonly SQLiteGamesTable _gamesTable;
        private readonly SQLiteDatabaseObjectTranslator _objectTranslator;

        public event EventHandler<GameUpdatedEventArgs> GameUpdated;

        public SQLiteGameDataBridge(SQLiteDatabase database)
        {
            _gamesTable = database.GamesTable;
            _objectTranslator = database.ObjectTranslator;
        }

        public List<Game> GetAllGames()
        {
            return
                _gamesTable.GetAllGames()
                .Select(dbG => _objectTranslator.ToGame(dbG))
                .ToList();
        }

        public List<Game> GetAllGamesNotOccupied()
        {
            return
                _gamesTable.GetAllGamesNotOccupied()
                .Select(dbG => _objectTranslator.ToGame(dbG))
                .ToList();
        }

        public Game GetGame(long id)
        {
            return _objectTranslator.ToGame(_gamesTable.GetGameById(id));
        }

        public Game GetGameOccupiedBy(Group group)
        {
            return _objectTranslator.ToGame(_gamesTable.GetGameOccupiedBy(group.Id));
        }

        public void UpdateGame(Game game)
        {
            _gamesTable.UpdateGame(_objectTranslator.ToSQLiteGame(game));

            if (game.OccupiedBy != null)
            {
                // make sure the event passes the correct game to whomever wants to know
                game.OccupiedBy.CurrentlyPlaying = game;
            }

            GameUpdated?.Invoke(this, new GameUpdatedEventArgs { Game = game });
        }

        public void InsertGame(Game game)
        {
            _gamesTable.InsertGame(_objectTranslator.ToSQLiteGame(game));
        }

        public void DeleteGame(Game game)
        {
            _gamesTable.DeleteGame(_objectTranslator.ToSQLiteGame(game));
        }
    }
}
