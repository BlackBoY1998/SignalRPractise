using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TestAdminLTE.MyClass
{
    public class DataTable2JSON
    {
        public static string DataTableToJSON(DataTable _dt)
        {
            string data = "[";
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                string row = "{";
                for (int j = 0; j < _dt.Columns.Count; j++)
                {
                    row += $"\"{_dt.Columns[j].ColumnName.ToString()}\":\"{_dt.Rows[i][j].ToString()}\",";
                }
                row = row.Substring(0, row.Length - 1);
                row += "},";
                data += row;
            }
            data = data.Substring(0, data.Length - 1);
            data += "]";
            return data;
        }
    }
}
