using System;

namespace GameSelector.Model
{
    internal class PlayedGameAddedEventArgs : EventArgs
    {
        public PlayedGame PlayedGame { get; set; }
    }
}
