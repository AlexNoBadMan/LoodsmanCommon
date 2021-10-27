using Ascon.Plm.Loodsman.PluginSDK;
using System;
using System.Collections.Generic;
using System.Data;

namespace LoodsmanCommon.Extensions
{
    public static class NetPluginCallExtensions
    {

        ///<summary>
        /// Позволяет получить список объектов, выделенных в дереве «ЛОЦМАН Клиент».
        /// </summary>
        /// <returns>Список идентификаторов выделенных в дереве объектов с разделителем «,».</returns>
        public static string Native_CGetTreeSelectedIDs(this INetPluginCall pc)
        {
            return pc.RunMethod("CGetTreeSelectedIDs") as string ?? string.Empty;
        }

        /// <summary>
        /// Возвращает признак того, является ли пользователь, подключенный к текущей базе данных, администратором этой базы.
        /// </summary>
        /// <returns>
        /// <br/>true – пользователь является администратором.
        /// <br/>false – пользователь не является администратором.
        /// </returns>
        public static bool Native_IsAdmin(this INetPluginCall pc)
        {
            return pc.RunMethod("IsAdmin") as int? == 1;
        }

        [Obsolete("Недокументированный метод")]
        /// <summary>
        /// Возвращает измеряемые сущности. 
        /// </summary>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_GUID] string -  уникальный идентификатор сущности;
        /// <br/>[_DISPLAY] string - отображаемое имя сущности;
        /// <br/>[_TAG] int - всегда равен -1.
        /// </returns>
        /// <remarks>
        /// Примечание:
        /// <br/>** Недокументированный метод.
        /// </remarks>
        public static DataTable Native_GetFromBO_Nature(this INetPluginCall pc)
        {
            return pc.GetDataTable("GetFromBO_Nature");
        }

        /// <summary>
        /// Возвращает список возможных единиц измерения для измеряемой сущности. 
        /// </summary>
        /// <returns>
        /// <param name="measureGuid">Идентификатор измеряемой сущности</param>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_UNIT] string – уникальный идентификатор единицы измерения;
        /// <br/>[_NAME] string – название единицы измерения;
        /// <br/>[_BASICUNIT] int – признак базовой единицы измерения (1 – является базовой, 0 – не является базовой).
        /// </returns>
        public static DataTable Native_GetMUnitList(this INetPluginCall pc, string measureGuid)
        {
            return pc.GetDataTable("GetMUnitList", measureGuid);
        }

        ///// <summary>
        ///// Приводит целочисленное значение к заданной единице измерения.
        ///// </summary>
        ///// <param name="value">Значение</param>
        ///// <param name="measureGuid">Идентификатор исходной единицы измерения</param>
        ///// <param name="destMeasureGuid">Идентификатор требуемой единицы измерения</param>
        ///// <returns>Возвращает преобразованное значение.</returns>
        //public static int Native_ConverseValue(this INetPluginCall pc, int value, string measureGuid, string destMeasureGuid)
        //{
        //    return (int)pc.RunMethod("ConverseValue", value, measureGuid, destMeasureGuid);
        //}

        /// <summary>
        /// Приводит значение к заданной единице измерения.
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="sourceMeasureGuid">Идентификатор исходной единицы измерения</param>
        /// <param name="destMeasureGuid">Идентификатор требуемой единицы измерения</param>
        /// <returns>Возвращает преобразованное значение.</returns>
        public static double Native_ConverseValue(this INetPluginCall pc, double value, string sourceMeasureGuid, string destMeasureGuid)
        {
            return (double)pc.RunMethod("ConverseValue", value, sourceMeasureGuid, destMeasureGuid);
        }

        /// <summary>
        /// Возвращает информацию о текущем пользователе.
        /// </summary>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_NAME] string – имя пользователя;
        /// <br/>[_FULLNAME] string – полное имя пользователя;
        /// <br/>[_MAIL] string – адрес электронной почты пользователя;
        /// <br/>[_USERDIR] string – рабочая папка для проектов пользователя; 
        /// <br/>[_FILEDIR] string – папка для хранения файлов пользователя. 
        /// </returns>
        public static DataTable Native_GetInfoAboutCurrentUser(this INetPluginCall pc)
        {
            return pc.GetDataTable("GetInfoAboutCurrentUser");
        }

        /// <summary>
        /// Возвращает список доступных типов, включая абстрактные.
        /// </summary>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID] int – уникальный идентификатор типа;
        /// <br/>[_TYPENAME] string – название типа;
        /// <br/>[_ICON] image – значок типа;
        /// <br/>[_ATTRNAME] string – название ключевого атрибута;
        /// <br/>[_DEFAULT] string – значение ключевого атрибута по умолчанию;
        /// <br/>[_LIST] text – список возможных значений атрибута;
        /// <br/>[_SYSTEM] int – зарезервировано;
        /// <br/>[_DEFAULTSTATE] string – состояние по умолчанию;
        /// <br/>[_DOCUMENT] int – является ли документом (1 – является, 0 – не является);
        /// <br/>[_EXTENSION] string – расширение файла, который редактируется в соответствующем инструменте;
        /// <br/>[_NOVERSIONS] int – имеет только одну версию (1 – объекты этого типа могут иметь только одну версию, 0 – объекты этого типа могут иметь множество версий);
        /// <br/>[_CANBEPROJECT] int – может ли быть проектом (1 – может, 0 – не может);
        /// <br/>[_CANCREATE] int – может ли текущий пользователь создавать объекты данного типа (1 – может, 0 – не может);
        /// <br/>[_NATIVENAME] string – имя бизнес-объекта;
        /// <br/>[_SERVERNAME] string – COM-сервер бизнес-объекта;
        /// <br/>[_NODE] string – идентификатор узла справочника.
        ///</returns>
        public static DataTable Native_GetTypeListEx(this INetPluginCall pc)
        {
            return pc.GetDataTable("GetTypeListEx");
        }
        
        /// <summary>
        /// Возвращает список возможных типов связей с указанием связываемых типов.
        /// </summary>
        /// <returns>
        /// Возвращает набор данных с полями:      
        /// <br/>[_TYPE_ID_1] int - ID первого типа;
        /// <br/>[_TYPE_NAME_1] string - название первого типа;
        /// <br/>[_TYPE_ID_2] int - ID второго типа;
        /// <br/>[_TYPE_NAME_2] string - название первого типа;
        /// <br/>[_ID_LINKTYPE] int - ID типа связи;
        /// <br/>[_LINKTYPE] string – название типа связи;
        /// <br/>[_INVERSENAME] string – название обратной связи;
        /// <br/>[_LINKKIND] short - вид связи (1 – горизонтальная, 0 – вертикальная);
        /// <br/>[_DIRECTION] int - направление связи (1 - прямая, -1 - обратная);
        /// <br/>[_IS_QUANTITY] short - является ли связь количественной (1 - да, 0 - нет).
        /// </returns>
        public static DataTable Native_GetLinkListEx(this INetPluginCall pc)
        {
            return pc.GetDataTable("GetLinkListEx");
        }

