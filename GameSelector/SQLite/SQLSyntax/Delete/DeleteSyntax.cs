using GameSelector.SQLite.Common;
using System;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class DeleteSyntax : SQLSyntax
    {
        private Type _table;

        public DeleteSyntax(QueryMetadata metadata, Type table)
            : base(metadata)
        {
            _table = table;
        }

        public WhereSyntax Where<Table>(string col)
        {
            return new WhereSyntax(Metadata, this, typeof(Table), col);
        }

        public override string Generate() => $"DELETE FROM `{SQLiteHelper.GetTableName(_table)}`";
    }
}
