using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSelector.Views.AdminSettingsView
{
    public partial class SelectEventView : Form
    {
        public event EventHandler<string> EventSelected;

        public SelectEventView(IEnumerable<string> options)
        {
            InitializeComponent();

            foreach (var option in options)
            {
                eventsCombobox.Items.Add(option);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            EventSelected?.Invoke(this, string.Empty);
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            EventSelected?.Invoke(this, eventsCombobox.SelectedItem.ToString());
        }
    }
}
