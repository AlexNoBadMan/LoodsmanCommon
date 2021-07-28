using Ascon.Plm.Loodsman.PluginSDK;
using System.Windows;

namespace LoodsmanCommon
{
    public abstract class LoodsmanWPFPluginBase : LoodsmanPluginBase, ILoodsmanNetPlugin
    {
        protected Application _WPFApp;
        protected IContext _context;

        public override void PluginLoad()
        {
            base.PluginLoad();
            _WPFApp = Application.Current ?? new Application() { ShutdownMode = ShutdownMode.OnExplicitShutdown };
            _context = new Context(_WPFApp.Dispatcher);
        }
    }
}
