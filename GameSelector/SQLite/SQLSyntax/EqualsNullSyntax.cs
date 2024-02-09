namespace GameSelector.SQLite.SQLSyntax
{
    internal class EqualsNullSyntax : SQLSyntax
    {
        private SQLSyntax _sqlSyntax;

        public EqualsNullSyntax(QueryMetadata metadata, SQLSyntax parentSyntax)
            : base(metadata)
        {
            _sqlSyntax = parentSyntax;
        }

        public GroupBySyntax GroupBy<Table>(string col)
        {
            return new GroupBySyntax(Metadata, this, typeof(Table), col);
        }

        public override string Generate() =>
            $"{_sqlSyntax.Generate()} IS NULL";
    }
}
