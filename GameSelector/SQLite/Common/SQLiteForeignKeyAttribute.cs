using System;

namespace GameSelector.SQLite.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class SQLiteForeignKeyAttribute : Attribute
    {
    }
}
