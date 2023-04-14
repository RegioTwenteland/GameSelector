using GameSelector.Database;
using System.Diagnostics;

namespace GameSelector.Model
{
    internal class GroupDataBridge
    {
        private readonly IGroupsTable _groupsTable;
        private readonly IDatabaseObjectTranslator _objectTranslator;

        public GroupDataBridge(IDatabase database)
        {
            _groupsTable = database.GroupsTable;
        }

        public static Group DbGroupToGroup(IDbGroup dbGroup, Game game = null)
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
            _groupsTable.UpdateGroup(_objectTranslator.ToDbGroup(group));
        }

        public void InsertGroup(Group group)
        {
            Debug.Assert(group.Id == 0);
            _groupsTable.InsertGroup(_objectTranslator.ToDbGroup(group));
        }
    }
}
