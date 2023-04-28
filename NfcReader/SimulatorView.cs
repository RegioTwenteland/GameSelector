using System;
using System.Windows.Forms;

namespace NfcReader
{
    public partial class SimulatorView : Form
    {
        public SimulatorView()
        {
            InitializeComponent();
        }

        public event EventHandler CardDetected;
        public event EventHandler CardRemoved;
        public event EventHandler DeviceDisconnected;

        public string SelectedCardId
        {
            get
            {
                if (selectedCardListbox.SelectedIndex == -1) return "";

                return selectedCardListbox.SelectedItem as string;
            }
        }

    private void insertCardButton_Click(object sender, EventArgs e)
    {
        CardDetected?.Invoke(this, EventArgs.Empty);
    }

    private void removeCardButton_Click(object sender, EventArgs e)
    {
        CardRemoved?.Invoke(this, EventArgs.Empty);
    }
}
}
