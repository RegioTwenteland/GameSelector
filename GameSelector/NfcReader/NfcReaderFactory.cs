using System;

namespace GameSelector.NfcReader
{
    public static class NfcReaderFactory
    {
        public static INfcReader CreateNfcReader(bool simulated)
        {
            return simulated ? new NfcReaderSimulator() : new ACR122U();
        }
    }
}
