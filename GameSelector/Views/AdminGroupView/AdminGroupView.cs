using CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace GameSelector.Views.AdminGroupView
{
    internal partial class AdminGroupView : UserControl
    {
        private readonly Action<string, object> SendMessage;

        private readonly SortableBindingList<GroupDataView> _groups = [];

        private const string DeleteColumnName = "delete";
        private const string NewCardColumnName = "new_card";

        private WaitingForCardForm _waitingForCard;

        public AdminGroupView(Action<string, object> sendMessage, IAdminViewScaffold adminScaffold)
        {
            InitializeComponent();

            SendMessage = sendMessage;
            _waitingForCard = new WaitingForCardForm(CancelWaitingForCard);

            adminScaffold.AddTabPage("Groepen", this, null);

            grid.AutoGenerateColumns = false;
            grid.DataSource = new BindingSource
            {
                DataSource = _groups,
            };

            grid.SetupColums(
            [
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(GroupDataView.CardId),
                        HeaderText = "Kaart ID",
                        DataPropertyName = nameof(GroupDataView.CardId),
                        ReadOnly = true,
                    },
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewButtonColumn
                    {
                        Name = NewCardColumnName,
                        HeaderText = string.Empty,
                        Text = "🃏",
                        UseColumnTextForButtonValue = true,
                    },
                    OnClick = OnSelectNewCardClicked,
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(GroupDataView.ScoutingName),
                        HeaderText = "Scouting",
                        DataPropertyName = nameof(GroupDataView.ScoutingName)
                    }
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(GroupDataView.GroupName),
                        HeaderText = "Groep",
                        DataPropertyName = nameof(GroupDataView.GroupName)
                    }
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(GroupDataView.StartTime),
                        HeaderText = "Starttijd",
                        DataPropertyName = nameof(GroupDataView.StartTime),
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
                        Name = nameof(GroupDataView.CurrentGame),
                        HeaderText = "Huidig spel",
                        DataPropertyName = nameof(GroupDataView.CurrentGame),
                        ReadOnly = true
                    }
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewCheckBoxColumn
                    {
                        Name = nameof(GroupDataView.IsAdmin),
                        HeaderText = "Admin",
                        DataPropertyName = nameof(GroupDataView.IsAdmin),
                    }
                },
                new GameSelectorDataGridView.ColumnOptions()
                {
                    Column = new DataGridViewTextBoxColumn
                    {
                        Name = nameof(GroupDataView.Remarks),
                        HeaderText = "Opmerkingen",
                        DataPropertyName = nameof(GroupDataView.Remarks),
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
        }

        private void OnSelectNewCardClicked(DataGridViewColumn column, DataGridViewRow row)
        {
            if (row.DataBoundItem is not GroupDataView gdv)
                return;

            _waitingForCard.GroupDataView = gdv;
            _waitingForCard.Show();
        }

        private void OnDeleteClicked(DataGridViewColumn column, DataGridViewRow row)
        {
            if (row.DataBoundItem is not GroupDataView gdv)
                return;

            var confirmResult = MessageBox.Show("Weet je zeker dat je deze groep wilt verwijderen?", "", MessageBoxButtons.YesNo);

            if (confirmResult != DialogResult.Yes)
                return;

            SendMessage("RequestDeleteGroup", gdv);
        }

        public void SetGroupsList(IEnumerable<GroupDataView> groups)
        {
            _groups.Clear();

            foreach (var group in groups)
            {
                _groups.Add(group);
            }

            grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        public void UpdateGroup(GroupDataView group)
        {
            var idx = _groups.IndexOf(_groups.Where(g => g.Id == group.Id).FirstOrDefault());

            if (idx == -1) return;

            _groups[idx] = group;
        }

        public void NewCardScanned(string cardId)
        {
            if (_waitingForCard.Visible)
            {
                _waitingForCard.GroupDataView.CardId = cardId;
                SendMessage("RequestSaveGroup", _waitingForCard.GroupDataView);
                _waitingForCard.Hide();
            }
        }

        public void NewGroup(GroupDataView group)
        {
            var idx = -1;

            foreach (var gdv in _groups)
            {
                idx++;

                if (gdv.Id == 0)
                {
                    _groups[idx] = group;
                    return;
                }
            }

            _groups.Add(group);
        }

        public void GroupDeleted(GroupDataView group)
        {
            _groups.Remove(group);
        }

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = grid.Rows[e.RowIndex];

            if (row.DataBoundItem is not GroupDataView gdv) return;

            if (gdv.Id == 0)
            {
                SendMessage("RequestNewGroup", gdv);
            }
            else
            {
                SendMessage("RequestSaveGroup", gdv);
            }
        }

        private void CancelWaitingForCard()
        {
            _waitingForCard.Hide();
        }

        private void bulkRemoveButton_Click(object sender, EventArgs e)
        {
            var selectedRows = grid.SelectedRows;

            var confirmResult = MessageBox.Show($"Weet je zeker dat je {selectedRows.Count} groepen wilt verwijderen?", "", MessageBoxButtons.YesNo);
            
            if (confirmResult != DialogResult.Yes)
                return;

            for (var i = selectedRows.Count - 1; i >= 0; i--)
            {
                var row = selectedRows[i];

                if (row.DataBoundItem is GroupDataView gdv)
                {
                    SendMessage("RequestDeleteGroup", gdv);
                }

                grid.Rows.Remove(row);
            }
        }
    }
}
