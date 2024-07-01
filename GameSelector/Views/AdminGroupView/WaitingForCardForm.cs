using System;
using System.Windows.Forms;

namespace GameSelector.Views.AdminGroupView
{
    internal partial class WaitingForCardForm : Form
    {
        private readonly Action _cancel;
        private GroupDataView groupDataView;

        public WaitingForCardForm(Action cancel)
        {
            InitializeComponent();

            _cancel = cancel;
        }

        public GroupDataView GroupDataView
        {
            get => groupDataView;

            set
            {
                label2.Text = $"{value.ScoutingName}: {value.GroupName}";
                groupDataView = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _cancel();
        }
    }
}
