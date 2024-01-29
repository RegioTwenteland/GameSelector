using System;

namespace GameSelector.SQLite.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class SQLiteColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public bool IsPK { get; set; } = false;
    }
}
