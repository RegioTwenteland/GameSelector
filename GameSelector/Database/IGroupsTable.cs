using System.Collections.Generic;

namespace GameSelector.Database
{
    internal interface IGroupsTable
    {
        List<IDbGroup> GetAllGroups();

        IDbGroup GetGroupById(long id);

        IDbGroup GetGroupByCardId(string cardId);

        void UpdateGroup(IDbGroup group);

        void InsertGroup(IDbGroup group);
    }
}
