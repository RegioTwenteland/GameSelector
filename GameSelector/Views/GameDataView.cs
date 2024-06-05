using GameSelector.Model;
using System;
using System.Collections.Generic;
namespace GameSelector.Views
{
    internal class GameDataView
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Explanation { get; set; }

        public bool Active { get; set; }

        public bool HasPriority { get; set; }

        public DateTime StartTime { get; set; }

        public string Remarks { get; set; }

        public long TimeoutMinutes { get; set; }

        public long MaxPlayerAmount { get; set; }

        public bool UnsavedChanges { get; set; } = false;

        public override string ToString()
        {
            return $"{Code}: {Description}{(HasPriority ? " (P)" : string.Empty)}";
        }

        public static GameDataView FromGame(Game game)
        {
            if (game == null) return null;
            
            var startTime = DateTime.MinValue;

            return new GameDataView
            {
                Id = game.Id,
                Code = game.Code,
                Description = game.Description,
                Explanation = game.Explanation,
                Active = game.Active,
                HasPriority = game.HasPriority,
                StartTime = startTime,
                Remarks = game.Remarks,
                TimeoutMinutes = game.Timeout.Minutes,
                MaxPlayerAmount = game.MaxPlayerAmount,
            };
        }
    }
}