        /// <summary>
        /// Возвращает список возможных типов связей.
        /// </summary>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID] int – уникальный идентификатор типа связи;
        /// <br/>[_NAME] string – название типа связи;
        /// <br/>[_INVERSENAME] string – название обратной связи;
        /// <br/>[_TYPE] int – вид связи (1– горизонтальная, 0 – вертикальная);
        /// <br/>[_ICON] image – значок типа связи;
        /// <br/>[_ORDER] int – порядок следования типа связи.
        /// </returns>
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
        /// Изменяет состояние объекта.
        /// </summary>
        /// <param name="objectId">Идентификатор версии</param>
        /// <param name="stateName">Название состояния</param>
        public static void Native_UpdateStateOnObject(this INetPluginCall pc, int objectId, string stateName)
        {
            pc.RunMethod("UpdateStateOnObjectById", objectId, stateName);
        }

        /// <summary>
        /// Изменяет состояние объекта.
        /// </summary>
        /// <param name="typeName">Название типа</param>
        /// <param name="product">Ключевой атрибут</param>
        /// <param name="version">Версия объекта</param>
        /// <param name="stateName">Название состояния</param>
        public static void Native_UpdateStateOnObject(this INetPluginCall pc, string typeName, string product, string version, string stateName)
        {
            pc.RunMethod("UpdateStateOnObject", typeName, product, version, stateName);
        }

        /// <summary>
        /// Возвращает полный список атрибутов базы данных.
        /// </summary>
        /// <param name="mode">Режим возврата списка атрибутов</param>
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

        /// <summary>
        /// Возвращает случаи использования прокси, типа объекта и типа документа.
        /// </summary>
        /// <param name="proxyId">Идентификатор прокси. Может быть передано значение 0, что означает «любое значение».</param>
        /// <param name="typeId">Идентификатор типа объекта. Может быть передано значение 0, что означает «любое значение».</param>
        /// <param name="documentId">Идентификатор типа документа. Может быть передано значение 0, что означает «любое значение».</param>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID] int – уникальный идентификатор случая использования;
        /// <br/>[_ID_PROXY] int – уникальный идентификатор прокси;
        /// <br/>[_PROXYNAME] string – название прокси;
        /// <br/>[_ID_DOCUMENT] int – уникальный идентификатор типа документа;
        /// <br/>[_DOCNAME] string – название типа документа;
        /// <br/>[_ID_PARENT] int – уникальный идентификатор типа объекта;
        /// <br/>[_PARENTNAME] string – название типа объекта;
        /// <br/>[_DESCRIPTION] string – описание случая использования;
        /// <br/>[_EXTENSION] string – расширение файла;
        /// <br/>[_ID_INTRANSLATOR] int – уникальный идентификатор входного транслятора;
        /// <br/>[_NAME_INTRANSLATOR] string – название входного транслятора;
        /// <br/>[_ID_OUTRANSLATOR] int – уникальный идентификатор выходного транслятора;
        /// <br/>[_NAME_OUTRANSLATOR] string – название выходного транслятора;
        /// <br/>[_ID_CONDITION] int – уникальный идентификатор условий преобразования.        
        ///  </returns>
        public static DataTable Native_GetProxyUseCases(this INetPluginCall pc, int proxyId = 0, int typeId = 0, int documentId = 0)
        {
            return pc.GetDataTable("GetProxyUseCases", proxyId, typeId, documentId);
        }

        /// <summary>
        /// Возвращает список версий, привязанных соответствующей связью.
        /// </summary>
        /// <param name="objectId">Идентификатор версии объекта.</param>
        /// <param name="linkType">Тип связи.</param>
        /// <param name="inverse">Направление (true – обратное, false – прямое).</param>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_LINK] int – уникальный идентификатор экземпляра связи;
        /// <br/>[_TYPE] string – тип объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_STATE] string – текущее состояние объекта;
        /// <br/>[_MIN_QUANTITY] double – нижняя граница количества;
        /// <br/>[_MAX_QUANTITY] double – верхняя граница количества;
        /// <br/>[_DOCUMENT] int – является ли документом (1 – является, 0 – не является);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись, 3 – Полный доступ);
        /// <br/>[_LOCKED] int – уровень блокировки объекта (0 – не блокирован, 1 – блокирован текущим пользователем, 2 – блокирован другим пользователем).
        /// </returns>
        public static DataTable Native_GetLinkedFast(this INetPluginCall pc, int objectId, string linkType, bool inverse = false)
        {
            return pc.GetDataTable("GetLinkedFast", objectId, linkType, inverse);
        }

        /// <summary>
        /// Возвращает список версий, привязанных соответствующей связью.
        /// </summary>
        /// <param name="objectId">Идентификатор версии объекта.</param>
        /// <param name="linkType">Тип связи.</param>
        /// <param name="inverse">Направление (true – обратное, false – прямое).</param>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_LINK] int – уникальный идентификатор экземпляра связи;
        /// <br/>[_ID_TYPE] int – идентификатор типа объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_ID_STATE] int – идентификатор текущего состояния объекта;
        /// <br/>[_ID_LOCK] int – идентификатор чекаута, в котором блокирован объект (если объект не блокирован, то вернется null);
        /// <br/>[_DOCUMENT] int – является ли документом (1 – является, 0 – не является);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись, 3 – Полный доступ).
        /// </returns>
        public static DataTable Native_GetLinkedFast2(this INetPluginCall pc, int objectId, string linkType, bool inverse = false)
        {
            return pc.GetDataTable("GetLinkedFast2", objectId, linkType, inverse);
        }

        /// <summary>
        /// Возвращает список связанных объектов для отображения в дереве.
        /// </summary>
        /// <param name="typeName">Название типа.</param>
        /// <param name="product">Ключевой атрибут.</param>
        /// <param name="version">Версия объекта.</param>
        /// <param name="linkTypeNames">Список связей.</param>
        /// <param name="withAttributes">Возвращать или не возвращать атрибуты.</param>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_LINK] int – уникальный идентификатор экземпляра связи;
        /// <br/>[_TYPE] string – тип объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_STATE] string – текущее состояние объекта;
        /// <br/>[_LINKTYPE] string – тип связи, которым привязан объект;
        /// <br/>[_DOCUMENT] int – является ли документом (1 – является, 0 – не является);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись, 3 – Полный доступ);
        /// <br/>[_LOCKED] int – уровень блокировки объекта (0 – не блокирован, 1 – блокирован текущим пользователем, 2 – блокирован другим пользователем).
        /// </returns>
        public static DataTable Native_GetTree(this INetPluginCall pc, string typeName, string product, string version, IEnumerable<string> linkTypeNames, bool withAttributes = false)
        {
            return pc.GetDataTable("GetTree", typeName, product, version, 0, string.Join(Constants.LINK_SEPARATOR, linkTypeNames), withAttributes);
        }

