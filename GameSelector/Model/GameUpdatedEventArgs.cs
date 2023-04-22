using System;

namespace GameSelector.Model
{
    internal class GameUpdatedEventArgs : EventArgs
    {
        public Game Game { get; set; }
    }
}
