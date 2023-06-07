using PDMObjects;
using System;
using System.Data;

namespace LoodsmanCommon
{
  /// <summary> Расширения для класса <see cref="DataRow"/>. </summary>
  public static class DataRowExtensions
  {
    /// <summary> Возвращает значение по заданному ключу, в случае отсутствия значения, возвращает значение по умолчанию. </summary>
    /// <typeparam name="T">Тип. </typeparam>
    /// <param name="row">Строка данных. </param>
    /// <param name="key">Наименование столбца. </param>
    public static T GetValueOrDefault<T>(this DataRow row, string key) => row.GetValueOrDefault(key, default(T));

    /// <inheritdoc cref="GetValueOrDefault{T}(DataRow, string)"/>
    /// <param name="defaultValue">Значение по умолчанию. </param>
    public static T GetValueOrDefault<T>(this DataRow row, string key, T defaultValue) => row.IsNull(key) ? defaultValue : (T)row[key];

    /// <summary> Некоторые методы сервера приложений возвращают значения типа object {short}, привидение такого типа к int будет ошибочным, так как вместо привидения будет использоваться операция распаковки. </summary>
    /// <param name="rowValue"> Объект <see cref="Object"/>, содержащий данные, полученный из <see cref="DataRow"/>. </param>
    private static int GetIntValue(object rowValue) => rowValue as int? ?? rowValue as short? ?? 0;

    /// <summary> Возвращает результат DataRow["_ID"]. </summary>
    public static int ID(this DataRow row) => GetIntValue(row["_ID"]);

    /// <summary> Возвращает результат DataRow["_ID_DOCUMENT"]. </summary>
    public static int ID_DOCUMENT(this DataRow row) => GetIntValue(row["_ID_DOCUMENT"]);

    /// <summary> Возвращает результат DataRow["_ID_VERSION"]. </summary>
    public static int ID_VERSION(this DataRow row) => GetIntValue(row["_ID_VERSION"]);

    /// <summary> Возвращает результат DataRow["_ID_FILE"]. </summary>
    public static int ID_FILE(this DataRow row) => GetIntValue(row["_ID_FILE"]);

    /// <summary> Возвращает результат DataRow["_ID_PARENT"]. </summary>
    public static int ID_PARENT(this DataRow row) => GetIntValue(row["_ID_PARENT"]);

    /// <summary> Возвращает результат DataRow["_ID_PARENTVERSION"]. </summary>
    public static int ID_PARENTVERSION(this DataRow row) => GetIntValue(row["_ID_PARENTVERSION"]);

    /// <summary> Возвращает результат DataRow["_ID_PARAM"]. </summary>
    public static int ID_PARAM(this DataRow row) => GetIntValue(row["_ID_PARAM"]);

    /// <summary> Возвращает результат DataRow["_ID_CHILD"]. </summary>
    public static int ID_CHILD(this DataRow row) => GetIntValue(row["_ID_CHILD"]);

    /// <summary> Возвращает результат DataRow["_ID_LINK"]. </summary>
    public static int ID_LINK(this DataRow row) => GetIntValue(row["_ID_LINK"]);

    /// <summary> Возвращает результат DataRow["_ID_LINKTYPE"]. </summary>
    public static int ID_LINKTYPE(this DataRow row) => GetIntValue(row["_ID_LINKTYPE"]);

    /// <summary> Возвращает результат DataRow["_ID_TYPE"]. </summary>
    public static int ID_TYPE(this DataRow row) => GetIntValue(row["_ID_TYPE"]);

    /// <summary> Возвращает результат DataRow["_TYPE_ID_1"]. </summary>
    public static int TYPE_ID_1(this DataRow row) => GetIntValue(row["_TYPE_ID_1"]);

    /// <summary> Возвращает результат DataRow["_TYPE_ID_2"]. </summary>
    public static int TYPE_ID_2(this DataRow row) => GetIntValue(row["_TYPE_ID_2"]);

    /// <summary> Возвращает результат DataRow["_ID_STATE"]. </summary>
    public static int ID_STATE(this DataRow row) => GetIntValue(row["_ID_STATE"]);

    /// <summary> Возвращает результат DataRow["_ID_NEXTSTATE"]. </summary>
    public static int ID_NEXTSTATE(this DataRow row) => GetIntValue(row["_ID_NEXTSTATE"]);

