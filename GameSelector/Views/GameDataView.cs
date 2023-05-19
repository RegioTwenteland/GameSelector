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

        public string Color { get; set; }

        public bool HasPriority { get; set; }

        public GroupDataView OccupiedBy { get; set; }

        public DateTime StartTime { get; set; }

        public bool UnsavedChanges { get; set; } = false;

        public override string ToString()
        {
            return $"{Code}: {Description}{(HasPriority ? " (P)" : string.Empty)}";
        }

        public static GameDataView FromGame(Game game)
        {
            if (game == null) return null;
            
            var startTime = DateTime.MinValue;

            if (game.StartTime != null && game.StartTime.HasValue)
            {
                startTime = game.StartTime.Value;
            }

            return new GameDataView
            {
                Id = game.Id,
                Code = game.Code,
                Description = game.Description,
                Explanation = game.Explanation,
                Color = game.Color,
                HasPriority = game.HasPriority,
                OccupiedBy = GroupDataView.FromGroup(game.OccupiedBy),
                StartTime = startTime,
            };
        }
    }
}
