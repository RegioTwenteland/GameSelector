using GameSelector.Database;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace GameSelector.Model
{
    internal class GroupDataBridge
    {
        private Table<DbGroup> _groupTable;
        private Table<DbGame> _gameTable;
        private Table<DbPlayedGame> _playedGamesTable;
        private DataContext _dataContext;

        public GroupDataBridge(IDatabase database)
        {
            _groupTable = database.CardTable;
            _gameTable = database.GameTable;
            _playedGamesTable = database.PlayedGameTable;
            _dataContext = database.DataContext;
        }

        public Group GetGroup(string cardId)
        {
            var dbGroup = _groupTable.Where(gr => gr.CardId == cardId).SingleOrDefault();
            return DbGroupToGroup(dbGroup);
        }

        public Group GetGroup(long id)
        {
            var dbGroup = _groupTable.Where(gr => gr.Id == id).SingleOrDefault();
            return DbGroupToGroup(dbGroup);
        }

        private Group DbGroupToGroup(DbGroup dbGroup)
        {
            if (dbGroup == null)
            {
                return null;
            }

            var playedGames = new List<PlayedGame>();

            Game game = null;

            var currentDbGame = _gameTable.Where(g => g.OccupiedById == dbGroup.Id).SingleOrDefault();

            if (currentDbGame != null)
                game = new Game();

            var group = new Group(playedGames);

            foreach (var dbPlayedGame in dbGroup.PlayedGames)
            {
                var playedGame = new PlayedGame(dbPlayedGame.Id)
                {
                    Player = group,
                    Game = game,
                    StartTime = new DateTime(dbPlayedGame.StartTime),
                    EndTime = new DateTime(dbPlayedGame.EndTime)
                };

                playedGames.Add(playedGame);
            }

            if (game != null)
            {
                game.Id = currentDbGame.Id;
                game.Code = currentDbGame.Code;
                game.Description = currentDbGame.Description;
                game.Explanation = currentDbGame.Explanation;
                game.Color = currentDbGame.Color;
                game.Priority = currentDbGame.Priority;
                game.OccupiedBy = group;
                game.StartTime = currentDbGame.StartTime.HasValue ? (DateTime?)new DateTime(currentDbGame.StartTime.Value) : null;
            }

            group.Id = dbGroup.Id;
            group.CardId = dbGroup.CardId;
            group.GroupName = dbGroup.GroupName;
            group.ScoutingName = dbGroup.ScoutingName;
            group.CurrentGame = game;

            return group;
        }

        public void UpdateGroup(Group group)
        {
            var dbGroup = _groupTable.Where(gr => gr.Id == group.Id).SingleOrDefault();

            if (dbGroup == null)
            {
                throw new InvalidOperationException("Can't update a group that does not exist");
            }

            if (dbGroup.CardId != group.CardId)
            {
                if(_groupTable.Where(gr => gr.CardId == group.CardId).Count() > 0)
                {
                    throw new InvalidOperationException("Can't associate a group with a card that is already associated with a group");
                }
            }

            var currentDbGame = _gameTable.Where(g => g.OccupiedById == dbGroup.Id).SingleOrDefault();

            dbGroup.CardId = group.CardId;
            dbGroup.GroupName = group.GroupName;
            dbGroup.ScoutingName = group.ScoutingName;

            if (group.CurrentGame == null && currentDbGame != null)
            {
                // remove the link to a game
                currentDbGame.OccupiedById = null;
                currentDbGame.OccupiedBy = null;
                currentDbGame = null;
            }
            else if(group.CurrentGame != null && currentDbGame == null)
            {
                // add a link to a game
                var newDbGame = _gameTable.Where(g => g.Id == group.CurrentGame.Id).Single();

                newDbGame.OccupiedBy = dbGroup;
                newDbGame.StartTime = group.CurrentGame.StartTime?.Ticks;
            }
            else if (group.CurrentGame != null && currentDbGame != null)
            {
                // either update the same game, or link to a new game.

                if (group.CurrentGame.Id != currentDbGame.Id)
                {
                    // link to a new game, here we remove the old link first
                    currentDbGame.OccupiedBy = null;
                    currentDbGame.StartTime = null;
                }

                // update the game info (+ create new link to a game)
                var dbGame = _gameTable.Where(g => g.Id == group.CurrentGame.Id).Single();
                dbGame.Code = group.CurrentGame.Code;
                dbGame.Description = group.CurrentGame?.Description;
                dbGame.Explanation = group.CurrentGame.Explanation;
                dbGame.Color = group.CurrentGame.Color;
                dbGame.Priority = group.CurrentGame.Priority;
                dbGame.OccupiedBy = dbGroup;
                dbGame.StartTime = group.CurrentGame.StartTime?.Ticks;
            }

            var dbPlayedGames = _playedGamesTable.Where(pg => pg.PlayerId == group.Id);
            var dbPlayedGamesCount = dbPlayedGames.Count();
            if (group.PlayedGames != null && group.PlayedGames.Count > dbPlayedGamesCount)
            {
                // one or more played games have been appended, so loop through the played games starting from the first new one
                for (var i = dbPlayedGamesCount; i < group.PlayedGames.Count; i++)
                {
                    var newPlayedGame = group.PlayedGames[i];
                    if (newPlayedGame.Id != 0)
                    {
                        // can not overwrite an existing played game here
                        throw new InvalidOperationException("New played games may not have the Id property set");
                    }

                    // insert the new played game into the table
                    var newDbPlayedGame = new DbPlayedGame
                    {
                        PlayerId = newPlayedGame.Player.Id,
                        GameId = newPlayedGame.Game.Id,
                        StartTime = newPlayedGame.StartTime.Ticks,
                        EndTime = newPlayedGame.EndTime.Ticks,
                    };

                    _playedGamesTable.InsertOnSubmit(newDbPlayedGame);
                }
            }

            _dataContext.SubmitChanges();
        }

        public void InsertGroup(Group group)
        {
            if (_groupTable.Where(gr => gr.Id == group.Id).SingleOrDefault() != null)
            {
                throw new InvalidOperationException("Can't insert a group with a card that is already associated with a group");
            }

            if (group.CurrentGame != null)
            {
                throw new InvalidOperationException("Can't create a new group with an active game.");
            }

            if (group.PlayedGames != null && group.PlayedGames.Count > 0)
            {
                throw new InvalidOperationException("Can't create a new group with games already played.");
            }

            var dbGroup = new DbGroup
            {
                Id = group.Id,
                CardId = group.CardId,
                GroupName = group.GroupName,
                ScoutingName = group.ScoutingName,
            };

            _groupTable.InsertOnSubmit(dbGroup);

            _dataContext.SubmitChanges();
        }
    }
}
