using System.Collections.Generic;
using System.Windows.Forms;

namespace GameSelector.Views.AdminGroupView
{
    internal class AdminGroupViewAdapter : AbstractView
    {
        public AdminGroupView Control { get; init; }

        public AdminGroupViewAdapter(MessageSender messageSender)
            : base(messageSender)
        {
            Control = new AdminGroupView(SendMessage);
        }

        public void UpdateGroup(GroupDataView group)
        {
            Control.Invoke(new MethodInvoker(() => Control.UpdateGroup(group)));
        }

        public void NewCardScanned(string cardId)
        {
            Control.Invoke(new MethodInvoker(() => Control.NewCardScanned(cardId)));
        }

        public void SetGroupsList(IEnumerable<GroupDataView> groups)
        {
            Control.Invoke(new MethodInvoker(() => Control.SetGroupsList(groups)));
        }

        public void NewGroup(GroupDataView group)
        {
            Control.Invoke(new MethodInvoker(() => Control.NewGroup(group)));
        }

        public void GroupDeleted(GroupDataView group)
        {
            Control.Invoke(new MethodInvoker(() => Control.GroupDeleted(group)));
        }
    }
}
