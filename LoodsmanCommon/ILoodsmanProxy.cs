using Ascon.Plm.Loodsman.PluginSDK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Threading;
using System.Data;
using LoodsmanCommon.Entities;
using System.Xml.Linq;

namespace LoodsmanCommon
{
    public interface ILoodsmanProxy
    {
        [Obsolete("Вместо свойства следует использовать имеющиеся методы/свойства, после того как будет закончена работа над ILoodsmanProxy данное свойство будет удалено.")]
        INetPluginCall INetPC { get; set; }
        ILoodsmanMeta Meta { get; }
        ILoodsmanObject SelectedObject { get; }
        IEnumerable<ILoodsmanObject> SelectedObjects { get; }

        /// <summary>
        /// Название подключенного чекаута.
        /// </summary>
        string CheckOutName { get; }
        bool IsAdmin { get; }
        string CurrentUser { get; }
        string UserFileDir { get; }
        int NewObject(ILoodsmanObject loodsmanObject, int isProject = 0);
        int NewObject(string typeName, string product, int isProject = 0, string stateName = null);
        int InsertObject(ILoodsmanObject parent, ILoodsmanObject child, string linkType, string stateName = null, bool reuse = false);
        int InsertObject(string parentTypeName, string parentProduct, string parentVersion, string linkType, string childTypeName, string childProduct, string childVersion = " ", string stateName = null, bool reuse = false);
        int NewLink(ILoodsmanObject parent, ILoodsmanObject child, string linkType, double minQuantity = 0, double maxQuantity = 0, string idUnit = null);
        int NewLink(int parentId, int childId, string linkType, double minQuantity = 0, double maxQuantity = 0, string idUnit = null);
        int NewLink(string parentTypeName, string parentProduct, string parentVersion, string childTypeName, string childProduct, string childVersion, string linkType, double minQuantity = 0, double maxQuantity = 0, string idUnit = null);
        void UpLink(int idLink, double minQuantity = 0, double maxQuantity = 0, string idUnit = null);
        void RemoveLink(int idLink);
        List<ILoodsmanObject> GetLinkedFast(int id, string linkType, bool inverse = false);
        void FillInfoFromLink(int idLink, string parentProduct, string childProduct, out int parentId, out string parentVersion, out int childId, out string childVersion);
        void UpAttrValueById(int id, string attributeName, string attributeValue, object unit = null);
        string RegistrationOfFile(int idDocumet, string filePath, string fileName);
        void SaveSecondaryView(int docId, string pathToPdf);
        bool CheckUniqueName(string typeName, string product);
        DataTable GetReport(string reportName, IEnumerable<int> objectsIds, string reportParams = null);
        List<ILoodsmanObject> GetPropObjects(IEnumerable<int> objectsIds);

        /// <summary>
        /// Проверка на существование бизнес объекта в базе Лоцман.
        /// </summary>
        /// <remarks>
        /// Примечание:
        /// Если объект не существует то свойство возвращаемого объекта ILoodsmanObject.Id будет равен 0.
        /// <para>** Для создания бизнес объекта необходимо чтобы product был в формате ***BOSimple</para>
        /// </remarks>
        ILoodsmanObject PreviewBoObject(string typeName, string uniqueId);

        /// <summary>
        /// Возвращает список идентификаторов версий объектов, заблокированных в текущем чекауте.
        /// </summary>
        List<int> GetLockedObjectsIds();

        /// <summary>
        /// Помечает объект, находящийся на изменении, как подлежащий удалению при возврате в базу данных.
        /// </summary>
        /// <param name="id">Идентификатор версии объекта</param>
        void KillVersion(int id);

        /// <summary>
        /// Помечает объекты, находящиеся на изменении, как подлежащий удалению при возврате в базу данных.
        /// </summary>
        /// <param name="objectsIds">Список идентификаторов версий объектов</param>
        void KillVersion(IEnumerable<int> objectsIds);

