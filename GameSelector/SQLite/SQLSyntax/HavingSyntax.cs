using GameSelector.SQLite.Common;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class HavingSyntax : SQLSyntax
    {
        private SQLSyntax _parentSyntax;

        public HavingSyntax(QueryMetadata metadata, SQLSyntax parentSyntax)
            : base(metadata)
        {
            _parentSyntax = parentSyntax;
        }

        public CountSyntax Count<Table>(string col)
        {
            return new CountSyntax(Metadata, this, typeof(Table), col);
        }

        public override string Generate() =>
            $"{_parentSyntax.Generate()} HAVING";
    }
}
