using GameSelector.SQLite.Common;
using System;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class SelectSyntax : SQLSyntax
    {
        private Type _table;

        public SelectSyntax(QueryMetadata metadata, Type table)
            : base(metadata)
        {
            _table = table;
        }

        public FromSyntax From<T>()
            where T : SQLiteObject
        {
            return new FromSyntax(Metadata, this, typeof(T));
        }

        public NestedSelectSyntax Select<Table>()
            where Table : SQLiteObject
        {
            return new NestedSelectSyntax(Metadata, this, typeof(Table));
        }

        public override string Generate() => $"SELECT {SQLiteHelper.SqlForSelectTableItems(_table)}";
    }
}
