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

        private readonly BindingSource _bindingSource = new();
        private readonly BindingList<GroupDataView> _groups = [];

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
            grid.DataSource = _bindingSource = new BindingSource
            {
                DataSource = _groups,
            };

            SetupColumns();
        }

        private void SetupColumns()
        {
            var cardId = new DataGridViewTextBoxColumn
            {
                Name = nameof(GroupDataView.CardId),
                HeaderText = "Kaart ID",
                DataPropertyName = nameof(GroupDataView.CardId),
                ReadOnly = true,
            };

            var selectNewCardButton = new DataGridViewButtonColumn
            {
                Name = NewCardColumnName,
                HeaderText = string.Empty,
                Text = "🃏",
                UseColumnTextForButtonValue = true,
            };

            var scoutingName = new DataGridViewTextBoxColumn
            {
                Name = nameof(GroupDataView.ScoutingName),
                HeaderText = "Scouting",
                DataPropertyName = nameof(GroupDataView.ScoutingName)
            };

            var groupName = new DataGridViewTextBoxColumn
            {
                Name = nameof(GroupDataView.GroupName),
                HeaderText = "Groep",
                DataPropertyName = nameof(GroupDataView.GroupName)
            };

            var startTime = new DataGridViewTextBoxColumn
            {
                Name = nameof(GroupDataView.StartTime),
                HeaderText = "Starttijd",
                DataPropertyName = nameof(GroupDataView.StartTime),
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "HH:mm:ss"
                }
            };

            var currentGame = new DataGridViewTextBoxColumn
            {
                Name = nameof(GroupDataView.CurrentGame),
                HeaderText = "Huidig spel",
                DataPropertyName = nameof(GroupDataView.CurrentGame),
                ReadOnly = true
            };

            var isAdmin = new DataGridViewCheckBoxColumn
            {
                Name = nameof(GroupDataView.IsAdmin),
                HeaderText = "Admin",
                DataPropertyName = nameof(GroupDataView.IsAdmin),
            };

            var remarks = new DataGridViewTextBoxColumn
            {
                Name = nameof(GroupDataView.Remarks),
                HeaderText = "Opmerkingen",
                DataPropertyName = nameof(GroupDataView.Remarks),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            };

            var delete = new DataGridViewButtonColumn
            {
                Name = DeleteColumnName,
                HeaderText = string.Empty,
                Text = "❌",
                UseColumnTextForButtonValue = true,
            };

            grid.Columns.Add(cardId);
            grid.Columns.Add(selectNewCardButton);
            grid.Columns.Add(scoutingName);
            grid.Columns.Add(groupName);
            grid.Columns.Add(startTime);
            grid.Columns.Add(currentGame);
            grid.Columns.Add(isAdmin);
            grid.Columns.Add(remarks);
            grid.Columns.Add(delete);
        }

        public void SetGroupsList(IEnumerable<GroupDataView> groups)
        {
            _bindingSource.Clear();

            foreach (var group in groups)
            {
                _bindingSource.Add(group);
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

            foreach (var row in _bindingSource)
            {
                idx++;
                var gdv = row as GroupDataView;

                if (gdv.Id == 0)
                {
                    _bindingSource[idx] = group;
                    return;
                }
            }

            _bindingSource.Add(group);
        }

        public void GroupDeleted(GroupDataView group)
        {
            _bindingSource.Remove(group);
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

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            var col = grid.Columns[e.ColumnIndex];
            var row = grid.Rows[e.RowIndex];

            if (col.Name == DeleteColumnName)
            {

                if (row.DataBoundItem is not GroupDataView gdv)
                    return;

                var confirmResult = MessageBox.Show("Weet je zeker dat je deze groep wilt verwijderen?", "", MessageBoxButtons.YesNo);

                if (confirmResult != DialogResult.Yes)
                    return;

                SendMessage("RequestDeleteGroup", gdv);

                return;
            }

            if (col.Name == NewCardColumnName)
            {
                if (row.DataBoundItem is not GroupDataView gdv)
                    return;

                _waitingForCard.GroupDataView = gdv;
                _waitingForCard.Show();
            }
        }
    }
}
