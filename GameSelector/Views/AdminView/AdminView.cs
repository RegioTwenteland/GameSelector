using CustomControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
            // if there are multiple screens, put this one on the primary
            if (Screen.AllScreens.Length > 1)
            {
                var screen = Screen.AllScreens
                    .Where(s => s.Primary == true)
                    .FirstOrDefault();

                Location = new System.Drawing.Point
                {
                    X = screen.WorkingArea.Left,
                    Y = screen.WorkingArea.Top
                };
            }

            SendMessage("RequestGames", null);
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

        private void writeCardButton_Click(object sender, EventArgs e)
        {
            var card = new GroupDataView
            {
                CardId = cardIdText.Text,
                GroupName = groupNameText.Text,
                ScoutingName = scoutingNameText.Text,
                StartTime = DateTime.Parse(startTimePicker.Text),
                CurrentGame = currentGameText.Text,
            };

            SendMessage("WriteCardData", card);
        }

        public void ShowGroup(GroupDataView group)
        {
            if (group == null)
            {
                cardIdText.Text = string.Empty;
                groupNameText.Text = string.Empty;
                scoutingNameText.Text = string.Empty;
                currentGameText.Text = string.Empty;
                return;
            }

            cardIdText.Text = group.CardId;
            groupNameText.Text = group.GroupName.ToString();
            scoutingNameText.Text = group.ScoutingName;
            currentGameText.Text = group.CurrentGame;
            startTimePicker.Text =
                group.StartTime.HasValue ?
                    (group.StartTime < startTimePicker.MinDate ? startTimePicker.MinDate :
                    group.StartTime > startTimePicker.MaxDate ? startTimePicker.MaxDate :
                    group.StartTime).ToString()
                : null;
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
                .OrderByDescending(gdv => gdv.Priority);

            foreach(var newItem in newList)
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

            if(gdv != null)
                SendMessage("RequestSaveGame", gdv);
        }

        private void addGameButton_Click(object sender, EventArgs e)
        {
            SendMessage("RequestNewGame", null);
        }

        private void incPrioButton_Click(object sender, EventArgs e)
        {
            var gdv = GetCurrentGameDataView();

            if (gdv != null)
                SendMessage("RequestIncreasePrio", gdv);
        }

        private void decPrioButton_Click(object sender, EventArgs e)
        {
            var gdv = GetCurrentGameDataView();

            if (gdv != null)
                SendMessage("RequestDecreasePrio", gdv);
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
            gdv.Color = gameColorComboBox.Text;
        }

        public void SetGamesList(IEnumerable<GameDataView> games)
        {
            gamesListBox.Items.Clear();
            foreach (var game in games)
            {
                gamesListBox.Items.Add(game);
            }

            SortGameListBox();
        }

        public void ShowGame(GameDataView game)
        {
            gameCodeTextbox.Text = string.Empty;
            gameDescriptionTextbox.Text = string.Empty;
            gameExplanationTextbox.Text = string.Empty;
            gameColorComboBox.Text = string.Empty;
            currentOccupantTextbox.Text = string.Empty;

            if (game == null) return;

            gameCodeTextbox.Text = game.Code;
            gameDescriptionTextbox.Text = game.Description;
            gameExplanationTextbox.Text = game.Explanation;
            gameColorComboBox.Text = game.Color;

            if (game.OccupiedBy != null)
                currentOccupantTextbox.Text = $"{game.OccupiedBy.ScoutingName} - {game.OccupiedBy.GroupName}";
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
    }
}
