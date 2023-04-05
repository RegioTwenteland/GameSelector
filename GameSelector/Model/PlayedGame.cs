using System;

namespace GameSelector.Model
{
    internal class PlayedGame
    {
        public PlayedGame()
        {
        }

        public PlayedGame(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id must be greater than 0");
            }

            Id = id;
        }

        public long Id { get; }
        
        public Group Player { get; set; }

        public Game Game { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
