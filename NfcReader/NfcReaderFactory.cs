using System;

namespace NfcReader
{
    public static class NfcReaderFactory
    {
        public static INfcReader CreateNfcReader(bool simulated)
        {
            return simulated ? new NfcReaderSimulator() : new NfcReader();
        }
    }
}
