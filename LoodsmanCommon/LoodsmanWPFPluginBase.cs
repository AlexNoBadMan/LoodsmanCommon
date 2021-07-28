using Ascon.Plm.Loodsman.PluginSDK;
using Loodsman;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

namespace UHM.PluginHelper
{
    public abstract class LoodsmanWPFPluginBase : ILoodsmanNetPlugin
    {
        protected ILoodsmanApplication LoodsmanApplication;
        protected IntPtr AppHandle;
        //private ILoodsmanProxy _loodsmanProxy;
        //private ILoodsmanMeta _loodsmanMeta;
        protected Application WPFApp;
        //private IContext _context;
        protected string SharedLibrariesPath;

        public abstract void BindMenu(IMenuDefinition menu);

        public abstract void OnCloseDb();

        public abstract void OnConnectToDb(INetPluginCall iNetPC);

        public virtual void PluginLoad()
        {
            WPFApp = Application.Current ?? new Application() { ShutdownMode = ShutdownMode.OnExplicitShutdown };
            var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var parentDirectory = new DirectoryInfo(assemblyDirectory).Parent.FullName;
            SharedLibrariesPath = $"{parentDirectory}\\Shared libraries";
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        public virtual void PluginUnload()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
        }

        protected virtual void PluginInit(INetPluginCall iNetPC)
        {
            if (iNetPC != null)
            {
                LoodsmanApplication = GetLoodsmanApplication(iNetPC);
                AppHandle = new IntPtr(LoodsmanApplication.AppHandle);
                //_loodsmanMeta = new LoodsmanMeta(iNetPC);
                //_loodsmanProxy = new LoodsmanProxy(iNetPC, _loodsmanMeta);
            }
        }

        protected virtual bool CheckCommand(INetPluginCall iNetPC)
        {
            return iNetPC != null &&
                   iNetPC.PluginCall.IdVersion != 0 &&
                 ((iNetPC.PluginCall.Selected.LockLevel == PDMObjects.PDMLockLevels.NoLock) || (iNetPC.PluginCall.Selected.LockLevel == PDMObjects.PDMLockLevels.SelfLock && iNetPC.PluginCall.CheckOut != 0));
        }

        protected static ILoodsmanApplication GetLoodsmanApplication(INetPluginCall iNetPC)
        {
            var pUnk = Marshal.GetIUnknownForObject(iNetPC.PluginCall);
            var guid = typeof(ILoodsmanApplication).GUID;
            var hr = Marshal.QueryInterface(pUnk, ref guid, out var pI);
            return (ILoodsmanApplication)Marshal.GetTypedObjectForIUnknown(pI, typeof(ILoodsmanApplication));
        }

        /// <summary>
        /// Обработчик события загрузки необходимых сборок.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                var name = args.Name.Split(',')[0];
                var exts = new string[] { ".dll", ".exe" };
                foreach (var ext in exts)
                {
                    var fullname = Path.Combine(SharedLibrariesPath, $"{name}{ext}");
                    if (File.Exists(fullname))
                        return Assembly.LoadFile(fullname);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
            }
            return null;
        }
    }
}
