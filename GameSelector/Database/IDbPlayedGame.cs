namespace GameSelector.Database
{
    internal interface IDbPlayedGame
    {
        long Id { get; set; }

        long PlayerId { get; set; }

        /// <summary>
        /// Warning: read-only. Dont use for modifying.
        /// </summary>
        IDbGroup Player { get; set; }

        long GameId { get; set; }

        /// <summary>
        /// Warning: read-only. Dont use for modifying.
        /// </summary>
        IDbGame Game { get; set; }

        long StartTime { get; set; }

        long EndTime { get; set; }
    }
}
