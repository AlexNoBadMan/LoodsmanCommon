using Ascon.Plm.Loodsman.PluginSDK;
using System.Windows;

namespace LoodsmanCommon
{
    public abstract class LoodsmanWPFNetPluginBase : LoodsmanNetPluginBase, ILoodsmanNetPlugin
    {
        public override void PluginLoad()
        {
            base.PluginLoad();

            if (Application.Current is null)
                new Application() { ShutdownMode = ShutdownMode.OnExplicitShutdown };
        }
    }
}