        /// <summary>
        /// Возвращает список связанных объектов для отображения в дереве.
        /// </summary>
        /// <param name="objectId">Идентификатор версии объекта.</param>
        /// <param name="linkTypeNames">Список связей.</param>
        /// <param name="withAttributes">Возвращать или не возвращать атрибуты.</param>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_LINK] int – уникальный идентификатор экземпляра связи;
        /// <br/>[_TYPE] string – тип объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_STATE] string – текущее состояние объекта;
        /// <br/>[_LINKTYPE] string – тип связи, которым привязан объект;
        /// <br/>[_DOCUMENT] int – является ли документом (1 – является, 0 – не является);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись, 3 – Полный доступ);
        /// <br/>[_LOCKED] int – уровень блокировки объекта (0 – не блокирован, 1 – блокирован текущим пользователем, 2 – блокирован другим пользователем).
        /// </returns>
        public static DataTable Native_GetTree(this INetPluginCall pc, int objectId, IEnumerable<string> linkTypeNames, bool withAttributes = false)
        {
            return pc.GetDataTable("GetTree", string.Empty, string.Empty, string.Empty, objectId, string.Join(Constants.LINK_SEPARATOR, linkTypeNames), withAttributes);
        }

        /// <summary>
        /// Возвращает список связанных объектов для отображения в дереве.
        /// </summary>
        /// <param name="objectId">Идентификатор версии объекта.</param>
        /// <param name="linkTypeNames">Список связей.</param>
        /// <param name="withAttributes">Возвращать или не возвращать атрибуты.</param>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_LINK] int – уникальный идентификатор экземпляра связи;
        /// <br/>[_ID_TYPE] int – идентификатор типа объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_ID_STATE] int – идентификатор текущего состояния объекта;
        /// <br/>[_ID_LINKTYPE] int – идентификатор типа связи, которым привязан объект;
        /// <br/>[_ID_LOCK] int – идентификатор чекаута, в котором блокирован объект (если объект не блокирован, то вернется null);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись, 3 – Полный доступ).
        /// </returns>
        public static DataTable Native_GetTree2(this INetPluginCall pc, int objectId, IEnumerable<string> linkTypeNames, bool withAttributes = false)
        {
            return pc.GetDataTable("GetTree2", objectId, string.Join(Constants.LINK_SEPARATOR, linkTypeNames), withAttributes ? 1 : 0);
        }

        /// <summary>
        /// Возвращает информацию о связанных объектах для группы объектов.
        /// </summary>
        /// <param name="objectId">Идентификатор версии объекта.</param>
        /// <param name="inverse">Направление (true – обратное, false – прямое).</param>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_LINK] int – уникальный идентификатор экземпляра связи;
        /// <br/>[_ID_TYPE] int – идентификатор типа объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_ID_STATE] int – идентификатор текущего состояния объекта;
        /// <br/>[_ID_LINKTYPE] int – идентификатор типа связи, которым привязан объект;
        /// <br/>[_ID_LOCK] int – идентификатор чекаута, в котором блокирован объект(если объект не блокирован, то вернется null);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись; 3 – Полный доступ).
        /// </returns>
        public static DataTable Native_GetLObjs(this INetPluginCall pc, int objectId, bool inverse = false)
        {
            return pc.GetDataTable("GetLObjs", objectId, inverse);
        }


        /// <summary>
        /// Возвращает информацию о связанных объектах для группы объектов.
        /// </summary>
        /// <param name="typeName">Название типа.</param>
        /// <param name="product">Ключевой атрибут.</param>
        /// <param name="version">Версия объекта.</param>
        /// <param name="linkType">Тип связи.</param>
        /// <param name="inverse">Направление (true – обратное, false – прямое).</param>
        /// <param name="fullLink">Признак полной разузловки.</param>
        /// <param name="groupByProduct">Признак группировки по изделиям (для случая полной разузловки).</param>
        /// <param name="forTree">Признак вывода для дерева объектов.</param>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_LINK] int – уникальный идентификатор экземпляра связи;
        /// <br/>[_ID_TYPE] int – идентификатор типа объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_ID_STATE] int – идентификатор текущего состояния объекта;
        /// <br/>[_ID_LOCK] int – идентификатор чекаута, в котором блокирован объект (если объект не блокирован, то вернется null);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись, 3 – Полный доступ);
        /// <br/>[_MIN_QUANTITY] double – нижняя граница количества;
        /// <br/>[_MAX_QUANTITY] double – верхняя граница количества;
        /// <br/>[_ID_UNIT] string – идентификатор единицы измерения, в которой задано количество.
        /// </returns>
        public static DataTable Native_GetLinkedObjects(this INetPluginCall pc, string typeName, string product, string version, string linkType, bool inverse, bool fullLink, bool groupByProduct, bool forTree)
        {
            return pc.GetDataTable("GetLinkedObjects", typeName, product, version, linkType, inverse, fullLink, groupByProduct, forTree);
        }
        

        /// <summary>
        /// Возвращает информацию о связанных объектах для группы объектов.
        /// </summary>
        /// <param name="objectIds">Идентификаторы версий объектов.</param>
        /// <param name="linkTypeIds">Список идентификаторов связей.</param>
        /// <param name="inverse">Направление (true – обратное, false – прямое).</param>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_LINK] int – числовой идентификатор связи;
        /// <br/>[_ID_PARENT] int – идентификатор исходного объекта связи (всегда соответствует указанному в #Ids);
        /// <br/>[_ID_CHILD] int – идентификатор связанного объекта;
        /// <br/>[_ID_LINK_TYPE] int – идентификатор типа связи;
        /// <br/>[_LINK_TYPE_NAME] string – имя типа связи;
        /// <br/>[_ID_TYPE] int – идентификатор типа связанного объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут связанного объекта;
        /// <br/>[_VERSION] string – версия связанного объекта;
        /// <br/>[_ID_STATE] int – состояние связанного объекта;
        /// <br/>[_MIN_QUANTITY] double – минимальное значение количества для данного типа связи;
        /// <br/>[_MAX_QUANTITY] double – максимальное значение количества для данного типа связи;
        /// <br/>[_ACCESSLEVEL] int – уровень доступа пользователя, подключившегося к связанному объекту;
        /// <br/>[_ID_LOCK] int – идентификатор рабочей области, блокирующей связанный объект.
        /// </returns>
        public static DataTable Native_GetLinkedObjectsForObjects(this INetPluginCall pc, IEnumerable<int> objectIds, IEnumerable<int> linkTypeIds, bool inverse = false)
        {
            return pc.GetDataTable("GetLinkedObjectsForObjects", string.Join(Constants.ID_SEPARATOR, objectIds), string.Join(Constants.ID_SEPARATOR, linkTypeIds), inverse);
        }

