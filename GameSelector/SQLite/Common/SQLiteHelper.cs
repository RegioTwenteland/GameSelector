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

            public string Alias { get; set; }

            public PropertyInfo Prop { get; set; }
        }

        public static List<Column> GetColumnsFromType(Type type, string tableName)
        {
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
                        Alias = $"{tableName}_{colAttr.Name}",
                        Prop = prop,
                    });
                }
            }

            return output;
        }

        public static string GetFullSelectQuery(Type type, string tableName)
        {
            var columns = GetColumnsFromType(type, tableName);

            var output = new List<string>();
            foreach (var col in columns)
            {
                output.Add($@"`{tableName}`.`{col.DbName}` AS {col.Alias}");
            }
            return string.Join($",{Environment.NewLine}", output);
        }

        public static string GetDbName<T>(string propertyName)
        {
            PropertyInfo info = typeof(T).GetProperty(propertyName);

            Debug.Assert(info != null);

            var attr = info.GetCustomAttributes(true)
                .Select(a => a as SQLiteColumnAttribute)
                .Where(a => a != null)
                .Single();

            return attr.Name;
        }
    }
}
