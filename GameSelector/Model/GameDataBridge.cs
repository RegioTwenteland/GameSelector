using NFC;
using GameSelector.Database;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace GameSelector.Model
{
    internal class GameDataBridge
    {
        private Table<DbGroup> _groupTable;
        private Table<DbGame> _gameTable;
        private Table<DbPlayedGame> _playedGamesTable;
        private DataContext _dataContext;
        private GroupDataBridge _groupDataBridge;

        public GameDataBridge(IDatabase database, GroupDataBridge groupDataBridge)
        {
            _groupTable = database.CardTable;
            _gameTable = database.GameTable;
            _playedGamesTable = database.PlayedGameTable;
            _dataContext = database.DataContext;
            _groupDataBridge = groupDataBridge;
        }

        private Game DbGameToGame(DbGame dbGame)
        {
            if (dbGame == null)
                return null;

            return new Game
            {
                Id = dbGame.Id,
                Code = dbGame.Code,
                Description = dbGame.Description,
                Explanation = dbGame.Explanation,
                Color = dbGame.Color,
                Priority = dbGame.Priority,
                OccupiedBy = dbGame.OccupiedById.HasValue ? _groupDataBridge.GetGroup(dbGame.OccupiedById.Value) : null,
                StartTime = dbGame.StartTime.HasValue ? (DateTime?)new DateTime(dbGame.StartTime.Value) : null,
            };
        }

        public List<Game> GetAllGames()
        {
            var output = new List<Game>();

            foreach (var dbGame in _gameTable)
            {
                output.Add(DbGameToGame(dbGame));
            }

            return output;
        }

        public List<Game> GetGamesPlayedBy(Group group)
        {
            return _gameTable.Where(g => g.OccupiedById == group.Id)
                .Select(g => DbGameToGame(g))
                .ToList();
        }

        public Game GetGame(long id)
        {
            return DbGameToGame(_gameTable.Where(g => g.Id == id).SingleOrDefault());
        }

        public void UpdateGame(Game game)
        {
            var dbGame = _gameTable.Where(g => g.Id == game.Id).SingleOrDefault();

            if (dbGame == null)
            {
                throw new InvalidOperationException("Can't update a game that does not exist");
            }

            dbGame.Code = game.Code;
            dbGame.Description = game.Description;
            dbGame.Explanation = game.Explanation;
            dbGame.Color = game.Color;
            dbGame.Priority = game.Priority;

            if (game.OccupiedBy == null && dbGame.OccupiedBy != null)
            {
                // remove the link to a card
                dbGame.OccupiedBy = null;
                dbGame.StartTime = null;
            }
            else if (game.OccupiedBy != null)
            {
                // add the link to a group, update the same group, or link to a new group
                var newDbGroup = _groupTable.Where(c => c.Id == game.OccupiedBy.Id).Single();

                // in case we update the same card
                newDbGroup.CardId = game.OccupiedBy.CardId;
                newDbGroup.GroupName = game.OccupiedBy.GroupName;
                newDbGroup.ScoutingName = game.OccupiedBy.ScoutingName;

                // in case we create a new link
                dbGame.OccupiedBy = newDbGroup;
                dbGame.StartTime = game.StartTime?.Ticks;
            }

            _dataContext.SubmitChanges();
        }
    }
}
