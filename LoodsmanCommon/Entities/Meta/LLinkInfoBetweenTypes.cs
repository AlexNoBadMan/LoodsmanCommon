using System.Data;

namespace LoodsmanCommon
{
  public class LLinkInfoBetweenTypes
  {
    private readonly ILoodsmanMeta _meta;
    private readonly string _name;
    private LLinkInfo _link;

    internal LLinkInfoBetweenTypes(ILoodsmanMeta meta, DataRow dataRow)
    {
      _meta = meta;
      _name = dataRow.LINKTYPE();
      TypeId1 = dataRow.TYPE_ID_1();
      TypeName1 = dataRow.TYPE_NAME_1();
      TypeId2 = dataRow.TYPE_ID_2();
      TypeName2 = dataRow.TYPE_NAME_2();
      Direction = dataRow.DIRECTION();
      IsQuantity = dataRow.IS_QUANTITY();
    }

    private LLinkInfo Link => _link ??= _meta.Links[_name];
    public int Id => Link.Id;
    public string Name => Link.Name;
    public string InverseName => Link.InverseName;
    public bool IsVertical => Link.VerticalLink;
    public int TypeId1 { get; }
    public string TypeName1 { get; }
    public int TypeId2 { get; }
    public string TypeName2 { get; }
    public LinkDirection Direction { get; internal set; }
    public bool IsQuantity { get; }
  }
}