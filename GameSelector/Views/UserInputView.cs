using System;
using System.Text;
using System.Windows.Forms;

namespace GameSelector.Views
{
    internal class UserInputView
    {
        private readonly External.NFCReader nfcReader;

        public UserInputView()
        {
            nfcReader = new External.NFCReader();


            Logger.LogInfo(this, "watching");
            nfcReader.Watch();
        }

        public string GetData()
        {
            try
            {
                if (nfcReader.Connect())
                {
                    //Writing   
                    //NFC.WriteBlock("Some string data that will not exceed block limit", "2"); //public bool WriteBlock(String Text, String Block)
                    string str = Encoding.Default.GetString(nfcReader.ReadBlock("4"));

                    return str;

                }
                else
                {
                    Logger.LogError(this, "Could not connect to NFC card");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(this, ex.ToString());
                return string.Empty;
            }
        }

        public void SetData(string data)
        {
            try
            {
                if (nfcReader.Connect())
                {
                    //Writing   
                    DateTime now = DateTime.Now;
                    nfcReader.WriteBlock(now.ToString(), "4"); //public bool WriteBlock(String Text, String Block)


                }
                else
                {
                    Logger.LogError(this, "Could not connect to NFC card");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(this, ex.ToString());
            }
        }
    }
}
