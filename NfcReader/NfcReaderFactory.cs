using System;

namespace NfcReader
{
    public static class NfcReaderFactory
    {
        public static INfcReader CreateNfcReader(bool simulated, Action<Action> registerTerminateAction)
        {
            return simulated ? new NfcReaderSimulator(registerTerminateAction) : (INfcReader)new NfcReader();
        }
    }
}
