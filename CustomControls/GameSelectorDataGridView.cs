using System;
using System.Linq;
using System.Windows.Forms;

namespace CustomControls
{
    public class GameSelectorDataGridView : DataGridView
    {
        public GameSelectorDataGridView()
        {
            DoubleBuffered = true;

            CurrentCellDirtyStateChanged += OnCurrentCellDirtyStateChanged;
        }

        private void OnCurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (SelectedCells.Cast<DataGridViewCell>().Any(c => c.OwningColumn is DataGridViewCheckBoxColumn))
            {
                // Checkboxes don't auto-commit the change.
                CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
