using System;

namespace NfcReader
{
    internal class NfcReader : INfcReader
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
            catch (Exception)
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

        private const int BYTES_PER_BLOCK = 16;

        private string[] _allowedBlocks = { "4", "5", "6", "8", "9", "10", "12", "13", "14", "16", "17", "18", "20", "21", "22", "24", "25", "26", "28", "29", "30", "32", "33", "34", "36", "37", "38", "40", "41", "42", "44", "45", "46", "48", "49", "50", "52", "53", "54", "56", "57", "58", "60", "61", "62" };
        //private byte[] _allowedBlocks = { 4, 5, 6, 8, 9, 10, 12, 13, 14, 16, 17, 18, 20, 21, 22, 24, 25, 26, 28, 29, 30, 32, 33, 34, 36, 37, 38, 40, 41, 42, 44, 45, 46, 48, 49, 50, 52, 53, 54, 56, 57, 58, 60, 61, 62 };


        public bool WriteMessage(NdefMessage message)
        {
            try
            {
                if (!_nfcReader.Connect())
                {
                    return false;
                }

                var bytes = message.ToBytes();
                if (bytes.Length > _allowedBlocks.Length * BYTES_PER_BLOCK)
                {
                    throw new ArgumentException("Message is too large");
                }

                var fullBlocks = bytes.Length / BYTES_PER_BLOCK;
                int i;
                for (i = 0; i < fullBlocks; ++i)
                {
                    if (!_nfcReader.WriteBlock(new ArraySegment<byte>(bytes, i * BYTES_PER_BLOCK, BYTES_PER_BLOCK), _allowedBlocks[i]))
                    {
                        return false;
                    }
                }

                var remainingBytes = bytes.Length % BYTES_PER_BLOCK;
                if (!_nfcReader.WriteBlock(new ArraySegment<byte>(bytes, i * BYTES_PER_BLOCK, remainingBytes), _allowedBlocks[i]))
                {
                    return false;
                }

                return true;

            }
            finally
            {
                _nfcReader.Disconnect();
            }
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
