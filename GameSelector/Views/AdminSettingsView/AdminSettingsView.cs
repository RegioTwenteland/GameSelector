using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSelector.Views.AdminSettingsView
{
    public partial class AdminSettingsView : UserControl
    {
        private readonly Action<string, object> SendMessage;

        public AdminSettingsView(Action<string, object> sendMessage)
        {
            InitializeComponent();

            SendMessage = sendMessage;
        }

        private void OnLoad()
        {
#if DEBUG
            testUserViewButton.Visible = true;
#endif
        }

        private void startStopGameButton_Click(object sender, EventArgs e)
        {
            SendMessage("RequestStartStopGame", null);
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            SendMessage("Lock", null);
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

        public void ShowGameTimeout(int timeoutMinutes)
        {
            gameTimeoutTextbox.Text = timeoutMinutes.ToString();
        }

        public void ShowAnimationLength(int lengthMs)
        {
            animationLengthTextbox.Text = lengthMs.ToString();
        }

        private void saveGlobalSettings_Click(object sender, EventArgs e)
        {
            SendMessage("SaveGameTimeout", int.Parse(gameTimeoutTextbox.Text));
            SendMessage("SaveAnimationLength", int.Parse(animationLengthTextbox.Text));
        }

        private void testUserViewButton_Click(object sender, EventArgs e)
        {
            SendMessage("TestUserView", null);
        }
    }
}
