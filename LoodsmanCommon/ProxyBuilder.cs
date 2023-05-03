using Ascon.Plm.Loodsman.PluginSDK;

namespace LoodsmanCommon
{
  public static class ProxyBuilder
  {
    private static ILoodsmanProxy _loodsmanProxy;
    public static ILoodsmanProxy GetInstance(INetPluginCall iNetPC)
    {
      return _loodsmanProxy ??= new LoodsmanProxy(iNetPC, MetaBuilder.GetInstance(iNetPC));
    }
  }
}
