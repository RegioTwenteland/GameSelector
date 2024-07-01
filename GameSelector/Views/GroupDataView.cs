using GameSelector.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameSelector.Views
{
    internal class GroupDataView : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private long id;
        private string cardId;
        private string groupName;
        private string scoutingName;
        private DateTime? startTime;
        private string currentGame;
        private bool isAdmin;
        private string remarks;

        [Browsable(false)]
        public long Id
        {
            get => id;

            set
            {
                id = value;
            }
        }

        [DisplayName("Kaart ID")]
        public string CardId
        {
            get => cardId ?? string.Empty;

            set
            {
                cardId = value;
                NotifyPropertyChanged();
            }
        }

        [DisplayName("Groep naam")]
        public string GroupName
        {
            get => groupName ?? string.Empty;

            set
            {
                groupName = value;
                NotifyPropertyChanged();
            }
        }

        [DisplayName("Scouting naam")]
        public string ScoutingName
        {
            get => scoutingName ?? string.Empty;

            set
            {
                scoutingName = value;
                NotifyPropertyChanged();
            }
        }

        [ReadOnly(true)]
        [DisplayName("Starttijd")]
        public DateTime? StartTime
        {
            get => startTime;

            set
            {
                startTime = value;
                NotifyPropertyChanged();
            }
        }

        [ReadOnly(true)]
        [DisplayName("Huidig spel")]
        public string CurrentGame
        {
            get => currentGame ?? string.Empty;

            set
            {
                currentGame = value;
                NotifyPropertyChanged();
            }
        }

        [DisplayName("Admin")]
        public bool IsAdmin
        {
            get => isAdmin;

            set
            {
                isAdmin = value;
                NotifyPropertyChanged();
            }
        }

        [DisplayName("Opmerkingen")]
        public string Remarks
        {
            get => remarks ?? string.Empty;

            set
            {
                remarks = value;
                NotifyPropertyChanged();
            }
        }

        [Browsable(false)]
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
