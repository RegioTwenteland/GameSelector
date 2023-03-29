using System;
using System.Collections.Concurrent;
using System.Text;
using System.Windows.Forms;

namespace GameSelector.Views
{
    internal class UserInputView : AbstractView
    {
        private readonly External.NFCReader _nfcReader;

        public UserInputView(BlockingCollection<Message> messages)
            : base(messages)
        {
            _nfcReader = External.NFCReader.Instance;

            _nfcReader.CardInserted += OnCardInserted;
            _nfcReader.CardEjected += OnCardEjected;
        }

        private void OnCardInserted()
        {
            SendMessage("CardInserted");
        }

        private void OnCardEjected()
        {
            SendMessage("CardEjected");
        }
    }
}
