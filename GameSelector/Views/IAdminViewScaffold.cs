using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSelector.Views
{
    public interface IAdminViewScaffold
    {
        void AddTabPage(string name, Control control, Action loadedCallback);
    }
}
