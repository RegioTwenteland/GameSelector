using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace GameSelector.SQLite.Common
{
    internal class SQLiteObject
    {
        public SQLiteObject()
        {
        }

        public SQLiteObject(SQLiteDataReader reader)
        {
            FillPropsFromSqlReader(reader);
        }

        private void FillPropsFromSqlReader(SQLiteDataReader reader)
        {
            var cols = SQLiteHelper.GetColumnsFromType(GetType());

            foreach (var col in cols)
            {
                object val = SQLiteDatabase.FromDbNull(col.Prop.PropertyType, reader[col.Alias]);

                col.Prop.SetValue(this, val);
            }
        }

        public void AddAllParametersForPreparedStatementToDict(Dictionary<string, object> dict)
        {
            var columns = SQLiteHelper.GetColumnsFromType(GetType())
                .Where(c => !c.IsPK);

            foreach (var col in columns)
            {
                dict.Add($"@{col.Alias}", SQLiteDatabase.ToDbNull(col.Prop.GetValue(this)));
            }
        }
    }
}
