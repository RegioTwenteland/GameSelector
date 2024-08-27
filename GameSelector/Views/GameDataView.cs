using GameSelector.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace GameSelector.Views
{
    internal class GameDataView : IComparable<GameDataView>, IComparable
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Explanation { get; set; }

        public bool Active { get; set; }

        public long Priority { get; set; }

        public DateTime StartTime { get; set; }

        public string Remarks { get; set; }

        public long TimeoutMinutes { get; set; }

        public long MaxPlayerAmount { get; set; }

        public override string ToString() => Code;

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
                Priority = game.Priority,
                StartTime = startTime,
                Remarks = game.Remarks,
                TimeoutMinutes = game.Timeout.Minutes,
                MaxPlayerAmount = game.MaxPlayerAmount,
            };
        }

        /// <summary>
        /// For sorting in grid.
        /// </summary>
        public int CompareTo(GameDataView other)
        {
            if (other is null) return 1;

            return ToString().CompareTo(other.ToString());
        }

        public int CompareTo(object obj) => CompareTo(obj as GameDataView);
    }
}
