using System;

namespace GameSelector.Model
{
    internal class PlayedGame
    {
        public uint Id { get; set; }
        
        public string Player { get; set; }

        public uint Game { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
