using System;
using System.Data.SQLite;

namespace GameSelector.SQLite.SQLSyntax
{
    internal abstract class SQLSyntax
    {
        protected QueryMetadata Metadata { get; }

        protected SQLSyntax(QueryMetadata _metadata)
        {
            Metadata = _metadata;
        }

        public abstract string Generate();

        public SQLiteDataReader Execute()
        {
            var sql = Generate();

            var command = new SQLiteCommand(sql, Metadata.Connection);

            foreach (var parameter in Metadata.PreparedParameters)
            {
                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }

            return command.ExecuteReader();
        }

        public int ExecuteNonQuery()
        {
            var sql = Generate();

            var command = new SQLiteCommand(sql, Metadata.Connection);

            foreach (var parameter in Metadata.PreparedParameters)
            {
                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }

            return command.ExecuteNonQuery();
        }
    }
}
