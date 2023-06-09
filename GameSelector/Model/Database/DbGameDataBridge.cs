﻿using GameSelector.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSelector.Model.Database
{
    internal class DbGameDataBridge : IGameDataBridge
    {
        private readonly IGamesTable _gamesTable;
        private readonly IDatabaseObjectTranslator _objectTranslator;

        public event EventHandler<GameUpdatedEventArgs> GameUpdated;

        public DbGameDataBridge(IDatabase database)
        {
            _gamesTable = database.GamesTable;
            _objectTranslator = database.ObjectTranslator;
        }

        public static Game DbGameToGame(IDbGame dbGame, Group group = null)
        {
            if (dbGame == null) return null;

            var output = new Game
            {
                Id = dbGame.Id,
                Code = dbGame.Code,
                Description = dbGame.Description,
                Explanation = dbGame.Explanation,
                Active = dbGame.Active == 0 ? false : true,
                Color = dbGame.Color,
                HasPriority = dbGame.Priority == 0 ? false : true,
                StartTime = dbGame.StartTime.HasValue ? (DateTime?)new DateTime(dbGame.StartTime.Value) : null,
                Remarks = dbGame.Remarks,
            };

            output.OccupiedBy = group ?? DbGroupDataBridge.DbGroupToGroup(dbGame.OccupiedBy, output);

            return output;
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
            _gamesTable.UpdateGame(_objectTranslator.ToDbGame(game));

            if (game.OccupiedBy != null)
            {
                // make sure the event passes the correct game to whomever wants to know
                game.OccupiedBy.CurrentlyPlaying = game;
            }

            GameUpdated?.Invoke(this, new GameUpdatedEventArgs { Game = game });
        }

        public void InsertGame(Game game)
        {
            _gamesTable.InsertGame(_objectTranslator.ToDbGame(game));
        }

        public void DeleteGame(Game game)
        {
            _gamesTable.DeleteGame(_objectTranslator.ToDbGame(game));
        }
    }
}
