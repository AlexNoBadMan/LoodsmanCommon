using System.Data;

namespace LoodsmanCommon
{
  public class LLinkInfo : EntityIcon
  {
    internal LLinkInfo(DataRow dataRow) : base(dataRow.ID(), dataRow.NAME(), dataRow.ICON())
    {
      InverseName = dataRow.INVERSENAME();
      VerticalLink = dataRow.TYPE_BOOL();
      Order = dataRow.ORDER();
    }

    public string InverseName { get; }
    public bool VerticalLink { get; }
    public int Order { get; }
  }
}