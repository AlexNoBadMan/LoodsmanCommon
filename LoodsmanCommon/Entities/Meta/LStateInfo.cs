using System.Data;


namespace LoodsmanCommon
{
  public class LStateInfo : EntityIcon
  {
    internal LStateInfo(DataRow dataRow) : base(dataRow.ID(), dataRow.NAME(), dataRow.ICON())
    {

    }
  }
}