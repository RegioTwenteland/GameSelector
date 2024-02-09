using GameSelector.SQLite.Common;
using System;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class OrSyntax : SQLSyntax
    {
        private SQLSyntax _parentSyntax;

        private Type _table;
        private string _col;

        public OrSyntax(QueryMetadata metadata, SQLSyntax parentSyntax, Type table, string prop)
            : base(metadata)
        {
            _parentSyntax = parentSyntax;

            _table = table;
            _col = SQLiteHelper.GetDbName(_table, prop);
        }

        public EqualsNullSyntax EqualsNull()
        {
            return new EqualsNullSyntax(Metadata, this);
        }

        public override string Generate() =>
            $"{_parentSyntax.Generate()} OR `{SQLiteHelper.GetTableName(_table)}`.`{_col}`";
    }
}
