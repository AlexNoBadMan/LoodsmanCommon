using Ascon.Plm.Loodsman.PluginSDK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Threading;
using System.Data;

namespace LoodsmanCommon
{
    public interface ILoodsmanProxy
    {
        INetPluginCall INetPC { get; set; }
        ILoodsmanMeta LoodsmanMeta { get; }
        ILoodsmanObject SelectedObject { get; }
        bool IsAdmin { get; }
        string CurrentUser { get; }
        string UserFileDir { get; }
        void NewObject(ILoodsmanObject loodsmanObject, int isProject = 0);
        int NewObject(string typeName, string product, int isProject = 0, string stateName = null);
        int InsertObject(string parentTypeName, string parentProduct, string parentVersion, string relationName, string stateName, string childTypeName, string childProduct, string childVersion = " ", bool reuse = false);
        void FillInfoFromLink(int idLink, string parentProduct, string childProduct, out int parentId, out string parentVersion, out int childId, out string childVersion);
        void UpAttrValueById(int id, string attributeName, string attributeValue, object unit = null);
        string RegistrationOfFile(int idDocumet, string filePath, string fileName);
        void SaveSecondaryView(int docId, string pathToPdf);
        bool CheckUniqueName(string typeName, string product);
        DataTable GetReport(string reportName, IEnumerable<int> objectsIds, string reportParams = null);
        List<ILoodsmanObject> GetPropObjects(IEnumerable<int> objectsIds);
        List<int> GetLockedObjects();
        string CheckOut();
        void AddToCheckOut(int objectId, bool isRoot = false);
        void CheckIn();
        void SaveChanges();
        void CancelCheckOut();
    }

    internal class LoodsmanProxy : ILoodsmanProxy
    {
        public const string DEFAULT_NEW_VERSION = " ";

        private string _checkOutName;
        private INetPluginCall _iNetPC;
        private readonly List<(string TypeName, string Product)> _uniqueNames = new List<(string typeName, string product)>();
        private readonly List<(string TypeName, string Product)> _notUniqueNames = new List<(string typeName, string product)>();
        private readonly ILoodsmanMeta _loodsmanMeta;
        private ILoodsmanObject _selectedObject;

        public virtual INetPluginCall INetPC
        {
            get => _iNetPC;
            set
            {
                _iNetPC = value;
                _uniqueNames.Clear();
            }
        }
        public ILoodsmanMeta LoodsmanMeta => _loodsmanMeta;
        public ILoodsmanObject SelectedObject => _selectedObject?.Id == _iNetPC.PluginCall.IdVersion ? _selectedObject : _selectedObject = new LoodsmanObject(_iNetPC.PluginCall);
        public bool IsAdmin { get; }
        public string CurrentUser { get; }
        public string UserFileDir { get; }
        public LoodsmanProxy(INetPluginCall iNetPC, ILoodsmanMeta loodsmanMeta)
        {
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

            _loodsmanMeta = loodsmanMeta;
        }

        public void NewObject(ILoodsmanObject loodsmanObject, int isProject = 0)
        {
            if (string.IsNullOrEmpty(loodsmanObject.State))
                loodsmanObject.State = _loodsmanMeta.Types.First(x => x.Name == loodsmanObject.Type).DefaultState.Name;
            loodsmanObject.Id = NewObject(loodsmanObject.Type, loodsmanObject.Product, isProject, loodsmanObject.State);
        }

        public int NewObject(string typeName, string product, int isProject = 0, string stateName = null)
        {
            if (string.IsNullOrEmpty(stateName))
                stateName = _loodsmanMeta.Types.First(x => x.Name == typeName).DefaultState.Name;
            return (int)_iNetPC.RunMethod("NewObject", typeName, stateName, product, isProject);
        }

        public int InsertObject(string parentTypeName, string parentProduct, string parentVersion, string relationName, string stateName,
                                string childTypeName, string childProduct, string childVersion = DEFAULT_NEW_VERSION, bool reuse = false)
        {
            //Id = (int)proxy.INetPC.RunMethod("NewObject", TypeName, LoodsmanType.DefaultState.Name, Product, 0);
            //proxy.INetPC.RunMethod("UpLink", parent.TypeName, parent.Product, parent.Version, 
            //                                        TypeName, Product, Version, 0, 0, 0, string.Empty, false, relationName);
            return (int)_iNetPC.RunMethod("InsertObject", parentTypeName, parentProduct, parentVersion, childTypeName, childProduct, childVersion, relationName, stateName, reuse);
        }

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
            var objects = new List<ILoodsmanObject>();
            var dtProps = _iNetPC.GetDataTable("GetPropObjects", string.Join(",", objectsIds), 0);
            objects.AddRange(dtProps.Select().Select(x => new LoodsmanObject(x)));
            return objects;
        }

        public List<int> GetLockedObjects()
        {
            return _iNetPC.GetDataTable("GetLockedObjects", 0).Select().Select(x => (int)x[0]).ToList();
        }

        #region CheckOut
        public string CheckOut()
        {
            var wasCheckout = _iNetPC.PluginCall.CheckOut != 0;
            _checkOutName = wasCheckout
                ? _iNetPC.PluginCall.CheckOut.ToString()
                : (string)_iNetPC.RunMethod("CheckOut", _iNetPC.PluginCall.stType, _iNetPC.PluginCall.stProduct, _iNetPC.PluginCall.stVersion, 0);
            if (!wasCheckout)
                _iNetPC.RunMethod("ConnectToCheckOut", _checkOutName, _iNetPC.PluginCall.DBName);
            return _checkOutName;
        }

        public void AddToCheckOut(int objectId, bool isRoot = false)
        {
            if (!(_iNetPC.PluginCall.CheckOut != 0))
                CheckOut();
            _iNetPC.RunMethod("AddToCheckOut", objectId, isRoot);
        }

        public void CheckIn()
        {
            _iNetPC.RunMethod("DisconnectCheckOut", _checkOutName, _iNetPC.PluginCall.DBName);
            _iNetPC.RunMethod("CheckIn", _checkOutName, _iNetPC.PluginCall.DBName);
        }

        public void SaveChanges()
        {
            _iNetPC.RunMethod("SaveChanges", _checkOutName, _iNetPC.PluginCall.DBName);
        }

        public void CancelCheckOut()
        {
            if (!(_iNetPC.PluginCall.CheckOut != 0))
                return;

            _iNetPC.RunMethod("DisconnectCheckOut", _checkOutName, _iNetPC.PluginCall.DBName);
            _iNetPC.RunMethod("CancelCheckOut", _checkOutName, _iNetPC.PluginCall.DBName);
        }

        #endregion
    }
}
 