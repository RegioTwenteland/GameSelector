using GameSelector.Database.SQLite;
using System.Diagnostics;

namespace GameSelector.Model
{
    internal class GroupDataBridge
    {
        private GroupsTable _groupsTable;

        public GroupDataBridge(GroupsTable groupsTable)
        {
            _groupsTable = groupsTable;
        }

        public static Group DbGroupToGroup(DbGroup dbGroup, Game game = null)
        {
            if (dbGroup == null) return null;

            var output = new Group
            {
                Id = dbGroup.Id,
                CardId = dbGroup.CardId,
                Name = dbGroup.Name,
                ScoutingName = dbGroup.ScoutingName,
            };

            output.CurrentlyPlaying = game ?? GameDataBridge.DbGameToGame(dbGroup.CurrentlyPlaying, output);

            return output;
        }

        public static DbGroup GroupToDbGroup(Group group)
        {
            if (group == null) return null;

            return new DbGroup
            {
                Id = group.Id,
                CardId = group.CardId,
                Name = group.Name,
                ScoutingName = group.ScoutingName
            };
        }

        public Group GetGroup(string cardId)
        {
            return DbGroupToGroup(_groupsTable.GetGroupByCardId(cardId));
        }

        public Group GetGroup(long id)
        {
            return DbGroupToGroup(_groupsTable.GetGroupById(id));
        }

        public void UpdateGroup(Group group)
        {
            _groupsTable.UpdateGroup(GroupToDbGroup(group));
        }

        public void InsertGroup(Group group)
        {
            Debug.Assert(group.Id == 0);
            _groupsTable.InsertGroup(GroupToDbGroup(group));
        }
    }
}
