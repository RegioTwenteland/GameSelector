using GameSelector.Model;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace GameSelector
{
    internal partial class TestView : Form
    {
        private Action<string, object> SendMessage;

        Regex _intRgx = new Regex("[^0-9]");

        public TestView(Action<string, object> sendMessage)
        {
            InitializeComponent();
            SendMessage = sendMessage;

            startTimePicker.Format = DateTimePickerFormat.Custom;
            startTimePicker.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            startTimePicker.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

        private void ForceTextboxToInt(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            textbox.Text = _intRgx.Replace(textbox.Text, "");
        }

        public void ShowCardData(CardData cardData)
        {
            if (cardData == null)
            {
                groupIdText.Text = string.Empty;
                groupNameText.Text = string.Empty;
                currentGameText.Text = string.Empty;
                startTimePicker.Enabled = false;
                return;
            }
            startTimePicker.Enabled = true;

            groupIdText.Text = cardData.GroupId.ToString();
            groupNameText.Text = cardData.GroupName;
            currentGameText.Text = cardData.CurrentGame;
            startTimePicker.Text = cardData.StartTime.ToString();
        }
    }
}
