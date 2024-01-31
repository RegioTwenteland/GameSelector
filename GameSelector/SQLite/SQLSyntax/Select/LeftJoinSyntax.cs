using GameSelector.SQLite.Common;
using System;
using System.Data.SQLite;

namespace GameSelector.SQLite.SQLSyntax.Select
{
    internal class LeftJoinSyntax : SQLSyntax
    {
        private FromSyntax _from;
        private Type _table;

        public LeftJoinSyntax(QueryMetadata metadata, FromSyntax from, Type table)
            : base(metadata)
        {
            _from = from;
            _table = table;
        }

        public OnSyntax On<Table1, Table2>(string col1, string col2)
        {
            return new OnSyntax(Metadata, this, typeof(Table1), typeof(Table2), col1, col2);
        }

        public override string Generate() => $"{_from.Generate()} LEFT JOIN `{SQLiteHelper.GetTableName(_table)}`";
    }
}
