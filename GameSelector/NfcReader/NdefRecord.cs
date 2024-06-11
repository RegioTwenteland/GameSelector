using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameSelector.NfcReader
{
    public class NdefRecord
    {
        private byte _ndefHeader;
        private byte _languageLen;
        private byte[] _language;
        private byte _textLen;
        private byte[] _text;

        private NdefRecord()
        {
        }

        public NdefRecord(bool messageBegin, bool messageEnd, bool chunkFlag, bool shortFlag, bool idLenFlag, string text)
        {
            // we only support this configuration for now.
            Debug.Assert(!chunkFlag);
            Debug.Assert(shortFlag);
            Debug.Assert(!idLenFlag);
            Debug.Assert(text.Length < 255);

            _ndefHeader = 0x00;
            if (messageBegin) _ndefHeader |= 1;
            _ndefHeader <<= 1;
            if (messageEnd) _ndefHeader |= 1;
            _ndefHeader <<= 1;
            if (chunkFlag) _ndefHeader |= 1;
            _ndefHeader <<= 1;
            if (shortFlag) _ndefHeader |= 1;
            _ndefHeader <<= 1;
            if (idLenFlag) _ndefHeader |= 1;
            _ndefHeader <<= 3;

            _ndefHeader |= 1; // tnf = well-known

            _language = Encoding.UTF8.GetBytes("nl");
            _languageLen = (byte)_language.Length;
            _text = Encoding.UTF8.GetBytes(text);
            _textLen = (byte)_text.Length;
        }

        public byte[] ToBytes()
        {
            if (_textLen == 0)
            {
                return new byte[0];
            }

            var output = new List<byte>();

            output.Add(_ndefHeader);
            output.Add(0x01); // type length
            output.Add((byte)(3 + _text.Length)); // payload length = language + text length
            output.Add((byte)'T'); // record type = text
            output.Add(_languageLen);
            output.AddRange(_language);
            output.AddRange(_text);

            return output.ToArray();
        }
    }
}
