using CustomControls;
using GameSelector.Model;
using System;
using System.Collections.Generic;
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
            //var card = new CardData
            //{
            //    CardUID = cardIdText.Text,
            //    GroupId = uint.Parse(groupIdText.Text),
            //    GroupName = groupNameText.Text,
            //    LastInserted = DateTime.Parse(startTimePicker.Text),
            //};

            var card = new CardDataView
            {
                Id = cardIdText.Text,
                GroupId = uint.Parse(groupIdText.Text),
                ScoutingName = groupNameText.Text,
                StartTime = DateTime.Parse(startTimePicker.Text),
                CurrentGame = currentGameText.Text,
            };

            SendMessage("WriteCardData", card);
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

        public void ShowCard(CardDataView card)
        {
            if (card == null)
            {
                cardIdText.Text = string.Empty;
                groupIdText.Text = string.Empty;
                groupNameText.Text = string.Empty;
                currentGameText.Text = string.Empty;
                return;
            }

            cardIdText.Text = card.Id;
            groupIdText.Text = card.GroupId.ToString();
            groupNameText.Text = card.ScoutingName;
            currentGameText.Text = card.CurrentGame;
            startTimePicker.Text =
                card.StartTime.HasValue ?
                    (card.StartTime < startTimePicker.MinDate ? startTimePicker.MinDate :
                    card.StartTime > startTimePicker.MaxDate ? startTimePicker.MaxDate :
                    card.StartTime).ToString()
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
    }
}
