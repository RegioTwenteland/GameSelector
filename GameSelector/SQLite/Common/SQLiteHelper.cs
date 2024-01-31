using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

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

            var output = new List<Column>();

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

        private static Dictionary<Type, List<PropertyInfo>> _fkCache = new Dictionary<Type, List<PropertyInfo>>();


        public static List<PropertyInfo> GetForeignKeysFromType(Type type)
        {
            if (_fkCache.ContainsKey(type))
            {
                return _fkCache[type];
            }

            var output = new List<PropertyInfo>();

            var allProps = type.GetProperties();

            foreach (var prop in allProps)
            {
                var attributes = prop.GetCustomAttributes(true);

                foreach (var attr in attributes)
                {
                    if (attr is SQLiteForeignKeyAttribute fkAttr)
                    {
                        output.Add(prop);
                        break;
                    }
                }
            }

            _fkCache.Add(type, output);

            return output;
        }

        public static string SqlForSelectTableItems(Type table) =>
            string.Join(
                $",{Environment.NewLine}",
                GetColumnsFromType(table)
                    .Select(c => $@"`{GetTableName(table)}`.`{c.DbName}` AS {c.Alias}"));

        public static string SqlForInsertTableItems(Type table)
        {
            var output = new StringBuilder();
            
            output.AppendLine("(");

            var columns = GetColumnsFromType(table)
                .Where(c => !c.IsPK);

            output.Append(string.Join(
                $",{Environment.NewLine}",
                columns.Select(c => $"    `{c.DbName}`")));
            output.AppendLine();
            output.AppendLine(")");
            output.AppendLine("VALUES");
            output.AppendLine("(");
            output.Append(string.Join(
                $",{Environment.NewLine}",
                columns.Select(c => $"    @{c.Alias}")));
            output.AppendLine();
            output.Append(")");

            return output.ToString();
        }

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
