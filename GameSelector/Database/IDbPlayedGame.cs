namespace GameSelector.Database
{
    internal interface IDbPlayedGame
    {
        long Id { get; set; }

        long PlayerId { get; set; }

        long GameId { get; set; }

        long StartTime { get; set; }

        long EndTime { get; set; }
    }
}
