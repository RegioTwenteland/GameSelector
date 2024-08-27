using CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GameSelector.Views.AdminGameView
{
    internal partial class AdminGameView : UserControl
    {
        private readonly Action<string, object> SendMessage;

        private readonly SortableBindingList<GameDataView> _games = [];

        private const string DeleteColumnName = "delete";

        public AdminGameView(Action<string, object> sendMessage)
        {
            InitializeComponent();

            SendMessage = sendMessage;

            grid.AutoGenerateColumns = false;
            grid.DataSource = new BindingSource
            {
                DataSource = _games,
            };

            grid.SetupColums(
            [
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(GameDataView.Code),
                        HeaderText = "Code",
                        DataPropertyName = nameof(GameDataView.Code),
                    },
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(GameDataView.Description),
                        HeaderText = "Omschrijving",
                        DataPropertyName = nameof(GameDataView.Description)
                    }
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(GameDataView.Explanation),
                        HeaderText = "Uitleg",
                        DataPropertyName = nameof(GameDataView.Explanation)
                    }
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewCheckBoxColumn
                    {
                        Name = nameof(GameDataView.Active),
                        HeaderText = "Actief",
                        DataPropertyName = nameof(GameDataView.Active),
                    }
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(GameDataView.Priority),
                        HeaderText = "Prioriteit",
                        DataPropertyName = nameof(GameDataView.Priority),
                        DefaultCellStyle = new DataGridViewCellStyle
                        {
                            Format = "N2"
                        }
                    }
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(GameDataView.StartTime),
                        HeaderText = "Starttijd",
                        DataPropertyName = nameof(GameDataView.StartTime),
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
                        Name = nameof(GameDataView.TimeoutMinutes),
                        HeaderText = "Timeout [m]",
                        DataPropertyName = nameof(GameDataView.TimeoutMinutes),
                        DefaultCellStyle = new DataGridViewCellStyle
                        {
                            Format = "N2"
                        }
                    }
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(GameDataView.MaxPlayerAmount),
                        HeaderText = "Max spelers",
                        DataPropertyName = nameof(GameDataView.MaxPlayerAmount),
                        DefaultCellStyle = new DataGridViewCellStyle
                        {
                            Format = "N0"
                        }
                    }
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(GameDataView.Remarks),
                        HeaderText = "Opmerkingen",
                        DataPropertyName = nameof(GameDataView.Remarks),
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    }
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewButtonColumn
                    {
                        Name = DeleteColumnName,
                        HeaderText = string.Empty,
                        Text = "❌",
                        UseColumnTextForButtonValue = true,
                    },
                    OnClick = OnDeleteClicked,
                },
            ]);

            grid.DataError += OnDataError;
        }

        private void OnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // TODO
        }

        private void OnDeleteClicked(DataGridViewColumn column, DataGridViewRow row)
        {
            if (row.DataBoundItem is not GameDataView gdv)
                return;

            var confirmResult = MessageBox.Show("Weet je zeker dat je dit spel wilt verwijderen?", "", MessageBoxButtons.YesNo);

            if (confirmResult != DialogResult.Yes)
                return;

            SendMessage("RequestDeleteGame", gdv);
        }

        public void SetGamesList(IEnumerable<GameDataView> games)
        {
            _games.Clear();

            foreach (var game in games)
            {
                _games.Add(game);
            }

            grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        public void UpdateGame(GameDataView game)
        {
            var idx = _games.IndexOf(_games.Where(g => g.Id == game.Id).FirstOrDefault());

            if (idx == -1) return;

            _games[idx] = game;
        }

        public void NewGame(GameDataView game)
        {
            var idx = -1;

            foreach (var gdv in _games)
            {
                idx++;

                if (gdv.Id == 0)
                {
                    _games[idx] = game;
                    return;
                }
            }

            _games.Add(game);
        }

        public void GameDeleted(GameDataView game)
        {
            _games.Remove(game);
        }

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = grid.Rows[e.RowIndex];

            if (row.DataBoundItem is not GameDataView gdv) return;

            if (gdv.Id == 0)
            {
                SendMessage("RequestNewGame", gdv);
            }
            else
            {
                SendMessage("RequestSaveGame", gdv);
            }
        }

        private void bulkRemoveButton_Click(object sender, EventArgs e)
        {
            var selectedRows = grid.SelectedRows;

            var confirmResult = MessageBox.Show($"Weet je zeker dat je {selectedRows.Count} spellen wilt verwijderen?", "", MessageBoxButtons.YesNo);

            if (confirmResult != DialogResult.Yes)
                return;

            for (var i = selectedRows.Count - 1; i >= 0; i--)
            {
                var row = selectedRows[i];

                if (row.DataBoundItem is GameDataView gdv)
                {
                    SendMessage("RequestDeleteGame", gdv);

                    grid.Rows.Remove(row);
                }
            }
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            sumDisplayLabel.Text = ((int)grid.Sum).ToString();
            selectedRowsLabel.Text = grid.SelectedRows.Count.ToString();
        }
    }
}
