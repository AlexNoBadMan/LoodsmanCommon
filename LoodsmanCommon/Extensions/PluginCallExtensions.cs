using Ascon.Plm.DataPacket;
using Ascon.Plm.Loodsman.PluginSDK;
using Loodsman;
using System.Data;

namespace LoodsmanCommon.Extensions
{
  /// <summary> Расширения для интерфейса <see cref="IPluginCall"/>. /summary>
  public static class PluginCallExtensions
  {
    /// <summary> Возвращает интерфейс предназначенный для .NET плагинов ЛОЦМАН:PLM. </summary>
    /// <returns> Возвращает новый экземпляр объекта <see cref="INetPluginCall"/>. </returns>
    public static INetPluginCall GetNetPluginCall(this IPluginCall pc) => new NetPuginCall(pc);

    private class NetPuginCall : INetPluginCall
    {
      public NetPuginCall(IPluginCall pluginCall)
      {
        PluginCall = pluginCall;
      }
      
      public IPluginCall PluginCall { get; }

      public DataTable GetDataTable(string methodName, params object[] arguments)
      {
        var data = (byte[])RunMethod(methodName, arguments);
        var dataTable = new DataTable();
        using (var reader = new DataPacketReader(data))
        {
          dataTable.Load(reader);
        }
        return dataTable;
      }

      public object RunMethod(string methodName, params object[] arguments)
      {
        return PluginCall.RunMethod(methodName, arguments);
      }
    }
  }
}
