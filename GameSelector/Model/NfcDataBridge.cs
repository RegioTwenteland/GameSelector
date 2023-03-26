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

        private CardData ReadCardData()
        {
            var groupIdBytes = _nfcReader.ReadBlock(_blockNames[0]);
            var groupNameBytes = _nfcReader.ReadBlock(_blockNames[1]);
            var currentGameBytes = _nfcReader.ReadBlock(_blockNames[2]);
            var startTimeBytes = _nfcReader.ReadBlock(_blockNames[3]);

            CardData output;
            try
            {
                var unixOffset = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                var unixTimeStampStr = DecodeBytes(startTimeBytes);
                var unixTimeStamp = long.Parse(unixTimeStampStr);
                var dateTime = unixOffset.AddSeconds(unixTimeStamp).ToLocalTime();

                output = new CardData
                {
                    GroupId = int.Parse(DecodeBytes(groupIdBytes)),
                    GroupName = DecodeBytes(groupNameBytes),
                    CurrentGame = DecodeBytes(currentGameBytes),
                    StartTime = dateTime,
                };
            }
            catch(Exception)
            {
                // Card was not formatted correctly. Format the card.
                output = CardData.Empty;
                CardData = output;
            }

            return output;
        }

        public CardData CardData
        {
            get
            {
                try
                {
                    if (!_nfcReader.Connect())
                    {
                        Logger.LogError(this, "Could not connect to NFC card");
                        return null;
                    }

                    return ReadCardData();
                }
                catch (Exception ex)
                {
                    Logger.LogError(this, ex.ToString());
                    return null;
                }
                finally
                {
                    _nfcReader.Disconnect();
                }
            }

            set
            {
                try
                {
                    if (!_nfcReader.Connect())
                    {
                        Logger.LogError(this, "Could not connect to NFC card");
                        return;
                    }

                    var unixTimeStamp = new DateTimeOffset(value.StartTime.ToUniversalTime()).ToUnixTimeSeconds();

                    _nfcReader.WriteBlock(EncodeBytes(value.GroupId.ToString()), _blockNames[0]);
                    _nfcReader.WriteBlock(EncodeBytes(value.GroupName), _blockNames[1]);
                    _nfcReader.WriteBlock(EncodeBytes(value.CurrentGame), _blockNames[2]);
                    _nfcReader.WriteBlock(EncodeBytes(unixTimeStamp.ToString()), _blockNames[3]);
                }
                catch (Exception ex)
                {
                    Logger.LogError(this, ex.ToString());
                }
                finally
                {
                    _nfcReader.Disconnect();
                }
            }
        }
    }
}
