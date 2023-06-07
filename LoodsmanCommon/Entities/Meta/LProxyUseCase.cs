using System.Data;


namespace LoodsmanCommon
{
  public class LProxyUseCase : Entity
  {
    public string TypeName { get; }
    public string DocumentType { get; }
    public string Extension { get; }
    internal LProxyUseCase(DataRow dataRow, string nameField = "_PROXYNAME") : base(dataRow, nameField)
    {
      TypeName = dataRow.PARENTNAME();
      DocumentType = dataRow.DOCNAME();
      Extension = $".{dataRow.EXTENSION()}";
    }
  }
}