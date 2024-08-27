using GameSelector.Model;
using System;

namespace GameSelector.Views
{
    internal class PlayedGameDataView
    {
        public long Id { get; set; }

        public GameDataView Game { get; set; }

        public GroupDataView Player { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public static PlayedGameDataView FromPlayedGame(PlayedGame playedGame)
        {
            return new PlayedGameDataView
            {
                Id = playedGame.Id,
                Game = GameDataView.FromGame(playedGame.Game),
                Player = GroupDataView.FromGroup(playedGame.Player),
                StartTime = playedGame.StartTime,
                EndTime = playedGame.EndTime,
            };
        }
    }
}