        /// <summary>
        /// Помечает объект, находящийся на изменении, как подлежащий удалению при возврате в базу данных.
        /// </summary>
        /// <param name="typeName">Название типа</param>
        /// <param name="product">Ключевой атрибут</param>
        /// <param name="version">Версия объекта</param>
        void KillVersion(string typeName, string product, string version);

        /// <summary>
        /// Берет объект на редактирование.
        /// </summary>
        /// <param name="typeName">Название типа</param>
        /// <param name="product">Ключевой атрибут</param>
        /// <param name="version">Версия объекта</param>
        /// <param name="mode">Режим</param>
        /// <returns>Возвращает внутреннее название редактируемого объекта (название чекаута).</returns>
        /// <remarks>
        /// Примечание:
        /// При пустых значениях typeName, product и version, будет создан пустой чекаут. С ним можно работать, как с обычным. 
        /// Отличие в том, что в списке, возвращаемым методом GetInfoAboutCurrentBase (режим 2) в качестве значений полей[_ID_VERSION], [_TYPE], [_PRODUCT], [_VERSION] будут пустые значения.
        /// <para>** Для дальнейшей работы с объектом необходимо подключиться к чекауту методом ConnectToCheckOut.</para>
        /// </remarks>
        string CheckOut(string typeName = null, string product = null, string version = null, CheckOutMode mode = CheckOutMode.Default);
        
        /// <summary>
        /// Берет в работу текущий SelectedObject, если он не был в работе (Нет необходимости использовать метод ConnectToCheckOut).
        /// </summary>
        /// <returns>Возвращает внутреннее название редактируемого объекта (название чекаута).</returns>
        string SelectedObjectCheckOut(CheckOutMode mode = CheckOutMode.Default);

        /// <summary>
        /// Подключается к указанному чекауту.
        /// </summary>
        /// <param name="checkOutName">Название чекаута</param>
        /// <param name="dBName">Название базы данных</param>
        /// <remarks>
        /// Примечание:
        /// При пустых значении: checkOutName - метод не отработает, dBName - используется PluginCall.DBName. 
        /// </remarks>
        void ConnectToCheckOut(string checkOutName = null, string dBName = null);
        
        /// <summary>
        /// Делает объект доступным для изменения только в рамках текущего чекаута (блокирует его).
        /// </summary>
        /// <param name="objectId">Идентификатор версии</param>
        /// <param name="isRoot">Признак головного объекта</param>
        void AddToCheckOut(int objectId, bool isRoot = false);

        /// <summary>
        /// Возвращает измененный объект в базу данных.
        /// </summary>
        /// <param name="checkOutName">Название чекаута</param>
        /// <param name="dBName">Название базы данных</param>
        /// <remarks>
        /// Примечание:
        /// При пустых значении: checkOutName - используется PluginCall.CheckOut, dBName - используется PluginCall.DBName. 
        /// </remarks>
        void CheckIn(string checkOutName = null, string dBName = null);

        /// <summary>
        /// Возвращает измененный объект в базу данных.
        /// </summary>
        /// <param name="checkOutName">Название чекаута</param>
        /// <param name="dBName">Название базы данных</param>
        /// <remarks>
        /// Примечание:
        /// При пустых значении: checkOutName - используется PluginCall.CheckOut, dBName - используется PluginCall.DBName. 
        /// </remarks>
        void SaveChanges(string checkOutName = null, string dBName = null);

        /// <summary>
        /// Выполняет отказ от изменения объекта и вызывает метод DisconnectCheckOut
        /// </summary>
        /// <param name="checkOutName">Название чекаута</param>
        /// <param name="dBName">Название базы данных</param>
        /// <remarks>
        /// Примечание:
        /// При пустых значении: checkOutName - используется PluginCall.CheckOut, dBName - используется PluginCall.DBName. 
        /// </remarks>
        void CancelCheckOut(string checkOutName = null, string dBName = null);
    }

    internal class LoodsmanProxy : ILoodsmanProxy
    {
        public const string DEFAULT_INSERT_NEW_VERSION = " ";
        public const string DEFAULT_NEW_VERSION = "1.0";

