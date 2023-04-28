using Ascon.Plm.DataPacket;
using Ascon.Plm.Loodsman.PluginSDK;
using Loodsman;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoodsmanCommon.Extensions
{
  public static class PluginCallExtensions
  {
    /// <summary>  </summary>
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
        using (DataPacketReader reader = new DataPacketReader(data))
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
