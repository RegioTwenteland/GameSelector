namespace GameSelector.SQLite.SQLSyntax
{
    internal class LimitSyntax(QueryMetadata metadata, SQLSyntax parentSyntax, int amount) : SQLSyntax(metadata)
    {
        public override string Generate() =>
            $"{parentSyntax.Generate()} LIMIT {amount}";
    }
}
