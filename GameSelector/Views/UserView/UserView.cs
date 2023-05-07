using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSelector.Views
{
    internal partial class UserView : MaterialForm
    {
        private const int ANIMATION_LENGTH_MS = 4000;
        private const int ANIMATION_FRAME_AMT = 99;

        private const string PAUSED_MESSAGE = "Spel is gepauzeerd";
        private const string SELECTED_MESSAGE = "Speciaal geselecteerd voor ";

        private Action<string, object> SendMessage;
        private AudioPlayer _audioPlayer;

        private InsertCardView _insertCardView = new InsertCardView();

        private string[] _gameCodes = new[] { string.Empty };

        public UserView(Action<string, object> sendMessage, AudioPlayer audioPlayer)
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;

            InitializeComponent();
            SendMessage = sendMessage;
            _audioPlayer = audioPlayer;
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

        public void SetGameCodes(string[] gameCodes)
        {
            _gameCodes = gameCodes;
        }

        private Timer _animationTimer;
        private int _animationFrame;
        private GameDataView _selectedGame;

        public void ShowNoGamesLeft()
        {
            _insertCardView.Hide();

            gameAnnouncerLabel.Text = "Je hebt alle spellen al gespeeld!";
            gameCodeLabel.Text = "";
            gameDescriptionLabel.Text = "";
            gameExplanationLabel.Text = "";

            Task.Delay(2000).ContinueWith(t => Invoke(new Action(() => SendMessage("AnimationComplete", null))));
        }

        public void ShowGame(GameDataView game)
        {
            _insertCardView.Hide();

            if (game == null)
            {
                gameAnnouncerLabel.Text = "Er is geen spel gevonden";
                gameCodeLabel.Text = "";
                gameDescriptionLabel.Text = "";
                gameExplanationLabel.Text = "";

                Task.Delay(2000).ContinueWith(t => Invoke(new Action(() => SendMessage("AnimationComplete", null))));

                return;
            }

            _audioPlayer.PlaySelectionStart();

            _selectedGame = game;

            _animationFrame = 0;
            _animationTimer = new Timer();
            _animationTimer.Tick += AnimationFrame;
            _animationTimer.Interval = ANIMATION_LENGTH_MS / ANIMATION_FRAME_AMT;

            searchingProgressBar.Maximum = ANIMATION_FRAME_AMT;
            _animationTimer.Start();
        }

        Random random = new Random();

        private void AnimationFrame(object sender, EventArgs args)
        {
            if (_animationFrame++ >= ANIMATION_FRAME_AMT)
            {
                EndAnimation();
                return;
            }

            if (_animationFrame % 10 == 0)
            {
                searchingGameNameLabel.Text = _gameCodes[random.Next(0, _gameCodes.Length)];
            }

            searchingProgressBar.Value++;
        }

        private void EndAnimation()
        {
            _animationTimer.Stop();

            gameAnnouncerLabel.Text = SELECTED_MESSAGE + _selectedGame.OccupiedBy.ScoutingName + ":";
            gameCodeLabel.Text = _selectedGame.Code;
            searchingGameNameLabel.Text = _selectedGame.Code;
            gameDescriptionLabel.Text = _selectedGame.Description;
            gameExplanationLabel.Text = _selectedGame.Explanation;

            _audioPlayer.PlaySelectionComplete();
            SendMessage("AnimationComplete", null);
        }

        public void ShowAlreadyPlaying(GameDataView game)
        {
            _insertCardView.Hide();

            if (game == null)
            {
                gameAnnouncerLabel.Text = "Je bent al een spel aan het spelen";
                gameCodeLabel.Text = "";
                gameDescriptionLabel.Text = "";
                gameExplanationLabel.Text = "";

                SendMessage("AnimationComplete", null);

                return;
            }

            gameAnnouncerLabel.Text = "Momenteel aan het spelen:";
            gameCodeLabel.Text = game.Code;
            gameDescriptionLabel.Text = game.Description;
            gameExplanationLabel.Text = game.Explanation;
            Task.Delay(2000).ContinueWith(t => Invoke(new Action(() => SendMessage("AnimationComplete", null))));
        }

        public void ShowPaused()
        {
            _insertCardView.Hide();

            gameAnnouncerLabel.Text = PAUSED_MESSAGE;
            gameCodeLabel.Text = string.Empty;
            gameDescriptionLabel.Text = string.Empty;
            gameExplanationLabel.Text = string.Empty;
        }

        public void ShowReady()
        {
            _insertCardView.Show();

            searchingProgressBar.Value = 0;

            gameAnnouncerLabel.Text = string.Empty;
            gameCodeLabel.Text = string.Empty;
            gameDescriptionLabel.Text = string.Empty;
            gameExplanationLabel.Text = string.Empty;

            SendMessage("UserViewReady", null);
        }
        public void ShowReadyAfter(TimeSpan delay)
        {
            Task.Delay(delay).ContinueWith(t =>
            {
                Invoke(new Action(ShowReady));
            });
        }
    }
}
