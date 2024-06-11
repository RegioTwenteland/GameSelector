using System.Collections.Generic;
using System.Windows.Forms;

namespace GameSelector.Views.AdminGroupView
{
    internal class AdminGroupViewAdapter : AbstractView
    {
        private readonly AdminGroupView form;

        public AdminGroupViewAdapter(MessageSender messageSender, IAdminViewScaffold adminScaffold)
            : base(messageSender)
        {
            form = new AdminGroupView(SendMessage, adminScaffold);
        }

        public void UpdateGroup(GroupDataView group)
        {
            form.Invoke(new MethodInvoker(() => form.UpdateGroup(group)));
        }

        public void SetGroupSelected(GroupDataView group)
        {
            form.Invoke(new MethodInvoker(() => form.SetGroupSelected(group)));
        }

        public void ShowLastScannedCardId(string cardId)
        {
            form.Invoke(new MethodInvoker(() => form.ShowLastScannedCardId(cardId)));
        }

        public void SetGroupsList(IEnumerable<GroupDataView> groups)
        {
            form.Invoke(new MethodInvoker(() => form.SetGroupsList(groups)));
        }
    }
}
