using GameSelector.SQLite.Common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;

namespace GameSelector.SQLite
{
    internal static class ExtensionMethods
    {
        private static void FindForeignKeyTypesRecursive(Type T, HashSet<Type> buffer)
        {
            var foreignKeyProps = SQLiteHelper.GetForeignKeysFromType(T);

            foreach (var prop in foreignKeyProps)
            {
                if (buffer.Contains(prop.PropertyType))
                {
                    continue;
                }

                buffer.Add(prop.PropertyType);
                FindForeignKeyTypesRecursive(prop.PropertyType, buffer);
            }
        }

        private static object GetSingle(Type T, SQLiteDataReader reader)
        {
            var cols = SQLiteHelper.GetColumnsFromType(T);

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

            return foundAnyProps ? output : null;
        }

        public static IEnumerable<T> Get<T>(this SQLiteDataReader reader)
            where T : SQLiteObject
        {
            // Find all the types of all the foreign keys in the entire tree of objects
            // Throw it on a single pile including the requested type.
            var possibleTypes = new HashSet<Type>
            {
                typeof(T)
            };
            FindForeignKeyTypesRecursive(typeof(T), possibleTypes);

            var outputList = new List<T>();

            // Iterate the SQL reader
            while (reader.Read())
            {
                // Create instances from the SQL reader from all found types
                var instances = (from t in possibleTypes
                                 let obj = GetSingle(t, reader)
                                 where obj != null
                                 select (SQLiteObject)obj)
                                 .ToDictionary(obj => obj.GetType(), obj => obj);

                // iterate through the instances and fill in any foreign keys
                foreach(var instance in instances)
                {
                    var instanceType = instance.Key;
                    var instanceObj = instance.Value;

                    var foreignKeyProps = SQLiteHelper.GetForeignKeysFromType(instanceType);

                    foreach(var foreignKey in foreignKeyProps)
                    {
                        if (instances.ContainsKey(foreignKey.PropertyType))
                        {
                            foreignKey.SetValue(instanceObj, instances[foreignKey.PropertyType]);
                        }
                    }
                }

                outputList.Add((T)instances[typeof(T)]);
            }

            return outputList;
        }
    }
}
