namespace GameSelector.SQLite.SQLSyntax
{
    internal class EqualsLiteralSyntax : SQLSyntax
    {
        private WhereSyntax _whereSyntax;
        private string _parameterName;

        public EqualsLiteralSyntax(QueryMetadata metadata, WhereSyntax whereSyntax, object literal)
            : base(metadata)
        {
            _whereSyntax = whereSyntax;

            _parameterName = $"@equals{Metadata.Counter}";
            Metadata.Counter++;

            Metadata.PreparedParameters.Add(_parameterName, literal);
        }

        public GroupBySyntax GroupBy<Table>(string col)
        {
            return new GroupBySyntax(Metadata, this, typeof(Table), col);
        }

        public override string Generate() =>
            $"{_whereSyntax.Generate()} = {_parameterName}";
    }
}
