using GameSelector.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GameSelector.Views
{
    internal partial class UserView : Form
    {
        private const string PROMPT_MESSAGE = "Houd je kaart tegen de lezer";
        private const string SELECTED_MESSAGE = "Speciaal geselecteerd voor ";

        private Action<string, object> SendMessage;

        public UserView(Action<string, object> sendMessage)
        {
            InitializeComponent();
            SendMessage = sendMessage;
        }

        private void UserView_Load(object sender, EventArgs e)
        {
            // if there are multiple screens, put this one on a secondary screen
            if (Screen.AllScreens.Length > 1)
            {
                var screen = Screen.AllScreens
                    .Where(s => s.Primary == false)
                    .FirstOrDefault();

                Location = new System.Drawing.Point
                {
                    X = screen.WorkingArea.Left,
                    Y = screen.WorkingArea.Top
                };
            }

            gameAnnouncerLabel.Text = PROMPT_MESSAGE;
            gameCodeLabel.Text = string.Empty;
            gameDescriptionLabel.Text = string.Empty;
            gameExplanationLabel.Text = string.Empty;
        }

        public void ShowCard(CardData card)
        {
            gameAnnouncerLabel.Text = SELECTED_MESSAGE + card.GroupName + ":";
            gameCodeLabel.Text = card.CurrentGame.Code;
            gameDescriptionLabel.Text = card.CurrentGame.Description;
            gameExplanationLabel.Text = card.CurrentGame.Explanation;
        }
    }
}