    /// <summary> Возвращает результат DataRow["_ID_OWNER"]. </summary>
    public static int ID_OWNER(this DataRow row) => GetIntValue(row["_ID_OWNER"]);

    /// <summary> Возвращает результат DataRow["_LEVEL"]. </summary>
    public static int LEVEL(this DataRow row) => GetIntValue(row["_LEVEL"]);

    /// <summary> Возвращает результат DataRow["_LINKORDER"]. </summary>
    public static int LINKORDER(this DataRow row) => GetIntValue(row["_LINKORDER"]);

    /// <summary> Возвращает результат DataRow["_DOC_ID"]. </summary>
    public static int DOC_ID(this DataRow row) => GetIntValue(row["_DOC_ID"]);

    /// <summary> Возвращает результат DataRow["_USER_ID"]. </summary>
    public static int USER_ID(this DataRow row) => GetIntValue(row["_USER_ID"]);

    /// <summary> Возвращает результат DataRow["_CLASS_ID"]. </summary>
    public static int CLASS_ID(this DataRow row) => GetIntValue(row["_CLASS_ID"]);

    /// <summary> Возвращает результат DataRow["_PARENT_ID"]. </summary>
    public static int PARENT_ID(this DataRow row) => GetIntValue(row["_PARENT_ID"]);

    /// <summary> Возвращает результат DataRow["_SIGNROLE_ID"]. </summary>
    public static int SIGNROLE_ID(this DataRow row) => GetIntValue(row["_SIGNROLE_ID"]);

    /// <summary> Возвращает результат DataRow["_REVISION_ID"]. </summary>
    public static int REVISION_ID(this DataRow row) => GetIntValue(row["_REVISION_ID"]);

    /// <summary> Возвращает результат DataRow["_READINGBYTE"]. </summary>
    public static int READINGBYTE(this DataRow row) => GetIntValue(row["_READINGBYTE"]);

    /// <summary> Возвращает результат DataRow["_SIZE"]. </summary>
    public static int SIZE(this DataRow row) => GetIntValue(row["_SIZE"]);

    /// <summary> Возвращает результат DataRow["_MAINPOSITION_ID"]. </summary>
    public static int MAINPOSITION_ID(this DataRow row) => GetIntValue(row["_MAINPOSITION_ID"]);

    /// <summary> Возвращает результат DataRow["_ID_EFF_TYPE"]. </summary>
    public static int ID_EFF_TYPE(this DataRow row) => GetIntValue(row["_ID_EFF_TYPE"]);
    /// <summary> Возвращает результат DataRow["_ATTR_TYPE"]. </summary>
    public static int ATTR_TYPE(this DataRow row) => GetIntValue(row["_ATTR_TYPE"]);
    /// <summary> Возвращает результат DataRow["_ATTR_ID"]. </summary>
    public static int ATTR_ID(this DataRow row) => GetIntValue(row["_ATTR_ID"]);
    /// <summary> Возвращает результат DataRow["_EFF_ID"]. </summary>
    public static int EFF_ID(this DataRow row) => GetIntValue(row["_EFF_ID"]);
    /// <summary> Возвращает результат DataRow["_EFF_TYPE_ID"]. </summary>
    public static int EFF_TYPE_ID(this DataRow row) => GetIntValue(row["_EFF_TYPE_ID"]);

    
      

    /// <summary> Возвращает результат DataRow["_CRC"]. </summary>
    public static int CRC(this DataRow row) => GetIntValue(row["_CRC"]);

    /// <summary> Возвращает результат DataRow["_ID_LOCK"]. </summary>
    public static int? ID_LOCK(this DataRow row) => row["_ID_LOCK"] as int?;

    /// <summary> Возвращает результат DataRow["_ICON"]. </summary>
    public static byte[] ICON(this DataRow row) => row["_ICON"] as byte[];

    /// <summary> Возвращает результат DataRow["_LINKICON"]. </summary>
    public static byte[] LINKICON(this DataRow row) => row["_LINKICON"] as byte[];

    /// <summary> Возвращает результат DataRow["_MIN_QUANTITY"]. </summary>
    public static double MIN_QUANTITY(this DataRow row) => row["_MIN_QUANTITY"] as double? ?? 0;