        private string _checkOutName;
        private INetPluginCall _iNetPC;
        private readonly List<(string TypeName, string Product)> _uniqueNames = new List<(string typeName, string product)>();
        private readonly List<(string TypeName, string Product)> _notUniqueNames = new List<(string typeName, string product)>();
        private readonly ILoodsmanMeta _loodsmanMeta;
        private ILoodsmanObject _selectedObject;
        private List<ILoodsmanObject> _selectedObjects = new List<ILoodsmanObject>();

        public virtual INetPluginCall INetPC
        {
            get => _iNetPC;
            set
            {
                _iNetPC = value;
                _uniqueNames.Clear();
            }
        }
        public ILoodsmanMeta Meta => _loodsmanMeta;
        public ILoodsmanObject SelectedObject => _selectedObject?.Id == _iNetPC.PluginCall.IdVersion ? _selectedObject : _selectedObject = new LoodsmanObject(_iNetPC.PluginCall);
        public IEnumerable<ILoodsmanObject> SelectedObjects => GetSelectedObjects();
        public string CheckOutName => _checkOutName;
        public bool IsAdmin { get; }
        public string CurrentUser { get; }
        public string UserFileDir { get; }
        public LoodsmanProxy(INetPluginCall iNetPC, ILoodsmanMeta loodsmanMeta)
        {
            _iNetPC = iNetPC;
            IsAdmin = (int)iNetPC.RunMethod("IsAdmin") == 1;
            var userInfo = iNetPC.GetDataTable("GetInfoAboutCurrentUser").Rows[0];
            CurrentUser = userInfo["_FULLNAME"] as string;
            UserFileDir = userInfo["_FILEDIR"] as string;

            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";
            culture.DateTimeFormat.DateSeparator = ".";
            culture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            culture.DateTimeFormat.ShortTimePattern = "HH:mm";
            culture.DateTimeFormat.LongDatePattern = "HH:mm:ss";
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            /*
            var userInfo = (DataSet)pluginCall.GetDataSet("GetInfoAboutCurrentUser", new object[] { });
            //CurrentUser = userInfo["_FULLNAME"] as string;
            var userFileDir = userInfo.FieldValue["_FILEDIR"] as string;
            var reportName = "Ведомость покупных.fp3";
            var reports = pluginCall.GetDataSet("GetReportsAndFolders", new object[] { -1 });//"rep_VEDOMOST_SPECIFIKACIY"




            var report = pluginCall.GetDataSet("GetReport", new object[] { "rep_VEDOMOST_SPECIFIKACIY", pluginCall.IdVersion, null });//"rep_VEDOMOST_MATERIALOV"
            var guid = typeof(ILoodsmanApplication).GUID;
            var hr = Marshal.QueryInterface(pc, ref guid, out var pI); 
            var application = (ILoodsmanApplication)Marshal.GetTypedObjectForIUnknown(pI, typeof(ILoodsmanApplication));
            var fRDesigner = new FRDesigner();
            fRDesigner.ReportParams = null;// "Изделие (заказ №)=123;Программа=2";
            fRDesigner.ParentHWND = application.AppHandle;
            fRDesigner.Connection = application.DataBase.Connection;
            fRDesigner.Context = pluginCall.Selected;
            fRDesigner.FileName = @"C:\Program Files (x86)\Common Files\ASCON Shared\COD\ReportTemplates\Конструкторские\Ведомость спецификаций ГОСТ 2.106-96.fr3";// "C:\Program Files (x86)\Common Files\ASCON Shared\COD\ReportTemplates\ПМЗ\tem_VEDOMOST_MATERIALOV.fr3";
            fRDesigner.ExternalDataset = report;
            var thread = new Thread(new ThreadStart(() =>
            {
                while (Thread.CurrentThread.ThreadState == ThreadState.Running)
                    if (User32.FindWindowsRaw(string.Empty, "TfrmReportsShow").FirstOrDefault() is IntPtr wnd)
                        User32.SendMessage(wnd, (uint)CMD.WM_CLOSE, 0, 0);
            }));
            thread.Start();
            fRDesigner.Open(frMode.mOpenReportExtData);
            thread.Abort();
            File.Copy(fRDesigner.FileName, $"{userFileDir}\\{reportName}", true);
             */
            _loodsmanMeta = loodsmanMeta;
        }

