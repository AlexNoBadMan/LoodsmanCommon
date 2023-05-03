using Ascon.Plm.Loodsman.PluginSDK;

namespace LoodsmanCommon
{
  public static class MetaBuilder
  {
    private static ILoodsmanMeta _loodsmanMeta;
    public static ILoodsmanMeta GetInstance(INetPluginCall iNetPC)
    {
      return _loodsmanMeta ??= new LoodsmanMeta(iNetPC);
    }
  }
}
