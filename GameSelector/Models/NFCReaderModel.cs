using External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSelector.Models
{
    internal class NFCReaderModel
    {
        private NFCReader _nfcReader;

        public NFCReaderModel()
        {
            _nfcReader = NFCReader.Instance;
        }

        public CardData CardData {
            get
            {
                return default;
            }
            set
            {

            }
        }
    }
}
