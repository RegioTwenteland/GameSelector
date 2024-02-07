using System;

namespace GameSelector.Model
{
    internal class GroupUpdatedEventArgs : EventArgs
    {
        public Group Group { get; set; }
    }
}
