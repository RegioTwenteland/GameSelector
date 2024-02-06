using System.Collections.Generic;
using System.Diagnostics;

namespace GameSelector.Model
{
    internal interface IGroupDataBridge
    {
        IEnumerable<Group> GetAllGroups();

        IEnumerable<Group> GetAllGroupsPlaying(Game game);

        Group GetGroup(string cardId);

        Group GetGroup(long id);

        void UpdateGroup(Group group);

        void InsertGroup(Group group);

        void DeleteGroup(Group group);
    }
}