    /// <summary> Возвращает результат DataRow["_MAX_QUANTITY"]. </summary>
    public static double MAX_QUANTITY(this DataRow row) => row["_MAX_QUANTITY"] as double? ?? 0;

    /// <summary> Возвращает результат DataRow["_ATTRNAME"]. </summary>
    public static string ATTRNAME(this DataRow row) => row["_ATTRNAME"] as string;

    /// <summary> Возвращает результат DataRow["_CODE"]. </summary>
    public static string CODE(this DataRow row) => row["_CODE"] as string;

    /// <summary> Возвращает результат DataRow["_GUID"]. </summary>
    public static string GUID(this DataRow row) => row["_GUID"] as string;

    /// <summary> Возвращает результат DataRow["_ID_UNIT"]. </summary>
    public static string ID_UNIT(this DataRow row) => row["_ID_UNIT"] as string;

    /// <summary> Возвращает результат DataRow["_ID_MEASURE"]. </summary>
    public static string ID_MEASURE(this DataRow row) => row["_ID_MEASURE"] as string;

    /// <summary> Возвращает результат DataRow["_DISPLAY"]. </summary>
    public static string DISPLAY(this DataRow row) => row["_DISPLAY"] as string;

    /// <summary> Возвращает результат DataRow["_DOCNAME"]. </summary>
    public static string DOCNAME(this DataRow row) => row["_DOCNAME"] as string;

    /// <summary> Возвращает результат DataRow["_NAME"]. </summary>
    public static string NAME(this DataRow row) => row["_NAME"] as string;

    /// <summary> Возвращает результат DataRow["_LINKTYPE"]. </summary>
    public static string LINKTYPE(this DataRow row) => row["_LINKTYPE"] as string;

    /// <summary> Возвращает результат DataRow["_LINK_TYPE_NAME"]. </summary>
    public static string LINK_TYPE_NAME(this DataRow row) => row["_LINK_TYPE_NAME"] as string;

    /// <summary> Возвращает результат DataRow["_INVERSENAME"]. </summary>
    public static string INVERSENAME(this DataRow row) => row["_INVERSENAME"] as string;

    /// <summary> Возвращает результат DataRow["_TYPE"].
    /// <br/>** В случаях когда поле содержит тип субъекта, необходимо использовать метод расширения <see cref="TYPE_SUBJECT(DataRow)">TYPE_SUBJECT</see>.
    /// </summary>
    public static string TYPE(this DataRow row) => row["_TYPE"] as string;

    /// <summary> Возвращает результат DataRow["_TYPENAME"]. </summary>
    public static string TYPENAME(this DataRow row) => row["_TYPENAME"] as string;

    /// <summary> Возвращает результат DataRow["_TYPE_NAME_1"]. </summary>
    public static string TYPE_NAME_1(this DataRow row) => row["_TYPE_NAME_1"] as string;

    /// <summary> Возвращает результат DataRow["_TYPE_NAME_2"]. </summary>
    public static string TYPE_NAME_2(this DataRow row) => row["_TYPE_NAME_2"] as string;

    /// <summary> Возвращает результат DataRow["_PARENTNAME"]. </summary>
    public static string PARENTNAME(this DataRow row) => row["_PARENTNAME"] as string;

    /// <summary> Возвращает результат DataRow["_PARENT_TYPE"]. </summary>
    public static string PARENT_TYPE(this DataRow row) => row["_PARENT_TYPE"] as string;

    /// <summary> Возвращает результат DataRow["_PARENT_PRODUCT"]. </summary>
    public static string PARENT_PRODUCT(this DataRow row) => row["_PARENT_PRODUCT"] as string;

    /// <summary> Возвращает результат DataRow["_PARENT_VERSION"]. </summary>
    public static string PARENT_VERSION(this DataRow row) => row["_PARENT_VERSION"] as string;

    /// <summary> Возвращает результат DataRow["_PARAM_NAME"]. </summary>
    public static string PARAM_NAME(this DataRow row) => row["_PARAM_NAME"] as string;

    /// <summary> Возвращает результат DataRow["_PARAM_HINT"]. </summary>
    public static string PARAM_HINT(this DataRow row) => row["_PARAM_HINT"] as string;

    /// <summary> Возвращает результат DataRow["_PROC_NAME"]. </summary>
    public static string PROC_NAME(this DataRow row) => row["_PROC_NAME"] as string;

