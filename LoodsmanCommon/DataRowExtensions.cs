using System.Data;

namespace LoodsmanCommon
{
    public static class DataRowExtensions
    {

        public static T GetValueOrDefault<T>(this DataRow row, string key)
        {
            return row.GetValueOrDefault(key, default(T));
        }

        public static T GetValueOrDefault<T>(this DataRow row, string key, T defaultValue)
        {
            return row.IsNull(key) ? defaultValue : (T)row[key];
        }
    }
}
