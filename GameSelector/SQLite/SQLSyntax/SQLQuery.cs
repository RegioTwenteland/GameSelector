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

        public UpdateSyntax Update<Table>()
            where Table : SQLiteObject
        {
            return new UpdateSyntax(_metadata, typeof(Table));
        }

        public InsertSyntax Insert<Table>(SQLiteObject obj)
            where Table : SQLiteObject
        {
            return new InsertSyntax(_metadata, typeof(Table), obj);
        }

        public DeleteSyntax Delete<Table>()
            where Table : SQLiteObject
        {
            return new DeleteSyntax(_metadata, typeof(Table));
        }
    }
}