        /// <summary>
        /// Возвращает информацию об экземпляре связи.
        /// </summary>
        /// <param name="idLink">Идентификатор экземпляра связи. Может быть получен методом GetLinkedObjects.</param>
        /// <param name="mode">Режим вывода. В зависимости от его значения выдается соответствующая информация.</param>
        /// <returns>
        /// Зависит от режима:
        /// <br/>inMode = 1
        /// <br/>    Возвращает общую информацию об экземпляре связи.
        /// <br/>    Возвращает набор данных с полями:
        /// <br/>    [_ID] int – уникальный идентификатор экземпляра связи;
        /// <br/>    [_NAME] string – тип связи;
        /// <br/>    [_MIN_QUANTITY] double – нижняя граница количества;
        /// <br/>    [_MAX_QUANTITY] double – верхняя граница количества;
        /// <br/>    [_ID_UNIT] string – идентификатор единицы измерения, в которой отображается значение количества;
        /// <br/>    [_UNIT] string – название единицы измерения, в которой отображается значение количества;
        /// <br/>    [_ID_MEASURE] string – идентификатор сущности, которая измерена количеством;
        /// <br/>    [_MEASURE] string – название сущности, которая измерена количеством.
        /// <br/> 
        /// <br/>inMode = 2
        /// <br/>    Возвращает объекты, которые связаны данным экземпляром связи.
        /// <br/>    Возвращает набор данных с полями:
        /// <br/>    [_NAME] string – тип связи;
        /// <br/>    [_ID_PARENT] int – уникальный идентификатор объекта-родителя;
        /// <br/>    [_PARENT_TYPE] string – название типа объекта-родителя;
        /// <br/>    [_PARENT_PRODUCT] string – ключевой атрибут объекта-родителя;
        /// <br/>    [_PARENT_VERSION] string – версия объекта объекта-родителя;
        /// <br/>    [_ID_CHILD] int – уникальный идентификатор объекта-потомка;
        /// <br/>    [_CHILD_TYPE] string – название типа объекта-потомка;
        /// <br/>    [_CHILD_PRODUCT] string – ключевой атрибут объекта-потомка;
        /// <br/>    [_CHILD_VERSION] string – версия объекта объекта-потомка.
        /// </returns>
        public static DataTable Native_GetInfoAboutLink(this INetPluginCall pc, int idLink, int mode)
        {
            return pc.GetDataTable("GetInfoAboutLink", idLink, mode);
        }

        /// <summary>
        /// Возвращает информацию о типе.
        /// </summary>
        /// <param name="typeName">Название типа</param>
        /// <param name="mode">Режим вывода. В зависимости от его значения выдается соответствующая информация</param>
        /// <returns>Зависит от режима.</returns>
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

        /// <summary>
        /// Возвращает свойства объектов.
        /// </summary>
        /// <param name="objectsIds">Список идентификаторов объектов. Задается в виде: id1,id2,id3, ... ,idn</param>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_TYPE] string – тип объекта; 
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_STATE] string – текущее состояние объекта;
        /// <br/>[_DOCUMENT] int – является ли документом (1 – является, 0 – не является);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись, 3 – Полный доступ);
        /// <br/>[_LOCKED] int – уровень блокировки объекта (0 – не блокирован, 1 – блокирован текущим пользователем, 2 – блокирован другим пользователем).
        /// </returns>
        public static DataTable Native_GetPropObjects(this INetPluginCall pc, IEnumerable<int> objectsIds)
        {
            return pc.GetDataTable("GetPropObjects", string.Join(Constants.ID_SEPARATOR, objectsIds), 0);
        }

        /// <summary>
        /// Создает новый объект.
        /// </summary>
        /// <param name="typeName">Название типа создаваемого объекта.</param>
        /// <param name="stateName">Состояние создаваемого объекта.</param>
        /// <param name="product">Ключевой атрибут создаваемого объекта.</param>
        /// <param name="isProject">Признак того, что объект будет являться проектом (0 – не будет проектом, 1 – будет проектом).</param>
        /// <returns>Возвращает идентификатор созданной версии.
        /// </returns>
        public static int Native_NewObject(this INetPluginCall pc, string typeName, string stateName, string product, bool isProject = false)
        {
            return (int)pc.RunMethod("NewObject", typeName, stateName, product, isProject ? 1 : 0);
        }

        /// <summary>
        /// Вставляет новое или существующее изделие в редактируемый объект.
        /// </summary>
        /// <param name="parentTypeName">Тип объекта-родителя.</param>
        /// <param name="parentProduct">Значение ключевого атрибута объекта-родителя</param>
        /// <param name="parentVersion">Номер версии объекта-родителя. Если равен #32, то будет осуществлена попытка создать объект типа stParentType с ключевым атрибутом stParentProduct.</param>
        /// <param name="linkType">Тип связи, которой нужно связать объекты.</param>
        /// <param name="childTypeName">Тип объекта-потомка.</param>
        /// <param name="childProduct">Значение ключевого атрибута объекта-потомка.</param>
        /// <param name="childVersion">Номер версии объекта-потомка. Если равен #32, то будет осуществлена попытка создать объект типа stChildType с ключевым атрибутом stChildProduct</param>
        /// <param name="stateName">Состояние созданного объекта (имеет смысл только если stParentVersion или stChildVersion равен #32).</param>
        /// <param name="reuse">Устанавливает возможность повторного применения объекта. 
        /// Если значение – true, то независимо от существования в базе данных такой пары связанных объектов будет создаваться еще один экземпляр связи.
        /// Если значение – false, то при существовании в базе данных такой пары связанных объектов новый экземпляр связи создаваться не будет, при этом код возврата inReturnCode будет иметь значение, отличное от нуля.
        ///</param>
        /// <returns>Возвращает идентификатор созданной связи.</returns>
        public static int Native_InsertObject(this INetPluginCall pc, string parentTypeName, string parentProduct, string parentVersion, string linkType, string childTypeName, string childProduct, string childVersion, string stateName, bool reuse)
        {
            return (int)pc.RunMethod("InsertObject", parentTypeName, parentProduct, parentVersion, childTypeName, childProduct, childVersion, linkType, stateName, reuse);
        }

