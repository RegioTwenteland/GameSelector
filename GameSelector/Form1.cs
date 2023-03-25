using GameSelector.Views;
using System;
using System.Text;
using System.Windows.Forms;

namespace GameSelector
{
    public partial class Form1 : Form
    {
        UserInputView ui = new UserInputView();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = ui.GetData();
            if (text != null)
            {
                MessageBox.Show(text);
            }
        }
        private void btnSchrijf_Click(object sender, EventArgs e)
        {
            var str = DateTime.Now.ToString();
            ui.SetData(str);
        }
    }
}
