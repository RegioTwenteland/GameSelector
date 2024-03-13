using NfcReader;
using System;
using System.Collections.Concurrent;

namespace GameSelector.Views
{
    internal class UserIdentificationView : AbstractView
    {
        private readonly INfcReader _nfcReader;

        private TargetedMessageSender _adminMessageSender;
        private TargetedMessageSender _broadcastMessageSender;

        public UserIdentificationView(
            TargetedMessageSender adminMessageSender,
            TargetedMessageSender broadcastMessageSender,
            INfcReader nfcReader)
        {
            _adminMessageSender = adminMessageSender;
            _broadcastMessageSender = broadcastMessageSender;

            _nfcReader = nfcReader;

            if (!_nfcReader.ConnectionInitialized)
            {
                _adminMessageSender.Send("ShowAdminError", "Kon niet verbinden met de NFC reader. Sluit de reader aan en start de applicatie opnieuw.");
                return;
            }

            _nfcReader.CardDetected += OnCardInserted;
            _nfcReader.CardRemoved += OnCardEjected;
            _nfcReader.DeviceDisconnected += OnReaderDisconnected;
        }

        private void OnCardInserted(object sender, EventArgs e)
        {
            var uid = _nfcReader.GetCardUID();

            if (!string.IsNullOrEmpty(uid))
            {
                _broadcastMessageSender.Send("CardInserted", uid);
            }
        }

        private void OnCardEjected(object sender, EventArgs e)
        {
            _broadcastMessageSender.Send("CardEjected");
        }

        private void OnReaderDisconnected(object sender, EventArgs e)
        {
            _adminMessageSender.Send("ShowAdminError", "Connectie met de NFC reader is verloren gegaan. Sluit de reader aan en start de applicatie opnieuw.");

            _nfcReader.CardDetected -= OnCardInserted;
            _nfcReader.CardRemoved -= OnCardEjected;
            _nfcReader.DeviceDisconnected -= OnReaderDisconnected;
        }
    }
}
