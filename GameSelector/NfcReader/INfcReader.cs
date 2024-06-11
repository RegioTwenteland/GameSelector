using System;

namespace GameSelector.NfcReader
{
    public interface INfcReader
    {
        event EventHandler CardDetected;
        event EventHandler CardRemoved;
        event EventHandler DeviceDisconnected;

        bool WriteMessage(NdefMessage message);

        bool ConnectionInitialized { get; }

        string GetCardUID();

        void Beep();
    }
}
