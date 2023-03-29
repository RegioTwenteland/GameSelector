using GameSelector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace GameSelector
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

        private void btnSchrijf_Click(object sender, EventArgs e)
        {
            var card = new CardData
            {
                GroupId = int.Parse(groupIdText.Text),
                GroupName = groupNameText.Text,
                CurrentGame = currentGameText.Text,
                StartTime = DateTime.Parse(startTimePicker.Text),
            };

            SendMessage("WriteCardData", card);
        }

        private void gamesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var code = (string)gamesListBox.Items[gamesListBox.SelectedIndex];
            SendMessage("RequestGame", code);
        }

        private void ForceTextboxToInt(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            textbox.Text = _intRgx.Replace(textbox.Text, "");
        }

        public void ShowCard(CardData card)
        {
            if (card == null)
            {
                groupIdText.Text = string.Empty;
                groupNameText.Text = string.Empty;
                currentGameText.Text = string.Empty;
                startTimePicker.Enabled = false;
                return;
            }
            startTimePicker.Enabled = true;

            groupIdText.Text = card.GroupId.ToString();
            groupNameText.Text = card.GroupName;
            currentGameText.Text = card.CurrentGame;
            startTimePicker.Text = card.StartTime.ToString();
        }

        public void SetGamesList(List<GameData> games)
        {
            gamesListBox.Items.Clear();
            foreach (var game in games)
            {
                gamesListBox.Items.Add(game.Code);
            }
        }

        public void ShowGame(GameData game)
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
    }
}
