using System.Globalization;
using System.Threading;

namespace LoodsmanCommon
{
  public static class CultureHelper
  {
    public static void ApplyLoodsmanCulture()
    {
      var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
      culture.NumberFormat.NumberDecimalSeparator = ".";
      culture.DateTimeFormat.DateSeparator = ".";
      culture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
      culture.DateTimeFormat.ShortTimePattern = "HH:mm";
      culture.DateTimeFormat.LongDatePattern = "HH:mm:ss";
      Thread.CurrentThread.CurrentCulture = culture;
      Thread.CurrentThread.CurrentUICulture = culture;
    }
  }
}
