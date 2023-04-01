using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameSelector.Model
{
    internal class Card
    {
        private List<PlayedGame> _playedGames;

        public Card()
            : this(new List<PlayedGame>())
        {
        }

        public Card(List<PlayedGame> playedGames)
        {
            _playedGames = playedGames;
        }

        private SetOnce<string> _id = new SetOnce<string>();

        public string Id
        {
            get => _id.Value;
            set => _id.Value = value;
        }

        public long GroupId { get; set; }

        public string ScoutingName { get; set; }

        public Game CurrentGame { get; set; }

        public ReadOnlyCollection<PlayedGame> PlayedGames => _playedGames?.AsReadOnly();

        public void AppendPlayedGame(PlayedGame game)
        {
            _playedGames.Add(game);
        }
    }
}
