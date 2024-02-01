using GameSelector.SQLite.Common;
using System;
using System.Data.SQLite;

namespace GameSelector.SQLite.SQLSyntax.Select
{
    internal class LeftJoinSyntax : SQLSyntax
    {
        private SQLSyntax _parent;
        private Type _table;

        public LeftJoinSyntax(QueryMetadata metadata, SQLSyntax parent, Type table)
            : base(metadata)
        {
            _parent = parent;
            _table = table;
        }

        public OnSyntax On<Table1, Table2>(string col1, string col2)
        {
            return new OnSyntax(Metadata, this, typeof(Table1), typeof(Table2), col1, col2);
        }

        public override string Generate() => $"{_parent.Generate()} LEFT JOIN `{SQLiteHelper.GetTableName(_table)}`";
    }
}
