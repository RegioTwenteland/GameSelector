using System;
using System.Threading;
using System.Windows.Forms;

namespace NfcReader
{
    internal class NfcReaderSimulator : INfcReader
    {
        public bool ConnectionInitialized => true;

        public event EventHandler CardDetected;
        public event EventHandler CardRemoved;
        public event EventHandler DeviceDisconnected;

        private SimulatorView _simulatorView;
        private Thread _simulatorThreadView;

        public NfcReaderSimulator(Action<Action> registerTerminateAction)
        {
            _simulatorThreadView = new Thread(ViewThread);
            _simulatorThreadView.Start();

            registerTerminateAction(Terminate);
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
    }
}
