using GameSelector.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GameSelector.Views
{
    internal partial class UserView : Form
    {
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
        }

        public void ShowGame(GameData game)
        {
            gameAnnouncerLabel.Text = game.Description;
        }
    }
}
