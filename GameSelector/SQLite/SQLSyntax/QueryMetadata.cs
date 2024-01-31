using System.Collections.Generic;
using System.Data.SQLite;

namespace GameSelector.SQLite.SQLSyntax
{
    internal class QueryMetadata
    {
        public SQLiteConnection Connection { get; set; }

        public Dictionary<string, object> PreparedParameters { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Can be used by any 'syntax' who needs a unique value per SQL statement.
        /// Must be incremented after use.
        /// </summary>
        public int Counter { get; set; }
    }
}
