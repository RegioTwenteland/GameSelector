using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSelector.Views
{
    internal partial class UserView : MaterialForm
    {
        private const int TIME_TO_SHOW_RANDOM_GAME_MS = 300;
        private const int FRAME_TIME_MS = 50;
        private const int TIME_TO_SHOW_RANDOM_GAME_FRAMES = TIME_TO_SHOW_RANDOM_GAME_MS / FRAME_TIME_MS;

        private const string PAUSED_MESSAGE = "Spel is gepauzeerd";
        private const string SELECTED_MESSAGE = "Speciaal geselecteerd voor ";

        private static readonly Random random = new Random();

        private readonly Action<string, object> _sendMessage;
        private readonly AudioPlayer _audioPlayer;

        private readonly InsertCardView _insertCardView = new InsertCardView();

        private string[] _gameCodes = new[] { string.Empty };

        /// <summary>
        /// Function to call when the <see cref="ShowReady"/> method is called.
        /// </summary>
        private Action _playOnReady = null;

        public UserView(Action<string, object> sendMessage, AudioPlayer audioPlayer)
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;

            InitializeComponent();
            _sendMessage = sendMessage;
            _audioPlayer = audioPlayer;
        }

        private void UserView_Load(object sender, EventArgs e)
        {
            var screen = Screen.AllScreens.First();

            // if there are multiple screens, put this one on a secondary screen
            if (Screen.AllScreens.Length > 1)
            {
                screen = Screen.AllScreens
                    .Where(s => s.Primary == false)
                    .FirstOrDefault();
            }

            Location = new Point
            {
                X = (screen.WorkingArea.Right + screen.WorkingArea.Left) / 2 - Width / 2,
                Y = (screen.WorkingArea.Bottom + screen.WorkingArea.Top) / 2 - Height / 2
            };

            _insertCardView.Load += (_s, _e) => _insertCardView.Location = new Point
            {
                X = (screen.WorkingArea.Right + screen.WorkingArea.Left) / 2 - _insertCardView.Width / 2,
                Y = (screen.WorkingArea.Bottom + screen.WorkingArea.Top) / 2 - _insertCardView.Height / 2
            };

            gameAnnouncerLabel.Text = PAUSED_MESSAGE;
            searchingGameNameLabel.Text = "---";
            gameCodeLabel.Text = string.Empty;
            gameDescriptionLabel.Text = string.Empty;
            gameExplanationLabel.Text = string.Empty;
        }

        public void SetGameCodes(string[] gameCodes)
        {
            _gameCodes = gameCodes;
        }

        private Timer _animationTimer;
        private GameDataView _selectedGame;
        private GroupDataView _gameSelectedFor;

        public void ShowNoGamesLeft()
        {
            _insertCardView.Hide();

            gameAnnouncerLabel.Text = "Er zijn geen spellen meer beschikbaar";
            gameCodeLabel.Text = "";
            gameDescriptionLabel.Text = "";
            gameExplanationLabel.Text = "";

            Task.Delay(2000).ContinueWith(t => Invoke(new Action(() => _sendMessage("AnimationComplete", null))));
        }

        public void ShowGameImmediate(GameDataView game, GroupDataView group)
        {
            gameAnnouncerLabel.Text = SELECTED_MESSAGE + group.ScoutingName + ":";
            gameCodeLabel.Text = game.Code;
            searchingGameNameLabel.Text = game.Code;
            gameDescriptionLabel.Text = game.Description;
            gameExplanationLabel.Text = game.Explanation;
        }

        private int _frameCounter = 0;

        public void ShowGame(GameDataView game, GroupDataView group)
        {
            _insertCardView.Hide();

            if (game == null)
            {
                gameAnnouncerLabel.Text = "Er is geen spel gevonden";
                gameCodeLabel.Text = "";
                gameDescriptionLabel.Text = "";
                gameExplanationLabel.Text = "";

                Task.Delay(2000).ContinueWith(t => Invoke(new Action(() => _sendMessage("AnimationComplete", null))));

                return;
            }

            _audioPlayer.PlaySelectionStart();

            _selectedGame = game;
            _gameSelectedFor = group;

            var frameAmt = GlobalSettings.AnimationLengthMilliseconds / FRAME_TIME_MS;

            _animationTimer = new Timer();
            _animationTimer.Tick += AnimationFrame;
            _animationTimer.Interval = FRAME_TIME_MS;

            searchingProgressBar.Maximum = frameAmt;
            _animationTimer.Start();
        }

        private void AnimationFrame(object sender, EventArgs args)
        {
            if (searchingProgressBar.Value >= searchingProgressBar.Maximum)
            {
                EndAnimation();
                return;
            }

            if (_frameCounter++ % TIME_TO_SHOW_RANDOM_GAME_FRAMES == 0)
            {
                searchingGameNameLabel.Text = _gameCodes.Length == 0
                    ? string.Empty
                    : _gameCodes[random.Next(0, _gameCodes.Length)];
            }

            searchingProgressBar.Value = Math.Min(searchingProgressBar.Maximum, searchingProgressBar.Value + 1);
        }

        private void EndAnimation()
        {
            _animationTimer.Stop();
            _animationTimer.Dispose();
            _animationTimer = null;

            gameAnnouncerLabel.Text = SELECTED_MESSAGE + _gameSelectedFor.ScoutingName + ":";
            gameCodeLabel.Text = _selectedGame.Code;
            searchingGameNameLabel.Text = _selectedGame.Code;
            gameDescriptionLabel.Text = _selectedGame.Description;
            gameExplanationLabel.Text = _selectedGame.Explanation;

            searchingProgressBar.Value = searchingProgressBar.Maximum;

            _audioPlayer.PlaySelectionComplete();
            _playOnReady = _audioPlayer.PlayEndSession;
            _sendMessage("AnimationComplete", null);
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

                _sendMessage("AnimationComplete", null);

                return;
            }

            gameAnnouncerLabel.Text = "Momenteel aan het spelen:";
            gameCodeLabel.Text = game.Code;
            gameDescriptionLabel.Text = game.Description;
            gameExplanationLabel.Text = game.Explanation;
            Task.Delay(2000).ContinueWith(t => Invoke(new Action(() => _sendMessage("AnimationComplete", null))));
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
            _playOnReady?.Invoke();
            _playOnReady = null;

            _insertCardView.Show();

            searchingProgressBar.Value = 0;

            searchingGameNameLabel.Text = "---";
            gameAnnouncerLabel.Text = string.Empty;
            gameCodeLabel.Text = string.Empty;
            gameDescriptionLabel.Text = string.Empty;
            gameExplanationLabel.Text = string.Empty;

            _sendMessage("UserViewReady", null);
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
