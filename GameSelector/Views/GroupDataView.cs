﻿using GameSelector.Model;
using System;

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

        public bool IsAdmin { get; set; }

        public string Remarks { get; set; }

        public bool UnsavedChanges { get; set; } = false;

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
                CurrentGame = group.CurrentlyPlaying?.Description,
                IsAdmin = group.IsAdmin,
                Remarks = group.Remarks
            };
        }

        public override string ToString()
        {
            return $"{ScoutingName}: {GroupName}";
        }
    }
}
