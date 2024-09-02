using System;
using System.IO;
using System.Windows.Forms;

namespace GameSelector.Views.StartupView
{
    public partial class StartupView : Form
    {
        private readonly Action<string, object> SendMessage;

        private string InitialDirectory => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public StartupView(Action<string, object> sendMessage)
        {
            InitializeComponent();

            SendMessage = sendMessage;
        }

        private void newFileButton_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            using var newFileDialog = new SaveFileDialog();

            newFileDialog.InitialDirectory = InitialDirectory;
            newFileDialog.RestoreDirectory = true;
            newFileDialog.Filter = "sqlite db files (*.sqlite)|*.sqlite";

            if (newFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = newFileDialog.FileName;
            }

            if (!string.IsNullOrEmpty(filePath))
            {
                SendMessage("RequestNewFile", filePath);
            }
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            using var openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = InitialDirectory;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "sqlite db files (*.sqlite)|*.sqlite";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }

            if (!string.IsNullOrEmpty(filePath))
            {
                SendMessage("RequestOpenFile", filePath);
            }
        }
    }
}