        private IEnumerable<ILoodsmanObject> GetSelectedObjects()
        {
            var ids = _iNetPC.RunMethod("CGetTreeSelectedIDs").ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            if (!_selectedObjects.Select(x => x.Id).OrderBy(x => x).SequenceEqual(ids.OrderBy(x => x)))
                _selectedObjects = GetPropObjects(ids);
            return _selectedObjects;
        }

        #region NewObject
        public int NewObject(ILoodsmanObject loodsmanObject, int isProject = 0)
        {
            loodsmanObject.State = StateIfNullGetDefault(loodsmanObject.Type, loodsmanObject.State);
            loodsmanObject.Id = NewObject(loodsmanObject.Type, loodsmanObject.State, loodsmanObject.Product, isProject);
            loodsmanObject.Version = _loodsmanMeta.Types.First(x => x.Name == loodsmanObject.Type).Versioned ? DEFAULT_NEW_VERSION : string.Empty;
            return loodsmanObject.Id;
            //Метод NewObject отрабатывает даже если объект с такими Type и Product уже есть в базе, просто вернёт Id, присвоение Version в таком случае ошибочно
        }

        public int NewObject(string typeName, string product, int isProject = 0, string stateName = null)
        {
            return NewObject(typeName, StateIfNullGetDefault(typeName, stateName), product, isProject);
        }

        private int NewObject(string typeName, string stateName, string product, int isProject)
        {
            if (string.IsNullOrEmpty(stateName))
                throw new ArgumentException($"{nameof(stateName)} - состояние не может быть пустым", nameof(stateName));

            if (string.IsNullOrEmpty(product))
                throw new ArgumentException($"{nameof(product)} - ключевой атрибут не может быть пустым", nameof(product)); 

            return (int)_iNetPC.RunMethod("NewObject", typeName, stateName, product, isProject);
        }

        private string StateIfNullGetDefault(string typeName, string stateName = null)
        {
            if (string.IsNullOrEmpty(typeName))
                throw new ArgumentException($"{nameof(typeName)} - тип не может быть пустым", nameof(typeName));

            if (string.IsNullOrEmpty(stateName))
                stateName = _loodsmanMeta.Types.First(x => x.Name == typeName).DefaultState.Name;
            return stateName;
        }
        #endregion

        #region Link - Insert/New/Update/Remove
        public int InsertObject(ILoodsmanObject parent, ILoodsmanObject child, string linkType, string stateName = null, bool reuse = false)
        {
            CheckLoodsmanObjectsForError(parent, child);
            CheckInsertedObject(parent);
            CheckInsertedObject(child);
            return InsertObject(parent.Type, parent.Product, parent.Version, linkType, child.Type, child.Product, child.Version, stateName, reuse);
        }

        private void CheckInsertedObject(ILoodsmanObject loodsmanObject)
        {
            if (loodsmanObject.Id <= 0 && string.IsNullOrEmpty(loodsmanObject.Version))
                loodsmanObject.Version = DEFAULT_INSERT_NEW_VERSION;
        }

        public int InsertObject(string parentTypeName, string parentProduct, string parentVersion, string linkType, string childTypeName, string childProduct, string childVersion = DEFAULT_INSERT_NEW_VERSION, string stateName = null, bool reuse = false)
        {
            CheckKeyAttributesForErrors(parentTypeName, parentProduct, childTypeName, childProduct);
            var linkInfo = GetLinkInfo(parentTypeName, childTypeName, linkType);
            if (linkInfo.Direction == LinkDirection.Backward)
                Swap(ref parentTypeName, ref parentProduct, ref parentVersion, ref childTypeName, ref childProduct, ref childVersion);

            if (string.IsNullOrEmpty(stateName))
                stateName = StateIfNullGetDefault(parentVersion == DEFAULT_INSERT_NEW_VERSION ? parentTypeName : childTypeName);
            return (int)_iNetPC.RunMethod("InsertObject", parentTypeName, parentProduct, parentVersion, childTypeName, childProduct, childVersion, linkType, stateName, reuse);
        }

