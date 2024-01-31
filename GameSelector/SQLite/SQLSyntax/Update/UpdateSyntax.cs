using GameSelector.SQLite.Common;
using System.Diagnostics;
using System;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class UpdateSyntax : SQLSyntax
    {
        private Type _table;

        public UpdateSyntax(QueryMetadata metadata, Type table)
            : base(metadata)
        {
            Debug.Assert(typeof(SQLiteObject).IsAssignableFrom(table));

            _table = table;
        }

        public SetSyntax Set(SQLiteObject obj)
        {
            return new SetSyntax(Metadata, this, obj);
        }

        public override string Generate() =>
            $"UPDATE `{SQLiteHelper.GetTableName(_table)}`";
    }
}
