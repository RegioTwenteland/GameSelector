using GameSelector.SQLite.Common;
using GameSelector.SQLite.SQLSyntax.Select;
using System;
using System.Data.SQLite;
using System.Diagnostics;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class FromSyntax : SQLSyntax
    {
        private SQLSyntax _parent;
        private Type _table;

        public FromSyntax(QueryMetadata metadata, SQLSyntax parent, Type table)
            : base(metadata)
        {
            Debug.Assert(typeof(SQLiteObject).IsAssignableFrom(table));

            _parent = parent;
            _table = table;
        }

        public LeftJoinSyntax LeftJoin<Table>()
        {
            return new LeftJoinSyntax(Metadata, this, typeof(Table));
        }

        public WhereSyntax Where<Table>(string col)
        {
            return new WhereSyntax(Metadata, this, typeof(Table), col);
        }

        public override string Generate() => $"{_parent.Generate()} FROM `{SQLiteHelper.GetTableName(_table)}`";
    }
}
