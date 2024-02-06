namespace GameSelector.SQLite.SQLSyntax
{
    internal class EqualsNullSyntax : SQLSyntax
    {
        private WhereSyntax _whereSyntax;

        public EqualsNullSyntax(QueryMetadata metadata, WhereSyntax whereSyntax)
            : base(metadata)
        {
            _whereSyntax = whereSyntax;
        }

        public GroupBySyntax GroupBy<Table>(string col)
        {
            return new GroupBySyntax(Metadata, this, typeof(Table), col);
        }

        public override string Generate() =>
            $"{_whereSyntax.Generate()} IS NULL";
    }
}
