using GameSelector.Model;
using GameSelector.Views;
using GameSelector.Views.AdminGroupView;
using GameSelector.Views.AdminGenericView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.Controllers
{
    internal class AdminGroupController : AbstractController
    {
        private AdminGenericViewAdapter _adminGenericView;
        private AdminGroupViewGridAdapter _adminGroupView;
        private UserIdentificationView _userIdentificationView;
        private IGroupDataBridge _groupDataBridge;
        private IGameDataBridge _gameDataBridge;
        private IPlayedGameDataBridge _playedGameDataBridge;

        public AdminGroupController(
            AdminGenericViewAdapter adminGenericView,
            AdminGroupViewGridAdapter adminGroupView,
            UserIdentificationView userIdentificationView,
            IGroupDataBridge groupDataBridge,
            IGameDataBridge gameDataBridge,
            IPlayedGameDataBridge playedGameDataBridge
        )
        {
            _adminGenericView = adminGenericView;
            _adminGroupView = adminGroupView;
            _userIdentificationView = userIdentificationView;
            _groupDataBridge = groupDataBridge;
            _gameDataBridge = gameDataBridge;
            _playedGameDataBridge = playedGameDataBridge;

            _groupDataBridge.GroupUpdated += OnGroupUpdated;

            SetMessageHandlers(new Dictionary<string, Action<Message>>
            {
                { "CardInserted", OnCardInserted },
                { "RequestSaveGroup", OnRequestSaveGroup },
                { "RequestNewGroup", OnRequestNewGroup },
                { "RequestDeleteGroup", OnRequestDeleteGroup },
            });
        }

        private void OnGroupUpdated(object sender, GroupUpdatedEventArgs e)
        {
            var gdv = GroupDataView.FromGroup(e.Group);
            _adminGroupView.UpdateGroup(gdv);
        }

        public override void Start(Action stop)
        {
            UpdateGroupsList(_groupDataBridge.GetAllGroups());
        }

        private void OnCardInserted(Message message)
        {
            Debug.Assert(message.Value is string);

            var cardId = (string)message.Value;
            var group = _groupDataBridge.GetGroup(cardId);

            var groupView = GroupDataView.FromGroup(group);

            _adminGroupView.NewCardScanned(cardId);
        }

        private void SaveFailed(string message, Group group)
        {
            _adminGenericView.ShowError(message);
            _adminGroupView.UpdateGroup(GroupDataView.FromGroup(group));
        }

        private void OnRequestSaveGroup(Message message)
        {
            Debug.Assert(message.Value is GroupDataView);

            var groupDataView = (GroupDataView)message.Value;

            var group = _groupDataBridge.GetGroup(groupDataView.Id);

            // check if another group already uses this card ID. Unless we try to remove the card ID. This is always allowed.
            if (!string.IsNullOrWhiteSpace(groupDataView.CardId) && (group.CardId != groupDataView.CardId))
            {
                var groupWithCardId = _groupDataBridge.GetGroup(groupDataView.CardId);

                if (groupWithCardId != null)
                {
                    SaveFailed("Niet opgeslagen: kaart ID is al toegekend", group);
                    return;
                }
            }

            if (group.Name.Length > 100)
            {
                SaveFailed("Niet opgeslagen: groep naam mag niet groter zijn dan 100 karakters", group);
                return;
            }

            if (group.ScoutingName.Length > 100)
            {
                SaveFailed("Niet opgeslagen: scouting naam mag niet groter zijn dan 100 karakters", group);
                return;
            }

            group.CardId = groupDataView.CardId;
            group.Name = groupDataView.GroupName;
            group.ScoutingName = groupDataView.ScoutingName;
            group.IsAdmin = groupDataView.IsAdmin;
            group.Remarks = groupDataView.Remarks;

            _groupDataBridge.UpdateGroup(group);

            _adminGroupView.UpdateGroup(GroupDataView.FromGroup(group));
        }

        private void OnRequestNewGroup(Message message)
        {
            Debug.Assert(message.Value is GroupDataView);

            var gdv = message.Value as GroupDataView;

            var newGroup = new Group
            {
                CardId = gdv.CardId,
                Name = gdv.GroupName ?? string.Empty,
                ScoutingName = gdv.ScoutingName ?? string.Empty,
                IsAdmin = gdv.IsAdmin,
                Remarks = gdv.Remarks ?? string.Empty,
            };

            _groupDataBridge.InsertGroup(newGroup); // populates the Id field
            _adminGroupView.NewGroup(GroupDataView.FromGroup(newGroup));
        }

        private void OnRequestDeleteGroup(Message message)
        {
            Debug.Assert(message.Value is GroupDataView);

            var groupDataView = (GroupDataView)message.Value;

            var group = _groupDataBridge.GetGroup(groupDataView.Id);

            // Do some sanity checks:
            if (group.CurrentlyPlaying != null)
            {
                _adminGenericView.ShowError("Verwijderen mislukt: groep is momenteel in een spel");
                return;
            }

            var playedGames = _playedGameDataBridge.GetPlayedGamesByPlayer(group);
            if (playedGames.Any())
            {
                _adminGenericView.ShowError("Verwijderen mislukt: groep heeft al spellen gespeeld");
                return;
            }

            _groupDataBridge.DeleteGroup(group);

            _adminGroupView.GroupDeleted(groupDataView);
        }

        private void UpdateGroupsList(IEnumerable<Group> groups)
        {
            var gdv = groups.Select(g => GroupDataView.FromGroup(g));
            _adminGroupView.SetGroupsList(gdv);
        }
    }
}
