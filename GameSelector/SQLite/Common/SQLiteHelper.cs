using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace GameSelector.SQLite.Common
{
    internal class SQLiteHelper
    {
        public class Column
        {
            public string DbName { get; set; }

            public bool IsPK { get; set; }

            public string Alias { get; set; }

            public PropertyInfo Prop { get; set; }
        }

        private static Dictionary<Type, List<Column>> _columnsCache = new Dictionary<Type, List<Column>>();

        public static List<Column> GetColumnsFromType(Type type, string tableName)
        {
            if (_columnsCache.ContainsKey(type))
            {
                return _columnsCache[type];
            }

            List<Column> output = new List<Column>();

            var allProps = type.GetProperties();

            foreach (var prop in allProps)
            {
                var attributes = prop.GetCustomAttributes(true);

                foreach (var attr in attributes)
                {
                    if (!(attr is SQLiteColumnAttribute colAttr))
                    {
                        continue;
                    }

                    output.Add(new Column
                    {
                        DbName = colAttr.Name,
                        IsPK = colAttr.IsPK,
                        Alias = $"{tableName}_{colAttr.Name}",
                        Prop = prop,
                    });
                }
            }

            _columnsCache.Add(type, output);

            return output;
        }

        public static Column GetColumnInfo<T>(string propertyName, string tableName) where T : SQLiteObject =>
            GetColumnsFromType(typeof(T), tableName)
                .Where(c => c.Prop.Name == propertyName)
                .Single();

        public static string GetFullSelectQuery(Type type, string tableName) =>
            string.Join(
                $",{Environment.NewLine}",
                GetColumnsFromType(type, tableName)
                    .Select(c => $@"`{tableName}`.`{c.DbName}` AS {c.Alias}"));

        public static string GetDbName<T>(string propertyName) where T : SQLiteObject =>
            typeof(T)
            .GetProperties()
            .Where(p => p.Name == propertyName)
            .Single()
            .GetCustomAttribute<SQLiteColumnAttribute>()
            ?.Name;
    }
}
