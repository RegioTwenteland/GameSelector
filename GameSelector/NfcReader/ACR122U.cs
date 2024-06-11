// This source file has been copied from https://github.com/sydesoft/Sydesoft.NfcDevice.ACR122U and adapted

using PCSC;
using PCSC.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GameSelector.NfcReader
{
    /// <summary>
    /// Require https://github.com/danm-de/pcsc-sharp
    /// </summary>
    public class ACR122U : INfcReader
    {
        private int maxReadWriteLength = 50;
        private int blockSize = 4;
        private int startBlock = 4;
        private int readbackDelayMilliseconds = 100;
        private string[] cardReaderNames = null;
        private ISCardContext cardContext = null;
        private bool buzzerSet = false;
        private bool buzzerOnOff = false;

        public bool ConnectionInitialized { get; private set; }

        public event EventHandler CardDetected;
        public event EventHandler DeviceDisconnected;
        public event EventHandler CardRemoved;

        public ACR122U()
        {
            buzzerOnOff = false;
            maxReadWriteLength = 50;
            blockSize = 4;
            startBlock = 4;
            readbackDelayMilliseconds = 200;

            cardContext = ContextFactory.Instance.Establish(SCardScope.System);

            cardReaderNames = cardContext.GetReaders();

            if (cardReaderNames.Length == 0)
            {
                return;
            }

            var monitor = MonitorFactory.Instance.Create(SCardScope.System);
            monitor.CardInserted += Monitor_CardInserted;
            monitor.CardRemoved += Monitor_CardRemoved;
            monitor.StatusChanged += Monitor_StatusChange;
            monitor.Start(cardReaderNames);

            ConnectionInitialized = true;
        }

        private void Monitor_StatusChange(object sender, StatusChangeEventArgs e)
        {
            if ((e.NewState & SCRState.Unavailable) != 0)
            {
                DeviceDisconnected?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Monitor_CardInserted(object sender, CardStatusEventArgs e)
        {
            ICardReader reader = null;
            if (!buzzerSet)
            {
                try
                {
                    reader = cardContext.ConnectReader(cardReaderNames[0], SCardShareMode.Shared, SCardProtocol.Any);


                    byte[] ret = new byte[2];

                    reader.Transmit([0xFF, 0x00, 0x52, (byte)(buzzerOnOff ? 0xff : 0x00), 0x00], ret);

                    buzzerSet = true;

                    reader.Disconnect(SCardReaderDisposition.Leave);
                }
                catch { }
            }

            CardDetected?.Invoke(this, EventArgs.Empty);
        }

        private void Monitor_CardRemoved(object sender, CardStatusEventArgs e)
        {
            CardRemoved?.Invoke(this, EventArgs.Empty);
        }

        private byte[] GetUID(ICardReader reader)
        {
            byte[] uid = new byte[10];

            reader.Transmit([0xFF, 0xCA, 0x00, 0x00, 0x00], uid);

            Array.Resize(ref uid, 7);

            return uid;
        }

        public byte[] Read(ICardReader reader, int block, int len)
        {
            byte[] data = new byte[len + 2];

            reader.Transmit([0xFF, 0xB0, 0x00, (byte)block, (byte)len], data);

            Array.Resize(ref data, len);

            return data;
        }

        private void Write(ICardReader reader, int block, int len, byte[] data)
        {
            byte[] ret = new byte[2];
            List<byte> cmd = [0xFF, 0xD6, 0x00, (byte)block, (byte)len];
            cmd.AddRange(data);

            reader.Transmit([.. cmd], ret);
        }

        private bool WriteData(ICardReader reader, byte[] data)
        {
            Array.Resize(ref data, maxReadWriteLength);

            int pos = 0;
            while (pos < data.Length)
            {
                byte[] buf = new byte[blockSize];
                int len = data.Length - pos > blockSize ? blockSize : data.Length - pos;
                Array.Copy(data, pos, buf, 0, len);

                Write(reader, pos / blockSize + startBlock, blockSize, buf);

                pos += blockSize;
            }

            Thread.Sleep(readbackDelayMilliseconds);

            byte[] readback = ReadData(reader);

            return data.SequenceEqual(readback);
        }

        public byte[] ReadData(ICardReader reader)
        {
            List<byte> data = new List<byte>();

            int pos = 0;
            while (pos < maxReadWriteLength)
            {
                int len = maxReadWriteLength - pos > blockSize ? blockSize : maxReadWriteLength - pos;

                byte[] buf = Read(reader, pos / blockSize + startBlock, len);

                data.AddRange(buf);

                pos += blockSize;
            }

            return data.ToArray();
        }

        private void Beep(ICardReader reader)
        {
            byte P2 = 0x00;
            P2 <<= 0; // blink green LED
            P2 <<= 0; // blink red LED
            P2 <<= 0; // green LED initial blinking state
            P2 <<= 0; // red LED initial blinking state
            P2 <<= 0; // update green LED
            P2 <<= 0; // update red LED
            P2 <<= 0; // green LED on
            P2 <<= 0; // red LED on

            var ret = new byte[2];
            reader.Transmit([0xFF, 0x00, 0x40, P2, 0x04, 0x01, 0x00, 0x01, 0x01], ret);
        }

        public bool WriteMessage(NdefMessage message)
        {
            ICardReader reader = null;

            try
            {
                reader = cardContext.ConnectReader(cardReaderNames[0], SCardShareMode.Shared, SCardProtocol.Any);
            }
            catch { }

            var result = false;

            if (reader != null)
            {
                try
                {
                    result = WriteData(reader, message.ToBytes());
                    reader.Disconnect(SCardReaderDisposition.Leave);
                }
                catch { }
            }

            return result;
        }

        public string GetCardUID()
        {
            ICardReader reader = null;

            try
            {
                reader = cardContext.ConnectReader(cardReaderNames[0], SCardShareMode.Shared, SCardProtocol.Any);
            }
            catch { }

            var result = string.Empty;

            if (reader != null)
            {
                try
                {
                    var uid = GetUID(reader);
                    var asHex = BitConverter.ToString(uid);
                    var withoutDashes = asHex.Replace("-", "");
                    var eightLong = withoutDashes[..8];
                    var lowerCase = eightLong.ToLower();
                    result = lowerCase;
                    reader.Disconnect(SCardReaderDisposition.Leave);
                }
                catch { }
            }

            return result;
        }

        public void Beep()
        {
            ICardReader reader = null;

            try
            {
                reader = cardContext.ConnectReader(cardReaderNames[0], SCardShareMode.Shared, SCardProtocol.Any);
            }
            catch { }

            if (reader != null)
            {
                try
                {
                    Beep(reader);

                    reader.Disconnect(SCardReaderDisposition.Leave);
                }
                catch { }
            }
        }
    }
}