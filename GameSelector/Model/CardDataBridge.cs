using GameSelector.Database;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace GameSelector.Model
{
    internal class CardDataBridge
    {
        private Table<DbCard> _cardTable;
        private Table<DbGame> _gameTable;
        private Table<DbPlayedGame> _playedGamesTable;
        private DataContext _dataContext;

        public CardDataBridge(IDatabase database)
        {
            _cardTable = database.CardTable;
            _gameTable = database.GameTable;
            _playedGamesTable = database.PlayedGameTable;
            _dataContext = database.DataContext;
        }

        public Card GetCard(string id)
        {
            var dbCard = _cardTable.Where(c => c.Id == id).SingleOrDefault();

            if (dbCard == null)
            {
                return null;
            }


            var playedGames = new List<PlayedGame>();

            Game game = null;

            var currentDbGame = _gameTable.Where(g => g.OccupiedById == dbCard.Id).SingleOrDefault();

            if (currentDbGame != null)
                game = new Game();

            var card = new Card(playedGames);

            foreach (var dbPlayedGame in dbCard.PlayedGames)
            {
                var playedGame = new PlayedGame(dbPlayedGame.Id)
                {
                    Player = card,
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
                game.OccupiedBy = card;
                game.StartTime = currentDbGame.StartTime.HasValue ? (DateTime?)new DateTime(currentDbGame.StartTime.Value) : null;
            }

            card.Id = dbCard.Id;
            card.GroupId = dbCard.GroupId;
            card.ScoutingName = dbCard.ScoutingName;
            card.CurrentGame = game;

            return card;
        }

        public void UpdateCard(Card card)
        {
            var dbCard = _cardTable.Where(c => c.Id == card.Id).SingleOrDefault();

            if (dbCard == null)
            {
                throw new InvalidOperationException("Can't update a card that does not exist");
            }

            var currentDbGame = _gameTable.Where(g => g.OccupiedById == dbCard.Id).SingleOrDefault();

            dbCard.GroupId = card.GroupId;
            dbCard.ScoutingName = card.ScoutingName;

            if (card.CurrentGame == null && currentDbGame != null)
            {
                // remove the link to a game
                currentDbGame.OccupiedById = null;
                currentDbGame.OccupiedBy = null;
                currentDbGame = null;
            }
            else if(card.CurrentGame != null && currentDbGame == null)
            {
                // add a link to a game
                var newDbGame = _gameTable.Where(g => g.Id == card.CurrentGame.Id).Single();

                newDbGame.OccupiedBy = dbCard;
                newDbGame.StartTime = card.CurrentGame.StartTime?.Ticks;
            }
            else if (card.CurrentGame != null && currentDbGame != null)
            {
                // either update the same game, or link to a new game.

                if (card.CurrentGame.Id != currentDbGame.Id)
                {
                    // link to a new game, here we remove the old link first
                    currentDbGame.OccupiedBy = null;
                    currentDbGame.StartTime = null;
                }

                // update the game info (+ create new link to a game)
                var dbGame = _gameTable.Where(g => g.Id == card.CurrentGame.Id).Single();
                dbGame.Code = card.CurrentGame.Code;
                dbGame.Description = card.CurrentGame?.Description;
                dbGame.Explanation = card.CurrentGame.Explanation;
                dbGame.Color = card.CurrentGame.Color;
                dbGame.Priority = card.CurrentGame.Priority;
                dbGame.OccupiedBy = dbCard;
                dbGame.StartTime = card.CurrentGame.StartTime?.Ticks;
            }

            var dbPlayedGames = _playedGamesTable.Where(pg => pg.PlayerId == card.Id);
            var dbPlayedGamesCount = dbPlayedGames.Count();
            if (card.PlayedGames != null && card.PlayedGames.Count > dbPlayedGamesCount)
            {
                // one or more played games have been appended, so loop through the played games starting from the first new one
                for (var i = dbPlayedGamesCount; i < card.PlayedGames.Count; i++)
                {
                    var newPlayedGame = card.PlayedGames[i];
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

        public void InsertCard(Card card)
        {
            if (_cardTable.Where(c => c.Id == card.Id).SingleOrDefault() != null)
            {
                throw new InvalidOperationException("Can't insert a card that already exists");
            }

            if (card.CurrentGame != null)
            {
                throw new InvalidOperationException("Can't create a new card with an active game.");
            }

            if (card.PlayedGames != null && card.PlayedGames.Count > 0)
            {
                throw new InvalidOperationException("Can't create a new card with games already played.");
            }

            var dbCard = new DbCard
            {
                Id = card.Id,
                GroupId = card.GroupId,
                ScoutingName = card.ScoutingName,
            };

            _cardTable.InsertOnSubmit(dbCard);

            _dataContext.SubmitChanges();
        }
    }
}
