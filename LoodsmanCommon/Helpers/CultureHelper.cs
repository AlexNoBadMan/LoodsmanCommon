using System.Globalization;
using System.Threading;

namespace LoodsmanCommon
{
  /// <summary> Помогает инициализировать стандартные настройки культуры Лоцман. </summary>
  public static class CultureHelper
  {
    /// <summary> Применяет стандартные настройки культуры Лоцман. </summary>
    public static void ApplyLoodsmanCulture()
    {
      var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
      culture.NumberFormat.NumberDecimalSeparator = ".";
      culture.DateTimeFormat.DateSeparator = ".";
      culture.DateTimeFormat.LongDatePattern = "dd.MM.yyyy";
      culture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
      culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
      culture.DateTimeFormat.ShortTimePattern = "HH:mm";
      culture.DateTimeFormat.FullDateTimePattern = "dd.MM.yyyy HH:mm:ss";
      Thread.CurrentThread.CurrentCulture = culture;
      Thread.CurrentThread.CurrentUICulture = culture;
    }
  }
}
