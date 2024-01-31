using GameSelector.SQLite.Common;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class SetSyntax : SQLSyntax
    {
        private UpdateSyntax _updateSyntax;
        private SQLiteObject _obj;

        public SetSyntax(QueryMetadata metadata, UpdateSyntax updateSyntax, SQLiteObject obj)
            : base(metadata)
        {
            _updateSyntax = updateSyntax;
            _obj = obj;

            obj.AddAllParametersForPreparedStatementToDict(Metadata.PreparedParameters);
        }

        public WhereSyntax Where<Table>(string prop)
            where Table : SQLiteObject
        {
            return new WhereSyntax(Metadata, this, typeof(Table), prop);
        }

        public override string Generate() =>
            $"{_updateSyntax.Generate()} SET {_obj.SQLUpdateFull}";
    }
}
