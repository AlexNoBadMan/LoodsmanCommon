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
        bool IsAdmin { get; }
        string CurrentUser { get; }
        string UserFileDir { get; }
        int InsertObject(string parentTypeName, string parentDesignation, string parentVersion, string relationName, string stateName, string childTypeName, string childDesignation, string childVersion = " ", bool reuse = false);
        void FillInfoFromLink(int idLink, string parentDesignation, string childDesignation, out int parentId, out string parentVersion, out int childId, out string childVersion);
        void UpAttrValueById(int id, string attributeName, string attributeValue, object unit = null);
        string RegistrationOfFile(int idDocumet, string filePath, string fileName);
        void SaveSecondaryView(int docId, string pathToPdf);
        bool CheckUniqueName(string typeName, string designation);
        DataTable GetReport(string reportName, int[] objectsIds, string reportParams = null);
        DataTable GetReport(string reportName, int objectId, string reportParams = null);
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
        private readonly List<(string TypeName, string Designation)> _uniqueNames = new List<(string typeName, string designation)>();
        private readonly List<(string TypeName, string Designation)> _notUniqueNames = new List<(string typeName, string designation)>();
        public virtual INetPluginCall INetPC 
        { 
            get => _iNetPC; 
            set 
            { 
                _iNetPC = value;
                _uniqueNames.Clear();
            } 
        }
        public bool IsAdmin { get; }
        public string CurrentUser { get; }
        public string UserFileDir { get; }
        public ILoodsmanMeta LoodsmanMeta { get; }

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

            LoodsmanMeta = loodsmanMeta;
        }

        public int InsertObject(string parentTypeName, string parentDesignation, string parentVersion, string relationName, string stateName, 
                                string  childTypeName, string  childDesignation, string  childVersion = DEFAULT_NEW_VERSION, bool reuse = false)
        {
            //Id = (int)proxy.INetPC.RunMethod("NewObject", TypeName, LoodsmanType.DefaultState.Name, Designation, 0);
            //proxy.INetPC.RunMethod("UpLink", parent.TypeName, parent.Designation, parent.Version, 
            //                                        TypeName, Designation, Version, 0, 0, 0, string.Empty, false, relationName);
            return (int)_iNetPC.RunMethod("InsertObject", parentTypeName, parentDesignation, parentVersion,  childTypeName, childDesignation, childVersion, relationName, stateName, reuse);
        }

        public void FillInfoFromLink(int idLink, string parentDesignation, string childDesignation, out int parentId, out string parentVersion, out int childId, out string childVersion)
        {
            var linkInfo = _iNetPC.GetDataTable("GetInfoAboutLink", idLink, 2).Select()
                                       .FirstOrDefault(x => x["_PARENT_PRODUCT"] as string == parentDesignation && x["_CHILD_PRODUCT"] as string == childDesignation);
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
                //var floderPath = string.Join("\\", Ancestors().OrderBy(x => x.Level).Cast<LoodsmanObjectVM>().Where(x => x.TypeName == _settingImport.FolderTypeName).Select(x => x.Designation));
                //var workDirPath = $"{proxy.UserFileDir}\\{floderPath}";
                //if (!Directory.Exists(workDirPath))
                //    Directory.CreateDirectory(workDirPath);
                //var newPath = $"{workDirPath}\\{Designation} - {Name} ({TypeName}){FileNode.Info.Extension}";
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

        public bool CheckUniqueName(string typeName, string designation)
        {
            //Этот вариант оказался медленнее
            //    item.IsUniqueName = ((DataProvider.DataSet)_iNetPC.PluginCall.GetDataSet("CheckUniqueName", new object[] { item.TypeName, item.Designation })).Eof;
            var isUnique = true;
            if (!_uniqueNames.Any(x => x.TypeName == typeName && x.Designation.Equals(designation, StringComparison.OrdinalIgnoreCase))) // белый список для объектов которых нет в Лоцман
            {
                if (_notUniqueNames.Any(x => x.TypeName == typeName && x.Designation.Equals(designation, StringComparison.OrdinalIgnoreCase)))
                {
                    isUnique = false;
                }
                else if (_iNetPC.GetDataTable("CheckUniqueName", typeName, designation).Rows.Count != 0)
                {
                    _notUniqueNames.Add((typeName, designation));
                    isUnique = false;
                }
                else
                {
                    _uniqueNames.Add((typeName, designation));
                }
            }
            return isUnique;
        }

        public DataTable GetReport(string reportName, int[] objectsIds, string reportParams = null)
        {
            return _iNetPC.GetDataTable("GetReport", reportName, objectsIds, reportParams);
        }
        
        public DataTable GetReport(string reportName, int objectId, string reportParams = null)
        {
            return _iNetPC.GetDataTable("GetReport", reportName, objectId, reportParams);
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
 