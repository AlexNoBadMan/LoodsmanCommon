using Ascon.Plm.Loodsman.PluginSDK;
using Loodsman;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

namespace LoodsmanCommon
{
    public abstract class LoodsmanPluginBase
    {
        protected IntPtr _appHandle;
        protected ILoodsmanApplication _application;
        protected ILoodsmanProxy _proxy;
        protected ILoodsmanMeta _meta;

        protected static ILoodsmanApplication GetLoodsmanApplication(INetPluginCall iNetPC)
        {
            var pUnk = Marshal.GetIUnknownForObject(iNetPC.PluginCall);
            var guid = typeof(ILoodsmanApplication).GUID;
            var hr = Marshal.QueryInterface(pUnk, ref guid, out var pI);
            return (ILoodsmanApplication)Marshal.GetTypedObjectForIUnknown(pI, typeof(ILoodsmanApplication));
        }
        
        public abstract void BindMenu(IMenuDefinition menu);

        public virtual void OnCloseDb() { }

        public virtual void OnConnectToDb(INetPluginCall iNetPC)
        {
            if (_proxy is null && iNetPC != null)
                PluginInit(iNetPC);
        }

        public virtual void PluginLoad()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        public virtual void PluginUnload()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
        }

        protected virtual bool CheckCommand(INetPluginCall iNetPC)
        {
            return iNetPC != null &&
                   iNetPC.PluginCall.IdVersion != 0 &&
                   iNetPC.PluginCall.Selected.LockLevel == PDMObjects.PDMLockLevels.NoLock;
            // || (iNetPC.PluginCall.Selected.LockLevel == PDMObjects.PDMLockLevels.SelfLock && iNetPC.PluginCall.CheckOut != 0));
        }

        /// <summary>
        /// Инициализация свойств плагина, рекомендуется переопределить метод с вызовом base.
        /// </summary>
        /// <param name="iNetPC">Интерфейс взаимодействия с плагином</param>
        protected virtual void PluginInit(INetPluginCall iNetPC)
        {
            _application = GetLoodsmanApplication(iNetPC);
            _appHandle = new IntPtr(_application.AppHandle);
            _meta = GetLoodsmanMeta(iNetPC);
            _proxy = GetLoodsmanProxy(iNetPC);
        }

        /// <summary>
        /// Возвращает прокси объект, имеет смысл переопределять в случае собственной реализации интерфейса.
        /// </summary>
        /// <param name="iNetPC">Интерфейс взаимодействия с плагином</param>
        protected virtual ILoodsmanProxy GetLoodsmanProxy(INetPluginCall iNetPC)
        {
            return _proxy ?? new LoodsmanProxy(iNetPC, _meta);
        }

        /// <summary>
        /// Возвращает объект меты, имеет смысл переопределять в случае собственной реализации интерфейса.
        /// </summary>
        /// <param name="iNetPC">Интерфейс взаимодействия с плагином</param>
        protected virtual ILoodsmanMeta GetLoodsmanMeta(INetPluginCall iNetPC)
        {
            return _meta ?? new LoodsmanMeta(iNetPC);
        }

        /// <summary>
        /// Обработчик события загрузки необходимых сборок.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                var name = args.Name.Split(',')[0];
                var exts = new string[] { ".dll", ".exe" };
                var folders = Directory.GetDirectories(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                foreach (var path in folders)
                {
                    foreach (var ext in exts)
                    {
                        var fullname = Path.Combine(path, $"{name}{ext}");
                        if (File.Exists(fullname))
                            return Assembly.LoadFile(fullname);
                    }
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