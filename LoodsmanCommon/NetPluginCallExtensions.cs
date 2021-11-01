using Ascon.Plm.Loodsman.PluginSDK;
using System.Collections.Generic;
using System.Data;

namespace LoodsmanCommon
{
    public static class NetPluginCallExtensions
    {

        ///<summary>
        /// Позволяет получить список объектов, выделенных в дереве «ЛОЦМАН Клиент».
        /// </summary>
        /// <returns>Список идентификаторов выделенных в дереве объектов с разделителем «,».</returns>
        public static string Native_CGetTreeSelectedIDs(this INetPluginCall pc) => 
            pc.RunMethod("CGetTreeSelectedIDs") as string ?? string.Empty;

        /// <summary>
        /// Возвращает признак того, является ли пользователь, подключенный к текущей базе данных, администратором этой базы.
        /// </summary>
        /// <returns>
        /// <br/>true – пользователь является администратором.
        /// <br/>false – пользователь не является администратором.
        /// </returns>
        public static bool Native_IsAdmin(this INetPluginCall pc) => 
            pc.RunMethod("IsAdmin") as int? == 1;

        /// <summary>
        /// ** Недокументированный метод.
        /// <br/>Возвращает измеряемые сущности. 
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_GUID] string -  уникальный идентификатор сущности;
        /// <br/>[_DISPLAY] string - отображаемое имя сущности;
        /// <br/>[_TAG] int - всегда равен -1.
        /// </summary>
        public static DataTable Native_GetFromBO_Nature(this INetPluginCall pc) => 
            pc.GetDataTable("GetFromBO_Nature");

