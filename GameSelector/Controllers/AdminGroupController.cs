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
        private AdminGroupViewAdapter _adminGroupView;
        private UserIdentificationView _userIdentificationView;
        private IGroupDataBridge _groupDataBridge;
        private IGameDataBridge _gameDataBridge;
        private IPlayedGameDataBridge _playedGameDataBridge;

        public AdminGroupController(
            AdminGenericViewAdapter adminGenericView,
            AdminGroupViewAdapter adminGroupView,
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

            _adminGroupView.SetGroupSelected(groupView);
            _adminGroupView.ShowLastScannedCardId(cardId);
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
                    _adminGenericView.ShowError("Niet opgeslagen: kaart ID is al toegekend");
                    return;
                }
            }

            if (group.Name.Length > 100)
            {
                _adminGenericView.ShowError("Niet opgeslagen: groep naam mag niet groter zijn dan 100 karakters");
                return;
            }

            if (group.ScoutingName.Length > 100)
            {
                _adminGenericView.ShowError("Niet opgeslagen: scouting naam mag niet groter zijn dan 100 karakters");
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
            Debug.Assert(message.Value is null);

            var newGroup = new Group
            {
                ScoutingName = "Nieuwe",
                Name = "Groep",
            };

            _groupDataBridge.InsertGroup(newGroup);

            var groups = _groupDataBridge.GetAllGroups().Select(g => GroupDataView.FromGroup(g));
            _adminGroupView.SetGroupsList(groups);
            _adminGroupView.SetGroupSelected(groups.First(g => g.ScoutingName == "Nieuwe"));
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

            UpdateGroupsList(_groupDataBridge.GetAllGroups());
        }

        private void UpdateGroupsList(IEnumerable<Group> groups)
        {
            var gdv = groups.Select(g => GroupDataView.FromGroup(g));
            _adminGroupView.SetGroupsList(gdv);
        }
    }
}
