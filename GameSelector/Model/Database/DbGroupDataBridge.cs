using GameSelector.Database;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.Model.Database
{
    internal class DbGroupDataBridge : IGroupDataBridge
    {
        private readonly IGroupsTable _groupsTable;
        private readonly IDatabaseObjectTranslator _objectTranslator;

        public DbGroupDataBridge(IDatabase database)
        {
            _groupsTable = database.GroupsTable;
            _objectTranslator = database.ObjectTranslator;
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
                IsAdmin = dbGroup.IsAdmin != 0,
            };

            output.CurrentlyPlaying = game ?? DbGameDataBridge.DbGameToGame(dbGroup.CurrentlyPlaying, output);

            return output;
        }

        public List<Group> GetAllGroups()
        {
            return
                _groupsTable.GetAllGroups()
                .Select(dbG => DbGroupToGroup(dbG))
                .ToList();
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

        public void DeleteGroup(Group group)
        {
            _groupsTable.DeleteGroup(_objectTranslator.ToDbGroup(group));
        }
    }
}
