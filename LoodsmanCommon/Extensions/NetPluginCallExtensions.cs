using Ascon.Plm.Loodsman.PluginSDK;
using System.Collections.Generic;
using System.Data;

namespace LoodsmanCommon.Extensions
{
    public static class NetPluginCallExtensions
    {
        public static string Native_CGetTreeSelectedIDs(this INetPluginCall pc)
        {
            return pc.RunMethod("CGetTreeSelectedIDs") as string ?? string.Empty;
        }

        public static bool Native_IsAdmin(this INetPluginCall pc)
        {
            return pc.RunMethod("IsAdmin") as int? == 1;
        }

        public static DataTable Native_GetInfoAboutCurrentUser(this INetPluginCall pc)
        {
            return pc.GetDataTable("GetInfoAboutCurrentUser");
        }

        public static DataTable Native_GetTypeListEx(this INetPluginCall pc)
        {
            return pc.GetDataTable("GetTypeListEx");
        }
        

        public static DataTable Native_GetLinkListEx(this INetPluginCall pc)
        {
            return pc.GetDataTable("GetLinkListEx");
        }

        public static DataTable Native_GetLinkList(this INetPluginCall pc)
        {
            return pc.GetDataTable("GetLinkList");
        }

        /// <summary>
        /// Возвращает список возможных состояний.
        /// </summary>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID] int – уникальный идентификатор состояния;
        /// <br/>[_NAME] string – название состояния;
        /// <br/>[_ICON] image – значок состояния.
        /// </returns>
        public static DataTable Native_GetStateList(this INetPluginCall pc)
        {
            return pc.GetDataTable("GetStateList");
        }

        /// <summary>
        /// Возвращает полный список атрибутов базы данных.
        /// </summary>
        /// <param name="mode">Режим возврата списка атрибутов</param>g
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID] int – уникальный идентификатор атрибута;
        /// <br/>[_NAME] string – название атрибута;
        /// <br/>[_ATTRTYPE] int – тип атрибута:
        /// <br/>0 – строка;
        /// <br/>1 – целое число;
        /// <br/>2 – действительное число;
        /// <br/>3 – дата и время;
        /// <br/>5 – текст;
        /// <br/>6 – изображение;
        /// <br/>[_DEFAULT] string – значение атрибута по умолчанию;
        /// <br/>[_LIST] text – список возможных значений атрибута;
        /// <br/>[_ACCESSLEVEL] int – уровень прав доступа (1 – Только чтение, 2 – Чтение/запись). Начиная с версии ЛОЦМАН:PLM 2017 поле устарело и его значение всегда равно 2;
        /// <br/>[_ONLYLISTITEMS] int – атрибут может принимать значения только из списка (0 - Любое, 1 - Из списка);
        /// <br/>[_SYSTEM] int – признак того, что атрибут является служебным (0 – Обычный, 1 – Служебный).
        /// </returns>
        public static DataTable Native_GetAttributeList(this INetPluginCall pc, GetAttributeListMode mode = GetAttributeListMode.All)
        {
            return pc.GetDataTable("GetAttributeList2", mode);
        }

        public static DataTable Native_GetProxyUseCases(this INetPluginCall pc, int proxyId = 0, int typeId = 0, int documentId = 0)
        {
            return pc.GetDataTable("GetProxyUseCases", proxyId, typeId, documentId);
        }

        public static DataTable Native_GetLinkedFast(this INetPluginCall pc, int objectId, string linkType, bool inverse = false)
        {
            return pc.GetDataTable("GetLinkedFast", objectId, linkType, inverse);
        }

        public static DataTable Native_GetLinkedFast2(this INetPluginCall pc, int objectId, string linkType, bool inverse = false)
        {
            return pc.GetDataTable("GetLinkedFast2", objectId, linkType, inverse);
        }

        public static DataTable Native_GetInfoAboutLink(this INetPluginCall pc, int idLink, int mode)
        {
            return pc.GetDataTable("GetInfoAboutLink", idLink, mode);
        }

        /// <summary>
        /// Возвращает информацию о типе.
        /// </summary>
        /// <param name="typeName">Название типа</param>
        /// <param name="mode">Режим вывода. В зависимости от его значения выдается соответствующая информация</param>
        /// <returns>Зависит от режима</returns>
        public static DataTable Native_GetInfoAboutType(this INetPluginCall pc, string typeName, GetInfoAboutTypeMode mode)
        {
            return pc.GetDataTable("GetInfoAboutType", typeName, (int)mode);
        }

        public static DataTable Native_GetInfoAboutVersion(this INetPluginCall pc, int objectId, GetInfoAboutVersionMode mode)
        {
            return pc.GetDataTable("GetInfoAboutVersion", string.Empty, string.Empty, string.Empty, objectId, (int)mode);
        }

        public static DataTable Native_GetInfoAboutVersion(this INetPluginCall pc, string typeName, string product, string version, GetInfoAboutVersionMode mode)
        {
            return pc.GetDataTable("GetInfoAboutVersion", typeName, product, version, 0, (int)mode);
        }

        public static DataTable Native_GetPropObjects(this INetPluginCall pc, IEnumerable<int> objectsIds)
        {
            return pc.GetDataTable("GetPropObjects", string.Join(",", objectsIds), 0);
        }

