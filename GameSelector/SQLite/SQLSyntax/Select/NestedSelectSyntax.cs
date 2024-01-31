using GameSelector.SQLite.Common;
using System;
using System.Data.Linq;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class NestedSelectSyntax : SQLSyntax
    {
        private SQLSyntax _parentSyntax;
        private Type _table;

        public NestedSelectSyntax(QueryMetadata metadata, SQLSyntax selectSyntax, Type table)
            : base(metadata)
        {
            _parentSyntax = selectSyntax;
            _table = table;
        }

        public FromSyntax From<T>() where T : SQLiteObject
        {
            return new FromSyntax(Metadata, this, typeof(T));
        }

        public NestedSelectSyntax Select<Table>()
        {
            return new NestedSelectSyntax(Metadata, this, typeof(Table));
        }

        public override string Generate() => $"{_parentSyntax.Generate()}, {SQLiteHelper.GetFullSelectQuery(_table)}";
    }
}
