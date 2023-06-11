namespace GameSelector.Model
{
    internal class Group
    {
        private SetOnce<long> _id = new SetOnce<long>();

        public long Id
        {
            get => _id.Value;
            set => _id.Value = value;
        }

        public string CardId { get; set; }

        public string Name { get; set; }

        public string ScoutingName { get; set; }

        public bool IsAdmin { get; set; }

        public Game CurrentlyPlaying { get; set; }

        public string Remarks { get; set; }
    }
}
