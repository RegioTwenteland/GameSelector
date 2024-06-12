using System.Collections.Generic;
using System.Windows.Forms;

namespace GameSelector.Views.AdminGroupView
{
    internal class AdminGroupViewGridAdapter : AbstractView
    {
        private readonly AdminGroupViewGrid form;

        public AdminGroupViewGridAdapter(MessageSender messageSender, IAdminViewScaffold adminScaffold)
            : base(messageSender)
        {
            form = new AdminGroupViewGrid(SendMessage, adminScaffold);
        }

        public void UpdateGroup(GroupDataView group)
        {
            form.Invoke(new MethodInvoker(() => form.UpdateGroup(group)));
        }

        public void NewCardScanned(string cardId)
        {
            form.Invoke(new MethodInvoker(() => form.NewCardScanned(cardId)));
        }

        public void SetGroupsList(IEnumerable<GroupDataView> groups)
        {
            form.Invoke(new MethodInvoker(() => form.SetGroupsList(groups)));
        }

        public void NewGroup(GroupDataView group)
        {
            form.Invoke(new MethodInvoker(() => form.NewGroup(group)));
        }

        public void GroupDeleted(GroupDataView group)
        {
            form.Invoke(new MethodInvoker(() => form.GroupDeleted(group)));
        }
    }
}
