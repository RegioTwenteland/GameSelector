using GameSelector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSelector.Views
{
    internal class CardDataView
    {
        public string Id { get; set; }

        public long GroupId { get; set; }

        public string ScoutingName { get; set; }

        public DateTime StartTime { get; set; }

        public string CurrentGame { get; set; }

        public static CardDataView FromCard(Card card)
        {
            if (card == null)
            {
                return null;
            }

            var startTime = DateTime.MinValue;

            if (card.CurrentGame != null && card.CurrentGame.StartTime.HasValue)
            {
                startTime = card.CurrentGame.StartTime.Value;
            }

            return new CardDataView
            {
                Id = card.Id,
                GroupId = card.GroupId,
                ScoutingName = card.ScoutingName,
                StartTime = startTime,
                CurrentGame = card.CurrentGame?.Description
            };
        }
    }
}
