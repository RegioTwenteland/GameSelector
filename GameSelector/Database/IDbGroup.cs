using GameSelector.Database.SQLite;

namespace GameSelector.Database
{
    internal interface IDbGroup
    {
        long Id { get; set; }

        string CardId { get; set; }

        string Name { get; set; }

        string ScoutingName { get; set; }

        long IsAdmin { get; set; }

        /// <summary>
        /// Warning: read-only. Dont use for modifying.
        /// </summary>
        IDbGame CurrentlyPlaying { get; set; }
    }
}
