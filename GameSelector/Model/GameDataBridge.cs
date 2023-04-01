using External;
using GameSelector.Database;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace GameSelector.Model
{
    internal class GameDataBridge
    {
        private Table<DbCard> _cardTable;
        private Table<DbGame> _gameTable;
        private Table<DbPlayedGame> _playedGamesTable;
        private DataContext _dataContext;
        private CardDataBridge _cardDataBridge;

        public GameDataBridge(IDatabase database, CardDataBridge cardDataBridge)
        {
            _cardTable = database.CardTable;
            _gameTable = database.GameTable;
            _playedGamesTable = database.PlayedGameTable;
            _dataContext = database.DataContext;
            _cardDataBridge = cardDataBridge;
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
                OccupiedBy = _cardDataBridge.GetCard(dbGame.OccupiedById),
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

        public List<Game> GetGamesPlayedBy(Card card)
        {
            return _gameTable.Where(g => g.OccupiedById == card.Id)
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
                // add the link to a card, update the same card, or link to a new card
                var newDbCard = _cardTable.Where(c => c.Id == game.OccupiedBy.Id).Single();

                // in case we update the same card
                newDbCard.GroupId = game.OccupiedBy.GroupId;
                newDbCard.ScoutingName = game.OccupiedBy.ScoutingName;

                // in case we create a new link
                dbGame.OccupiedBy = newDbCard;
                dbGame.StartTime = game.StartTime?.Ticks;
            }

            _dataContext.SubmitChanges();
        }
    }
}
