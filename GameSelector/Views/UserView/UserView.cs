using GameSelector.Model;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSelector.Views
{
    internal partial class UserView : Form
    {
        private const int ANIMATION_LENGTH_MS = 4000;
        private const int ANIMATION_FRAME_AMT = 9;

        private const string PAUSED_MESSAGE = "Spel is gepauzeerd";
        private const string PROMPT_MESSAGE = "Houd je kaart tegen de lezer";
        private const string SELECTED_MESSAGE = "Speciaal geselecteerd voor ";
        private const string SEARCHING_MESSAGE = "Spel zoeken";

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

            gameAnnouncerLabel.Text = PAUSED_MESSAGE;
            gameCodeLabel.Text = string.Empty;
            gameDescriptionLabel.Text = string.Empty;
            gameExplanationLabel.Text = string.Empty;
        }

        private Timer _animationTimer;
        private int _animationFrame;
        private GameDataView _selectedGame;

        public void ShowGame(GameDataView game)
        {
            if (game == null)
            {
                gameAnnouncerLabel.Text = "Er is geen spel gevonden";
                gameCodeLabel.Text = "";
                gameDescriptionLabel.Text = "";
                gameExplanationLabel.Text = "";
                return;
            }

            _selectedGame = game;

            _animationFrame = 0;
            _animationTimer = new Timer();
            _animationTimer.Tick += AnimationFrame;
            _animationTimer.Interval = ANIMATION_LENGTH_MS / ANIMATION_FRAME_AMT;
            _animationTimer.Start();
        }

        private void AnimationFrame(object sender, EventArgs args)
        {
            if (_animationFrame++ > ANIMATION_FRAME_AMT)
            {
                EndAnimation();
                return;
            }

            int periodAmt = _animationFrame % 4;

            var text = new StringBuilder();
            text.Append(SEARCHING_MESSAGE);
            while (periodAmt-- > 0)
            {
                text.Append('.');
            }

            gameAnnouncerLabel.Text = text.ToString();
        }

        private void EndAnimation()
        {
            _animationTimer.Stop();

            gameAnnouncerLabel.Text = SELECTED_MESSAGE + _selectedGame.OccupiedBy.ScoutingName + ":";
            gameCodeLabel.Text = _selectedGame.Code;
            gameDescriptionLabel.Text = _selectedGame.Description;
            gameExplanationLabel.Text = _selectedGame.Explanation;


            Task.Delay(5000).ContinueWith(t =>
            {
                Invoke(new Action(() =>
                {
                    ShowUnpaused();
                    SendMessage("AnimationComplete", null);
                }));
            });
        }

        public void ShowPaused()
        {
            gameAnnouncerLabel.Text = PAUSED_MESSAGE;
            gameCodeLabel.Text = string.Empty;
            gameDescriptionLabel.Text = string.Empty;
            gameExplanationLabel.Text = string.Empty;
        }

        public void ShowUnpaused()
        {
            gameAnnouncerLabel.Text = PROMPT_MESSAGE;
            gameCodeLabel.Text = string.Empty;
            gameDescriptionLabel.Text = string.Empty;
            gameExplanationLabel.Text = string.Empty;
        }
    }
}
