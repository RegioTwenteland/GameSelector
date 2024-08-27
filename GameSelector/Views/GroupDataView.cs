using GameSelector.Model;
using System;
using System.Diagnostics;

namespace GameSelector.Views
{
    internal class GroupDataView : IComparable<GroupDataView>, IComparable
    {
        public long Id { get; set; }

        public string CardId { get; set; }

        public string GroupName { get; set; }

        public string ScoutingName { get; set; }

        public DateTime? StartTime { get; set; }

        public GameDataView CurrentGame { get; set; }

        public bool IsAdmin { get; set; }

        public string Remarks { get; set; }

        public static GroupDataView FromGroup(Group group)
        {
            if (group == null)
            {
                return null;
            }

            var startTime = group.StartTime ?? DateTime.MinValue;

            return new GroupDataView
            {
                Id = group.Id,
                CardId = group.CardId,
                GroupName = group.Name,
                ScoutingName = group.ScoutingName,
                StartTime = startTime,
                CurrentGame = GameDataView.FromGame(group.CurrentlyPlaying),
                IsAdmin = group.IsAdmin,
                Remarks = group.Remarks
            };
        }

        public override string ToString() => $"{ScoutingName}: {GroupName}";

        public int CompareTo(object obj) => CompareTo(obj as GroupDataView);

        /// <summary>
        /// For sorting in grid.
        /// </summary>
        public int CompareTo(GroupDataView other)
        {
            if (other is null) return 1;

            return ToString().CompareTo(other.ToString());
        }
    }
}
