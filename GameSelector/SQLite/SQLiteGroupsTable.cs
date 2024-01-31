using GameSelector.SQLite.SQLSyntax;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;

namespace GameSelector.SQLite
{
    internal class SQLiteGroupsTable
    {
        private SQLiteConnection _connection;

        public const string TableName = "groups";

        public SQLiteGroupsTable(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public List<SQLiteGroup> GetAllGroups()
        {
            return new SQLQuery(_connection)
                .Select<SQLiteGroup>().Select<SQLiteGame>()
                .From<SQLiteGroup>().LeftJoin<SQLiteGame>()
                .On<SQLiteGroup, SQLiteGame>(nameof(SQLiteGroup.Id), nameof(SQLiteGame.OccupiedById))
                .Execute()
                .Get<SQLiteGroup>().ToList();
        }

        public SQLiteGroup GetGroupById(long id)
        {
            return new SQLQuery(_connection)
                .Select<SQLiteGroup>().Select<SQLiteGame>()
                .From<SQLiteGroup>().LeftJoin<SQLiteGame>()
                .On<SQLiteGroup, SQLiteGame>(nameof(SQLiteGroup.Id), nameof(SQLiteGame.OccupiedById))
                .Where<SQLiteGroup>(nameof(SQLiteGroup.Id)).Equals(id)
                .Execute()
                .Get<SQLiteGroup>()
                .SingleOrDefault();
        }

        public SQLiteGroup GetGroupByCardId(string cardId)
        {
            return new SQLQuery(_connection)
                .Select<SQLiteGroup>().Select<SQLiteGame>()
                .From<SQLiteGroup>().LeftJoin<SQLiteGame>()
                .On<SQLiteGroup, SQLiteGame>(nameof(SQLiteGroup.Id), nameof(SQLiteGame.OccupiedById))
                .Where<SQLiteGroup>(nameof(SQLiteGroup.CardId)).Equals(cardId)
                .Execute()
                .Get<SQLiteGroup>()
                .SingleOrDefault();
        }

        public void UpdateGroup(SQLiteGroup group)
        {
            var rowsUpdated = new SQLQuery(_connection)
                .Update<SQLiteGroup>().Set(group)
                .Where<SQLiteGroup>(nameof(SQLiteGroup.Id)).Equals(group.Id)
                .ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }

        public void InsertGroup(SQLiteGroup group)
        {
            var rowsUpdated = new SQLQuery(_connection)
                .Insert<SQLiteGroup>(group)
                .ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }

        public void DeleteGroup(SQLiteGroup group)
        {
            var rowsUpdated = new SQLQuery(_connection)
                .Delete<SQLiteGroup>()
                .Where<SQLiteGroup>(nameof(SQLiteGroup.Id)).Equals(group.Id)
                .ExecuteNonQuery();

            Debug.Assert(rowsUpdated == 1);
        }
    }
}
