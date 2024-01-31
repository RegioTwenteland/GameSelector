using GameSelector.SQLite.Common;
using System.Data.SQLite;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class SQLQuery
    {
        private QueryMetadata _metadata = new QueryMetadata();

        public SQLQuery(SQLiteConnection connection)
        {
            _metadata.Connection = connection;
        }

        public SelectSyntax Select<Table>()
            where Table : SQLiteObject
        {
            return new SelectSyntax(_metadata, typeof(Table));
        }
    }
}
