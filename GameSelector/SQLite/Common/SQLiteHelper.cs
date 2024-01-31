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

        public static List<Column> GetColumnsFromType(Type type)
        {
            if (_columnsCache.ContainsKey(type))
            {
                return _columnsCache[type];
            }

            string tableName = GetTableName(type);

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
            GetColumnsFromType(typeof(T))
                .Where(c => c.Prop.Name == propertyName)
                .Single();

        public static string GetFullSelectQuery(Type table) =>
            string.Join(
                $",{Environment.NewLine}",
                GetColumnsFromType(table)
                    .Select(c => $@"`{GetTableName(table)}`.`{c.DbName}` AS {c.Alias}"));

        public static string GetDbName<T>(string propertyName) where T : SQLiteObject =>
            GetDbName(typeof(T), propertyName);

        public static string GetDbName(Type t, string propertyName) =>
            t.GetProperties()
            .Where(p => p.Name == propertyName)
            .Single()
            .GetCustomAttribute<SQLiteColumnAttribute>()
            ?.Name;

        public static string GetTableName<T>() where T : SQLiteObject =>
            GetTableName(typeof(T));

        public static string GetTableName(Type tableType) =>
            tableType
            .GetCustomAttribute<SQLiteTableAttribute>()
            ?.TableName;
    }
}
