using GameSelector.SQLite.Common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GameSelector.SQLite
{
    internal static class ExtensionMethods
    {
        private static bool GetSingle(Type T, SQLiteDataReader reader, out object result)
        {
            var cols = SQLiteHelper.GetColumnsFromType(T);
            var foreignKeyProps = SQLiteHelper.GetForeignKeysFromType(T);

            var output = Activator.CreateInstance(T);

            var foundAnyProps = false;
            foreach (var col in cols)
            {
                try
                {
                    object val = SQLiteDatabase.FromDbNull(col.Prop.PropertyType, reader[col.Alias]);
                    col.Prop.SetValue(output, val);

                    foundAnyProps = true;
                }
                catch (IndexOutOfRangeException)
                {
                    // we don't have the information for this column, skip it
                }
            }

            foreach(var prop in foreignKeyProps)
            {
                if (GetSingle(prop.PropertyType, reader, out var obj))
                {
                    prop.SetValue(output, obj);
                    foundAnyProps = true;
                }
            }

            result = output;

            return foundAnyProps;
        }

        public static IEnumerable<T> Get<T>(this SQLiteDataReader reader)
            where T : SQLiteObject
        {
            var outputList = new List<T>();

            while (reader.Read())
            {
                if (!GetSingle(typeof(T), reader, out var result))
                {
                    continue;
                }

                outputList.Add((T)result);
            }

            return outputList;
        }
    }
}
