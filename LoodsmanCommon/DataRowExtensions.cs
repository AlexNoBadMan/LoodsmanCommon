using PDMObjects;
using System;
using System.Data;

namespace LoodsmanCommon
{
    public static class DataRowExtensions
    {

        public static T GetValueOrDefault<T>(this DataRow row, string key) => row.GetValueOrDefault(key, default(T));
        public static T GetValueOrDefault<T>(this DataRow row, string key, T defaultValue) => row.IsNull(key) ? defaultValue : (T)row[key];


        public static int ID(this DataRow row) => row["_ID"] as int? ?? 0;
        public static int ID_VERSION(this DataRow row) => row["_ID_VERSION"] as int? ?? 0;
        public static int ID_FILE(this DataRow row) => row["_ID_FILE"] as int? ?? 0;
        public static int ID_PARENT(this DataRow row) => row["_ID_PARENT"] as int? ?? 0;
        public static int ID_PARENTVERSION(this DataRow row) => row["_ID_PARENTVERSION"] as int? ?? 0;
        public static int ID_PARAM(this DataRow row) => row["_ID_PARAM"] as int? ?? 0;
        public static int ID_CHILD(this DataRow row) => row["_ID_CHILD"] as int? ?? 0;
        public static int ID_LINK(this DataRow row) => row["_ID_LINK"] as int? ?? 0;
        public static int ID_LINKTYPE(this DataRow row) => row["_ID_LINKTYPE"] as int? ?? 0;
        public static int ID_TYPE(this DataRow row) => row["_ID_TYPE"] as int? ?? 0;
        public static int ID_STATE(this DataRow row) => row["_ID_STATE"] as int? ?? 0;
        public static int ID_NEXTSTATE(this DataRow row) => row["_ID_NEXTSTATE"] as int? ?? 0;
        public static int ID_OWNER(this DataRow row) => row["_ID_OWNER"] as int? ?? 0;
        public static int LEVEL(this DataRow row) => row["_LEVEL"] as int? ?? 0;
        public static int LINKORDER(this DataRow row) => row["_LINKORDER"] as int? ?? 0;
        public static int DOC_ID(this DataRow row) => row["_DOC_ID"] as int? ?? 0;
        public static int USER_ID(this DataRow row) => row["_USER_ID"] as int? ?? 0;
        public static int CLASS_ID(this DataRow row) => row["_CLASS_ID"] as int? ?? 0;
        public static int PARENT_ID(this DataRow row) => row["_PARENT_ID"] as int? ?? 0;
        public static int SIGNROLE_ID(this DataRow row) => row["_SIGNROLE_ID"] as int? ?? 0;
        public static int REVISION_ID(this DataRow row) => row["_REVISION_ID"] as int? ?? 0;
        public static int READINGBYTE(this DataRow row) => row["_READINGBYTE"] as int? ?? 0;
        public static int SIZE(this DataRow row) => row["_SIZE"] as int? ?? 0;
        public static int CRC(this DataRow row) => row["_CRC"] as int? ?? 0;
        public static int? ID_LOCK(this DataRow row) => row["_ID_LOCK"] as int?;
        public static byte[] ICON(this DataRow row) => row["_ICON"] as byte[];
        public static byte[] LINKICON(this DataRow row) => row["_LINKICON"] as byte[];
        public static double MIN_QUANTITY(this DataRow row) => row["_MIN_QUANTITY"] as double? ?? 0;
        public static double MAX_QUANTITY(this DataRow row) => row["_MAX_QUANTITY"] as double? ?? 0;
        public static string ID_UNIT(this DataRow row) => row["_ID_UNIT"] as string;
        public static string ID_MEASURE(this DataRow row) => row["_ID_MEASURE"] as string;
        public static string NAME(this DataRow row) => row["_NAME"] as string;
        public static string LINKTYPE(this DataRow row) => row["_LINKTYPE"] as string;
        public static string LINK_TYPE_NAME(this DataRow row) => row["_LINK_TYPE_NAME"] as string;
        public static string INVERSENAME(this DataRow row) => row["_INVERSENAME"] as string;
        /// <summary>
        /// ** В случаях когда поле содержит тип субъекта, необходимо использовать метод расширения <see cref="TYPE_SUBJECT(DataRow)">TYPE_SUBJECT</see>.
        /// </summary>
        public static string TYPE(this DataRow row) => row["_TYPE"] as string;
        public static string TYPENAME(this DataRow row) => row["_TYPENAME"] as string;
        public static string PARENT_TYPE(this DataRow row) => row["_PARENT_TYPE"] as string;
        public static string PARENT_PRODUCT(this DataRow row) => row["_PARENT_PRODUCT"] as string;
        public static string PARENT_VERSION(this DataRow row) => row["_PARENT_VERSION"] as string;
        public static string PARAM_NAME(this DataRow row) => row["_PARAM_NAME"] as string;
        public static string PARAM_HINT(this DataRow row) => row["_PARAM_HINT"] as string;
        public static string PROC_NAME(this DataRow row) => row["_PROC_NAME"] as string;
        public static string PRODUCT(this DataRow row) => row["_PRODUCT"] as string;
        public static string PRODUCTSOURCE(this DataRow row) => row["_PRODUCTSOURCE"] as string;
        public static string PROGID(this DataRow row) => row["_PROGID"] as string;
        public static string VERSION(this DataRow row) => row["_VERSION"] as string;
        public static string SIGNROLE_NAME(this DataRow row) => row["_SIGNROLE_NAME"] as string;
        public static string SERVERNAME(this DataRow row) => row["_SERVERNAME"] as string;
        public static string STATE(this DataRow row) => row["_STATE"] as string;
        public static string STATE_NAME(this DataRow row) => row["_STATE_NAME"] as string;
        public static string STATESOURCE(this DataRow row) => row["_STATESOURCE"] as string;
        public static string NEXTSTATE(this DataRow row) => row["_NEXTSTATE"] as string;
        public static string DEFAULTSTATE(this DataRow row) => row["_DEFAULTSTATE"] as string;
        public static string USERNAME(this DataRow row) => row["_USERNAME"] as string;
        public static string USERFULLNAME(this DataRow row) => row["_USERFULLNAME"] as string;
        public static string USERS_ANNOTATED_BY(this DataRow row) => row["_USERS_ANNOTATED_BY"] as string;
        public static string UNIT(this DataRow row) => row["_UNIT"] as string;
        public static string MEASURE(this DataRow row) => row["_MEASURE"] as string;
        public static string LOCALNAME(this DataRow row) => row["_LOCALNAME"] as string;
        public static string LIST(this DataRow row) => row["_LIST"] as string;
        public static string VALUE_LIST(this DataRow row) => row["_VALUE_LIST"] as string;
        public static string OBJECT_HINT(this DataRow row) => row["_OBJECT_HINT"] as string;
        public static string OBJECT_NAME(this DataRow row) => row["_OBJECT_NAME"] as string;
        public static string OWNER(this DataRow row) => row["_OWNER"] as string;
        public static string CHILD_TYPE(this DataRow row) => row["_CHILD_TYPE"] as string;
        public static string CHILD_PRODUCT(this DataRow row) => row["_CHILD_PRODUCT"] as string;
        public static string CHILD_VERSION(this DataRow row) => row["_CHILD_VERSION"] as string;
        public static string CAPTION(this DataRow row) => row["_CAPTION"] as string;
        public static string COMMENTS(this DataRow row) => row["_COMMENTS"] as string;
        public static string CHECKOUTNAME(this DataRow row) => row["_CHECKOUTNAME"] as string;
        public static string NATIVENAME(this DataRow row) => row["_NATIVENAME"] as string;
        public static string FULLNAME(this DataRow row) => row["_FULLNAME"] as string;
        public static string FILE_NAME(this DataRow row) => row["_FILE_NAME"] as string;
        public static string MIN_VALUE(this DataRow row) => row["_MIN_VALUE"] as string;
        public static string MAX_VALUE(this DataRow row) => row["_MAX_VALUE"] as string;
        public static string MAIL(this DataRow row) => row["_MAIL"] as string;
        public static string NODE(this DataRow row) => row["_NODE"] as string;
        public static string TOOLNAME(this DataRow row) => row["_TOOLNAME"] as string;
        public static string EXT(this DataRow row) => row["_EXT"] as string;
        public static string EXTENSION(this DataRow row) => row["_EXTENSION"] as string;
        public static string DEFAULT_VALUE(this DataRow row) => row["_DEFAULT_VALUE"] as string;

