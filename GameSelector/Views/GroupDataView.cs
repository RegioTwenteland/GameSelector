using GameSelector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSelector.Views
{
    internal class GroupDataView
    {
        public long Id { get; set; }

        public string CardId { get; set; }

        public string GroupName { get; set; }

        public string ScoutingName { get; set; }

        public DateTime? StartTime { get; set; }

        public string CurrentGame { get; set; }

        public static GroupDataView FromGroup(Group group)
        {
            if (group == null)
            {
                return null;
            }

            var startTime = DateTime.MinValue;

            if (group.CurrentGame != null && group.CurrentGame.StartTime.HasValue)
            {
                startTime = group.CurrentGame.StartTime.Value;
            }

            return new GroupDataView
            {
                Id = group.Id,
                CardId = group.CardId,
                GroupName = group.GroupName,
                ScoutingName = group.ScoutingName,
                StartTime = startTime,
                CurrentGame = group.CurrentGame?.Description
            };
        }
    }
}
