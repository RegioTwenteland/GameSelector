﻿using System;

namespace GameSelector.Model
{
    internal class PlayedGame
    {
        private SetOnce<long> _id = new SetOnce<long>();

        public long Id
        {
            get => _id.Value;
            set => _id.Value = value;
        }

        public Group Player { get; set; }

        public Game Game { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
