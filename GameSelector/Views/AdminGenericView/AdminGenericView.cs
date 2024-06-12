using CustomControls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GameSelector.Views.AdminGenericView
{
    internal partial class AdminGenericView : Form
    {
        private const string SaveText = "Opslaan";
        private const string UnsavedModifier = "*";

        /////////////////////////////////////////////////////
        // GLOBAL
        /////////////////////////////////////////////////////
        private Action<string, object> SendMessage;

        public AdminGenericView(Action<string, object> sendMessage)
        {
            InitializeComponent();
            SendMessage = sendMessage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var screen = Screen.AllScreens.First();
            if (Screen.AllScreens.Length > 1)
            {
                // if there are multiple screens, put this one on the primary
                screen = Screen.AllScreens
                    .Where(s => s.Primary == true)
                    .FirstOrDefault();
            }

            Location = new Point
            {
                X = (screen.WorkingArea.Right + screen.WorkingArea.Left) / 2 - Width / 2,
                Y = (screen.WorkingArea.Bottom + screen.WorkingArea.Top) / 2 - Height / 2
            };
        }

        /////////////////////////////////////////////////////
        // ERRORS
        /////////////////////////////////////////////////////

        private void AddNewError(string errorText)
        {
            var newError = new ErrorBoxControl();
            newError.ErrorText = errorText;
            newError.CloseRequested += OnErrorCloseRequested;

            errorFlowLayout.Controls.Add(newError);
        }

        private void OnErrorCloseRequested(object sender, ErrorBoxControl.CloseRequestedEventArgs e)
        {
            errorFlowLayout.Controls.Remove(e.Which);
        }

        public void ShowError(string errorText)
        {
            AddNewError(errorText);
        }

        public void ShowView()
        {
            Show();
        }

        public void HideView()
        {
            Hide();
        }

        public void AddTabPage(string name, Control control, Action loadedCallback)
        {
            var newTab = new TabPage(name);
            control.Dock = DockStyle.Fill;
            newTab.Controls.Add(control);
            tabControl.TabPages.Add(newTab);

            if (loadedCallback is not null)
            {
                control.Invoke(loadedCallback);
            }
        }
    }
}
