using GameSelector.Database;
using System.Diagnostics;

namespace GameSelector.Model
{
    internal interface IGroupDataBridge
    {
        Group GetGroup(string cardId);

        Group GetGroup(long id);

        void UpdateGroup(Group group);

        void InsertGroup(Group group);
    }
}
