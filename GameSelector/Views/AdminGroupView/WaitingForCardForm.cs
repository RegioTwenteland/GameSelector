using System;
using System.Windows.Forms;

namespace GameSelector.Views.AdminGroupView
{
    internal partial class WaitingForCardForm : Form
    {
        private GroupDataView groupDataView;

        public WaitingForCardForm(Action cancel)
        {
            InitializeComponent();
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
    }
}
