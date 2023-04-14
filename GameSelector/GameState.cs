using System;

namespace GameSelector
{
    internal class GameState
    {
        public enum State
        {
            Paused,
            Playing
        }

        public event EventHandler StateChanged;

        private State _currentState = State.Paused;

        public State CurrentState
        {
            get
            {
                return _currentState;
            }

            set
            {
                _currentState = value;

                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
