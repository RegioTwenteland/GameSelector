using GameSelector.SQLite.Common;
using System.Diagnostics;
using System;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class GroupBySyntax : SQLSyntax
    {
        private SQLSyntax _parentSyntax;

        private Type _table;
        private string _col;

        public GroupBySyntax(QueryMetadata metadata, SQLSyntax parentSyntax, Type table, string prop)
            : base(metadata)
        {
            Debug.Assert(typeof(SQLiteObject).IsAssignableFrom(table));

            _parentSyntax = parentSyntax;
            _table = table;
            _col = SQLiteHelper.GetDbName(_table, prop);
        }

        public override string Generate() =>
            $"{_parentSyntax.Generate()} GROUP BY {SQLiteHelper.GetTableName(_table)}.`{_col}`";
    }
}
