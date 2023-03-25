using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using External;

namespace GameSelector
{

    //info: https://hakbas.medium.com/nfcreader-a-very-simple-nfc-library-for-c-that-supports-insert-and-discard-events-93db29f79b5
    public partial class Form1 : Form
    {

        NFCReader NFC = new NFCReader();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Connecting
            NFC.Watch();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (NFC.Connect())
                {
                    //Writing   
                    //NFC.WriteBlock("Some string data that will not exceed block limit", "2"); //public bool WriteBlock(String Text, String Block)
                    string str = Encoding.Default.GetString(NFC.ReadBlock("4"));

                    MessageBox.Show(str); //public byte[] ReadBlock(String Block)

                }
                else
                {
                    //Give error message about connection...
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnSchrijf_Click(object sender, EventArgs e)
        {
            try
            {
                if (NFC.Connect())
                {
                    //Writing   
                    DateTime now = DateTime.Now;
                    NFC.WriteBlock(now.ToString(), "4"); //public bool WriteBlock(String Text, String Block)
                    

                }
                else
                {
                    //Give error message about connection...
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
