using Ascon.Plm.Loodsman.PluginSDK;
using Loodsman;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

namespace LoodsmanCommon
{
    public abstract class LoodsmanPluginBase
    {
        protected ILoodsmanApplication _loodsmanApplication;
        protected IntPtr _appHandle;
        protected ILoodsmanProxy _loodsmanProxy;
        protected ILoodsmanMeta _loodsmanMeta;
        protected string SharedLibrariesPath { get; set; }

        protected static ILoodsmanApplication GetLoodsmanApplication(INetPluginCall iNetPC)
        {
            var pUnk = Marshal.GetIUnknownForObject(iNetPC.PluginCall);
            var guid = typeof(ILoodsmanApplication).GUID;
            var hr = Marshal.QueryInterface(pUnk, ref guid, out var pI);
            return (ILoodsmanApplication)Marshal.GetTypedObjectForIUnknown(pI, typeof(ILoodsmanApplication));
        }

        public abstract void BindMenu(IMenuDefinition menu);

        public abstract void OnCloseDb();

        public abstract void OnConnectToDb(INetPluginCall iNetPC);

        public virtual void PluginLoad()
        {
            if (!string.IsNullOrEmpty(SharedLibrariesPath))
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        public virtual void PluginUnload()
        {
            if (!string.IsNullOrEmpty(SharedLibrariesPath))
                AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
        }

        protected virtual bool CheckCommand(INetPluginCall iNetPC)
        {
            return iNetPC != null &&
                   iNetPC.PluginCall.IdVersion != 0 &&
                 ((iNetPC.PluginCall.Selected.LockLevel == PDMObjects.PDMLockLevels.NoLock) || (iNetPC.PluginCall.Selected.LockLevel == PDMObjects.PDMLockLevels.SelfLock && iNetPC.PluginCall.CheckOut != 0));
        }

        protected virtual void PluginInit(INetPluginCall iNetPC)
        {
            if (_loodsmanProxy is null && iNetPC != null)
            {
                _loodsmanApplication = GetLoodsmanApplication(iNetPC);
                _appHandle = new IntPtr(_loodsmanApplication.AppHandle);
                _loodsmanMeta = GetLoodsmanMeta(iNetPC);
                _loodsmanProxy = GetLoodsmanProxy(iNetPC);
            }
        }

        protected virtual ILoodsmanProxy GetLoodsmanProxy(INetPluginCall iNetPC)
        {
            return new LoodsmanProxy(iNetPC, _loodsmanMeta);
        }

        protected virtual ILoodsmanMeta GetLoodsmanMeta(INetPluginCall iNetPC)
        {
            return new LoodsmanMeta(iNetPC);
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