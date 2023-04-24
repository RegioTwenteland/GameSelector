using System.Collections.Generic;

namespace GameSelector.NFC
{
    internal class NdefMessage
    {
        private enum TLV : byte
        {
            NULL = 0x00,
            NDEF = 0x03,
            Terminator = 0xFE,
        }

        private List<NdefRecord> _records;

        public NdefMessage(params string[] strings)
        {
            _records = new List<NdefRecord>();

            for (var i = 0; i < strings.Length; ++i)
            {
                _records.Add(new NdefRecord
                (
                    messageBegin: i == 0,
                    messageEnd: i == strings.Length - 1,
                    chunkFlag: false,
                    shortFlag: true,
                    idLenFlag: false,
                    text: strings[i]
                ));
            }
        }

        public byte[] ToBytes()
        {
            var messages = new List<byte>();
            foreach (var record in _records)
            {
                messages.AddRange(record.ToBytes());
            }

            var output = new List<byte>
            {
                (byte)TLV.NULL,
                0x00,
                (byte)TLV.NDEF
            };

            if (messages.Count < 255)
            {
                output.Add((byte)messages.Count);
            }
            else
            {
                output.Add(0xFF);
                output.Add((byte)((messages.Count & 0xFF00) >> 8));
                output.Add((byte)((messages.Count & 0xFF)));
            }

            output.AddRange(messages);
            output.Add((byte)TLV.Terminator);

            return output.ToArray();
        }
    }
}
