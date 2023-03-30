using External;
using System;
using System.Text;

namespace GameSelector.Model
{
    internal class NfcDataBridge
    {
        private NFCReader _nfcReader = NFCReader.Instance;

        private string[] _blockNames = new[]
        {
            "4", "5", "6", "8", "9", "10", "12", "13", "14"
        };

        private string DecodeBytes(byte[] bytes)
        {
            // find the index of the first 0x00
            int i = 0;
            while (i < bytes.Length && bytes[i] != 0x00)
            {
                ++i;
            }

            return Encoding.UTF8.GetString(bytes, 0, i);
        }

        private byte[] EncodeBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public string GetCardUID()
        {
            try
            {
                if (_nfcReader.Connect())
                {
                    return _nfcReader.GetCardUID();
                }
                else
                {
                    throw new Exception("Could not connect to NFC reader");
                }
            }
            finally
            {
                _nfcReader.Disconnect();
            }
        }

        //public CardData CardData
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (!_nfcReader.Connect())
        //            {
        //                Logger.LogError(this, "Could not connect to NFC card");
        //                return null;
        //            }

        //            return ReadCardData();
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.LogError(this, ex.ToString());
        //            return null;
        //        }
        //        finally
        //        {
        //            _nfcReader.Disconnect();
        //        }
        //    }

        //    set
        //    {
        //        try
        //        {
        //            if (!_nfcReader.Connect())
        //            {
        //                Logger.LogError(this, "Could not connect to NFC card");
        //                return;
        //            }

        //            var unixTimeStamp = new DateTimeOffset(value.StartTime.ToUniversalTime()).ToUnixTimeSeconds();

        //            _nfcReader.WriteBlock(EncodeBytes(value.GroupId.ToString()), _blockNames[0]);
        //            _nfcReader.WriteBlock(EncodeBytes(value.GroupName), _blockNames[1]);
        //            _nfcReader.WriteBlock(EncodeBytes(value.CurrentGame), _blockNames[2]);
        //            _nfcReader.WriteBlock(EncodeBytes(unixTimeStamp.ToString()), _blockNames[3]);
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.LogError(this, ex.ToString());
        //        }
        //        finally
        //        {
        //            _nfcReader.Disconnect();
        //        }
        //    }
        //}
    }
}
