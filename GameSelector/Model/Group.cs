using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameSelector.Model
{
    internal class Group
    {
        private List<PlayedGame> _playedGames;

        public Group()
            : this(new List<PlayedGame>())
        {
        }

        public Group(List<PlayedGame> playedGames)
        {
            _playedGames = playedGames;
        }

        private SetOnce<long> _id = new SetOnce<long>();

        public long Id
        {
            get => _id.Value;
            set => _id.Value = value;
        }

        public string CardId { get; set; }

        public string GroupName { get; set; }

        public string ScoutingName { get; set; }

        public Game CurrentGame { get; set; }

        public ReadOnlyCollection<PlayedGame> PlayedGames => _playedGames?.AsReadOnly();

        public void AppendPlayedGame(PlayedGame game)
        {
            _playedGames.Add(game);
        }
    }
}
