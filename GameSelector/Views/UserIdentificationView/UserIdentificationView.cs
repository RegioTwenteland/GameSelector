using NFC;
using System;
using System.Collections.Concurrent;

namespace GameSelector.Views
{
    internal class UserIdentificationView : AbstractView
    {
        private readonly NfcReader _nfcReader;

        public UserIdentificationView(BlockingCollection<Message> messages, NfcReader nfcReader)
            : base(messages)
        {
            _nfcReader = nfcReader;

            _nfcReader.CardDetected += OnCardInserted;
            _nfcReader.CardRemoved += OnCardEjected;
        }

        private void OnCardInserted(object sender, EventArgs e)
        {
            SendMessage("UserLogin", _nfcReader.GetCardUID());
        }

        private void OnCardEjected(object sender, EventArgs e)
        {
            SendMessage("UserLeft");
        }
    }
}
