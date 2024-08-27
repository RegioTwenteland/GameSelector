using CustomControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameSelector.Views.AdminPlayedGameView
{
    internal partial class AdminPlayedGameView : UserControl
    {
        private readonly SortableBindingList<PlayedGameDataView> _playedGames = [];

        public AdminPlayedGameView()
        {
            InitializeComponent();

            grid.AutoGenerateColumns = false;
            grid.DataSource = new BindingSource
            {
                DataSource = _playedGames,
            };

            grid.SetupColums(
            [
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(PlayedGameDataView.Game),
                        HeaderText = "Spel",
                        DataPropertyName = nameof(PlayedGameDataView.Game),
                        ReadOnly = true,
                    },
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(PlayedGameDataView.Player),
                        HeaderText = "Groep",
                        DataPropertyName = nameof(PlayedGameDataView.Player),
                        ReadOnly = true,
                    },
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(PlayedGameDataView.StartTime),
                        HeaderText = "Starttijd",
                        DataPropertyName = nameof(PlayedGameDataView.StartTime),
                        ReadOnly = true,
                        DefaultCellStyle = new DataGridViewCellStyle
                        {
                            Format = "HH:mm:ss"
                        }
                    }
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(PlayedGameDataView.EndTime),
                        HeaderText = "Eindtijd",
                        DataPropertyName = nameof(PlayedGameDataView.EndTime),
                        ReadOnly = true,
                        DefaultCellStyle = new DataGridViewCellStyle
                        {
                            Format = "HH:mm:ss"
                        }
                    }
                },
            ]);
        }

        public void SetPlayedGamesList(IEnumerable<PlayedGameDataView> playedGames)
        {
            _playedGames.Clear();

            foreach (var playedGame in playedGames)
            {
                _playedGames.Add(playedGame);
            }

            grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        public void NewPlayedGame(PlayedGameDataView playedGame)
        {
            var idx = -1;

            foreach (var pgdv in _playedGames)
            {
                idx++;

                if (pgdv.Id == 0)
                {
                    _playedGames[idx] = playedGame;
                    return;
                }
            }

            _playedGames.Add(playedGame);
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            sumDisplayLabel.Text = ((int)grid.Sum).ToString();
            selectedRowsLabel.Text = grid.SelectedRows.Count.ToString();
        }
    }
}
