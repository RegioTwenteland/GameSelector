using GameSelector.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.SQLite
{
    internal class SQLiteGroupDataBridge : IGroupDataBridge
    {
        private readonly SQLiteGroupsTable _groupsTable;
        private readonly SQLiteDatabaseObjectTranslator _objectTranslator;

        public event EventHandler<GroupUpdatedEventArgs> GroupUpdated;

        public SQLiteGroupDataBridge(SQLiteDatabase database)
        {
            _groupsTable = database.GroupsTable;
            _objectTranslator = database.ObjectTranslator;
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return
                _groupsTable.GetAllGroups()
                .Select(dbG => _objectTranslator.ToGroup(dbG));
        }

        public IEnumerable<Group> GetAllGroupsPlaying(Game game)
        {
            return
                _groupsTable.GetAllGroupsPlaying(game.Id)
                .Select(dbG => _objectTranslator.ToGroup(dbG));
        }

        public Group GetGroup(string cardId)
        {
            return _objectTranslator.ToGroup(_groupsTable.GetGroupByCardId(cardId));
        }

        public Group GetGroup(long id)
        {
            return _objectTranslator.ToGroup(_groupsTable.GetGroupById(id));
        }

        public void UpdateGroup(Group group)
        {
            _groupsTable.UpdateGroup(_objectTranslator.ToSQLiteGroup(group));

            GroupUpdated?.Invoke(this, new GroupUpdatedEventArgs { Group = group });
        }

        public void InsertGroup(Group group)
        {
            Debug.Assert(group.Id == 0);
            _groupsTable.InsertGroup(_objectTranslator.ToSQLiteGroup(group));
        }

        public void DeleteGroup(Group group)
        {
            _groupsTable.DeleteGroup(_objectTranslator.ToSQLiteGroup(group));
        }
    }
}
