using NFC;
using System;
using System.Text;

namespace NFC
{
    internal class NfcReader
    {
        private InternalNfcReader _nfcReader;

        public event EventHandler CardDetected;
        public event EventHandler CardRemoved;
        public event EventHandler DeviceDisconnected;

        private bool _connectionInitialized;

        public NfcReader()
        {
            try
            {
                _nfcReader = InternalNfcReader.Instance;
                _nfcReader.CardInserted += OnCardInserted;
                _nfcReader.CardEjected += OnCardEjected;
                _nfcReader.DeviceDisconnected += OnDeviceDisconnected;
                _connectionInitialized = true;
            }
            catch(Exception)
            {
                _connectionInitialized = false;
            }
        }

        private void OnCardInserted()
        {
            CardDetected?.Invoke(this, EventArgs.Empty);
        }

        private void OnCardEjected()
        {
            CardRemoved?.Invoke(this, EventArgs.Empty);
        }

        private void OnDeviceDisconnected()
        {
            DeviceDisconnected?.Invoke(this, EventArgs.Empty);
        }

        public bool ConnectionInitialized { get => _connectionInitialized; }

        public string GetCardUID()
        {
            try
            {
                if (_nfcReader.Connect())
                {
                    return _nfcReader.GetCardUID();
                }
                else
                {
                    throw new Exception("Could not connect to NFC reader");
                }
            }
            finally
            {
                _nfcReader.Disconnect();
            }
        }
    }
}