        public int NewLink(ILoodsmanObject parent, ILoodsmanObject child, string linkType, double minQuantity = 0, double maxQuantity = 0, string idUnit = null)
        {
            CheckLoodsmanObjectsForError(parent, child);
            if (parent.Id <= 0 && child.Id <= 0)
            {
                if (string.IsNullOrEmpty(parent.Product) && string.IsNullOrEmpty(parent.Type) && string.IsNullOrEmpty(child.Product) && string.IsNullOrEmpty(child.Type))
                    throw new InvalidOperationException("Не заданы ключевые атрибуты объектов для формирования связи");
            }
            else
            {
                if (parent.Id <= 0)
                    NewObject(parent);

                if (child.Id <= 0)
                    NewObject(child);
            }
            return NewLink(parent.Id, parent.Type, parent.Product, parent.Version, child.Id, child.Type, child.Product, child.Version, linkType, minQuantity, maxQuantity, idUnit);
        }


        public int NewLink(string parentTypeName, string parentProduct, string parentVersion, string childTypeName, string childProduct, string childVersion, string linkType, double minQuantity = 0, double maxQuantity = 0, string idUnit = null)
        {
            CheckKeyAttributesForErrors(parentTypeName, parentProduct, childTypeName, childProduct);
            return NewLink(0, parentTypeName, parentProduct, parentVersion, 0, childTypeName, childProduct, childVersion, linkType, minQuantity, maxQuantity, idUnit);
        }

        public int NewLink(int parentId, int childId, string linkType, double minQuantity = 0, double maxQuantity = 0, string idUnit = null)
        {
            if (parentId <= 0)
                throw new ArgumentException($"{nameof(parentId)} - отсутствует или неверно задан идентификатор объекта", nameof(parentId));

            if (childId <= 0)
                throw new ArgumentException($"{nameof(childId)} - отсутствует или неверно задан идентификатор объекта", nameof(childId));

            return NewLink(parentId, string.Empty, string.Empty, string.Empty, childId, string.Empty, string.Empty, string.Empty, linkType, minQuantity, maxQuantity, idUnit);
        }

        public void UpLink(int idLink, double minQuantity = 0, double maxQuantity = 0, string idUnit = null)
        {
            UpLink(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, idLink, minQuantity, maxQuantity, idUnit, false, string.Empty);
        }

        public void RemoveLink(int idLink)
        {
            UpLink(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, idLink, 0, 0, string.Empty, true, string.Empty);
        }

        private int NewLink(int parentId, string parentTypeName, string parentProduct, string parentVersion, int childId, string childTypeName, string childProduct, string childVersion, string linkType, double minQuantity, double maxQuantity, string idUnit)
        {
            //if (string.IsNullOrEmpty(linkType))
            //linkInfo = _loodsmanMeta.LinksInfoBetweenTypes.SingleOrDefault(x => (x.TypeName1 == parentTypeName && x.TypeName2 == childTypeName) || (x.TypeName1 == childTypeName && x.TypeName2 == parentTypeName));
            //Способ автоматически найти подходящий тип связи, но он будет не стабильным если пользователь произведёт изменение конфигурации бд
            if (string.IsNullOrEmpty(linkType))
                throw new ArgumentException($"{nameof(linkType)} не может быть пустым, не указан тип связи", nameof(linkType));

            var linkInfo = GetLinkInfo(parentTypeName, childTypeName, linkType);
            if (linkInfo.Direction == LinkDirection.Backward)
                Swap(ref parentId, ref parentTypeName, ref parentProduct, ref parentVersion, ref childId, ref childTypeName, ref childProduct, ref childVersion);

            if (linkInfo.IsQuantity && minQuantity <= 0 && maxQuantity <= 0)
            {
                minQuantity = 1;
                maxQuantity = 1;
            }
            return (int)_iNetPC.RunMethod("NewLink", parentId, parentTypeName, parentProduct, parentVersion, childId, childTypeName, childProduct, childVersion, minQuantity, maxQuantity, idUnit, linkType);
        }

