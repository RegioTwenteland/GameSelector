﻿using CustomControls;
using GameSelector.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;

namespace GameSelector.Views
{
    internal partial class AdminView : Form
    {
        /////////////////////////////////////////////////////
        // GLOBAL
        /////////////////////////////////////////////////////
        private Action<string, object> SendMessage;

        private const string SaveText = "Opslaan";
        private const string UnsavedModifier = "*";

        Regex _intRgx = new Regex("[^0-9]");

        public AdminView(Action<string, object> sendMessage)
        {
            InitializeComponent();
            SendMessage = sendMessage;

            startTimePicker.Format = DateTimePickerFormat.Custom;
            startTimePicker.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            startTimePicker.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var screen = Screen.AllScreens.First();
            if (Screen.AllScreens.Length > 1)
            {
                // if there are multiple screens, put this one on the primary
                screen = Screen.AllScreens
                    .Where(s => s.Primary == true)
                    .FirstOrDefault();
            }

            Location = new Point
            {
                X = (screen.WorkingArea.Right + screen.WorkingArea.Left) / 2 - Width / 2,
                Y = (screen.WorkingArea.Bottom + screen.WorkingArea.Top) / 2 - Height / 2
            };

#if DEBUG
            testUserViewButton.Visible = true;
#endif
        }

        public void ShowGameTimeout(int timeout)
        {
            gameTimeoutTextbox.Text = timeout.ToString();
        }

        private void saveGameTimeout_Click(object sender, EventArgs e)
        {
            SendMessage("SaveGameTimeout", int.Parse(gameTimeoutTextbox.Text));
        }

        private void testUserViewButton_Click(object sender, EventArgs e)
        {
            SendMessage("TestUserView", null);
        }

        private void ForceTextboxToInt(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            textbox.Text = _intRgx.Replace(textbox.Text, "");
        }

        /////////////////////////////////////////////////////
        // ERRORS
        /////////////////////////////////////////////////////

        private void AddNewError(string errorText)
        {
            var newError = new ErrorBoxControl();
            newError.ErrorText = errorText;
            newError.CloseRequested += OnErrorCloseRequested;

            errorFlowLayout.Controls.Add(newError);
        }

        private void OnErrorCloseRequested(object sender, ErrorBoxControl.CloseRequestedEventArgs e)
        {
            errorFlowLayout.Controls.Remove(e.Which);
        }

        public void ShowError(string errorText)
        {
            AddNewError(errorText);
        }

        /////////////////////////////////////////////////////
        // ADMIN
        /////////////////////////////////////////////////////

        private void startStopGameButton_Click(object sender, EventArgs e)
        {
            SendMessage("RequestStartStopGame", null);
        }

        public void ShowGamePaused()
        {
            gameStateLabel.Text = "GEPAUZEERD";
            startStopGameButton.Text = "Start";
            gameStateLabel.ForeColor = Color.Red;
        }

        public void ShowGameRunning()
        {
            gameStateLabel.Text = "BEZIG";
            startStopGameButton.Text = "Pauze";
            gameStateLabel.ForeColor = Color.Green;
        }

        /////////////////////////////////////////////////////
        // GROUPS
        /////////////////////////////////////////////////////

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

        /////////////////////////////////////////////////////
        // GAMES
        /////////////////////////////////////////////////////

        private bool _gameDataUserControl = true;

        private GameDataView GetCurrentGameDataView()
        {
            if (gamesListBox.SelectedIndex < 0) return null;
            return (GameDataView)gamesListBox.Items[gamesListBox.SelectedIndex];
        }

        private void SortGameListBox()
        {
            object[] itemsCopy = new object[gamesListBox.Items.Count];
            gamesListBox.Items.CopyTo(itemsCopy, 0);
            gamesListBox.Items.Clear();

            var newList = itemsCopy
                .Select(o => (GameDataView)o)
                .OrderBy(gdv => gdv.ToString());

            foreach (var newItem in newList)
            {
                gamesListBox.Items.Add(newItem);
            }
        }

        private int GetGameListBoxIndex(GameDataView game)
        {
            var idx = -1;

            for (var i = 0; i < gamesListBox.Items.Count; ++i)
            {
                var gameItem = (GameDataView)gamesListBox.Items[i];

                if (gameItem.Id == game.Id)
                {
                    idx = i;
                    break;
                }
            }

            return idx;
        }

