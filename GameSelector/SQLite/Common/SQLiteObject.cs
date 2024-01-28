using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSelector.SQLite.Common
{
    internal class SQLiteObject
    {
        public string TableName { get; set; }

        public SQLiteObject(string tableName)
        {
            TableName = tableName;
        }

        protected void FillPropsFromSqlReader(SQLiteDataReader reader)
        {
            var cols = SQLiteHelper.GetColumnsFromType(GetType(), TableName);

            foreach (var col in cols)
            {
                object val = SQLiteDatabase.FromDbNull(col.Prop.PropertyType, reader[col.Alias]);

                col.Prop.SetValue(this, val);
            }
        }
    }
}
