using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GameSelector.Views.AdminGroupView
{
    internal partial class AdminGroupView : UserControl
    {
        private const string SaveText = "Opslaan";
        private const string UnsavedModifier = "*";

        private readonly Action<string, object> SendMessage;

        public AdminGroupView(Action<string, object> sendMessage, IAdminViewScaffold adminScaffold)
        {
            InitializeComponent();

            SendMessage = sendMessage;

            adminScaffold.AddTabPage("Groepen", this, null);

            startTimePicker.Format = DateTimePickerFormat.Custom;
            startTimePicker.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            startTimePicker.Enabled = false;
        }

        private bool _groupDataUserControl = true;

        private GroupDataView GetCurrentGroupDataView()
        {
            if (groupsListBox.SelectedIndex < 0) return null;
            return (GroupDataView)groupsListBox.Items[groupsListBox.SelectedIndex];
        }

        private void SortGroupListBox()
        {
            object[] itemsCopy = new object[groupsListBox.Items.Count];
            groupsListBox.Items.CopyTo(itemsCopy, 0);
            groupsListBox.Items.Clear();

            var newList = itemsCopy
                .Select(o => (GroupDataView)o)
                .OrderBy(gdv => gdv.ToString());

            foreach (var newItem in newList)
            {
                groupsListBox.Items.Add(newItem);
            }
        }

        private int GetGroupListBoxIndex(GroupDataView group)
        {
            var idx = -1;

            for (var i = 0; i < groupsListBox.Items.Count; ++i)
            {
                var groupItem = (GroupDataView)groupsListBox.Items[i];

                if (groupItem.Id == group.Id)
                {
                    idx = i;
                    break;
                }
            }

            return idx;
        }

        public void SetGroupsList(IEnumerable<GroupDataView> groups)
        {
            groupsListBox.Items.Clear();
            foreach (var group in groups)
            {
                groupsListBox.Items.Add(group);
            }

            SortGroupListBox();
            ShowGroup(null);
        }

        private void saveGroupButton_Click(object sender, EventArgs e)
        {
            var gdv = GetCurrentGroupDataView();

            if (gdv == null) return;

            gdv.UnsavedChanges = false;
            saveGroupButton.Text = SaveText;
            SendMessage("RequestSaveGroup", gdv);
        }

        private void addGroupButton_Click(object sender, EventArgs e)
        {
            SendMessage("RequestNewGroup", null);
        }

        private void deleteGroupButton_Click(object sender, EventArgs e)
        {
            var gdv = GetCurrentGroupDataView();

            if (gdv != null)
                SendMessage("RequestDeleteGroup", gdv);
        }

        private void addCardToGroupButton_Click(object sender, EventArgs e)
        {
            var lastScannedId = lastScannedCardTextbox.Text;

            if (lastScannedId == null || lastScannedId == string.Empty) return;

            cardIdTextbox.Text = lastScannedId;
        }

        private void removeCardFromGroupButton_Click(object sender, EventArgs e)
        {
            cardIdTextbox.Text = "";
        }

        private void endGameForGroup_Click(object sender, EventArgs e)
        {
            var gdv = GetCurrentGroupDataView();

            if (gdv != null && gdv.CurrentGame != null)
            {
                var confirm = MessageBox.Show("Weet je zeker dat je dit spel geforceerd wil beëindigen?", "", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    SendMessage("RequestForceEndGameForGroup", gdv);
                }
            }
        }

        private void showPlayedGamesButton_Click(object sender, EventArgs e)
        {
            var gdv = GetCurrentGroupDataView();

            if (gdv != null)
                SendMessage("RequestPlayedGames", gdv.Id);
        }

        private void GroupDataChanged(object sender, EventArgs e)
        {
            if (!_groupDataUserControl) return;

            var gdv = GetCurrentGroupDataView();

            if (gdv == null) return;

            gdv.CardId = cardIdTextbox.Text;
            gdv.GroupName = groupNameTextbox.Text;
            gdv.ScoutingName = scoutingNameTextbox.Text;
            gdv.IsAdmin = isAdminCheckbox.Checked;
            gdv.Remarks = groupRemarksText.Text;
            gdv.UnsavedChanges = true;

            saveGroupButton.Text = SaveText + UnsavedModifier;
        }

        private void groupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _groupDataUserControl = false;
            ShowGroup(GetCurrentGroupDataView());
            _groupDataUserControl = true;
        }

        private void ShowGroup(GroupDataView group)
        {
            cardIdTextbox.Text = string.Empty;
            groupNameTextbox.Text = string.Empty;
            scoutingNameTextbox.Text = string.Empty;
            currentGameText.Text = string.Empty;
            saveGroupButton.Text = SaveText;
            groupRemarksText.Text = string.Empty;
            isAdminCheckbox.Checked = false;

            if (group == null) return;

            cardIdTextbox.Text = group.CardId;
            groupNameTextbox.Text = group.GroupName.ToString();
            scoutingNameTextbox.Text = group.ScoutingName;
            currentGameText.Text = group.CurrentGame;
            isAdminCheckbox.Checked = group.IsAdmin;
            startTimePicker.Text =
                group.StartTime.HasValue ?
                    (group.StartTime < startTimePicker.MinDate ? startTimePicker.MinDate :
                    group.StartTime > startTimePicker.MaxDate ? startTimePicker.MaxDate :
                    group.StartTime).ToString()
                : null;
            groupRemarksText.Text = group.Remarks;

            saveGroupButton.Text = SaveText + (group.UnsavedChanges ? UnsavedModifier : string.Empty);
        }

        public void UpdateGroup(GroupDataView group)
        {
            if (group == null) return;

            var idx = GetGroupListBoxIndex(group);

            if (idx < 0)
            {
                groupsListBox.Items.Add(group);
            }
            else
            {
                groupsListBox.Items[idx] = group;
            }

            SortGroupListBox();
            idx = GetGroupListBoxIndex(group);

            groupsListBox.SelectedIndex = idx;
        }

        public void SetGroupSelected(GroupDataView group)
        {
            if (group == null) return;

            var idx = GetGroupListBoxIndex(group);

            groupsListBox.SelectedIndex = idx;
        }

        public void ShowLastScannedCardId(string cardId)
        {
            lastScannedCardTextbox.Text = cardId;
        }
    }
}