    /// <summary> Возвращает результат DataRow["_PRODUCT"]. </summary>
    public static string PRODUCT(this DataRow row) => row["_PRODUCT"] as string;

    /// <summary> Возвращает результат DataRow["_PRODUCTSOURCE"]. </summary>
    public static string PRODUCTSOURCE(this DataRow row) => row["_PRODUCTSOURCE"] as string;

    /// <summary> Возвращает результат DataRow["_PROGID"]. </summary>
    public static string PROGID(this DataRow row) => row["_PROGID"] as string;

    /// <summary> Возвращает результат DataRow["_VERSION"]. </summary>
    public static string VERSION(this DataRow row) => row["_VERSION"] as string;

    /// <summary> Возвращает результат DataRow["_SIGNROLE_NAME"]. </summary>
    public static string SIGNROLE_NAME(this DataRow row) => row["_SIGNROLE_NAME"] as string;

    /// <summary> Возвращает результат DataRow["_SERVERNAME"]. </summary>
    public static string SERVERNAME(this DataRow row) => row["_SERVERNAME"] as string;

    /// <summary> Возвращает результат DataRow["_STATE"]. </summary>
    public static string STATE(this DataRow row) => row["_STATE"] as string;

    /// <summary> Возвращает результат DataRow["_STATE_NAME"]. </summary>
    public static string STATE_NAME(this DataRow row) => row["_STATE_NAME"] as string;

    /// <summary> Возвращает результат DataRow["_STATESOURCE"]. </summary>
    public static string STATESOURCE(this DataRow row) => row["_STATESOURCE"] as string;

    /// <summary> Возвращает результат DataRow["_NEXTSTATE"]. </summary>
    public static string NEXTSTATE(this DataRow row) => row["_NEXTSTATE"] as string;

    /// <summary> Возвращает результат DataRow["_DEFAULTSTATE"]. </summary>
    public static string DEFAULTSTATE(this DataRow row) => row["_DEFAULTSTATE"] as string;

    /// <summary> Возвращает результат DataRow["_USERNAME"]. </summary>
    public static string USERNAME(this DataRow row) => row["_USERNAME"] as string;

    /// <summary> Возвращает результат DataRow["_USERFULLNAME"]. </summary>
    public static string USERFULLNAME(this DataRow row) => row["_USERFULLNAME"] as string;

    /// <summary> Возвращает результат DataRow["_USERS_ANNOTATED_BY"]. </summary>
    public static string USERS_ANNOTATED_BY(this DataRow row) => row["_USERS_ANNOTATED_BY"] as string;

    /// <summary> Возвращает результат DataRow["_UNIT"]. </summary>
    public static string UNIT(this DataRow row) => row["_UNIT"] as string;

    /// <summary> Возвращает результат DataRow["_MEASURE"]. </summary>
    public static string MEASURE(this DataRow row) => row["_MEASURE"] as string;

    /// <summary> Возвращает результат DataRow["_LOCALNAME"]. </summary>
    public static string LOCALNAME(this DataRow row) => row["_LOCALNAME"] as string;

    /// <summary> Возвращает результат DataRow["_LIST"]. </summary>
    public static string LIST(this DataRow row) => row["_LIST"] as string ?? string.Empty;

    /// <summary> Возвращает результат DataRow["_VALUE_LIST"]. </summary>
    public static string VALUE_LIST(this DataRow row) => row["_VALUE_LIST"] as string ?? string.Empty;

    /// <summary> Возвращает результат DataRow["_OBJECT_HINT"]. </summary>
    public static string OBJECT_HINT(this DataRow row) => row["_OBJECT_HINT"] as string;

    /// <summary> Возвращает результат DataRow["_OBJECT_NAME"]. </summary>
    public static string OBJECT_NAME(this DataRow row) => row["_OBJECT_NAME"] as string;

    /// <summary> Возвращает результат DataRow["_OWNER"]. </summary>
    public static string OWNER(this DataRow row) => row["_OWNER"] as string;

    /// <summary> Возвращает результат DataRow["_CHILD_TYPE"]. </summary>
    public static string CHILD_TYPE(this DataRow row) => row["_CHILD_TYPE"] as string;

