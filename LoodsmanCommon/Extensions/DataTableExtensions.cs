using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoodsmanCommon.Extensions
{
    public static class DataTableExtensions
    {
        public static IEnumerable<DataRow> GetRows(this DataTable dataTable)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
                yield return dataTable.Rows[i];
        }
    }
}
