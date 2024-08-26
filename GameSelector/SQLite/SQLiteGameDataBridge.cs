using GameSelector.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public IEnumerable<Game> GetAllGames()
        {
            return
                _gamesTable.GetAllGames()
                .Select(dbG => _objectTranslator.ToGame(dbG));
        }

        public IEnumerable<Game> GetAllGamesAvailable()
        {
            return
                _gamesTable.GetAllGamesAvailable()
                .Select(dbG => _objectTranslator.ToGame(dbG));
        }

        public Game GetGameBeingPlayedBy(Group group)
        {
            var dbGame = _gamesTable.GetGameBeingPlayedBy(group.Id);
            return _objectTranslator.ToGame(dbGame);
        }


        public Game GetGame(long id)
        {
            return _objectTranslator.ToGame(_gamesTable.GetGameById(id));
        }

        public void UpdateGame(Game game)
        {
            _gamesTable.UpdateGame(_objectTranslator.ToSQLiteGame(game));

            GameUpdated?.Invoke(this, new GameUpdatedEventArgs { Game = game });
        }

        public void InsertGame(Game game)
        {
            Debug.Assert(game.Id == 0);
            _gamesTable.InsertGame(_objectTranslator.ToSQLiteGame(game));

            var addedGame = _gamesTable.GetNewestGame();
            game.Id = addedGame.Id;
        }

        public void DeleteGame(Game game)
        {
            _gamesTable.DeleteGame(_objectTranslator.ToSQLiteGame(game));
        }
    }
}