    /// <summary> Возвращает результат DataRow["_CHILD_PRODUCT"]. </summary>
    public static string CHILD_PRODUCT(this DataRow row) => row["_CHILD_PRODUCT"] as string;

    /// <summary> Возвращает результат DataRow["_CHILD_VERSION"]. </summary>
    public static string CHILD_VERSION(this DataRow row) => row["_CHILD_VERSION"] as string;

    /// <summary> Возвращает результат DataRow["_CAPTION"]. </summary>
    public static string CAPTION(this DataRow row) => row["_CAPTION"] as string;

    /// <summary> Возвращает результат DataRow["_COMMENTS"]. </summary>
    public static string COMMENTS(this DataRow row) => row["_COMMENTS"] as string;

    /// <summary> Возвращает результат DataRow["_CHECKOUTNAME"]. </summary>
    public static string CHECKOUTNAME(this DataRow row) => row["_CHECKOUTNAME"] as string;

    /// <summary> Возвращает результат DataRow["_NATIVENAME"]. </summary>
    public static string NATIVENAME(this DataRow row) => row["_NATIVENAME"] as string;

    /// <summary> Возвращает результат DataRow["_FULLNAME"]. </summary>
    public static string FULLNAME(this DataRow row) => row["_FULLNAME"] as string;

    /// <summary> Возвращает результат DataRow["_FILE_NAME"]. </summary>
    public static string FILE_NAME(this DataRow row) => row["_FILE_NAME"] as string;

    /// <summary> Возвращает результат DataRow["_MIN_VALUE"]. </summary>
    public static string MIN_VALUE(this DataRow row) => row["_MIN_VALUE"] as string;

    /// <summary> Возвращает результат DataRow["_MAX_VALUE"]. </summary>
    public static string MAX_VALUE(this DataRow row) => row["_MAX_VALUE"] as string;

    /// <summary> Возвращает результат DataRow["_MAIL"]. </summary>
    public static string MAIL(this DataRow row) => row["_MAIL"] as string;

    /// <summary> Возвращает результат DataRow["_PHONE"]. </summary>
    public static string PHONE(this DataRow row) => row["_PHONE"] as string;

    /// <summary> Возвращает результат DataRow["_SKYPE"]. </summary>
    public static string SKYPE(this DataRow row) => row["_SKYPE"] as string;

    /// <summary> Возвращает результат DataRow["_IM"]. </summary>
    public static string IM(this DataRow row) => row["_IM"] as string;

    /// <summary> Возвращает результат DataRow["_WEBPAGE"]. </summary>
    public static string WEBPAGE(this DataRow row) => row["_WEBPAGE"] as string;

    /// <summary> Возвращает результат DataRow["_WORKDIR"]. </summary>
    public static string WORKDIR(this DataRow row) => row["_WORKDIR"] as string;

    /// <summary> Возвращает результат DataRow["_FILEDIR"]. </summary>
    public static string FILEDIR(this DataRow row) => row["_FILEDIR"] as string;

    /// <summary> Возвращает результат DataRow["_NOTE"]. </summary>
    public static string NOTE(this DataRow row) => row["_NOTE"] as string;

    /// <summary> Возвращает результат DataRow["_NODE"]. </summary>
    public static string NODE(this DataRow row) => row["_NODE"] as string;

    /// <summary> Возвращает результат DataRow["_TOOLNAME"]. </summary>
    public static string TOOLNAME(this DataRow row) => row["_TOOLNAME"] as string;

    /// <summary> Возвращает результат DataRow["_EXT"]. </summary>
    public static string EXT(this DataRow row) => row["_EXT"] as string;

    /// <summary> Возвращает результат DataRow["_EXTENSION"]. </summary>
    public static string EXTENSION(this DataRow row) => row["_EXTENSION"] as string;

    /// <summary> Возвращает результат DataRow["_EFF_TYPE_NAME"]. </summary>
    public static string EFF_TYPE_NAME(this DataRow row) => row["_EFF_TYPE_NAME"] as string;

    /// <summary> Возвращает результат DataRow["_ATTR_NAME"]. </summary>
    public static string ATTR_NAME(this DataRow row) => row["_ATTR_NAME"] as string;
    /// <summary> Возвращает результат DataRow["_EFFNAME"]. </summary>
    public static string EFFNAME(this DataRow row) => row["_EFFNAME"] as string;

    


