﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSelector.NfcReader
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

        public void Beep()
        {
            var original = BackColor;

            BackColor = System.Drawing.Color.Maroon;
            Task.Delay(TimeSpan.FromMilliseconds(500)).ContinueWith(t =>
            {
                BackColor = original;
            });
        }

        private void insertCardButton_Click(object sender, EventArgs e)
        {
            CardDetected?.Invoke(this, EventArgs.Empty);
        }

        private void removeCardButton_Click(object sender, EventArgs e)
        {
            CardRemoved?.Invoke(this, EventArgs.Empty);
        }

        private void swipeButton_Click(object sender, EventArgs e)
        {
            insertCardButton.Enabled = false;
            removeCardButton.Enabled = false;
            swipeButton.Enabled = false;

            CardDetected?.Invoke(this, EventArgs.Empty);
            Task.Delay(TimeSpan.FromMilliseconds(500)).ContinueWith(t => Invoke(new MethodInvoker(() =>
            {
                insertCardButton.Enabled = true;
                removeCardButton.Enabled = true;
                swipeButton.Enabled = true;

                CardRemoved?.Invoke(this, EventArgs.Empty);
            })));
        }

        private void SimulatorView_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeviceDisconnected?.Invoke(this, EventArgs.Empty);
        }
    }
}
