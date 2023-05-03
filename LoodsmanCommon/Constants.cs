namespace LoodsmanCommon
{
  /// <summary>
  /// Константы используемые в методах Лоцмана.
  /// </summary>
  public static class Constants
  {
    /// <summary>
    /// Строка используемая для вставки еще не существующего объекта, например метод <see cref="NetPluginCallExtensions.Native_InsertObject(Ascon.Plm.Loodsman.PluginSDK.INetPluginCall, string, string, string, string, string, string, string, string, bool)">InsertObject</see>.
    /// </summary>
    public const string DEFAULT_INSERT_NEW_VERSION = " ";

    /// <summary>
    /// Наименование версии по умолчанию, для нового "версионного" объекта.
    /// </summary>
    public const string DEFAULT_NEW_VERSION = "1.0";

    /// <summary>
    /// Наименование версии по умолчанию, для нового "не версионного" объекта.
    /// </summary>
    public const string DEFAULT_NEW_NO_VERSION = "";

    /// <summary>
    /// Разделитель наименований связей '#' используется в методах. 
    /// </summary>
    public const string LINK_SEPARATOR = "\u0001";

    /// <summary>
    /// Разделитель идентификаторов используемый в методах.
    /// </summary>
    public const string ID_SEPARATOR = ",";
  }
}