        public static int Native_NewObject(this INetPluginCall pc, string typeName, string stateName, string product, bool isProject = false)
        {
            return (int)pc.RunMethod("NewObject", typeName, stateName, product, isProject ? 1 : 0);
        }

        public static int Native_InsertObject(this INetPluginCall pc, string parentTypeName, string parentProduct, string parentVersion, string linkType, string childTypeName, string childProduct, string childVersion, string stateName, bool reuse)
        {
            return (int)pc.RunMethod("InsertObject", parentTypeName, parentProduct, parentVersion, childTypeName, childProduct, childVersion, linkType, stateName, reuse);
        }

        public static int Native_NewLink(this INetPluginCall pc, int parentId, string parentTypeName, string parentProduct, string parentVersion,
            int childId, string childTypeName, string childProduct, string childVersion, 
            string linkType, double minQuantity, double maxQuantity, string unitId)
        {
            return (int)pc.RunMethod("NewLink", parentId, parentTypeName, parentProduct, parentVersion,
                childId, childTypeName, childProduct, childVersion,
                minQuantity, maxQuantity, unitId, linkType);
        }

        public static int Native_UpLink(this INetPluginCall pc, string parentTypeName, string parentProduct, string parentVersion, 
            string childTypeName, string childProduct, string childVersion, 
            int idLink, double minQuantity, double maxQuantity, string unitId, bool toDel, string linkType)
        {
            return (int)pc.RunMethod("UpLink", parentTypeName, parentProduct, parentVersion,
                childTypeName, childProduct, childVersion,
                idLink, minQuantity, maxQuantity, unitId, toDel, linkType);
        }

        public static DataTable Native_CheckUniqueName(this INetPluginCall pc, string typeName, string product)
        {
            return pc.GetDataTable("CheckUniqueName", typeName, product);
        }

        public static void Native_UpAttrValueById(this INetPluginCall pc, int objectId, string attributeName, object attributeValue, string unitId = null, bool toDel = false)
        {
            pc.RunMethod("UpAttrValueById", objectId, attributeName, attributeValue, unitId, toDel);
        }

        public static void Native_RegistrationOfFile(this INetPluginCall pc, string typeName, string product, string version, int documentId, string fileName, string filePath)
        {
            pc.RunMethod("RegistrationOfFile", typeName, product, version, documentId, fileName, filePath);
        }

        public static void Native_SaveSecondaryView(this INetPluginCall pc, int docId, string filePath)
        {
            pc.RunMethod("SaveSecondaryView", docId, filePath);
        }

        public static string Native_PreviewBoObject(this INetPluginCall pc, string typeName, string uniqueId)
        {
            return (string)pc.RunMethod("PreviewBoObject", typeName, uniqueId);
        }

        public static DataTable Native_GetReport(this INetPluginCall pc, string reportName, IEnumerable<int> objectsIds = null, string reportParams = null)
        {
            return pc.GetDataTable("GetReport", reportName, objectsIds, reportParams);
        }

        public static DataTable Native_GetLockedObjects(this INetPluginCall pc)
        {
            return pc.GetDataTable("GetLockedObjects", 0);
        }

        public static void Native_KillVersion(this INetPluginCall pc, int id)
        {
            pc.RunMethod("KillVersionById", id);
        }

        public static void Native_KillVersion(this INetPluginCall pc, string typeName, string product, string version)
        {
            pc.RunMethod("KillVersion", typeName, product, version);
        }

        public static DataTable Native_KillVersions(this INetPluginCall pc, IEnumerable<int> objectsIds)
        {
            return pc.GetDataTable("KillVersions", string.Join(",", objectsIds), 0);
        }

        public static string Native_CheckOut(this INetPluginCall pc, string typeName, string product, string version, CheckOutMode mode = CheckOutMode.Default)
        {
            return (string)pc.RunMethod("CheckOut", typeName, product, version, (int)mode);
        }

        public static void Native_AddToCheckOut(this INetPluginCall pc, int objectId, bool isRoot = false)
        {
            pc.RunMethod("AddToCheckOut", objectId, isRoot);
        }

        public static void Native_ConnectToCheckOut(this INetPluginCall pc, string checkOutName, string dBName)
        {
            pc.RunMethod("ConnectToCheckOut", checkOutName, dBName);
        }

        public static void Native_DisconnectCheckOut(this INetPluginCall pc, string checkOutName, string dBName)
        {
            pc.RunMethod("DisconnectCheckOut", checkOutName, dBName);
        }

        public static DataTable Native_CheckIn(this INetPluginCall pc, string checkOutName, string dBName)
        {
            return pc.GetDataTable("CheckIn", checkOutName, dBName);
        }

        public static DataTable Native_SaveChanges(this INetPluginCall pc, string checkOutName, string dBName)
        {
            return pc.GetDataTable("SaveChanges", checkOutName, dBName);
        }

        public static void Native_CancelCheckOut(this INetPluginCall pc, string checkOutName, string dBName)
        {
            pc.RunMethod("CancelCheckOut", checkOutName, dBName);
        }

    }
}