        /// <summary>
        /// Добавляет связь между объектами.
        /// </summary>
        /// <param name="parentId">Идентификатор версии объекта-родителя.</param>
        /// <param name="parentTypeName">Наименование типа объекта-родителя.</param>
        /// <param name="parentProduct">Значение ключевого атрибута объекта-родителя.</param>
        /// <param name="parentVersion">Номер версии объекта-родителя.</param>
        /// <param name="childId">Идентификатор версии объекта-потомка.</param>
        /// <param name="childTypeName">Наименование типа объекта-потомка.</param>
        /// <param name="childProduct">Значение ключевого атрибута объекта-потомка.</param>
        /// <param name="childVersion">Номер версии объекта-потомка.</param>
        /// <param name="linkType">Тип связи, которой нужно связать объекты.</param>
        /// <param name="minQuantity">Нижняя граница количества.</param>
        /// <param name="maxQuantity">Верхняя граница количества.</param>
        /// <param name="unitId">Уникальный идентификатор единицы измерения.</param>
        /// <returns>Возвращает идентификатор созданной связи.</returns>
        public static int Native_NewLink(this INetPluginCall pc, int parentId, string parentTypeName, string parentProduct, string parentVersion,
            int childId, string childTypeName, string childProduct, string childVersion, 
            string linkType, double minQuantity, double maxQuantity, string unitId)
        {
            return (int)pc.RunMethod("NewLink", parentId, parentTypeName, parentProduct, parentVersion,
                childId, childTypeName, childProduct, childVersion,
                minQuantity, maxQuantity, unitId, linkType);
        }

        /// <summary>
        /// Добавляет, удаляет, обновляет связь между объектами.
        /// </summary>
        /// <param name="parentTypeName">Тип объекта-родителя.</param>
        /// <param name="parentProduct">Значение ключевого атрибута объекта-родителя.</param>
        /// <param name="parentVersion">Номер версии объекта-родителя.</param>
        /// <param name="childTypeName">Тип объекта-потомка.</param>
        /// <param name="childProduct">Значение ключевого атрибута объекта-потомка.</param>
        /// <param name="childVersion">Номер версии объекта-потомка.</param>
        /// <param name="idLink">Идентификатор связи. </param>
        /// <param name="minQuantity">Нижняя граница количества</param>
        /// <param name="maxQuantity">Верхняя граница количества.</param>
        /// <param name="unitId">Уникальный идентификатор единицы измерения.</param>
        /// <param name="toDel">Признак удаления экземпляра связи (true – удалять, false – не удалять)</param>
        /// <param name="linkType">Тип связи, которой нужно перевязать объекты.</param>
        /// <returns>Возвращает идентификатор созданной связи.</returns>
        /// <remarks>
        /// Примечание:
        /// <br/> Метод работает следующим образом: 
        /// <br/>-если boDel =true, то заданный экземпляр связи с идентификатором inIdLink удаляется;
        /// <br/>-если экземпляр связи с идентификатором inIdLink существует, то его свойства (количество, единицы измерения) будут изменены;
        /// <br/>-если inIdLink=0, то будет добавлена связь между объектами, указанными в качестве родителя и потомка.
        /// </remarks>
        public static int Native_UpLink(this INetPluginCall pc, string parentTypeName, string parentProduct, string parentVersion, 
            string childTypeName, string childProduct, string childVersion, 
            int idLink, double minQuantity, double maxQuantity, string unitId, bool toDel, string linkType)
        {
            return (int)pc.RunMethod("UpLink", parentTypeName, parentProduct, parentVersion,
                childTypeName, childProduct, childVersion,
                idLink, minQuantity, maxQuantity, unitId, toDel, linkType);
        }

