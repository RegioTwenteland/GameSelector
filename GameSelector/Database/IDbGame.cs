using GameSelector.Database.SQLite;

namespace GameSelector.Database
{
    internal interface IDbGame
    {
        long Id { get; set; }

        string Code { get; set; }

        string Description { get; set; }

        string Explanation { get; set; }
        
        string Color { get; set; }

        long Priority { get; set; }

        long? OccupiedById { get; set; }

        /// <summary>
        /// Warning: read-only. Dont use for modifying.
        /// </summary>
        IDbGroup OccupiedBy { get; set; }

        long? StartTime { get; set; }
    }
}