        /// <summary>
        /// В случае когда поле содержит значение по умолчанию. 
        /// <br/>** В случаях когда поле содержит признак по умолчанию, необходимо использовать метод расширения <see cref="DEFAULT_BOOL(DataRow)">DEFAULT_BOOL</see>.
        /// </summary>
        public static string DEFAULT(this DataRow row) => row["_DEFAULT"] as string;
        public static bool DEFAULT_BOOL(this DataRow row) => row["_DEFAULT"] as int? == 1;
        public static bool IS_ADMIN(this DataRow row) => row["_IS_ADMIN"] as int? == 1;
        public static bool IS_FAVORITE(this DataRow row) => row["_IS_FAVORITE"] as int? == 1;
        public static bool USED(this DataRow row) => row["_USED"] as int? == 1;
        public static bool BASIC(this DataRow row) => row["_BASIC"] as int? == 1;
        public static bool BASICUNIT(this DataRow row) => row["_BASICUNIT"] as int? == 1;
        public static bool DOCUMENT(this DataRow row) => row["_DOCUMENT"] as int? == 1;
        public static bool SYSTEM(this DataRow row) => row["_SYSTEM"] as int? == 1;
        public static bool ONLYLISTITEMS(this DataRow row) => row["_ONLYLISTITEMS"] as int? == 1;
        public static bool OBLIGATORY(this DataRow row) => row["_OBLIGATORY"] as int? == 1;
        public static bool CANCREATE(this DataRow row) => row["_CANCREATE"] as int? == 1;
        public static bool NOVERSIONS(this DataRow row) => row["_NOVERSIONS"] as int? == 1;
        public static bool TRANSSIGNROLES(this DataRow row) => row["_TRANSSIGNROLES"] as int? == 1;
        public static DateTime DATE(this DataRow row) => row["_DATE"] as DateTime? ?? DateTime.MaxValue;
        public static DateTime DATEOFCREATE(this DataRow row) => row["_DATEOFCREATE"] as DateTime? ?? DateTime.MaxValue;
        public static DateTime MODIFIED(this DataRow row) => row["_MODIFIED"] as DateTime? ?? DateTime.MaxValue;
        public static LinkKind LINKKIND(this DataRow row) => (LinkKind)row["_LINKKIND"];
        public static SubjectType TYPE_SUBJECT(this DataRow row) => (SubjectType)row["_TYPE"];
        public static LinkDirection DIRECTION(this DataRow row) => (LinkDirection)row["_DIRECTION"];
        public static AttributeType ATTRTYPE(this DataRow row) => (AttributeType)row["_ATTRTYPE"];
        public static ReportRecordType OBJECT_TYPE(this DataRow row) => (ReportRecordType)row["_OBJECT_TYPE"];
        public static ReportTargetType USED_REP(this DataRow row) => (ReportTargetType)row["_USED"];
        public static PDMAccessLevels ACCESSLEVEL(this DataRow row) => (PDMAccessLevels)row["_ACCESSLEVEL"];
        public static PDMAccessLevels DEFACCESSLEVEL(this DataRow row) => (PDMAccessLevels)row["_DEFACCESSLEVEL"];
        public static PDMAccessLevels MAXACCESSLEVEL(this DataRow row) => (PDMAccessLevels)row["_MAXACCESSLEVEL"];
        public static PDMAccessLevels APPOINTEDACCESSLEVEL(this DataRow row) => (PDMAccessLevels)row["_APPOINTEDACCESSLEVEL"];
        public static PDMLockLevels LOCKED(this DataRow row) => (PDMLockLevels)row["_LOCKED"];
    }
}