        /// <summary>
        /// Возвращает все версии заданного объекта или «похожего» объекта, если в базе данных установлена соответствующая настройка.
        /// </summary>
        /// <param name="typeName">Название типа.</param>
        /// <param name="product">Ключевой атрибут.</param>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта (если найдены «похожие» объекты, то содержится ключевой атрибут «похожего» объекта);
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_STATE] string – текущее состояние объекта;
        /// <br/>[_DATEOFCREATE] datetime – дата создания объекта;
        /// <br/>[_LOCKID] int – «область видимости» объекта (0 – объект существует в базе данных и доступен; >0 – объект создан в чекауте, но еще не сохранен в базе данных, поэтому он доступен только в рамках этого чекаута);
        /// <br/>[_USERNAME] string – имя пользователя, заблокировавшего объект.
        /// </returns>
        public static DataTable Native_CheckUniqueName(this INetPluginCall pc, string typeName, string product)
        {
            return pc.GetDataTable("CheckUniqueName", typeName, product);
        }

        /// <summary>
        /// Добавляет, удаляет, обновляет значение атрибута объекта.
        /// </summary>
        /// <param name="objectId">Идентификатор версии объекта.</param>
        /// <param name="attributeName">Название атрибута.</param>
        /// <param name="attributeValue">Значение атрибута</param>
        /// <param name="unitId">Уникальный идентификатор единицы измерения.</param>
        /// <param name="toDel">Признак удаления атрибута (true – удалять, false – не удалять).</param>
        /// <remarks>
        /// Метод работает следующим образом:
        /// <br/>-если boDel =true, то заданный атрибут удаляется (параметры vaAttrValue и stIdUnit игнорируются);
        /// <br/>-если атрибут у объекта с именем stAttrName уже существует, то его значение будет изменено;
        /// <br/>-если атрибут у объекта с именем stAttrName отсутствует, то он будет добавлен. 
        /// </remarks>
        public static void Native_UpAttrValueById(this INetPluginCall pc, int objectId, string attributeName, object attributeValue, string unitId = null, bool toDel = false)
        {
            pc.RunMethod("UpAttrValueById", objectId, attributeName, attributeValue, unitId, toDel);
        }

        /// <summary>
        /// Регистрирует в базе данных файл, находящийся на рабочем диске пользователя.
        /// </summary>
        /// <param name="typeName">Название типа.</param>
        /// <param name="product">Ключевой атрибут.</param>
        /// <param name="version">Версия объекта.</param>
        /// <param name="fileName">Название файла.</param>
        /// <param name="filePath">Путь к файлу относительно диска из настройки «Буква рабочего диска».</param>
        public static void Native_RegistrationOfFile(this INetPluginCall pc, string typeName, string product, string version, string fileName, string filePath)
        {
            pc.RunMethod("RegistrationOfFile", typeName, product, version, 0, fileName, filePath);
        }

        /// <summary>
        /// Регистрирует в базе данных файл, находящийся на рабочем диске пользователя.
        /// </summary>
        /// <param name="documentId">Идентификатор версии документа.</param>
        /// <param name="fileName">Название файла.</param>
        /// <param name="filePath">Путь к файлу относительно диска из настройки «Буква рабочего диска».</param>
        public static void Native_RegistrationOfFile(this INetPluginCall pc, int documentId, string fileName, string filePath)
        {
            pc.RunMethod("RegistrationOfFile", string.Empty, string.Empty, string.Empty, documentId, fileName, filePath);
        }

        #region Вторичное представление

        /// <summary>
        /// Удаляет вторичное представление документа.
        /// <br/>
        /// <br/>Для ЛОЦМАН:PLM 2017 и более поздних версий системы этот метод устарел и оставлен только для совместимости с предыдущими версиями.
        /// <br/>В ЛОЦМАН:PLM 2017 и более поздних версиях метод удаляет все ревизии вторичного представления указанного документа. Чтобы удалить конкретную ревизию вторичного представления, воспользуйтесь методом <see cref="Native_DelSecondaryViewRevision(INetPluginCall, int, int)"/>.
        /// </summary>
        /// <param name="documenId">Идентификатор версии документа.</param>
        public static void Native_DelSecondaryView(this INetPluginCall pc, int documenId)
        {
            pc.RunMethod("DelSecondaryView", documenId);
        }

        /// <summary>
        /// Удаляет аннотацию или ревизию вторичного представления документа.
        /// <br/>
        /// <br/>Удаление аннотации возможно только в режиме базы данных.
        /// <br/>Удаление корневой ревизии вторичного представления возможно и в режиме базы данных, и в режиме изменения объектов.
        /// <br/>
        /// <br/>Идентификатор ревизии вторичного представления или аннотации. Может быть получен с помощью метода <see cref="Native_GetSecondaryViewRevisions(INetPluginCall, int)"/>
        /// </summary>
        /// <param name="documenId">Идентификатор версии документа.</param>
        /// <param name="revisionId">Идентификатор ревизии вторичного представления или аннотации.</param>
        public static void Native_DelSecondaryViewRevision(this INetPluginCall pc, int documenId, int revisionId)
        {
            pc.RunMethod("DelSecondaryViewRevision", documenId, revisionId);
        }

        /// <summary>
        /// Возвращает информацию о последней корневой ревизии вторичного представления для группы документов.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_REVISION_ID] int – идентификатор последней корневой ревизии вторичного представления;
        /// <br/>[_DOC_ID] int – идентификатор версии документа;
        /// <br/>[_EXT] string – расширение файла последней корневой ревизии вторичного представления.
        /// <br/>
        /// </summary>
        /// <param name="documentsIds">Идентификаторы версий документов.</param>
        public static void Native_GetInfoAboutSecondaryViews(this INetPluginCall pc, IEnumerable<int> documentsIds)
        {
            pc.RunMethod("GetInfoAboutSecondaryViews", documentsIds);
        }

        /// <summary>
        /// Выгружает файл ревизии вторичного представления и возвращает сетевой путь к нему.
        /// </summary>
        /// <param name="revisionId">Идентификатор ревизии вторичного представления.</param>
        /// <returns>
        /// Возвращает сетевой путь к файлу вторичного представления.
        /// </returns>
        public static string Native_GetSecondaryViewRevisionFile(this INetPluginCall pc, int revisionId)
        {
            return pc.RunMethod("GetSecondaryViewRevisionFile", revisionId) as string;
        }

        /// <summary>
        /// Возвращает дерево ревизий вторичного представления.
        /// <br/>
        /// <br/>Метод позволяет получать корневые и некорневые ревизии вторичного представления.
        /// <br/>Корневая ревизия создается при получении информации из программы-инструмента либо при вызове метода <see cref="Native_SaveSecondaryView(INetPluginCall, int, string)"/> и содержит только вторичное представление без аннотирования. 
        /// <br/>Поле [_PARENT_ID] для корневой ревизии имеет значение NULL, а поле [_NUMBER] содержит одноразрядный номер ревизии, соответствующий порядку создания ревизий.
        /// <br/>Некорневая ревизия содержит информацию об аннотировании корневой ревизии вторичного представления. 
        /// <br/>Поле [_PARENT_ID] содержит идентификатор корневой ревизии, а поле [_NUMBER] содержит двухразрядный порядковый номер ревизии, где первый разряд соответствует номеру корневой ревизии, а второй разряд – номеру аннотации соответствующей корневой ревизии, например, «12.2»..
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID] int – идентификатор ревизии вторичного представления;
        /// <br/>[_PARENT_ID] int – идентификатор корневой ревизии вторичного представления;
        /// <br/>[_NUMBER] string – номер ревизии вторичного представления;
        /// <br/>[_USER_ID] int – идентификатор пользователя, являющегося автором ревизии вторичного представления;
        /// <br/>[_USERNAME] string – имя пользователя, являющегося автором ревизии вторичного представления;
        /// <br/>[_DATEOFCREATE] date – дата создания ревизии вторичного представления;
        /// <br/>[_STATE] string – состояние объекта на момент создания ревизии вторичного представления;
        /// <br/>[_USERS_ANNOTATED_BY] string – список имен пользователей, аннотирующих в данный момент ревизию вторичного представления. Разделитель – символ с кодом «1».
        /// </summary>
        /// <param name="documenId">Идентификатор версии документа.</param>
        public static DataTable Native_GetSecondaryViewRevisions(this INetPluginCall pc, int documenId)
        {
            return pc.GetDataTable("GetSecondaryViewRevisions", documenId);
        }

        /// <summary>
        /// Возвращает состояние «флага» аннотирования текущим пользователем.
        /// <br/>
        /// <br/>Начиная с версии ЛОЦМАН:PLM 2017 допускается аннотирование документа несколькими пользователями одновременно. 
        /// <br/>Получить список пользователей, выполняющих в данный момент аннотирование документа, можно с помощью метода <see cref="Native_GetSecondaryViewRevisions(INetPluginCall, int)"/>.
        /// </summary>
        /// <param name="documenId">Идентификатор версии документа.</param>
        public static bool Native_GetViewLock(this INetPluginCall pc, int documenId)
        {
            return pc.RunMethod("GetViewLock", documenId) as int? == 1;
        }

        /// <summary>
        /// Сохраняет аннотацию документа.
        /// <br/>
        /// <br/>Метод работает только в режиме базы данных. 
        /// <br/>Перед вызовом этого метода необходимо начать аннотирование вторичного представления с помощью метода <see cref="Native_SetViewLock(INetPluginCall, int, bool)"/>.
        /// <br/>
        /// <br/>Аннотация прикрепляется к корневой ревизии, последней на момент начала аннотирования.
        /// </summary>
        /// <param name="documenId">Идентификатор версии документа.</param>
        /// <param name="annotationPath">Сетевой путь к файлу аннотации, доступный серверу приложений.</param>
        public static void Native_SaveAnnotationFile(this INetPluginCall pc, int documenId, string annotationPath)
        {
            pc.RunMethod("SaveAnnotationFile", documenId, annotationPath);
        }

        /// <summary>
        /// Сохраняет вторичное представление документа.
        /// <br/>
        /// <br/>Метод работает только в режиме изменения объектов. 
        /// <br/>Список ревизий можно получить с помощью метода <see cref="Native_GetSecondaryViewRevisions(INetPluginCall, int)"/>.
        /// <br/>
        /// <br/>Начиная с версии ЛОЦМАН:PLM 2017 каждый следующий вызов метода создает новую корневую ревизию вторичного представления. Предыдущие ревизии остаются доступными для просмотра и аннотирования.
        /// </summary>
        /// <param name="documenId">Идентификатор версии документа.</param>
        /// <param name="filePath">Сетевой путь к файлу вторичного представления, доступный серверу приложений.</param>
        public static void Native_SaveSecondaryView(this INetPluginCall pc, int documenId, string filePath)
        {
            pc.RunMethod("SaveSecondaryView", documenId, filePath);
        }

        /// <summary>
        /// Указывает, что текущий пользователь начал или окончил аннотирование последней доступной корневой ревизии вторичного представления.
        /// <br/>
        /// <br/>Начиная с версии ЛОЦМАН:PLM 2017 допускается аннотирование документа несколькими пользователями одновременно. 
        /// <br/>Получить список пользователей, выполняющих в данный момент аннотирование документа, можно с помощью метода <see cref="Native_GetSecondaryViewRevisions(INetPluginCall, int)"/>.
        /// </summary>
        /// <param name="documenId">Идентификатор версии документа.</param>
        /// <param name="isLock">Флаг аннотирования. Установите в true, чтобы указать, что текущий пользователь начал аннотирование; false – что текущий пользователь окончил аннотирование.</param>
        public static void Native_SetViewLock(this INetPluginCall pc, int documenId, bool isLock)
        {
            pc.RunMethod("SetViewLock", documenId, isLock);
        }

        /// <summary>
        /// Изменяет аннотацию документа.
        /// <br/>
        /// <br/>Метод работает только в режиме базы данных. 
        /// <br/>Метод изменяет существующую ревизию вторичного представления. Список ревизий можно получить с помощью метода <see cref="Native_GetSecondaryViewRevisions(INetPluginCall, int)"/>.
        /// <br/>
        /// <br/>Изменять аннотацию может только тот пользователь, который ее создал.
        /// <br/>Изменять можно только аннотации (корневые ревизии изменять запрещено).
        /// </summary>
        /// <param name="revisionId">Идентификатор ревизии вторичного представления (аннотации), содержимое которой требуется изменить.</param>
        /// <param name="annotationPath">Сетевой путь к файлу аннотации, доступный серверу приложений.</param>
        public static void Native_UpdateAnnotationFile(this INetPluginCall pc, int revisionId, string annotationPath)
        {
            pc.RunMethod("UpdateAnnotationFile", revisionId, annotationPath);
        }
        #endregion

        [Obsolete("Недокументированный метод")]
        /// <summary>
        /// Проверка на существование бизнес объекта в базе Лоцман.
        /// </summary>
        /// <param name="typeName">Название типа</param>
        /// <param name="uniqueId">Ключевой атрибут формата ***BOSimple</param>
        /// <remarks>
        /// Примечание:
        /// <br/>** Для создания бизнес объекта необходимо чтобы product был в формате ***BOSimple
        /// </remarks>
        /// <returns>
        /// <br/>В случае когда бизнес объект отсутствует в Лоцмане:
        /// <br/>&lt;?xml version="1.0" encoding="utf-16"?&gt;
        /// <br/>&lt;PreviewBoObjectResult&gt;
        /// <br/>  &lt;Type&gt;Материал по КД&lt;/Type&gt;
        /// <br/>  &lt;Product&gt;Сталь 12К&lt;/Product&gt;
        /// <br/>  &lt;State&gt;Разрешено к применению&lt;/State&gt;
        /// <br/>  &lt;Attributes&gt;
        /// <br/>    &lt;Attribute Name="Марка материала" Type="0"&gt;Сталь 12К&lt;/Attribute&gt;
        /// <br/>    &lt;Attribute Name="Плотность" Type="2"&gt;7800&lt;/Attribute&gt;
        /// <br/>  &lt;/Attributes&gt;
        /// <br/>&lt;/PreviewBoObjectResult&gt; 
        /// <br/>
        /// <br/>В случае когда бизнес объект существует в Лоцмане:
        /// <br/>&lt;?xml version="1.0" encoding="utf-16"?&gt;
        /// <br/>&lt;PreviewBoObjectResult&gt;
        /// <br/>  &lt;VersionId&gt;179&lt;/VersionId&gt;
        /// <br/>  &lt;Type&gt;Материал по КД&lt;/Type&gt;
        /// <br/>  &lt;Product&gt;Сталь 10 ГОСТ 1050-2013&lt;/Product&gt;
        /// <br/>  &lt;Version /&gt;
        /// <br/>  &lt;State&gt;Разрешено к применению&lt;/State&gt;
        /// <br/>  &lt;Attributes&gt;
        /// <br/>    &lt;Attribute Name="Марка материала" Type="0"&gt;Сталь 10&lt;/Attribute&gt;
        /// <br/>    &lt;Attribute Name="НТД на материал" Type="0"&gt;ГОСТ 1050-2013&lt;/Attribute&gt;
        /// <br/>    &lt;Attribute Name="Плотность" Type="2" Unit="кг/м3"&gt;7856&lt;/Attribute&gt;
        /// <br/>  &lt;/Attributes&gt;
        /// <br/>&lt;/PreviewBoObjectResult&gt; 
        /// </returns>
        public static string Native_PreviewBoObject(this INetPluginCall pc, string typeName, string uniqueId)
        {
            return (string)pc.RunMethod("PreviewBoObject", typeName, uniqueId);
        }

        /// <summary>
        /// Возвращает данные для формирования отчета.
        /// </summary>
        /// <param name="reportName">Название хранимой процедуры.</param>
        /// <param name="objectsIds">Коллекция идентификаторов объектов.</param>
        /// <param name="reportParams">Произвольный набор параметров. Применяется по усмотрению разработчика.</param>
        /// <returns>
        /// Возвращает набор данных с полями, определенными в соответствующей хранимой процедуре.
        /// </returns>
        public static DataTable Native_GetReport(this INetPluginCall pc, string reportName, IEnumerable<int> objectsIds = null, string reportParams = null)
        {
            return pc.GetDataTable("GetReport", reportName, objectsIds, reportParams);
        }

        /// <summary>
        /// Возвращает список объектов, заблокированных в текущем чекауте
        /// </summary>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии.
        /// </returns>
        public static DataTable Native_GetLockedObjects(this INetPluginCall pc)
        {
            return pc.GetDataTable("GetLockedObjects", 0);
        }
        /// <summary>
        /// Помечает объект, находящийся на изменении, как подлежащий удалению при возврате в базу данных.        
        /// </summary>
        /// <param name="id">Идентификатор версии объекта.</param>
        public static void Native_KillVersion(this INetPluginCall pc, int id)
        {
            pc.RunMethod("KillVersionById", id);
        }

        /// <summary>
        /// Помечает объект, находящийся на изменении, как подлежащий удалению при возврате в базу данных.
        /// </summary>
        /// <param name="typeName">Название типа.</param>
        /// <param name="product">Ключевой атрибут.</param>
        /// <param name="version">Версия объекта.</param>
        public static void Native_KillVersion(this INetPluginCall pc, string typeName, string product, string version)
        {
            pc.RunMethod("KillVersion", typeName, product, version);
        }

        /// <summary>
        /// Удаляет объекты.
        /// </summary>
        /// <param name="objectsIds">Список идентификаторов объектов.</param>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_TYPE] int – идентификатор типа объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – версия документа;
        /// <br/>[_ERR_CODE] int – код ошибки;
        /// <br/>[_ERR_MSG] string – сообщение об ошибке.
        /// </returns>
        public static DataTable Native_KillVersions(this INetPluginCall pc, IEnumerable<int> objectsIds)
        {
            return pc.GetDataTable("KillVersions", string.Join(Constants.ID_SEPARATOR, objectsIds), 0);
        }

        /// <summary>
        /// Берет объект на редактирование.
        /// </summary>
        /// <param name="typeName">Название типа объекта.</param>
        /// <param name="product">Ключевой атрибут объекта.</param>
        /// <param name="version">Номер версии объекта.</param>
        /// <param name="mode">Определяет дополнительные опции при взятии объекта на изменение</param>
        /// <returns>
        /// Внутреннее название редактируемого объекта (название чекаута).
        /// </returns>
        public static string Native_CheckOut(this INetPluginCall pc, string typeName, string product, string version, CheckOutMode mode = CheckOutMode.Default)
        {
            return (string)pc.RunMethod("CheckOut", typeName, product, version, (int)mode);
        }

        /// <summary>
        /// Делает объект доступным для изменения только в рамках текущего чекаута (блокирует его).
        /// </summary>
        /// <param name="objectId">Идентификатор объекта. </param>
        /// <param name="isRoot">Помещать или не помещать объект в качестве головного объекта чекаута.</param>
        public static void Native_AddToCheckOut(this INetPluginCall pc, int objectId, bool isRoot = false)
        {
            pc.RunMethod("AddToCheckOut", objectId, isRoot);
        }

        /// <summary>
        /// Осуществляет подключение к редактируемому объекту (чекауту).
        /// </summary>
        /// <param name="checkOutName">Название чекаута.</param>
        /// <param name="dBName">Название базы данных, к которой нужно подключиться.</param>
        public static void Native_ConnectToCheckOut(this INetPluginCall pc, string checkOutName, string dBName)
        {
            pc.RunMethod("ConnectToCheckOut", checkOutName, dBName);
        }

        /// <summary>
        /// Отключается от редактируемого объекта (чекаута).
        /// </summary>
        /// <param name="checkOutName">Название чекаута.</param>
        /// <param name="dBName">Название базы данных, к которой нужно подключиться.</param>
        public static void Native_DisconnectCheckOut(this INetPluginCall pc, string checkOutName, string dBName)
        {
            pc.RunMethod("DisconnectCheckOut", checkOutName, dBName);
        }

        /// <summary>
        /// Возвращает измененный объект в базу данных.
        /// </summary>
        /// <param name="checkOutName">Название чекаута.</param>
        /// <param name="dBName">Название базы данных, к которой нужно подключиться.</param>
        /// <returns>
        /// Если один или несколько файлов невозможно сохранить в базе данных, работа метода завершается ошибкой с кодом 1030. При этом метод возвращает набор данных с информацией о файлах, которые не удалось сохранить в базе. 
        /// <br/>[_ID_VERSION] int – идентификатор версии;
        /// <br/>[_ID_FILE] int – идентификатор файла;
        /// <br/>[_NAME] string – имя файла;
        /// <br/>[_LOCALNAME] string – путь к файлу относительно рабочего диска;
        /// <br/>[_CACHED] boolean – флаг – установлен в true, если файл находится в кеше версий;
        /// <br/>[_NEW] boolean – флаг – установлен в true, если файл в рабочем проекте новый;
        /// <br/>[_ERROR] string – сообщение об ошибке;
        /// <br/>[_ERRORCODE] int – код ошибки (поддерживается начиная с ЛОЦМАН:PLM 2014).
        /// </returns>
        public static DataTable Native_CheckIn(this INetPluginCall pc, string checkOutName, string dBName)
        {
            return pc.GetDataTable("CheckIn", checkOutName, dBName);
        }

        /// <summary>
        /// Сохраняет изменения в базе данных.
        /// </summary>
        /// <param name="checkOutName">Название чекаута.</param>
        /// <param name="dBName">Название базы данных, к которой нужно подключиться.</param>
        /// <returns>
        /// В случае, если один или несколько файлов невозможно сохранить в базе данных, работа метода завершается ошибкой с кодом 1030. При этом метод возвращает набор данных с информацией о файлах, которые не удалось сохранить в базе. 
        /// <br/>[_ID_VERSION] int – идентификатор версии;
        /// <br/>[_ID_FILE] int – идентификатор файла;
        /// <br/>[_NAME] string – имя файла;
        /// <br/>[_LOCALNAME] string – путь к файлу относительно рабочего диска;
        /// <br/>[_CACHED] boolean – флаг – установлен в true, если файл находится в кеше версий;
        /// <br/>[_NEW] boolean – флаг – установлен в true, если файл в рабочем проекте новый;
        /// <br/>[_ERROR] string – сообщение об ошибке;
        /// <br/>[_ERRORCODE] int – код ошибки (поддерживается начиная с ЛОЦМАН:PLM 2014).
        /// </returns>
        /// <remarks>
        /// <br/> После выполнения этой операции все изменения, произведенные в чекауте, сохраняются в базе данных.
        ///  Объект по-прежнему остается на изменении.
        /// </remarks>
        public static DataTable Native_SaveChanges(this INetPluginCall pc, string checkOutName, string dBName)
        {
            return pc.GetDataTable("SaveChanges", checkOutName, dBName);
        }

        /// <summary>
        /// Выполняет отказ от изменения объекта.
        /// </summary>
        /// <param name="checkOutName">Название чекаута.</param>
        /// <param name="dBName">Название базы данных, к которой нужно подключиться.</param>
        public static void Native_CancelCheckOut(this INetPluginCall pc, string checkOutName, string dBName)
        {
            pc.RunMethod("CancelCheckOut", checkOutName, dBName);
        }

    }
}
