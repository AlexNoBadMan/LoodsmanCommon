using System.Data;

namespace LoodsmanCommon
{
  public class LProxyUseCase : Entity
  {
    internal LProxyUseCase(DataRow dataRow) : base(dataRow.ID(), dataRow.PROXYNAME())
    {
      TypeName = dataRow.PARENTNAME();
      DocumentType = dataRow.DOCNAME();
      Extension = $".{dataRow.EXTENSION()}";
    }

    public string TypeName { get; }
    public string DocumentType { get; }
    public string Extension { get; }
  }
}