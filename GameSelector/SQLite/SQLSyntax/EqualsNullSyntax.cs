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

        public override string Generate() =>
            $"{_whereSyntax.Generate()} IS NULL";
    }
}
