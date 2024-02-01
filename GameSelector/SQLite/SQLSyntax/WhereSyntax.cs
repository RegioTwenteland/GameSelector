using GameSelector.SQLite.Common;
using System.Diagnostics;
using System;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class WhereSyntax : SQLSyntax
    {
        private SQLSyntax _parentSyntax;

        private Type _table;
        private string _col;

        public WhereSyntax(QueryMetadata metadata, SQLSyntax parentSyntax, Type table, string prop)
            : base(metadata)
        {
            Debug.Assert(typeof(SQLiteObject).IsAssignableFrom(table));

            _parentSyntax = parentSyntax;
            _table = table;
            _col = SQLiteHelper.GetDbName(_table, prop);
        }

        public new EqualsLiteralSyntax Equals(object value)
        {
            return new EqualsLiteralSyntax(Metadata, this, value);
        }

        public EqualsNullSyntax EqualsNull()
        {
            return new EqualsNullSyntax(Metadata, this);
        }

        public override string Generate() =>
            $"{_parentSyntax.Generate()} WHERE `{SQLiteHelper.GetTableName(_table)}`.`{_col}`";
    }
}