        private void gamesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _gameDataUserControl = false;
            ShowGame(GetCurrentGameDataView());
            _gameDataUserControl = true;
        }

        private void saveGameButton_Click(object sender, EventArgs e)
        {
            var gdv = GetCurrentGameDataView();

            if (gdv == null) return;
            
            gdv.UnsavedChanges = false;
            saveGameButton.Text = SaveText;
            SendMessage("RequestSaveGame", gdv);
        }

        private void addGameButton_Click(object sender, EventArgs e)
        {
            SendMessage("RequestNewGame", null);
        }

        private void deleteGameButton_Click(object sender, EventArgs e)
        {
            var gdv = GetCurrentGameDataView();

            if (gdv != null)
                SendMessage("RequestDeleteGame", gdv);
        }

        private void GameDataChanged(object sender, EventArgs e)
        {
            if (!_gameDataUserControl) return;

            var gdv = GetCurrentGameDataView();

            if (gdv == null) return;

            gdv.Code = gameCodeTextbox.Text;
            gdv.Description = gameDescriptionTextbox.Text;
            gdv.Explanation = gameExplanationTextbox.Text;
            gdv.Active = gameActiveCheckbox.Checked;
            gdv.Color = gameColorComboBox.Text;
            gdv.HasPriority = gamePriorityCheckbox.Checked;
            gdv.Remarks = gameRemarksText.Text;
            gdv.TimeoutMinutes = (long)timeoutNumber.Value;
            gdv.MaxPlayerAmount = (long)maxPlayerAmountNumber.Value;
            gdv.UnsavedChanges = true;
            saveGameButton.Text = SaveText + UnsavedModifier;
        }

        public void SetGamesList(IEnumerable<GameDataView> games)
        {
            gamesListBox.Items.Clear();
            foreach (var game in games)
            {
                gamesListBox.Items.Add(game);
            }

            SortGameListBox();

            ShowGame(null);
        }

        private void ShowGame(GameDataView game)
        {
            gameCodeTextbox.Text = string.Empty;
            gameDescriptionTextbox.Text = string.Empty;
            gameExplanationTextbox.Text = string.Empty;
            gameActiveCheckbox.Checked = false;
            gameColorComboBox.Text = string.Empty;
            gamePriorityCheckbox.Checked = false;
            gameRemarksText.Text = string.Empty;
            saveGameButton.Text = SaveText;
            timeoutNumber.Value = 0;
            maxPlayerAmountNumber.Value = 0;

            if (game == null) return;

            gameCodeTextbox.Text = game.Code;
            gameDescriptionTextbox.Text = game.Description;
            gameExplanationTextbox.Text = game.Explanation;
            gameActiveCheckbox.Checked = game.Active;
            gameColorComboBox.Text = game.Color;
            gamePriorityCheckbox.Checked = game.HasPriority;
            gameRemarksText.Text = game.Remarks;
            timeoutNumber.Value = game.TimeoutMinutes;
            maxPlayerAmountNumber.Value = game.MaxPlayerAmount;

            saveGameButton.Text += (game.UnsavedChanges ? UnsavedModifier : string.Empty);
        }

        public void UpdateGame(GameDataView game)
        {
            var idx = GetGameListBoxIndex(game);

            if (idx < 0)
            {
                gamesListBox.Items.Add(game);
                idx = GetGameListBoxIndex(game);
            }
            else
            {
                gamesListBox.Items[idx] = game;
            }

            SortGameListBox();
            gamesListBox.SelectedIndex = idx;
        }

        public void SetGameSelected(GameDataView game)
        {
            if (game == null) return;

            var idx = GetGameListBoxIndex(game);

            gamesListBox.SelectedIndex = idx;
        }

        public void ShowPlayedGames(IEnumerable<PlayedGame> playedGames)
        {
            var sb = new StringBuilder();

            foreach (var playedGame in playedGames)
            {
                sb.Append(playedGame.StartTime.ToString("HH:mm:ss"))
                    .Append(" - ")
                    .Append(playedGame.EndTime.ToString("HH:mm:ss"))
                    .Append(": ")
                    .Append(playedGame.Game.Description)
                    .AppendLine();
            }

            MessageBox.Show(sb.ToString());
        }

        private void showPlayedGamesButton_Click(object sender, EventArgs e)
        {
            var gdv = GetCurrentGroupDataView();

            if (gdv != null)
                SendMessage("RequestPlayedGames", gdv.Id);
        }

        public void ShowView()
        {
            Show();
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
