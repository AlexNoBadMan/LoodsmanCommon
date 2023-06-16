using System.Data;

namespace LoodsmanCommon
{
  public class LLinkInfo : EntityIcon
  {
    public string InverseName { get; }
    public bool VerticalLink { get; }
    public int Order { get; }

    internal LLinkInfo(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
    {
      InverseName = dataRow.INVERSENAME();
      VerticalLink = dataRow.TYPE_BOOL();
      Order = dataRow.ORDER();
    }
  }
}