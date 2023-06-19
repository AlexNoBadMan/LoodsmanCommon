using System.Data;

namespace LoodsmanCommon
{
  public class LLinkInfoBetweenTypes
  {
    internal LLinkInfoBetweenTypes() { }

    internal LLinkInfoBetweenTypes(DataRow dataRow)
    {
      Id = dataRow.ID_LINKTYPE();
      Name = dataRow.LINKTYPE();
      InverseName = dataRow.INVERSENAME();
      TypeId1 = dataRow.TYPE_ID_1();
      TypeName1 = dataRow.TYPE_NAME_1();
      TypeId2 = dataRow.TYPE_ID_2();
      TypeName2 = dataRow.TYPE_NAME_2();
      IsVertical = dataRow.LINKKIND() == 0;
      Direction = dataRow.DIRECTION();
      IsQuantity = dataRow.IS_QUANTITY() == LinkQuantityType.OnlyAsParent;
    }

    public int Id { get; }
    public string Name { get; }
    public string InverseName { get; }
    public int TypeId1 { get; }
    public string TypeName1 { get; }
    public int TypeId2 { get; }
    public string TypeName2 { get; }
    public bool IsVertical { get; }
    public LinkDirection Direction { get; internal set; }
    public bool IsQuantity { get; }
  }
}