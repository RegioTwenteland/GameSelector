using GameSelector.SQLite.Common;
using System;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class InsertSyntax : SQLSyntax
    {
        private Type _table;

        public InsertSyntax(QueryMetadata metadata, Type table, SQLiteObject obj)
            : base(metadata)
        {
            _table = table;

            obj.AddAllParametersForPreparedStatementToDict(Metadata.PreparedParameters);
        }

        public override string Generate() => $"INSERT INTO `{SQLiteHelper.GetTableName(_table)}` {SQLiteHelper.SqlForInsertTableItems(_table)}";
    }
}
