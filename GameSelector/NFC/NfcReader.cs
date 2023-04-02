using NFC;
using System;
using System.Text;

namespace NFC
{
    internal class NfcReader
    {
        private InternalNfcReader _nfcReader = InternalNfcReader.Instance;

        public event EventHandler CardDetected;
        public event EventHandler CardRemoved;

        public NfcReader()
        {
            _nfcReader.CardInserted += OnCardInserted;
            _nfcReader.CardEjected += OnCardEjected;
        }

        private void OnCardInserted()
        {
            CardDetected?.Invoke(this, EventArgs.Empty);
        }

        private void OnCardEjected()
        {
            CardRemoved?.Invoke(this, EventArgs.Empty);
        }

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
