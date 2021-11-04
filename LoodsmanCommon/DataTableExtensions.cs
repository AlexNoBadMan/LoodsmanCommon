using System;
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

        public static IEnumerable<T> Select<T>(this DataTable dataTable, Func<DataRow, T> selector)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
                yield return selector(dataTable.Rows[i]);
        }
        public static IEnumerable<DataRow> Where(this DataTable dataTable, Func<DataRow, bool> predicate)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
                if (predicate(dataTable.Rows[i]))
                    yield return dataTable.Rows[i];
        }
    }
}
