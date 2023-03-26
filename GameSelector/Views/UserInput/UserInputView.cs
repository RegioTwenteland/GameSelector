using System;
using System.Text;
using System.Windows.Forms;

namespace GameSelector.Views
{
    internal class UserInputView
    {
        private readonly External.NFCReader nfcReader;

        public UserInputView()
        {
            nfcReader = External.NFCReader.Instance;
        }
    }
}
