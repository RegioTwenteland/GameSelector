using NfcReader;
using System;
using System.Collections.Concurrent;

namespace GameSelector.Views
{
    internal class UserIdentificationView : AbstractView
    {
        private readonly INfcReader _nfcReader;

        //private string _errorMessage = string.Empty;

        public UserIdentificationView(BlockingCollection<Message> messages, INfcReader nfcReader)
            : base(messages)
        {
            _nfcReader = nfcReader;

            if (!_nfcReader.ConnectionInitialized)
            {
                SendMessage("ShowAdminError", "Kon niet verbinden met de NFC reader. Sluit de reader aan en start de applicatie opnieuw.");
                return;
            }

            _nfcReader.CardDetected += OnCardInserted;
            _nfcReader.CardRemoved += OnCardEjected;
            _nfcReader.DeviceDisconnected += OnReaderDisconnected;
        }

        private void OnCardInserted(object sender, EventArgs e)
        {
            SendMessage("CardInserted", _nfcReader.GetCardUID());
        }

        private void OnCardEjected(object sender, EventArgs e)
        {
            SendMessage("CardEjected");
        }

        private void OnReaderDisconnected(object sender, EventArgs e)
        {
            SendMessage("ShowAdminError", "Connectie met de NFC reader is verloren gegaan. Sluit de reader aan en start de applicatie opnieuw.");

            _nfcReader.CardDetected -= OnCardInserted;
            _nfcReader.CardRemoved -= OnCardEjected;
            _nfcReader.DeviceDisconnected -= OnReaderDisconnected;
        }
    }
}