        private int UpLink(string parentTypeName, string parentProduct, string parentVersion, string childTypeName, string childProduct, string childVersion, int idLink, double minQuantity, double maxQuantity, string idUnit, bool toDel, string linkType)
        {
            return (int)_iNetPC.RunMethod("UpLink", parentTypeName, parentProduct, parentVersion, childTypeName, childProduct, childVersion, idLink, minQuantity, maxQuantity, idUnit, toDel, linkType);
        }

        private static void CheckKeyAttributesForErrors(string parentTypeName, string parentProduct, string childTypeName, string childProduct)
        {
            if (string.IsNullOrEmpty(parentTypeName))
                throw new ArgumentException($"{nameof(parentTypeName)} не может быть пустым или иметь значение null", nameof(parentTypeName));

            if (string.IsNullOrEmpty(parentProduct))
                throw new ArgumentException($"{nameof(parentProduct)} не может быть пустым или иметь значение null", nameof(parentProduct));

            if (string.IsNullOrEmpty(childTypeName))
                throw new ArgumentException($"{nameof(childTypeName)} не может быть пустым или иметь значение null", nameof(childTypeName));

            if (string.IsNullOrEmpty(childProduct))
                throw new ArgumentException($"{nameof(childProduct)} не может быть пустым или иметь значение null", nameof(childProduct));
        }

        private static void CheckLoodsmanObjectsForError(ILoodsmanObject parent, ILoodsmanObject child)
        {
            if (parent is null)
                throw new ArgumentNullException($"{nameof(parent)} не задан родитель для создания связи");

            if (child is null)
                throw new ArgumentNullException($"{nameof(child)} не задан потомок для создания связи");
        }

        private LLinkInfoBetweenTypes GetLinkInfo(string parentTypeName, string childTypeName, string linkType)
        {
            var linkInfo = _loodsmanMeta.LinksInfoBetweenTypes.FirstOrDefault(x => x.TypeName1 == parentTypeName && x.TypeName2 == childTypeName && x.Name == linkType);
            if (linkInfo is null)
                throw new InvalidOperationException($"Не удалось найти информацию о связи типов {nameof(parentTypeName)} {parentTypeName} - {nameof(childTypeName)} {childTypeName}, по связи - {linkType}");
            return linkInfo;
        }

        private static void Swap(ref int parentId, ref string parentTypeName, ref string parentProduct, ref string parentVersion, ref int childId, ref string childTypeName, ref string childProduct, ref string childVersion)
        {
            var tId = parentId;
            parentId = childId;
            childId = tId;
            Swap(ref parentTypeName, ref parentProduct, ref parentVersion, ref childTypeName, ref childProduct, ref childVersion);
        }

        private static void Swap(ref string parentTypeName, ref string parentProduct, ref string parentVersion, ref string childTypeName, ref string childProduct, ref string childVersion)
        {
            var tTypeName = parentTypeName;
            var tProduct = parentProduct;
            var tVersion = parentVersion;
            parentTypeName = childTypeName;
            parentProduct = childProduct;
            parentVersion = childVersion;
            childTypeName = tTypeName;
            childProduct = tProduct;
            childVersion = tVersion;
        }

        public List<ILoodsmanObject> GetLinkedFast(int id, string linkType, bool inverse = false)
        {
            return new List<ILoodsmanObject>(_iNetPC.GetDataTable("GetLinkedFast", id, linkType, inverse).Select().Select(x => new LoodsmanObject(x)));
        }
        #endregion

