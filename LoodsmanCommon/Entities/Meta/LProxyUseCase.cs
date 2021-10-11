using System.Data;


namespace LoodsmanCommon.Entities.Meta
{
    public class LProxyUseCase : Entity
    {
        public string TypeName { get; }
        public string DocumentType { get; }
        public string Extension { get; }
        public LProxyUseCase(DataRow dataRow, string nameField = "_PROXYNAME") : base(dataRow, nameField)
        {
            TypeName = dataRow["_PARENTNAME"] as string;
            DocumentType = dataRow["_DOCNAME"] as string;
            Extension = $".{dataRow["_EXTENSION"]}";
        }
    }
}