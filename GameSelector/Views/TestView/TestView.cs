using System;
using System.Windows.Forms;

namespace GameSelector
{
    public partial class TestView : Form
    {
        private Action<string, object> SendMessage;

        public TestView(Action<string, object> sendMessage)
        {
            InitializeComponent();
            SendMessage = sendMessage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMessage("RequestData", null);
        }
        private void btnSchrijf_Click(object sender, EventArgs e)
        {
            var str = DateTime.Now.ToString();
            SendMessage("SendData", str);
        }

        public void ShowData(string data)
        {
            MessageBox.Show(data);
        }
    }
}
