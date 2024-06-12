using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSelector.NfcReader
{
    internal class NfcReaderSimulator : INfcReader
    {
        public bool ConnectionInitialized => true;

        public event EventHandler CardDetected;
        public event EventHandler CardRemoved;
        public event EventHandler DeviceDisconnected;

        private SimulatorView _simulatorView;

        public NfcReaderSimulator()
        {
            Task.Run(ViewThread);
        }

        public void Terminate()
        {
            if (!_simulatorView.IsDisposed)
                _simulatorView.Invoke(new MethodInvoker(_simulatorView.Close));
        }

        private void ViewThread()
        {
            _simulatorView = new SimulatorView();
            _simulatorView.CardDetected += (s, e) => CardDetected?.Invoke(this, EventArgs.Empty);
            _simulatorView.CardRemoved += (s, e) => CardRemoved?.Invoke(this, EventArgs.Empty);
            _simulatorView.DeviceDisconnected += (s, e) => DeviceDisconnected?.Invoke(this, EventArgs.Empty);

            Application.Run(_simulatorView);
        }

        public string GetCardUID()
        {
            return _simulatorView.SelectedCardId;
        }

        public bool WriteMessage(NdefMessage message)
        {
            return true;
        }

        public void Beep()
        {
            if (!_simulatorView.IsDisposed)
                _simulatorView.Invoke(new MethodInvoker(_simulatorView.Beep));
        }
    }
}
