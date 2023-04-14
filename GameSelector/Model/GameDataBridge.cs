using GameSelector.Database.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSelector.Model
{
    internal class GameDataBridge
    {
        private GamesTable _gamesTable;

        public GameDataBridge(GamesTable gamesTable)
        {
            _gamesTable = gamesTable;
        }

        public static Game DbGameToGame(DbGame dbGame, Group group = null)
        {
            if (dbGame == null) return null;

            var output = new Game
            {
                Id = dbGame.Id,
                Code = dbGame.Code,
                Description = dbGame.Description,
                Explanation = dbGame.Explanation,
                Color = dbGame.Color,
                Priority = dbGame.Priority,
                StartTime = dbGame.StartTime.HasValue ? (DateTime?)new DateTime(dbGame.StartTime.Value) : null,
            };

            output.OccupiedBy = group ?? GroupDataBridge.DbGroupToGroup(dbGame.OccupiedBy, output);

            return output;
        }

        public static DbGame GameToDbGame(Game game)
        {
            if (game == null) return null;

            return new DbGame
            {
                Id = game.Id,
                Code = game.Code,
                Description = game.Description,
                Explanation = game.Explanation,
                Color = game.Color,
                Priority = game.Priority,
                OccupiedById = game.OccupiedBy?.Id,
                StartTime = game.StartTime.HasValue ? (long?)game.StartTime.Value.Ticks : null
            };
        }

        public List<Game> GetAllGames()
        {
            return
                _gamesTable.GetAllGames()
                .Select(dbG => DbGameToGame(dbG))
                .ToList();
        }

        public List<Game> GetAllGamesNotOccupied()
        {
            return
                _gamesTable.GetAllGamesNotOccupied()
                .Select(dbG => DbGameToGame(dbG))
                .ToList();
        }

        public Game GetGame(long id)
        {
            return DbGameToGame(_gamesTable.GetGameById(id));
        }

        public Game GetGameOccupiedBy(Group group)
        {
            return DbGameToGame(_gamesTable.GetGameOccupiedBy(group.Id));
        }

        public void UpdateGame(Game game)
        {
            _gamesTable.UpdateGame(GameToDbGame(game));
        }
    }
}
