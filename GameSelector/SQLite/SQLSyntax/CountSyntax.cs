using GameSelector.SQLite.Common;
using System;
using System.Diagnostics;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class CountSyntax : SQLSyntax
    {
        private SQLSyntax _parentSyntax;

        private Type _table;
        private string _col;

        public CountSyntax(QueryMetadata metadata, SQLSyntax parentSyntax, Type table, string prop)
            : base(metadata)
        {
            Debug.Assert(typeof(SQLiteObject).IsAssignableFrom(table));

            _parentSyntax = parentSyntax;
            _table = table;
            _col = SQLiteHelper.GetDbName(_table, prop);
        }

        public LessThanSyntax LessThan<Table>(string col)
        {
            return new LessThanSyntax(Metadata, this, typeof(Table), col);
        }

        public override string Generate() =>
            $"{_parentSyntax.Generate()} COUNT(`{SQLiteHelper.GetTableName(_table)}`.`{_col}`)";
    }
}