    /// <summary> Возвращает результат DataRow["_DEFAULT_VALUE"]. </summary>
    public static string DEFAULT_VALUE(this DataRow row) => row["_DEFAULT_VALUE"] as string;

    /// <summary> Возвращает результат DataRow["_DEFAULT"]. Использовать в случае когда поле содержит значение по умолчанию (string). 
    /// <br/>** В случаях когда поле содержит признак по умолчанию (int), необходимо использовать метод расширения <see cref="DEFAULT_BOOL(DataRow)">DEFAULT_BOOL</see>.
    /// </summary>
    public static string DEFAULT(this DataRow row) => row["_DEFAULT"] as string;

    /// <summary> Возвращает результат DataRow["_DEFAULT"]. Использовать в случае когда поле содержит признак по умолчанию (int). 
    /// <br/>** В случаях когда поле содержит значение по умолчанию (string), необходимо использовать метод расширения <see cref="DEFAULT(DataRow)">DEFAULT</see>.
    /// </summary>
    public static bool DEFAULT_BOOL(this DataRow row) => GetIntValue(row["_DEFAULT"]) == 1;

    /// <summary> Возвращает результат DataRow["_IS_ADMIN"]. </summary>
    public static bool IS_ADMIN(this DataRow row) => GetIntValue(row["_IS_ADMIN"]) == 1;

    /// <summary> Возвращает результат DataRow["_WINUSER"]. </summary>
    public static bool WINUSER(this DataRow row) => GetIntValue(row["_WINUSER"]) == 1;

    /// <summary> Возвращает результат DataRow["_LEADER"]. </summary>
    public static bool LEADER(this DataRow row) => GetIntValue(row["_LEADER"])==1;

    /// <summary> Возвращает результат DataRow["_IS_FAVORITE"]. </summary>
    public static bool IS_FAVORITE(this DataRow row) => GetIntValue(row["_IS_FAVORITE"]) == 1;

    /// <summary> Возвращает результат DataRow["_USED"]. </summary>
    public static bool USED(this DataRow row) => GetIntValue(row["_USED"]) == 1;

    /// <summary> Возвращает результат DataRow["_BASIC"]. </summary>
    public static bool BASIC(this DataRow row) => GetIntValue(row["_BASIC"]) == 1;

    /// <summary> Возвращает результат DataRow["_BASICUNIT"]. </summary>
    public static bool BASICUNIT(this DataRow row) => GetIntValue(row["_BASICUNIT"]) == 1;

    /// <summary> Возвращает результат DataRow["_DOCUMENT"]. </summary>
    public static bool DOCUMENT(this DataRow row) => GetIntValue(row["_DOCUMENT"]) == 1;

    /// <summary> Возвращает результат DataRow["_SYSTEM"]. </summary>
    public static bool SYSTEM(this DataRow row) => GetIntValue(row["_SYSTEM"]) == 1;

    /// <summary> Возвращает результат DataRow["_ONLYLISTITEMS"]. </summary>
    public static bool ONLYLISTITEMS(this DataRow row) => GetIntValue(row["_ONLYLISTITEMS"]) == 1;

    /// <summary> Возвращает результат DataRow["_OBLIGATORY"]. </summary>
    public static bool OBLIGATORY(this DataRow row) => GetIntValue(row["_OBLIGATORY"]) == 1;

    /// <summary> Возвращает результат DataRow["_CANCREATE"]. </summary>
    public static bool CANCREATE(this DataRow row) => GetIntValue(row["_CANCREATE"]) == 1;

    /// <summary> Возвращает результат DataRow["_CANBEPROJECT"]. </summary>
    public static bool CANBEPROJECT(this DataRow row) => GetIntValue(row["_CANBEPROJECT"]) == 1;

    /// <summary> Возвращает результат DataRow["_NOVERSIONS"]. </summary>
    public static bool NOVERSIONS(this DataRow row) => GetIntValue(row["_NOVERSIONS"]) == 1;

    /// <summary> Возвращает результат DataRow["_TRANSSIGNROLES"]. </summary>
    public static bool TRANSSIGNROLES(this DataRow row) => GetIntValue(row["_TRANSSIGNROLES"]) == 1;