        public void FillInfoFromLink(int idLink, string parentProduct, string childProduct, out int parentId, out string parentVersion, out int childId, out string childVersion)
        {
            var linkInfo = _iNetPC.GetDataTable("GetInfoAboutLink", idLink, 2).Select()
                                       .FirstOrDefault(x => x["_PARENT_PRODUCT"] as string == parentProduct && x["_CHILD_PRODUCT"] as string == childProduct);
            parentId = (int)linkInfo["_ID_PARENT"];
            parentVersion = linkInfo["_PARENT_VERSION"] as string;
            childId = (int)linkInfo["_ID_CHILD"];
            childVersion = linkInfo["_CHILD_VERSION"] as string;
        }

        public void UpAttrValueById(int id, string attributeName, string attributeValue, object unit = null) //Переделать attributeValue в object default()
        {
            _iNetPC.RunMethod("UpAttrValueById", id, attributeName, attributeValue, unit, string.IsNullOrEmpty(attributeValue));
        }

        public string RegistrationOfFile(int idDocumet, string filePath, string fileName)//, string workDirFilePath)
        {
            try
            {
                //var floderPath = string.Join("\\", Ancestors().OrderBy(x => x.Level).Cast<LoodsmanObjectVM>().Where(x => x.TypeName == _settingImport.FolderTypeName).Select(x => x.Product));
                //var workDirPath = $"{proxy.UserFileDir}\\{floderPath}";
                //if (!Directory.Exists(workDirPath))
                //    Directory.CreateDirectory(workDirPath);
                //var newPath = $"{workDirPath}\\{Product} - {Name} ({TypeName}){FileNode.Info.Extension}";
                //Лоцман не чистит за собой папки, поэтому пока без структуры папок и ложим всё в корень W:\
                var newPath = $"{UserFileDir}\\{fileName}";
                File.Copy(filePath, newPath, true);
                _iNetPC.RunMethod("RegistrationOfFile", string.Empty, string.Empty, string.Empty, idDocumet, fileName, string.Empty);
                return newPath;
            }
            catch// (Exception ex)
            {
                return string.Empty;
                //var test = ex;
                //logger?
            }
        }

        public void SaveSecondaryView(int docId, string pathToPdf)
        {
            _iNetPC.RunMethod("SaveSecondaryView", docId, pathToPdf);
        }

        public bool CheckUniqueName(string typeName, string product)
        {
            //Этот вариант оказался медленнее
            //    item.IsUniqueName = ((DataProvider.DataSet)_iNetPC.PluginCall.GetDataSet("CheckUniqueName", new object[] { item.TypeName, item.Product })).Eof;
            var isUnique = true;
            if (!_uniqueNames.Any(x => x.TypeName == typeName && x.Product.Equals(product, StringComparison.OrdinalIgnoreCase))) // белый список для объектов которых нет в Лоцман
            {
                if (_notUniqueNames.Any(x => x.TypeName == typeName && x.Product.Equals(product, StringComparison.OrdinalIgnoreCase)))
                {
                    isUnique = false;
                }
                else if (_iNetPC.GetDataTable("CheckUniqueName", typeName, product).Rows.Count != 0)
                {
                    _notUniqueNames.Add((typeName, product));
                    isUnique = false;
                }
                else
                {
                    _uniqueNames.Add((typeName, product));
                }
            }
            return isUnique;
        }

        public DataTable GetReport(string reportName, IEnumerable<int> objectsIds, string reportParams = null)
        {
            return _iNetPC.GetDataTable("GetReport", reportName, objectsIds, reportParams);
        }

        public List<ILoodsmanObject> GetPropObjects(IEnumerable<int> objectsIds)
        {
            return new List<ILoodsmanObject>(_iNetPC.GetDataTable("GetPropObjects", string.Join(",", objectsIds), 0).Select().Select(x => new LoodsmanObject(x)));
        }

