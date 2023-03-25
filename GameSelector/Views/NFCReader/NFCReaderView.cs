using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSelector.Views.NFCReader
{
    internal class NFCReaderView : IView
    {
        private readonly External.NFCReader nfcReader;
        public NFCReaderView()
        {
            nfcReader = new External.NFCReader();

            nfcReader.Watch();
        }

        public object GetData(string key)
        {
            switch(key)
            {
                case "Block4":
                    break;
            }

            return null;
        }

        public void SendData(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void SubscribeEvent(string eventName, Action<object> callback)
        {
            throw new NotImplementedException();
        }
    }
}