    /// <summary> Возвращает результат DataRow["_TYPE"]. Использовать в случае когда поле содержит признак по умолчанию (int). 
    /// <br/>** В случаях когда поле содержит значение по умолчанию (string), необходимо использовать метод расширения <see cref="TYPE(DataRow)">TYPE</see>.
    /// </summary>
    public static bool TYPE_BOOL(this DataRow row) => GetIntValue(row["_TYPE"]) == 0;

    /// <summary> Возвращает результат DataRow["_DATE"]. </summary>
    public static DateTime DATE(this DataRow row) => row["_DATE"] as DateTime? ?? DateTime.MaxValue;

    /// <summary> Возвращает результат DataRow["_DATEOFCREATE"]. </summary>
    public static DateTime DATEOFCREATE(this DataRow row) => row["_DATEOFCREATE"] as DateTime? ?? DateTime.MaxValue;

    /// <summary> Возвращает результат DataRow["_MODIFIED"]. </summary>
    public static DateTime MODIFIED(this DataRow row) => row["_MODIFIED"] as DateTime? ?? DateTime.MaxValue;

    /// <summary> Возвращает результат DataRow["_LINKKIND"]. </summary>
    public static LinkKind LINKKIND(this DataRow row) => (LinkKind)GetIntValue(row["_LINKKIND"]);

    /// <summary> Возвращает результат DataRow["_TYPE"].
    /// <br/>** В случаях когда поле содержит тип (string), необходимо использовать метод расширения <see cref="TYPE(DataRow)">TYPE</see>.
    /// </summary>
    public static SubjectType TYPE_SUBJECT(this DataRow row) => (SubjectType)GetIntValue(row["_TYPE"]);

    /// <summary> Возвращает результат DataRow["_DIRECTION"]. </summary>
    public static LinkDirection DIRECTION(this DataRow row) => (LinkDirection)GetIntValue(row["_DIRECTION"]);

    /// <summary> Возвращает результат DataRow["_ATTRTYPE"]. </summary>
    public static AttributeType ATTRTYPE(this DataRow row) => (AttributeType)GetIntValue(row["_ATTRTYPE"]);

    /// <summary> Возвращает результат DataRow["_IS_QUANTITY"]. </summary>
    public static LinkQuantityType IS_QUANTITY(this DataRow row) => (LinkQuantityType)GetIntValue(row["_IS_QUANTITY"]);

    /// <summary> Возвращает результат DataRow["_OBJECT_TYPE"]. </summary>
    public static ReportRecordType OBJECT_TYPE(this DataRow row) => (ReportRecordType)GetIntValue(row["_OBJECT_TYPE"]);

    /// <summary> Возвращает результат DataRow["_USED"]. </summary>
    public static ReportTargetType USED_REP(this DataRow row) => (ReportTargetType)GetIntValue(row["_USED"]);

    /// <summary> Возвращает результат DataRow["_ACCESSLEVEL"]. </summary>
    public static PDMAccessLevels ACCESSLEVEL(this DataRow row) => (PDMAccessLevels)GetIntValue(row["_ACCESSLEVEL"]);

    /// <summary> Возвращает результат DataRow["_DEFACCESSLEVEL"]. </summary>
    public static PDMAccessLevels DEFACCESSLEVEL(this DataRow row) => (PDMAccessLevels)GetIntValue(row["_DEFACCESSLEVEL"]);

    /// <summary> Возвращает результат DataRow["_MAXACCESSLEVEL"]. </summary>
    public static PDMAccessLevels MAXACCESSLEVEL(this DataRow row) => (PDMAccessLevels)GetIntValue(row["_MAXACCESSLEVEL"]);

    /// <summary> Возвращает результат DataRow["_APPOINTEDACCESSLEVEL"]. </summary>
    public static PDMAccessLevels APPOINTEDACCESSLEVEL(this DataRow row) => (PDMAccessLevels)GetIntValue(row["_APPOINTEDACCESSLEVEL"]);

    /// <summary> Возвращает результат DataRow["_LOCKED"]. </summary>
    public static PDMLockLevels LOCKED(this DataRow row) => (PDMLockLevels)GetIntValue(row["_LOCKED"]);

    /// <summary> Возвращает результат DataRow["_STATUS"]. </summary>
    public static UserState STATUS(this DataRow row) => (UserState)GetIntValue(row["_STATUS"]);
  }
}