        public ILoodsmanObject PreviewBoObject(string typeName, string uniqueId)
        {
            var xmlString = (string)_iNetPC.RunMethod("PreviewBoObject", typeName, uniqueId);
            var xDocument = XDocument.Parse(xmlString);
            var elements = xDocument.Descendants("PreviewBoObjectResult").Elements();
            var loodsmanObject = new LoodsmanObject();
            loodsmanObject.Id = int.TryParse(elements.FirstOrDefault(x => x.Name == "VersionId")?.Value, out var id) ? id : 0;
            loodsmanObject.Type = typeName; 
            loodsmanObject.Product = elements.FirstOrDefault(x => x.Name == "Product").Value;
            loodsmanObject.State = elements.FirstOrDefault(x => x.Name == "State")?.Value ?? StateIfNullGetDefault(typeName);
            loodsmanObject.Version = elements.FirstOrDefault(x => x.Name == "Version")?.Value;
            return loodsmanObject;
        }

        public List<int> GetLockedObjectsIds()
        {
            return _iNetPC.GetDataTable("GetLockedObjects", 0).Select().Select(x => (int)x[0]).ToList();
        }

        #region KillVersion
        public void KillVersion(int id)
        {
            _iNetPC.RunMethod("KillVersionById", id);
        }

        public void KillVersion(IEnumerable<int> objectsIds)
        {
            _iNetPC.RunMethod("KillVersions", string.Join(",", objectsIds), 0);
        }

        public void KillVersion(string typeName, string product, string version)
        {
            _iNetPC.RunMethod("KillVersion", typeName, product, version);
        }
        #endregion

        #region CheckOut
        public string CheckOut(string typeName, string product, string version, CheckOutMode mode = CheckOutMode.Default)
        {
            return (string)_iNetPC.RunMethod("CheckOut", typeName, product, version, (int)mode);
        }

        public string SelectedObjectCheckOut(CheckOutMode mode = CheckOutMode.Default)
        {
            var pc = _iNetPC.PluginCall;
            var wasCheckout = pc.CheckOut != 0;
            _checkOutName = wasCheckout ? pc.CheckOut.ToString() : CheckOut(pc.stType, pc.stProduct, pc.stVersion, mode);
            if (!wasCheckout)
                ConnectToCheckOut(_checkOutName, pc.DBName);

            return _checkOutName;
        }

        public void ConnectToCheckOut(string checkOutName = null, string dBName = null)
        {
            if (string.IsNullOrEmpty(checkOutName))
                return;

            _checkOutName = checkOutName;
            _iNetPC.RunMethod("ConnectToCheckOut", checkOutName, dBName ?? _iNetPC.PluginCall.DBName);
        }

        public void AddToCheckOut(int objectId, bool isRoot = false)
        {
            _iNetPC.RunMethod("AddToCheckOut", objectId, isRoot);
        }

        public void CheckIn(string checkOutName = null, string dBName = null)
        {
            var checkOut = checkOutName ?? _checkOutName;
            var databaseName = dBName ?? _iNetPC.PluginCall.DBName;
            _iNetPC.RunMethod("DisconnectCheckOut", checkOut, databaseName);
            _iNetPC.RunMethod("CheckIn", checkOut, databaseName);
            _checkOutName = string.Empty;
        }

        public void SaveChanges(string checkOutName = null, string dBName = null)
        {
            var checkOut = checkOutName ?? _checkOutName;
            var databaseName = dBName ?? _iNetPC.PluginCall.DBName;
            _iNetPC.RunMethod("SaveChanges", checkOut, databaseName);
        }

        public void CancelCheckOut(string checkOutName = null, string dBName = null)
        {
            var checkOut = checkOutName ?? _checkOutName;
            var databaseName = dBName ?? _iNetPC.PluginCall.DBName;
            _iNetPC.RunMethod("DisconnectCheckOut", checkOut, databaseName);
            _iNetPC.RunMethod("CancelCheckOut", checkOut, databaseName);
            _checkOutName = string.Empty;
        }

        #endregion
    }
}
 