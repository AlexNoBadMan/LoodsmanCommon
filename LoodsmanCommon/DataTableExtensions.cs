using System.Collections.Generic;
using System.Data;

namespace LoodsmanCommon
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
