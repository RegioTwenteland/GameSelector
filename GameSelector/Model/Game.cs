using System;

namespace GameSelector.Model
{
    internal class Game
    {
        private SetOnce<long> _id = new SetOnce<long>();

        public long Id
        {
            get => _id.Value;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Id must be greater than 0");
                }
                _id.Value = value;
            }
        }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Explanation { get; set; }

        public bool Active { get; set; }

        public long Priority { get; set; }

        public string Remarks { get; set; }

        public TimeSpan Timeout { get; set; }

        public long MaxPlayerAmount { get; set; }
    }
}