        /// <summary>
        /// Возвращает список возможных единиц измерения для измеряемой сущности. 
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID_UNIT] string – уникальный идентификатор единицы измерения;
        /// <br/>[_NAME] string – название единицы измерения;
        /// <br/>[_BASICUNIT] int – признак базовой единицы измерения (1 – является базовой, 0 – не является базовой).
        /// </summary>
        /// <param name="measureGuid">Идентификатор измеряемой сущности</param>
        public static DataTable Native_GetMUnitList(this INetPluginCall pc, string measureGuid) => 
            pc.GetDataTable("GetMUnitList", measureGuid);

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
        public static double Native_ConverseValue(this INetPluginCall pc, double value, string sourceMeasureGuid, string destMeasureGuid) => 
            (double)pc.RunMethod("ConverseValue", value, sourceMeasureGuid, destMeasureGuid);

        /// <summary>
        /// Возвращает информацию о текущем пользователе.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_NAME] string – имя пользователя;
        /// <br/>[_FULLNAME] string – полное имя пользователя;
        /// <br/>[_MAIL] string – адрес электронной почты пользователя;
        /// <br/>[_USERDIR] string – рабочая папка для проектов пользователя; 
        /// <br/>[_FILEDIR] string – папка для хранения файлов пользователя. 
        /// </summary>
        public static DataTable Native_GetInfoAboutCurrentUser(this INetPluginCall pc) => 
            pc.GetDataTable("GetInfoAboutCurrentUser");

        /// <summary>
        /// Возвращает список доступных типов, включая абстрактные.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
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
        /// </summary>
        public static DataTable Native_GetTypeListEx(this INetPluginCall pc) => 
            pc.GetDataTable("GetTypeListEx");

        /// <summary>
        /// Возвращает список возможных типов связей с указанием связываемых типов.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:      
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
        /// </summary>
        public static DataTable Native_GetLinkListEx(this INetPluginCall pc) => 
            pc.GetDataTable("GetLinkListEx");

        /// <summary>
        /// Возвращает список возможных типов связей.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID] int – уникальный идентификатор типа связи;
        /// <br/>[_NAME] string – название типа связи;
        /// <br/>[_INVERSENAME] string – название обратной связи;
        /// <br/>[_TYPE] int – вид связи (1– горизонтальная, 0 – вертикальная);
        /// <br/>[_ICON] image – значок типа связи;
        /// <br/>[_ORDER] int – порядок следования типа связи.
        /// </summary>
        public static DataTable Native_GetLinkList(this INetPluginCall pc) => 
            pc.GetDataTable("GetLinkList");

        /// <summary>
        /// Возвращает список возможных состояний.
        /// </summary>
        /// <returns>
        /// Возвращает набор данных с полями:
        /// <br/>[_ID] int – уникальный идентификатор состояния;
        /// <br/>[_NAME] string – название состояния;
        /// <br/>[_ICON] image – значок состояния.
        /// </returns>
        public static DataTable Native_GetStateList(this INetPluginCall pc) => 
            pc.GetDataTable("GetStateList");

        /// <summary>
        /// Изменяет состояние объекта.
        /// </summary>
        /// <inheritdoc cref="Native_KillVersion(INetPluginCall, string, string, string)"/>
        /// <param name="stateName">Название состояния.</param>
        public static void Native_UpdateStateOnObject(this INetPluginCall pc, string typeName, string product, string version, string stateName) => 
            pc.RunMethod("UpdateStateOnObject", typeName, product, version, stateName);

        /// <inheritdoc cref="Native_UpdateStateOnObject(INetPluginCall, string, string, string, string)"/>
        /// <inheritdoc cref="Native_KillVersion(INetPluginCall, int)"/>
        public static void Native_UpdateStateOnObject(this INetPluginCall pc, int objectId, string stateName) => 
            pc.RunMethod("UpdateStateOnObjectById", objectId, stateName);

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
        public static DataTable Native_GetAttributeList(this INetPluginCall pc, GetAttributeListMode mode = GetAttributeListMode.All) => 
            pc.GetDataTable("GetAttributeList2", mode);

        /// <summary>
        /// Возвращает случаи использования прокси, типа объекта и типа документа.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
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
        /// </summary>
        /// <param name="proxyId">Идентификатор прокси. Может быть передано значение 0, что означает «любое значение».</param>
        /// <param name="typeId">Идентификатор типа объекта. Может быть передано значение 0, что означает «любое значение».</param>
        /// <param name="documentId">Идентификатор типа документа. Может быть передано значение 0, что означает «любое значение».</param>
        public static DataTable Native_GetProxyUseCases(this INetPluginCall pc, int proxyId = 0, int typeId = 0, int documentId = 0) => 
            pc.GetDataTable("GetProxyUseCases", proxyId, typeId, documentId);

        /// <summary>
        /// Возвращает список версий, привязанных соответствующей связью.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
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
        /// </summary>
        /// <inheritdoc cref="Native_KillVersion(INetPluginCall, int)"/>
        /// <param name="linkType">Тип связи.</param>
        /// <param name="inverse">Направление (true – обратное, false – прямое).</param>
        public static DataTable Native_GetLinkedFast(this INetPluginCall pc, int objectId, string linkType, bool inverse = false) => 
            pc.GetDataTable("GetLinkedFast", objectId, linkType, inverse);

        /// <summary>
        /// Возвращает список версий, привязанных соответствующей связью.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_LINK] int – уникальный идентификатор экземпляра связи;
        /// <br/>[_ID_TYPE] int – идентификатор типа объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_ID_STATE] int – идентификатор текущего состояния объекта;
        /// <br/>[_ID_LOCK] int – идентификатор чекаута, в котором блокирован объект (если объект не блокирован, то вернется null);
        /// <br/>[_DOCUMENT] int – является ли документом (1 – является, 0 – не является);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись, 3 – Полный доступ).
        /// </summary>
        /// <inheritdoc cref="Native_GetLinkedFast(INetPluginCall, int, string, bool)"/>
        public static DataTable Native_GetLinkedFast2(this INetPluginCall pc, int objectId, string linkType, bool inverse = false) => 
            pc.GetDataTable("GetLinkedFast2", objectId, linkType, inverse);

        /// <summary>
        /// Возвращает список связанных объектов для отображения в дереве.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
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
        /// </summary>
        /// <inheritdoc cref="Native_KillVersion(INetPluginCall, string, string, string)"/>
        /// <param name="linkTypeNames">Список связей.</param>
        /// <param name="withAttributes">Возвращать или не возвращать атрибуты.</param>
        public static DataTable Native_GetTree(this INetPluginCall pc, string typeName, string product, string version, IEnumerable<string> linkTypeNames, bool withAttributes = false) => 
            pc.GetDataTable("GetTree", typeName, product, version, 0, string.Join(Constants.LINK_SEPARATOR, linkTypeNames), withAttributes);

        /// <inheritdoc cref="Native_GetTree(INetPluginCall, string, string, string, IEnumerable{string}, bool)"/>
        /// <inheritdoc cref="Native_KillVersion(INetPluginCall, int)"/>
        public static DataTable Native_GetTree(this INetPluginCall pc, int objectId, IEnumerable<string> linkTypeNames, bool withAttributes = false) => 
            pc.GetDataTable("GetTree", string.Empty, string.Empty, string.Empty, objectId, string.Join(Constants.LINK_SEPARATOR, linkTypeNames), withAttributes);

        /// <summary>
        /// Возвращает список связанных объектов для отображения в дереве.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_LINK] int – уникальный идентификатор экземпляра связи;
        /// <br/>[_ID_TYPE] int – идентификатор типа объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_ID_STATE] int – идентификатор текущего состояния объекта;
        /// <br/>[_ID_LINKTYPE] int – идентификатор типа связи, которым привязан объект;
        /// <br/>[_ID_LOCK] int – идентификатор чекаута, в котором блокирован объект (если объект не блокирован, то вернется null);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись, 3 – Полный доступ).
        /// </summary>
        /// <inheritdoc cref="Native_GetTree(INetPluginCall, int, IEnumerable{string}, bool)"/>
        public static DataTable Native_GetTree2(this INetPluginCall pc, int objectId, IEnumerable<string> linkTypeNames, bool withAttributes = false) => 
            pc.GetDataTable("GetTree2", objectId, string.Join(Constants.LINK_SEPARATOR, linkTypeNames), withAttributes ? 1 : 0);

        /// <summary>
        /// Возвращает информацию о связанных объектах для группы объектов.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_LINK] int – уникальный идентификатор экземпляра связи;
        /// <br/>[_ID_TYPE] int – идентификатор типа объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_ID_STATE] int – идентификатор текущего состояния объекта;
        /// <br/>[_ID_LINKTYPE] int – идентификатор типа связи, которым привязан объект;
        /// <br/>[_ID_LOCK] int – идентификатор чекаута, в котором блокирован объект(если объект не блокирован, то вернется null);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись; 3 – Полный доступ).
        /// </summary>
        /// <inheritdoc cref="Native_GetLinkedFast(INetPluginCall, int, string, bool)"/>
        public static DataTable Native_GetLObjs(this INetPluginCall pc, int objectId, bool inverse = false) => 
            pc.GetDataTable("GetLObjs", objectId, inverse);

        /// <summary>
        /// Возвращает список связанных объектов.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_LINK] int – уникальный идентификатор экземпляра связи (см. примечание);
        /// <br/>[_TYPE] string – тип объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_STATE] string – текущее состояние объекта;
        /// <br/>[_MIN_QUANTITY] double – нижняя граница количества;
        /// <br/>[_MAX_QUANTITY] double – верхняя граница количества;
        /// <br/>[_DOCUMENT] int – является ли документом (1 – является, 0 – не является);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись, 3 – Полный доступ);
        /// <br/>[_LOCKED] int – уровень блокировки объекта (0 – не блокирован, 1 – блокирован текущим пользователем, 2 – блокирован другим пользователем);
        /// <br/>[_ID_UNIT] string – идентификатор единицы измерения, в которой задавали количество.
        /// </summary>
        /// <inheritdoc cref="Native_KillVersion(INetPluginCall, string, string, string)"/>
        /// <inheritdoc cref="Native_GetLObjs(INetPluginCall, int, bool)"/>
        /// <param name="fullLink">Признак полной разузловки.</param>
        /// <param name="groupByProduct">Признак группировки по изделиям (для случая полной разузловки), если true дополнительно возвращается поле [_ASSEMBLY] string.</param>
        public static DataTable Native_GetLinkedObjects(this INetPluginCall pc, string typeName, string product, string version, string linkType, bool inverse, bool fullLink, bool groupByProduct) => 
            pc.GetDataTable("GetLinkedObjects", typeName, product, version, linkType, inverse, fullLink, groupByProduct, false);

        /// <param name="linkTypeNames">Список связей.</param>
        /// <inheritdoc cref="Native_GetLinkedObjects(INetPluginCall, string, string, string, string, bool, bool, bool)"/>        
        public static DataTable Native_GetLinkedObjects(this INetPluginCall pc, string typeName, string product, string version, IEnumerable<string> linkTypeNames) =>
            pc.GetDataTable("GetLinkedObjects2", typeName, product, version, string.Join(Constants.LINK_SEPARATOR, linkTypeNames), false, false, false, true);

        /// <summary>
        /// Возвращает список связанных объектов
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
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
        /// </summary>
        /// <inheritdoc cref="Native_GetLinkedObjects(INetPluginCall, string, string, string, string, bool, bool, bool)"/>
        public static DataTable Native_GetLinkedObjects(this INetPluginCall pc, int objectId, string linkType, bool inverse, bool fullLink, bool groupByProduct) => 
            pc.GetDataTable("GetLinkedObjects2", objectId, linkType, inverse, fullLink, groupByProduct, false);

        /// <param name="linkTypeNames">Список связей.</param>
        /// <inheritdoc cref="Native_GetLinkedObjects(INetPluginCall, int, string, bool, bool, bool)"/>        
        public static DataTable Native_GetLinkedObjects(this INetPluginCall pc, int objectId, IEnumerable<string> linkTypeNames) => 
            pc.GetDataTable("GetLinkedObjects2", objectId, string.Join(Constants.LINK_SEPARATOR, linkTypeNames), false, false, false, true);

        /// <summary>
        /// Возвращает информацию о связанных объектах для группы объектов.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
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
        /// </summary>
        /// <inheritdoc cref="Native_KillVersions(INetPluginCall, IEnumerable{int})"/>
        /// <param name="linkTypeIds">Список идентификаторов связей.</param>
        /// <param name="inverse">Направление (true – обратное, false – прямое).</param>
        public static DataTable Native_GetLinkedObjectsForObjects(this INetPluginCall pc, IEnumerable<int> objectsIds, IEnumerable<int> linkTypeIds, bool inverse = false) => 
            pc.GetDataTable("GetLinkedObjectsForObjects", string.Join(Constants.ID_SEPARATOR, objectsIds), string.Join(Constants.ID_SEPARATOR, linkTypeIds), inverse);

        /// <summary>
        /// Возвращает информацию об экземпляре связи.
        /// </summary>
        /// <param name="idLink">Идентификатор экземпляра связи. Может быть получен методом <see cref="Native_GetLinkedObjects(INetPluginCall, string, string, string, string, bool, bool, bool, bool)">GetLinkedObjects</see>.</param>
        /// <param name="mode">Режим вывода. В зависимости от его значения выдается соответствующая информация.</param>
        /// <returns>Зависит от режима <see cref="GetInfoAboutLinkMode"/></returns>
        public static DataTable Native_GetInfoAboutLink(this INetPluginCall pc, int idLink, GetInfoAboutLinkMode mode) => 
            pc.GetDataTable("GetInfoAboutLink", idLink, (int)mode);

        /// <summary>
        /// Возвращает информацию о типе.
        /// </summary>
        /// <param name="typeName">Название типа.</param>
        /// <param name="mode">Режим вывода. В зависимости от его значения выдается соответствующая информация.</param>
        /// <returns>Зависит от режима <see cref="GetInfoAboutTypeMode"/></returns>
        public static DataTable Native_GetInfoAboutType(this INetPluginCall pc, string typeName, GetInfoAboutTypeMode mode) => 
            pc.GetDataTable("GetInfoAboutType", typeName, (int)mode);

        /// <summary>
        /// Возвращает информацию о версии объекта.
        /// </summary>
        /// <inheritdoc cref="Native_KillVersion(INetPluginCall, int)"/>
        /// <param name="mode">Режим вывода. В зависимости от его значения выдается соответствующая информация.</param>
        /// <returns>Зависит от режима <see cref="GetInfoAboutVersionMode"/></returns>
        public static DataTable Native_GetInfoAboutVersion(this INetPluginCall pc, int objectId, GetInfoAboutVersionMode mode) => 
            pc.GetDataTable("GetInfoAboutVersion", string.Empty, string.Empty, string.Empty, objectId, (int)mode);

        /// <inheritdoc cref="Native_GetInfoAboutVersion(INetPluginCall, int, GetInfoAboutVersionMode)"/>
        /// <inheritdoc cref="Native_KillVersion(INetPluginCall, string, string, string)"/>
        public static DataTable Native_GetInfoAboutVersion(this INetPluginCall pc, string typeName, string product, string version, GetInfoAboutVersionMode mode) => 
            pc.GetDataTable("GetInfoAboutVersion", typeName, product, version, 0, (int)mode);

        /// <summary>
        /// Возвращает свойства объектов.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_TYPE] string – тип объекта; 
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_STATE] string – текущее состояние объекта;
        /// <br/>[_DOCUMENT] int – является ли документом (1 – является, 0 – не является);
        /// <br/>[_ACCESSLEVEL] int – уровень доступа к объекту (1 – Только чтение, 2 – Чтение/запись, 3 – Полный доступ);
        /// <br/>[_LOCKED] int – уровень блокировки объекта (0 – не блокирован, 1 – блокирован текущим пользователем, 2 – блокирован другим пользователем).
        /// </summary>
        /// <inheritdoc cref="Native_KillVersions(INetPluginCall, IEnumerable{int})"/>
        public static DataTable Native_GetPropObjects(this INetPluginCall pc, IEnumerable<int> objectsIds) => 
            pc.GetDataTable("GetPropObjects", string.Join(Constants.ID_SEPARATOR, objectsIds), 0);

        /// <summary>
        /// Создает новый объект.
        /// </summary>
        /// <inheritdoc cref="Native_UpdateStateOnObject(INetPluginCall, string, string, string, string)"/>
        /// <param name="isProject">Признак того, что объект будет являться проектом (true – будет проектом, false – не будет проектом).</param>
        /// <returns>Возвращает идентификатор созданной версии.</returns>
        public static int Native_NewObject(this INetPluginCall pc, string typeName, string stateName, string product, bool isProject = false) => 
            (int)pc.RunMethod("NewObject", typeName, stateName, product, isProject ? 1 : 0);

        /// <summary>
        /// Вставляет новое или существующее изделие в редактируемый объект.
        /// </summary>
        /// <param name="parentTypeName">Наименование типа объекта-родителя.</param>
        /// <param name="parentProduct">Значение ключевого атрибута объекта-родителя.</param>
        /// <param name="parentVersion">Номер версии объекта-родителя. Если равен #32, то будет осуществлена попытка создать объект типа <paramref name="parentTypeName"/> с ключевым атрибутом <paramref name="parentProduct"/>.</param>
        /// <param name="linkType">Тип связи, которой нужно связать объекты.</param>
        /// <param name="childTypeName">Наименование типа объекта-потомка.</param>
        /// <param name="childProduct">Значение ключевого атрибута объекта-потомка.</param>
        /// <param name="childVersion">Номер версии объекта-потомка. Если равен #32, то будет осуществлена попытка создать объект типа <paramref name="childTypeName"/> с ключевым атрибутом <paramref name="childProduct"/></param>
        /// <param name="stateName">Состояние созданного объекта (имеет смысл только если <paramref name="parentVersion"/> или <paramref name="childVersion"/> равен #32).</param>
        /// <param name="reuse">Устанавливает возможность повторного применения объекта. 
        /// <br/>Если значение – true, то независимо от существования в базе данных такой пары связанных объектов будет создаваться еще один экземпляр связи.
        /// <br/>Если значение – false, то при существовании в базе данных такой пары связанных объектов новый экземпляр связи создаваться не будет, при этом код возврата inReturnCode будет иметь значение, отличное от нуля.
        /// </param>
        /// <returns>Возвращает идентификатор созданной связи.</returns>
        public static int Native_InsertObject(this INetPluginCall pc, string parentTypeName, string parentProduct, string parentVersion, string linkType, string childTypeName, string childProduct, string childVersion, string stateName, bool reuse) => 
            (int)pc.RunMethod("InsertObject", parentTypeName, parentProduct, parentVersion, childTypeName, childProduct, childVersion, linkType, stateName, reuse);

        /// <summary>
        /// Добавляет связь между объектами.
        /// </summary>
        /// <param name="parentId">Идентификатор версии объекта-родителя.</param>
        /// <param name="childId">Идентификатор версии объекта-потомка.</param>
        /// <inheritdoc cref="Native_UpLink(INetPluginCall, string, string, string, string, string, string, int, double, double, string, bool, string)"/>
        public static int Native_NewLink(this INetPluginCall pc, int parentId, string parentTypeName, string parentProduct, string parentVersion, int childId, string childTypeName, string childProduct, string childVersion, double minQuantity, double maxQuantity, string unitId, string linkType) => 
            (int)pc.RunMethod("NewLink", parentId, parentTypeName, parentProduct, parentVersion, childId, childTypeName, childProduct, childVersion, minQuantity, maxQuantity, unitId, linkType);

        /// <summary>
        /// Добавляет, удаляет, обновляет связь между объектами.
        /// </summary>
        /// <param name="parentVersion">Номер версии объекта-родителя.</param>
        /// <param name="childVersion">Номер версии объекта-потомка.</param>
        /// <param name="idLink">Идентификатор связи. </param>
        /// <param name="minQuantity">Нижняя граница количества</param>
        /// <param name="maxQuantity">Верхняя граница количества.</param>
        /// <param name="unitId">Уникальный идентификатор единицы измерения.</param>
        /// <param name="toDel">Признак удаления экземпляра связи (true – удалять, false – не удалять)</param>
        /// <inheritdoc cref="Native_InsertObject(INetPluginCall, string, string, string, string, string, string, string, string, bool)"/>
        /// <returns>Возвращает идентификатор созданной связи.</returns>
        /// <remarks>
        /// <br/>Метод работает следующим образом: 
        /// <br/>Если <paramref name="toDel"/>=true, то заданный экземпляр связи с идентификатором <paramref name="idLink"/> удаляется;
        /// <br/>Если экземпляр связи с идентификатором <paramref name="idLink"/> существует, то его свойства (количество, единицы измерения) будут изменены;
        /// <br/>Если <paramref name="idLink"/>=0, то будет добавлена связь между объектами, указанными в качестве родителя и потомка.
        /// </remarks>
        public static int Native_UpLink(this INetPluginCall pc, string parentTypeName, string parentProduct, string parentVersion, string childTypeName, string childProduct, string childVersion, int idLink, double minQuantity, double maxQuantity, string unitId, bool toDel, string linkType) => 
            (int)pc.RunMethod("UpLink", parentTypeName, parentProduct, parentVersion, childTypeName, childProduct, childVersion, idLink, minQuantity, maxQuantity, unitId, toDel, linkType);

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
        /// <br/>[_LOCKID] int – «область видимости» объекта 
        /// <br/>0 – объект существует в базе данных и доступен; 
        /// <br/>>0 – объект создан в чекауте, но еще не сохранен в базе данных, поэтому он доступен только в рамках этого чекаута;
        /// <br/>[_USERNAME] string – имя пользователя, заблокировавшего объект.
        /// </returns>
        public static DataTable Native_CheckUniqueName(this INetPluginCall pc, string typeName, string product) => 
            pc.GetDataTable("CheckUniqueName", typeName, product);

        /// <summary>
        /// **Недокументированный метод.
        /// <br/>** Для создания бизнес объекта необходимо чтобы product был в формате (***BOSimple...).
        /// <br/>Проверка на существование бизнес объекта в базе Лоцман.
        /// <br/>
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
        /// </summary>
        /// <param name="typeName">Название типа.</param>
        /// <param name="uniqueId">Ключевой атрибут формата (***BOSimple...).</param>
        public static string Native_PreviewBoObject(this INetPluginCall pc, string typeName, string uniqueId) =>
            (string)pc.RunMethod("PreviewBoObject", typeName, uniqueId);

        /// <summary>
        /// Возвращает список объектов, заблокированных в текущем чекауте.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии.
        /// </summary>
        public static DataTable Native_GetLockedObjects(this INetPluginCall pc) =>
            pc.GetDataTable("GetLockedObjects", 0);


        #region Связанные объекты

        /// <summary>
        /// Возвращает список связанных объектов и их файлов.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
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
        /// <br/>[_LOCKED] int – уровень блокировки объекта (0 – не блокирован, 1 – блокирован текущим пользователем, 2 – блокирован другим пользователем); 
        /// <br/>[_ID_FILE] int – уникальный идентификатор файла в системе; 
        /// <br/>[_NAME] string – имя файла; 
        /// <br/>[_LOCALNAME] string – путь к файлу относительно диска из настройки «Буква рабочего диска»; 
        /// <br/>[_DATEOFCREATE] datetime – дата и время создания файла; 
        /// <br/>[_SIZE] int – размер файла; 
        /// <br/>[_CRC] long – контрольная сумма содержимого файла;
        /// <br/>[_MODIFIED] datetime – дата последнего изменения файла.
        /// </summary>
        /// <param name="typeName">Название типа.</param>
        /// <param name="product">Ключевой атрибут.</param>
        /// <param name="version">Версия объекта.</param>
        /// <param name="linkType">Тип связи.</param>
        /// <param name="fullLink">Признак полной разузловки.</param>
        /// <remarks>
        /// <br/>Записи приходят в виде:
        /// <br/>Документ1
        /// <br/>Файл1
        /// <br/>Документ2
        /// <br/>Файл1
        /// <br/>Файл2
        /// <br/>где:
        /// <br/>Документ – это запись, описывающая документ. В этой записи значимы только те поля, которые относятся к документу:
        /// <br/>[_ID_VERSION],[_ID_LINK], [_TYPE], [_PRODUCT], [_VERSION], [_STATE], [_MIN_QUANTITY], [_MAX_QUANTITY], [_DOCUMENT], [_ACCESSLEVEL], [_LOCKED];
        /// <br/>Файл – это запись, описывающая файл. В этой записи значимы только те поля, которые относятся к файлу:
        /// <br/>[_ID_FILE], [_NAME], [_LOCALNAME], [_DATEOFCREATE], [_DATEOFCREATE], [_SIZE], [_CRC], [_MODIFIED].
        /// </remarks>
        public static DataTable Native_GetLinkedObjectsAndFiles(this INetPluginCall pc, string typeName, string product, string version, string linkType, bool fullLink) =>
            pc.GetDataTable("GetLinkedObjectsAndFiles", typeName, product, version, linkType, fullLink, false);
        #endregion

        #region Работа с файлами

        /// <summary>
        /// Возвращает все версии файлов с указанным именем (включая новые, измененные и удаленные) для всех пользователей базы данных.  
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID_FILE] integer – идентификатор файла;
        /// <br/>[_ID_DOCUMENT] integer – идентификатор документа;
        /// <br/>[_TYPE] string – тип документа;
        /// <br/>[_PRODUCT] string – ключевой атрибут документа;
        /// <br/>[_VERSION] string – версия документа;
        /// <br/>[_OLD_NAME] string – имя файла на момент последнего сохранения в рабочем проекте;
        /// <br/>[_OLD_LOCALNAME] string – путь к файлу на момент последнего сохранения в рабочем проекте;
        /// <br/>[_OLD_SIZE] integer – размер файла на момент последнего сохранения в рабочем проекте;
        /// <br/>[_OLD_CRC] integer – контрольная сумма файла на момент последнего сохранения в рабочем проекте;
        /// <br/>[_NEW_NAME] string – текущее имя файла;
        /// <br/>[_NEW_LOCALNAME] string – текущий путь к файлу;
        /// <br/>[_DATEOFCREATE] datetime – дата создания файла;
        /// <br/>[_MODIFIED] datetime – дата последней модификации файла;
        /// <br/>[_ID_LOCK] integer – идентификатор рабочего проекта или NULL;
        /// <br/>[_DONE] integer – состояние файла: 0 – новый, 1 – измененный; 2 – удален из рабочего проекта;
        /// <br/>[_USERNAME] string – имя пользователя;
        /// <br/>[_FULLNAME] string – полное имя пользователя.
        /// </summary>
        /// <param name="fileName">Название файла. Может быть получено методами <see cref="Native_GetInfoAboutVersion">GetInfoAboutVersion</see> (режим <see cref="GetInfoAboutVersionMode.Mode7">режим 7</see>), <see cref="Native_GetLinkedObjectsAndFiles">GetLinkedObjectsAndFiles</see>.</param>
        /// <param name="filePath">Путь к файлу относительно диска из настройки «Буква рабочего диска». Может быть получено методами <see cref="Native_GetInfoAboutVersion">GetInfoAboutVersion</see> (режим <see cref="GetInfoAboutVersionMode.Mode7">режим 7</see>), <see cref="Native_GetLinkedObjectsAndFiles">GetLinkedObjectsAndFiles</see>.</param>
        /// <remarks>
        /// <br/>
        /// <br/>Название и путь файла, могут быть получены методами:
        /// <br/><see cref="Native_GetInfoAboutVersion">GetInfoAboutVersion</see> (режим <see cref="GetInfoAboutVersionMode.Mode7">режим 7</see>), <see cref="Native_GetLinkedObjectsAndFiles">GetLinkedObjectsAndFiles</see>.
        /// </remarks>
        public static DataTable Native_CheckFileNameEx(this INetPluginCall pc, string fileName, string filePath) => 
            pc.GetDataTable("CheckFileNameEx", fileName, filePath);

        /// <summary>
        /// Извлекает файл из базы данных (для просмотра).
        /// </summary>
        /// <returns>
        /// Возвращает полное имя файла с путем. Например: result='\\Server\DOMEN#USER\Temp\FileName.ext'.
        /// </returns>
        /// <inheritdoc cref="Native_GetFile(INetPluginCall, string, string, string, string, string)"/>
        /// <param name="restoreFullFilePath">Файл копируется в зарезервированную временную папку пользователя. (true – с восстановлением относительного пути к файлу, false – в корень)</param>
        public static string Native_ExtractFile(this INetPluginCall pc, string typeName, string product, string version, string fileName, string filePath, bool restoreFullFilePath = false) => 
            pc.RunMethod("ExtractFile", typeName, product, version, 0, fileName, filePath, restoreFullFilePath ? 1 : 0) as string;

        /// <inheritdoc cref="Native_ExtractFile(INetPluginCall, string, string, string, string, string, bool)"/>
        /// <param name="documentId">Идентификатор версии документа.</param>
        public static string Native_ExtractFile(this INetPluginCall pc, int documentId, string fileName, string filePath, bool restoreFullFilePath = false) => 
            pc.RunMethod("ExtractFile", string.Empty, string.Empty, string.Empty, documentId, fileName, filePath, restoreFullFilePath ? 1 : 0) as string;

        /// <summary>
        /// Возвращает информацию о файле.
        /// <br/> 
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_TYPE] string – тип объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – номер версии объекта;
        /// <br/>[_ID_FILE] int – уникальный идентификатор файла в системе;
        /// <br/>[_NAME] string – имя файла;
        /// <br/>[_LOCALNAME] string – путь к файлу относительно диска из настройки «Буква рабочего диска»;
        /// <br/>[_DATEOFCREATE] datetime – дата и время создания файла;
        /// <br/>[_SIZE] int –размер файла;
        /// <br/>[_CRC] long – контрольная сумма содержимого файла;
        /// <br/>[_MODIFIED] datetime – дата последнего изменения файла;
        /// <br/>[_READONLY] int – зарезервировано.
        /// </summary>
        /// <inheritdoc cref="Native_CheckFileNameEx(INetPluginCall, string, string)"/>
        public static DataTable Native_GetInfoAboutFile(this INetPluginCall pc, string fileName, string filePath) => 
            pc.GetDataTable("GetInfoAboutFile", fileName, filePath);

        /// <summary>
        /// Возвращает информацию о файлах, связанных с объектами.
        /// <br/> 
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID_DOCUMENT] int – идентификатор объекта (документа, т. к. в ЛОЦМАН:PLM файлы могут быть связаны только с документам);
        /// <br/>[_ID_FILE] int – идентификатор файла;
        /// <br/>[_NAME] string – имя файла;
        /// <br/>[_LOCALNAME] string – путь к файлу относительно диска из настройки «Буква рабочего диска»;
        /// <br/>[_DATEOFCREATE] datetime – дата и время создания файла;
        /// <br/>[_SIZE] int – размер файла (если метод вызывается в контексте рабочего проекта, в котором файл выгружался на рабочий диск, то возвращается размер файла на рабочем диске; в противном случае – размер файла на момент последнего сохранения в рабочем проекте);
        /// <br/>[_READONLY] int – зарезервировано;
        /// <br/>[_CRC] long – контрольная сумма содержимого файла на момент последнего сохранения в рабочем проекте;
        /// <br/>[_MODIFIED] datetime – дата последнего изменения файла (если метод вызывается в контексте рабочего проекта, в котором файл выгружался на рабочий диск, то возвращается дата последнего изменения файла на рабочем диске, в противном случае – дата последнего изменения файла на момент последнего сохранения в рабочем проекте).
        /// </summary>
        /// <param name="documentsIds">Список идентификаторов документов.</param>
        public static DataTable Native_GetInfoAboutVersionsFiles(this INetPluginCall pc, IEnumerable<int> documentsIds) => 
            pc.GetDataTable("GetInfoAboutVersionsFiles", string.Join(Constants.ID_SEPARATOR, documentsIds));

        /// <summary>
        /// Возвращает список одноименных файлов, которые были выгружены на рабочий диск текущим пользователем.
        /// <br/>
        /// <br/>Возвращает набор данных с полями
        /// <br/>[_ID_FILE] integer – идентификатор файла;
        /// <br/>[_ID_DOCUMENT] integer – идентификатор документа;
        /// <br/>[_OLD_NAME] string – имя файла на момент последнего сохранения в рабочем проекте;
        /// <br/>[_OLD_LOCALNAME] string – путь к файлу на момент последнего сохранения в рабочем проекте;
        /// <br/>[_OLD_SIZE] integer – размер файла на момент последнего сохранения в рабочем проекте;
        /// <br/>[_OLD_CRC] integer – контрольная сумма файла на момент последнего сохранения в рабочем проекте;
        /// <br/>[_NEW_NAME] string – текущее имя файла;
        /// <br/>[_NEW_LOCALNAME] string – текущий путь к файлу;
        /// <br/>[_DATEOFCREATE] datetime – дата создания файла;
        /// <br/>[_MODIFIED] datetime – дата последней модификации файла;
        /// <br/>[_CACHED] integer – если 0 – файл находится в ПХФ; если 1 – файл закеширован;
        /// <br/>[_READONLY] integer – если 0 – выгружен для редактирования; если 1 – выгружен только для чтения;
        /// <br/>[_USERNAME] string – имя пользователя;
        /// <br/>[_ID_LOCK] integer – идентификатор рабочего проекта или NULL.
        /// <br/>[_OUTDATED] integer – равен 1, если при закрытии рабочего проекта или разблокировке документа не удалось удалить файл с рабочего диска пользователя (например, если файл заблокирован инструментом). В этом случае поле [_ID_LOCK] имеет значение 0. Во всех остальных случаях поле [_OUTDATED] имеет значение 0.
        ///</summary>
        /// <inheritdoc cref="Native_CheckFileNameEx(INetPluginCall, string, string)"/>
        public static DataTable Native_GetChangedFileVersions(this INetPluginCall pc, string fileName, string filePath) => 
            pc.GetDataTable("GetChangedFileVersions", fileName, filePath);

        /// <summary>
        /// Возвращает информацию о файле, который на момент вызова находится в рабочей папке текущего пользователя.
        /// <br/>
        /// <br/>Возвращает набор данных с полями
        /// <br/>[_ID_FILE] integer – идентификатор файла;
        /// <br/>[_ID_DOCUMENT] integer – идентификатор документа;
        /// <br/>[_TYPE] string – тип документа;
        /// <br/>[_PRODUCT] string – ключевой атрибут документа;
        /// <br/>[_VERSION] string – номер версии документа;
        /// <br/>[_NAME] string – имя файла;
        /// <br/>[_LOCALNAME] string – путь к файлу;
        /// <br/>[_SIZE] integer – размер файла;
        /// <br/>[_CRC] integer – контрольная сумма файла;
        /// <br/>[_DATEOFCREATE] datetime – дата создания;
        /// <br/>[_MODIFIED] datetime – дата последней модификации файла.
        ///</summary>
        /// <inheritdoc cref="Native_CheckFileNameEx(INetPluginCall, string, string)"/>
        public static DataTable Native_GetTempFile(this INetPluginCall pc, string fileName, string filePath) => 
            pc.GetDataTable("GetTempFile", fileName, filePath);

        /// <summary>
        /// Выгружает файл из системы ЛОЦМАН:PLM.
        /// <br/>В режиме базы данных копирует файл в зарезервированную временную папку пользователя.
        /// <br/>В режиме редактирования объектов копирует файл на рабочий диск (настройка «Буква рабочего диска») пользователя.
        /// </summary>
        /// <inheritdoc cref="Native_CheckFileNameEx(INetPluginCall, string, string)"/>
        /// <inheritdoc cref="Native_KillVersion(INetPluginCall, string, string, string)"/>
        /// <returns>
        /// В режиме базы данных возвращает полное имя файла с путем. Например: result='\\Server\DOMEN#USER\Temp\FileName.ext'.
        /// <br/>В режиме редактирования объектов возвращает имя файла на рабочем диске. Например: result='Х:\Folder\SubFolder\FileName.ext'.
        /// </returns>
        public static string Native_GetFile(this INetPluginCall pc, string typeName, string product, string version, string fileName, string filePath) => 
            pc.RunMethod("GetFile", typeName, product, version, fileName, filePath) as string;

        /// <inheritdoc cref="Native_GetFile(INetPluginCall, string, string, string, string, string)"/>
        /// <param name="documentId">Идентификатор версии документа.</param>
        public static string Native_GetFile(this INetPluginCall pc, int documentId, string fileName, string filePath) => 
            pc.RunMethod("GetFileById", documentId, fileName, filePath) as string;

        /// <summary>
        /// Регистрирует в базе данных файл, находящийся на рабочем диске пользователя.
        /// </summary>
        /// <inheritdoc cref="Native_GetFile(INetPluginCall, string, string, string, string, string)"/>
        public static void Native_RegistrationOfFile(this INetPluginCall pc, string typeName, string product, string version, string fileName, string filePath) => 
            pc.RunMethod("RegistrationOfFile", typeName, product, version, 0, fileName, filePath);

        /// <param name="documentId">Идентификатор версии документа.</param>
        /// <inheritdoc cref="Native_RegistrationOfFile(INetPluginCall, string, string, string, string, string)"/>
        public static void Native_RegistrationOfFile(this INetPluginCall pc, int documentId, string fileName, string filePath) => 
            pc.RunMethod("RegistrationOfFile", string.Empty, string.Empty, string.Empty, documentId, fileName, filePath);

        /// <summary>
        /// Переименовывает файл.
        /// </summary>
        /// <returns/>
        /// <param name="newFileName">Новое название файла.</param>
        /// <param name="newFilePath">Новый путь к файлу относительно диска из настройки «Буква рабочего диска».</param>
        /// <inheritdoc cref="Native_GetFile(INetPluginCall, string, string, string, string, string)"/>
        public static void Native_RenameFile(this INetPluginCall pc, string typeName, string product, string version, string fileName, string filePath, string newFileName, string newFilePath) => 
            pc.RunMethod("RenameFile", typeName, product, version, fileName, filePath, newFileName, newFilePath);

        /// <param name="documentId">Идентификатор версии документа.</param>
        /// <inheritdoc cref="Native_RenameFile(INetPluginCall, string, string, string, string, string, string, string)"/>
        public static void Native_RenameFile(this INetPluginCall pc, int documentId, string fileName, string filePath, string newFileName, string newFilePath) => 
            pc.RunMethod("RenameFileById", documentId, fileName, filePath, newFileName, newFilePath);

        #endregion

        #region Отчёты
        /// <summary>
        /// Возвращает данные для формирования отчета.
        /// </summary>
        /// <param name="reportName">Название хранимой процедуры.</param>
        /// <param name="objectsIds">Коллекция идентификаторов объектов.</param>
        /// <param name="reportParams">Произвольный набор параметров. Применяется по усмотрению разработчика.</param>
        /// <returns>
        /// Возвращает набор данных с полями, определенными в соответствующей хранимой процедуре.
        /// </returns>
        public static DataTable Native_GetReport(this INetPluginCall pc, string reportName, IEnumerable<int> objectsIds = null, string reportParams = null) => 
            pc.GetDataTable("GetReport", reportName, objectsIds, reportParams);

        /// <summary>
        /// Возвращает список отчетов и папок.
        /// <br/>Параметры отчета можно получить с помощью метода <see cref="Native_GetParameterList(INetPluginCall, int)">GetParameterList</see>
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID] int – идентификатор отчета или папки;
        /// <br/>[_ID_PARENT] int – идентификатор родительской папки;
        /// <br/>[_OBJECT_TYPE] int – тип записи:
        /// <br/>0 – папка;
        /// <br/>1 – отчет SQL;
        /// <br/>2 – отчет FastReport;
        /// <br/>[_USED] int – область применения отчета:
        /// <br/>0 – объекты;
        /// <br/>1 – бизнес-процессы;
        /// <br/>2 – задания WorkFlow;
        /// <br/>3 – не определено;
        /// <br/>4 – задания СПиУПП;
        /// <br/>[_OBJECT_NAME] string – название отчета или папки;
        /// <br/>[_OBJECT_HINT] string – описание отчета;
        /// <br/>[_PROC_NAME] string – название хранимой процедуры отчета;
        /// <br/>[_FILE_NAME] string – относительный путь к файлу шаблона отчета;
        /// <br/>[_IS_FAVORITE] int – признак, входит ли отчет в список избранных отчетов текущего пользователя:
        /// <br/>0 – отчет не входит в список избранных отчетов текущего пользователя;
        /// <br/>1 – отчет входит в список избранных отчетов текущего пользователя.
        /// </summary>
        /// <param name="folderId">Идентификатор папки. Если значение параметра равно -1, то возвращаются все отчеты и папки.</param>
        public static DataTable Native_GetReportsAndFolders(this INetPluginCall pc, int folderId = -1) => 
            pc.GetDataTable("GetReportsAndFolders", folderId);

        /// <summary>
        /// Возвращает список параметров отчета.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID_PARAM] int – идентификатор параметра;
        /// <br/>[_PARAM_NAME] string – название параметра;
        /// <br/>[_PARAM_HINT] string – описание параметра;
        /// <br/>[_DEFAULT_VALUE] string – значение параметра по умолчанию;
        /// <br/>[_MIN_VALUE] string – минимальное значение параметра;
        /// <br/>[_MAX_VALUE] string – максимальное значение параметра;
        /// <br/>[_VALUE_LIST] text – список возможных значений параметра.
        /// </summary>
        /// <param name="reportId">Идентификатор отчета.</param>
        public static DataTable Native_GetParameterList(this INetPluginCall pc, int reportId) => 
            pc.GetDataTable("GetParameterList", reportId);
        
        #endregion

        #region Редактирование объектов

        /// <summary>
        /// Устанавливает или снимает с объекта признак проекта.
        /// </summary>
        /// <param name="typeName">Название типа.</param>
        /// <param name="product">Ключевой атрибут.</param>
        /// <param name="isProject">Признак проекта (true – установить, false – снять).</param>
        public static void Native_IsProject(this INetPluginCall pc, string typeName, string product, bool isProject) => 
            pc.RunMethod("IsProject", typeName, product, isProject);

        /// <summary>
        /// Помечает объект, находящийся на изменении, как подлежащий удалению при возврате в базу данных.
        /// </summary>
        /// <param name="typeName">Название типа.</param>
        /// <param name="product">Ключевой атрибут.</param>
        /// <param name="version">Версия объекта.</param>
        public static void Native_KillVersion(this INetPluginCall pc, string typeName, string product, string version) => 
            pc.RunMethod("KillVersion", typeName, product, version);

        /// <summary>
        /// Помечает объект, находящийся на изменении, как подлежащий удалению при возврате в базу данных.        
        /// </summary>
        /// <param name="objectId">Идентификатор версии объекта.</param>
        public static void Native_KillVersion(this INetPluginCall pc, int objectId) => 
            pc.RunMethod("KillVersionById", objectId);

        /// <summary>
        /// Удаляет объекты.
        /// <br/>
        /// <br/>Возвращает набор данных с полями:
        /// <br/>[_ID_VERSION] int – уникальный идентификатор версии;
        /// <br/>[_ID_TYPE] int – идентификатор типа объекта;
        /// <br/>[_PRODUCT] string – ключевой атрибут объекта;
        /// <br/>[_VERSION] string – версия документа;
        /// <br/>[_ERR_CODE] int – код ошибки;
        /// <br/>[_ERR_MSG] string – сообщение об ошибке.
        /// </summary>
        /// <param name="objectsIds">Список идентификаторов версий объектов.</param>
        public static DataTable Native_KillVersions(this INetPluginCall pc, IEnumerable<int> objectsIds) => 
            pc.GetDataTable("KillVersions", string.Join(Constants.ID_SEPARATOR, objectsIds), 0);

        /// <summary>
        /// Добавляет, удаляет, обновляет значение атрибута объекта.
        /// </summary>
        /// <inheritdoc cref="Native_KillVersion(INetPluginCall, int)"/>
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
        public static void Native_UpAttrValueById(this INetPluginCall pc, int objectId, string attributeName, object attributeValue, string unitId = null, bool toDel = false) => 
            pc.RunMethod("UpAttrValueById", objectId, attributeName, attributeValue, unitId, toDel);

        #endregion

        #region Вторичное представление

        /// <summary>
        /// Удаляет вторичное представление документа.
        /// <br/>
        /// <br/>Для ЛОЦМАН:PLM 2017 и более поздних версий системы этот метод устарел и оставлен только для совместимости с предыдущими версиями.
        /// <br/>В ЛОЦМАН:PLM 2017 и более поздних версиях метод удаляет все ревизии вторичного представления указанного документа. Чтобы удалить конкретную ревизию вторичного представления, воспользуйтесь методом <see cref="Native_DelSecondaryViewRevision(INetPluginCall, int, int)"/>.
        /// </summary>
        /// <param name="documenId">Идентификатор версии документа.</param>
        public static void Native_DelSecondaryView(this INetPluginCall pc, int documenId) => 
            pc.RunMethod("DelSecondaryView", documenId);

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
        public static void Native_DelSecondaryViewRevision(this INetPluginCall pc, int documenId, int revisionId) => 
            pc.RunMethod("DelSecondaryViewRevision", documenId, revisionId);

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
        public static DataTable Native_GetInfoAboutSecondaryViews(this INetPluginCall pc, int[] documentsIds) => 
            pc.GetDataTable("GetInfoAboutSecondaryViews", documentsIds);

        /// <summary>
        /// Выгружает файл ревизии вторичного представления и возвращает сетевой путь к нему.
        /// </summary>
        /// <param name="revisionId">Идентификатор ревизии вторичного представления.</param>
        /// <returns>
        /// Возвращает сетевой путь к файлу вторичного представления.
        /// </returns>
        public static string Native_GetSecondaryViewRevisionFile(this INetPluginCall pc, int revisionId) => 
            pc.RunMethod("GetSecondaryViewRevisionFile", revisionId) as string;

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
        public static DataTable Native_GetSecondaryViewRevisions(this INetPluginCall pc, int documenId) => 
            pc.GetDataTable("GetSecondaryViewRevisions", documenId);

        /// <summary>
        /// Возвращает состояние «флага» аннотирования текущим пользователем.
        /// <br/>
        /// <br/>Начиная с версии ЛОЦМАН:PLM 2017 допускается аннотирование документа несколькими пользователями одновременно. 
        /// <br/>Получить список пользователей, выполняющих в данный момент аннотирование документа, можно с помощью метода <see cref="Native_GetSecondaryViewRevisions(INetPluginCall, int)"/>.
        /// </summary>
        /// <param name="documenId">Идентификатор версии документа.</param>
        public static bool Native_GetViewLock(this INetPluginCall pc, int documenId) => 
            pc.RunMethod("GetViewLock", documenId) as int? == 1;

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
        public static void Native_SaveAnnotationFile(this INetPluginCall pc, int documenId, string annotationPath) => 
            pc.RunMethod("SaveAnnotationFile", documenId, annotationPath);

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
        public static void Native_SaveSecondaryView(this INetPluginCall pc, int documenId, string filePath) => 
            pc.RunMethod("SaveSecondaryView", documenId, filePath);

        /// <summary>
        /// Указывает, что текущий пользователь начал или окончил аннотирование последней доступной корневой ревизии вторичного представления.
        /// <br/>
        /// <br/>Начиная с версии ЛОЦМАН:PLM 2017 допускается аннотирование документа несколькими пользователями одновременно. 
        /// <br/>Получить список пользователей, выполняющих в данный момент аннотирование документа, можно с помощью метода <see cref="Native_GetSecondaryViewRevisions(INetPluginCall, int)"/>.
        /// </summary>
        /// <param name="documenId">Идентификатор версии документа.</param>
        /// <param name="isLock">Флаг аннотирования. Установите в true, чтобы указать, что текущий пользователь начал аннотирование; false – что текущий пользователь окончил аннотирование.</param>
        public static void Native_SetViewLock(this INetPluginCall pc, int documenId, bool isLock) => 
            pc.RunMethod("SetViewLock", documenId, isLock);

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
        public static void Native_UpdateAnnotationFile(this INetPluginCall pc, int revisionId, string annotationPath) => 
            pc.RunMethod("UpdateAnnotationFile", revisionId, annotationPath);
        #endregion

        #region CheckOut

        /// <summary>
        /// Берет объект на редактирование.
        /// </summary>
        /// <inheritdoc cref="Native_KillVersion(INetPluginCall, string, string, string)"/>
        /// <param name="mode">Определяет дополнительные опции при взятии объекта на изменение</param>
        /// <returns>
        /// Внутреннее название редактируемого объекта (название чекаута).
        /// </returns>
        public static string Native_CheckOut(this INetPluginCall pc, string typeName, string product, string version, CheckOutMode mode = CheckOutMode.Default) => 
            pc.RunMethod("CheckOut", typeName, product, version, (int)mode) as string;

        /// <summary>
        /// Делает объект доступным для изменения только в рамках текущего чекаута (блокирует его).
        /// </summary>
        /// <inheritdoc cref="Native_KillVersion(INetPluginCall, int)"/>
        /// <param name="isRoot">Помещать или не помещать объект в качестве головного объекта чекаута.</param>
        public static void Native_AddToCheckOut(this INetPluginCall pc, int objectId, bool isRoot = false) => 
            pc.RunMethod("AddToCheckOut", objectId, isRoot);

        /// <summary>
        /// Осуществляет подключение к редактируемому объекту (чекауту).
        /// </summary>
        /// <param name="checkOutName">Название чекаута.</param>
        /// <param name="dBName">Название базы данных, к которой нужно подключиться.</param>
        public static void Native_ConnectToCheckOut(this INetPluginCall pc, string checkOutName, string dBName) => 
            pc.RunMethod("ConnectToCheckOut", checkOutName, dBName);

        /// <summary>
        /// Отключается от редактируемого объекта (чекаута).
        /// </summary>
        /// <inheritdoc cref="Native_ConnectToCheckOut(INetPluginCall, string, string)"/>
        public static void Native_DisconnectCheckOut(this INetPluginCall pc, string checkOutName, string dBName) => 
            pc.RunMethod("DisconnectCheckOut", checkOutName, dBName);

        /// <summary>
        /// Возвращает измененный объект в базу данных.
        /// <br/>
        /// <br/>Если один или несколько файлов невозможно сохранить в базе данных, работа метода завершается ошибкой с кодом 1030. При этом метод возвращает набор данных с информацией о файлах, которые не удалось сохранить в базе. 
        /// <br/>[_ID_VERSION] int – идентификатор версии;
        /// <br/>[_ID_FILE] int – идентификатор файла;
        /// <br/>[_NAME] string – имя файла;
        /// <br/>[_LOCALNAME] string – путь к файлу относительно рабочего диска;
        /// <br/>[_CACHED] boolean – флаг – установлен в true, если файл находится в кеше версий;
        /// <br/>[_NEW] boolean – флаг – установлен в true, если файл в рабочем проекте новый;
        /// <br/>[_ERROR] string – сообщение об ошибке;
        /// <br/>[_ERRORCODE] int – код ошибки (поддерживается начиная с ЛОЦМАН:PLM 2014).
        /// </summary>
        /// <inheritdoc cref="Native_ConnectToCheckOut(INetPluginCall, string, string)"/>
        public static DataTable Native_CheckIn(this INetPluginCall pc, string checkOutName, string dBName) => 
            pc.GetDataTable("CheckIn", checkOutName, dBName);

        /// <summary>
        /// Сохраняет изменения в базе данных.
        /// <br/>
        /// <br/>В случае, если один или несколько файлов невозможно сохранить в базе данных, работа метода завершается ошибкой с кодом 1030. При этом метод возвращает набор данных с информацией о файлах, которые не удалось сохранить в базе. 
        /// <br/>[_ID_VERSION] int – идентификатор версии;
        /// <br/>[_ID_FILE] int – идентификатор файла;
        /// <br/>[_NAME] string – имя файла;
        /// <br/>[_LOCALNAME] string – путь к файлу относительно рабочего диска;
        /// <br/>[_CACHED] boolean – флаг – установлен в true, если файл находится в кеше версий;
        /// <br/>[_NEW] boolean – флаг – установлен в true, если файл в рабочем проекте новый;
        /// <br/>[_ERROR] string – сообщение об ошибке;
        /// <br/>[_ERRORCODE] int – код ошибки (поддерживается начиная с ЛОЦМАН:PLM 2014).
        /// </summary>
        /// <inheritdoc cref="Native_ConnectToCheckOut(INetPluginCall, string, string)"/>
        /// <remarks>
        /// <br/>После выполнения этой операции все изменения, произведенные в чекауте, сохраняются в базе данных. Объект по-прежнему остается на изменении.
        /// </remarks>
        public static DataTable Native_SaveChanges(this INetPluginCall pc, string checkOutName, string dBName) => 
            pc.GetDataTable("SaveChanges", checkOutName, dBName);

        /// <summary>
        /// Выполняет отказ от изменения объекта.
        /// </summary>
        /// <inheritdoc cref="Native_ConnectToCheckOut(INetPluginCall, string, string)"/>
        public static void Native_CancelCheckOut(this INetPluginCall pc, string checkOutName, string dBName) => 
            pc.RunMethod("CancelCheckOut", checkOutName, dBName);

        #endregion
    }
}
