using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Windows.ApplicationModel.Appointments;

namespace CustomControls
{
    public class GameSelectorDataGridView : DataGridView
    {
        private Dictionary<string, ColumnOptions> _columnOptions;

        public GameSelectorDataGridView()
        {
            DoubleBuffered = true;

            _columnOptions = [];
            CurrentCellDirtyStateChanged += OnCurrentCellDirtyStateChanged;

            CellClick += OnCellClicked;
        }

        private void OnCellClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex <= 0 || e.RowIndex <= 0) return;

            var col = Columns[e.ColumnIndex];
            var row = Rows[e.RowIndex];

            if (!_columnOptions.TryGetValue(col.Name, out var options))
            {
                return;
            }

            if (options.OnClick is null)
            {
                return;
            }

            options.OnClick(col, row);
        }

        private void OnCurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (SelectedCells.Cast<DataGridViewCell>().Any(c => c.OwningColumn is DataGridViewCheckBoxColumn))
            {
                // Checkboxes don't auto-commit the change.
                CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        public void SetupColums(IEnumerable<ColumnOptions> columnsOptions)
        {
            foreach (var columnOptions in columnsOptions)
            {
                Columns.Add(columnOptions.Column);
                _columnOptions.Add(columnOptions.Column.Name, columnOptions);
            }
        }

        /// <summary>
        /// Returns the sum of all selected numbers
        /// </summary>
        public double Sum
        {
            get
            {
                var result = 0d;

                for (var i = 0; i < SelectedCells.Count; i++)
                {
                    var cell = SelectedCells[i];

                    if (double.TryParse(cell.Value?.ToString(), out var parseResult))
                    {
                        result += parseResult;
                    }
                }

                return result;
            }
        }

        public class ColumnOptions
        {
            public DataGridViewColumn Column { get; init; }

            public Action<DataGridViewColumn, DataGridViewRow> OnClick { get; init; }
        }
    }
}
