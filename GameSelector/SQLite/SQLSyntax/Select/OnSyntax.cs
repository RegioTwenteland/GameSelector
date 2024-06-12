using GameSelector.SQLite.Common;
using System;
using System.Data.SQLite;

namespace GameSelector.SQLite.SQLSyntax.Select
{
    internal class OnSyntax : SQLSyntax
    {
        private LeftJoinSyntax _leftJoinSyntax;
        private Type _table1;
        private Type _table2;
        private string _col1;
        private string _col2;

        public OnSyntax(QueryMetadata metadata, LeftJoinSyntax leftJoinSyntax, Type table1, Type table2, string prop1, string prop2)
            : base(metadata)
        {
            _leftJoinSyntax = leftJoinSyntax;
            _table1 = table1;
            _table2 = table2;
            _col1 = SQLiteHelper.GetDbName(table1, prop1);
            _col2 = SQLiteHelper.GetDbName(table2, prop2);
        }

        public WhereSyntax Where<Table>(string col)
        {
            return new WhereSyntax(Metadata, this, typeof(Table), col);
        }

        public LeftJoinSyntax LeftJoin<Table>()
        {
            return new LeftJoinSyntax(Metadata, this, typeof(Table));
        }

        public GroupBySyntax GroupBy<Table>(string col)
        {
            return new GroupBySyntax(Metadata, this, typeof(Table), col);
        }

        public OrderBySyntax OrderBy<Table>(string col)
        {
            return new OrderBySyntax(Metadata, this, typeof(Table), col);
        }

        public OrderByDescSyntax OrderByDesc<Table>(string col)
        {
            return new OrderByDescSyntax(Metadata, this, typeof(Table), col);
        }

        public override string Generate() =>
            $"{_leftJoinSyntax.Generate()} ON " +
            $"`{SQLiteHelper.GetTableName(_table1)}`.`{_col1}` = `{SQLiteHelper.GetTableName(_table2)}`.`{_col2}`";
    }
}
