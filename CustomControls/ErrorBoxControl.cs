using System;
using System.Windows.Forms;

namespace CustomControls
{
    public partial class ErrorBoxControl : UserControl
    {
        public class CloseRequestedEventArgs : EventArgs
        {
            public CloseRequestedEventArgs(ErrorBoxControl which)
            {
                Which = which;
            }

            public ErrorBoxControl Which { get; set; }
        }

        public event EventHandler<CloseRequestedEventArgs> CloseRequested;

        public ErrorBoxControl()
        {
            InitializeComponent();
        }

        public string ErrorText
        {
            get
            {
                return errorTextbox.Text;
            }

            set
            {
                errorTextbox.Text = value;
            }
        }

        public Guid Guid { get; } = Guid.NewGuid();

        private void closeButton_Click(object sender, EventArgs e)
        {
            CloseRequested?.Invoke(this, new CloseRequestedEventArgs(this));
        }
    }
}
