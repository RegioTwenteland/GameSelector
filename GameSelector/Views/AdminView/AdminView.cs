using CustomControls;
using GameSelector.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace GameSelector.Views
{
    internal partial class AdminView : Form
    {
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

        private void startStopGameButton_Click(object sender, EventArgs e)
        {
            SendMessage("RequestStartStopGame", null);
        }

        private void gamesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var game = (GameDataView)gamesListBox.Items[gamesListBox.SelectedIndex];
            SendMessage("RequestGame", game.Id);
        }

        private void ForceTextboxToInt(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            textbox.Text = _intRgx.Replace(textbox.Text, "");
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

        public void SetGamesList(IEnumerable<GameDataView> games)
        {
            gamesListBox.Items.Clear();
            foreach (var game in games)
            {
                gamesListBox.Items.Add(game);
            }
        }

        public void ShowGame(GameDataView game)
        {
            if (game == null)
            {
                gameDescriptionTextbox.Text = string.Empty;
                gameExplanationTextbox.Text = string.Empty;
                gameColorComboBox.Text = string.Empty;
                return;
            }

            gameDescriptionTextbox.Text = game.Description;
            gameExplanationTextbox.Text = game.Explanation;
            gameColorComboBox.Text = game.Color;
        }

        public void ShowError(string errorText)
        {
            AddNewError(errorText);
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
    }
}
