using GameSelector.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameSelector.Views.AdminGameView
{
    internal partial class AdminGameView : UserControl
    {
        private const string SaveText = "Opslaan";
        private const string UnsavedModifier = "*";

        private readonly Action<string, object> SendMessage;

        public AdminGameView(Action<string, object> sendMessage)
        {
            InitializeComponent();

            SendMessage = sendMessage;
        }

        private bool _gameDataUserControl = true;

        private GameDataView GetCurrentGameDataView()
        {
            if (gamesListBox.SelectedIndex < 0) return null;
            return (GameDataView)gamesListBox.Items[gamesListBox.SelectedIndex];
        }

        private void SortGameListBox()
        {
            object[] itemsCopy = new object[gamesListBox.Items.Count];
            gamesListBox.Items.CopyTo(itemsCopy, 0);
            gamesListBox.Items.Clear();

            var newList = itemsCopy
                .Select(o => (GameDataView)o)
                .OrderBy(gdv => gdv.ToString());

            foreach (var newItem in newList)
            {
                gamesListBox.Items.Add(newItem);
            }
        }

        private int GetGameListBoxIndex(GameDataView game)
        {
            var idx = -1;

            for (var i = 0; i < gamesListBox.Items.Count; ++i)
            {
                var gameItem = (GameDataView)gamesListBox.Items[i];

                if (gameItem.Id == game.Id)
                {
                    idx = i;
                    break;
                }
            }

            return idx;
        }

        private void gamesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _gameDataUserControl = false;
            ShowGame(GetCurrentGameDataView());
            _gameDataUserControl = true;
        }

        private void saveGameButton_Click(object sender, EventArgs e)
        {
            var gdv = GetCurrentGameDataView();

            if (gdv == null) return;

            gdv.UnsavedChanges = false;
            saveGameButton.Text = SaveText;
            SendMessage("RequestSaveGame", gdv);
        }

        private void addGameButton_Click(object sender, EventArgs e)
        {
            SendMessage("RequestNewGame", null);
        }

        private void deleteGameButton_Click(object sender, EventArgs e)
        {
            var gdv = GetCurrentGameDataView();

            if (gdv != null)
                SendMessage("RequestDeleteGame", gdv);
        }

        private void GameDataChanged(object sender, EventArgs e)
        {
            if (!_gameDataUserControl) return;

            var gdv = GetCurrentGameDataView();

            if (gdv == null) return;

            gdv.Code = gameCodeTextbox.Text;
            gdv.Description = gameDescriptionTextbox.Text;
            gdv.Explanation = gameExplanationTextbox.Text;
            gdv.Active = gameActiveCheckbox.Checked;
            gdv.Priority = (long)priorityNumber.Value;
            gdv.Remarks = gameRemarksText.Text;
            gdv.TimeoutMinutes = (long)timeoutNumber.Value;
            gdv.MaxPlayerAmount = (long)maxPlayerAmountNumber.Value;
            gdv.UnsavedChanges = true;
            saveGameButton.Text = SaveText + UnsavedModifier;
        }

        public void SetGamesList(IEnumerable<GameDataView> games)
        {
            gamesListBox.Items.Clear();
            foreach (var game in games)
            {
                gamesListBox.Items.Add(game);
            }

            SortGameListBox();

            ShowGame(null);
        }

        private void ShowGame(GameDataView game)
        {
            gameCodeTextbox.Text = string.Empty;
            gameDescriptionTextbox.Text = string.Empty;
            gameExplanationTextbox.Text = string.Empty;
            gameActiveCheckbox.Checked = false;
            priorityNumber.Value = 0;
            gameRemarksText.Text = string.Empty;
            saveGameButton.Text = SaveText;
            timeoutNumber.Value = 0;
            maxPlayerAmountNumber.Value = 0;

            if (game == null) return;

            gameCodeTextbox.Text = game.Code;
            gameDescriptionTextbox.Text = game.Description;
            gameExplanationTextbox.Text = game.Explanation;
            gameActiveCheckbox.Checked = game.Active;
            priorityNumber.Value = game.Priority;
            gameRemarksText.Text = game.Remarks;
            timeoutNumber.Value = game.TimeoutMinutes;
            maxPlayerAmountNumber.Value = game.MaxPlayerAmount;

            saveGameButton.Text += (game.UnsavedChanges ? UnsavedModifier : string.Empty);
        }

        public void UpdateGame(GameDataView game)
        {
            var idx = GetGameListBoxIndex(game);

            if (idx < 0)
            {
                gamesListBox.Items.Add(game);
                idx = GetGameListBoxIndex(game);
            }
            else
            {
                gamesListBox.Items[idx] = game;
            }

            SortGameListBox();
            gamesListBox.SelectedIndex = idx;
        }

        public void SetGameSelected(GameDataView game)
        {
            if (game == null) return;

            var idx = GetGameListBoxIndex(game);

            gamesListBox.SelectedIndex = idx;
        }

        public void ShowPlayedGames(IEnumerable<PlayedGame> playedGames)
        {
            var sb = new StringBuilder();

            foreach (var playedGame in playedGames)
            {
                sb.Append(playedGame.StartTime.ToString("HH:mm:ss"))
                    .Append(" - ")
                    .Append(playedGame.EndTime.ToString("HH:mm:ss"))
                    .Append(": ")
                    .Append(playedGame.Game.Description)
                    .AppendLine();
            }

            MessageBox.Show(sb.ToString());
        }
    }
}
