using GameSelector.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.SQLite
{
    internal class SQLitePlayedGameDataBridge : IPlayedGameDataBridge
    {
        private readonly SQLitePlayedGamesTable _playedGamesTable;
        private readonly SQLiteDatabaseObjectTranslator _objectTranslator;

        public SQLitePlayedGameDataBridge(SQLiteDatabase database)
        {
            _playedGamesTable = database.PlayedGamesTable;
            _objectTranslator = database.ObjectTranslator;
        }

        public IEnumerable<PlayedGame> GetPlayedGamesByPlayer(Group group)
        {
            return
                _playedGamesTable.GetPlayedGamesByPlayerId(group.Id)
                .Select(_objectTranslator.ToPlayedGame);
        }

        public IEnumerable<PlayedGame> GetPlayedGamesByGame(Game game)
        {
            return
                _playedGamesTable.GetPlayedGamesByGameId(game.Id)
                .Select(_objectTranslator.ToPlayedGame);
        }

        public IEnumerable<PlayedGame> GetAllPlayedGames()
        {
            return
                _playedGamesTable.GetAllPlayedGames()
                .Select(_objectTranslator.ToPlayedGame);
        }

        public void InsertPlayedGame(PlayedGame playedGame)
        {
            Debug.Assert(playedGame.Id == 0);
            _playedGamesTable.InsertPlayedGame(_objectTranslator.ToSQLitePlayedGame(playedGame));
        }
    }
}
