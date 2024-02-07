using System;
using System.Collections.Generic;

namespace GameSelector.Model
{
    internal interface IGroupDataBridge
    {
        event EventHandler<GroupUpdatedEventArgs> GroupUpdated;

        IEnumerable<Group> GetAllGroups();

        IEnumerable<Group> GetAllGroupsPlaying(Game game);

        Group GetGroup(string cardId);

        Group GetGroup(long id);

        void UpdateGroup(Group group);

        void InsertGroup(Group group);

        void DeleteGroup(Group group);
    }
}
