using System;

namespace GameSelector.SQLite.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class SQLiteTableAttribute : Attribute
    {
        public string TableName { get; set; }
    }
}
