using GameSelector.Model;
using GameSelector.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.Controllers
{
    internal class AdminGroupController : AbstractController
    {
        private AdminViewAdapter _adminView;
        private UserIdentificationView _userIdentificationView;
        private IGroupDataBridge _groupDataBridge;
        private IGameDataBridge _gameDataBridge;
        private IPlayedGameDataBridge _playedGameDataBridge;

        public AdminGroupController(
            AdminViewAdapter adminView,
            UserIdentificationView userIdentificationView,
            IGroupDataBridge groupDataBridge,
            IGameDataBridge gameDataBridge,
            IPlayedGameDataBridge playedGameDataBridge
        )
        {
            _adminView = adminView;
            _userIdentificationView = userIdentificationView;
            _groupDataBridge = groupDataBridge;
            _gameDataBridge = gameDataBridge;
            _playedGameDataBridge = playedGameDataBridge;

            _gameDataBridge.GameUpdated += OnGameUpdated;

            SetMessageHandlers(new Dictionary<string, Action<object>>
            {
                { "RequestSaveGroup", OnRequestSaveGroup },
                { "RequestNewGroup", OnRequestNewGroup },
                { "RequestDeleteGroup", OnRequestDeleteGroup },
            });
        }

        private void OnGameUpdated(object sender, GameUpdatedEventArgs e)
        {
            _adminView.UpdateGroup(GroupDataView.FromGroup(e.Game.OccupiedBy));
        }

        public override void Start(Action stop)
        {
            UpdateGroupsList(_groupDataBridge.GetAllGroups());
        }

        private void OnRequestSaveGroup(object value)
        {
            Debug.Assert(value is GroupDataView);

            var groupDataView = (GroupDataView)value;

            var group = _groupDataBridge.GetGroup(groupDataView.Id);

            // check if another group already uses this card ID. Unless we try to remove the card ID. This is always allowed.
            if (!string.IsNullOrWhiteSpace(groupDataView.CardId) && (group.CardId != groupDataView.CardId))
            {
                var groupWithCardId = _groupDataBridge.GetGroup(groupDataView.CardId);

                if (groupWithCardId != null)
                {
                    _adminView.ShowError("Niet opgeslagen: kaart ID is al toegekend");
                    return;
                }
            }

            if (group.Name.Length > 100)
            {
                _adminView.ShowError("Niet opgeslagen: groep naam mag niet groter zijn dan 100 karakters");
                return;
            }

            if (group.ScoutingName.Length > 100)
            {
                _adminView.ShowError("Niet opgeslagen: scouting naam mag niet groter zijn dan 100 karakters");
                return;
            }

            group.CardId = groupDataView.CardId;
            group.Name = groupDataView.GroupName;
            group.ScoutingName = groupDataView.ScoutingName;

            _groupDataBridge.UpdateGroup(group);

            _adminView.UpdateGroup(GroupDataView.FromGroup(group));
        }

        private void OnRequestNewGroup(object value)
        {
            Debug.Assert(value is null);

            var newGroup = new Group
            {
                ScoutingName = "Nieuwe",
                Name = "Groep",
            };

            _groupDataBridge.InsertGroup(newGroup);

            var groups = _groupDataBridge.GetAllGroups().Select(g => GroupDataView.FromGroup(g));
            _adminView.SetGroupsList(groups);
        }

        private void OnRequestDeleteGroup(object value)
        {
            Debug.Assert(value is GroupDataView);

            var groupDataView = (GroupDataView)value;

            var group = _groupDataBridge.GetGroup(groupDataView.Id);

            // Do some sanity checks:
            if (group.CurrentlyPlaying != null)
            {
                _adminView.ShowError("Verwijderen mislukt: groep is momenteel in een spel");
                return;
            }

            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);
            if (playedGames.Count > 0)
            {
                _adminView.ShowError("Verwijderen mislukt: groep heeft al spellen gespeeld");
                return;
            }

            _groupDataBridge.DeleteGroup(group);

            UpdateGroupsList(_groupDataBridge.GetAllGroups());
        }

        private void UpdateGroupsList(List<Group> groups)
        {
            var gdv = groups.Select(g => GroupDataView.FromGroup(g));
            _adminView.SetGroupsList(gdv);
        }
    }
}
