using GameSelector.SQLite.Common;
using System;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class OrderByDescSyntax : SQLSyntax
    {
        private SQLSyntax _parentSyntax;

        private Type _table;
        private string _col;

        public OrderByDescSyntax(QueryMetadata metadata, SQLSyntax parentSyntax, Type table, string prop)
            : base(metadata)
        {
            _parentSyntax = parentSyntax;

            _table = table;
            _col = SQLiteHelper.GetDbName(_table, prop);
        }
        
        public LimitSyntax Limit(int amount)
        {
            return new LimitSyntax(Metadata, this, amount);
        }

        public override string Generate() =>
            $"{_parentSyntax.Generate()} ORDER BY `{SQLiteHelper.GetTableName(_table)}`.`{_col}` DESC";
    }
}
