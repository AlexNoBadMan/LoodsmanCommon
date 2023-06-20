using System.Data;

namespace LoodsmanCommon
{
  public class LLinkInfoBetweenTypes : ILAttributeInfoOwner<ILAttributeInfo>
  {
    private readonly ILoodsmanMeta _meta;
    private readonly string _parentTypeName;
    private readonly string _childTypeName;
    private readonly string _name;
    private NamedEntityCollection<ILAttributeInfo> _attributes;
    private LLinkInfo _link;
    private LTypeInfo _parentType;
    private LTypeInfo _childType;

    internal LLinkInfoBetweenTypes(ILoodsmanMeta meta, DataRow dataRow)
    {
      _meta = meta;
      _name = dataRow.LINKTYPE();
      Id = dataRow.ID_LINKTYPE();
      ParentTypeId = dataRow.TYPE_ID_1();
      _parentTypeName = dataRow.TYPE_NAME_1();
      _childTypeName = dataRow.TYPE_NAME_2();
      ChildTypeId = dataRow.TYPE_ID_2();
      Direction = dataRow.DIRECTION();
      IsQuantity = dataRow.IS_QUANTITY();
    }

    public int Id { get; }
    public LTypeInfo ParentType => _parentType ??= _meta.Types[_parentTypeName];
    public LTypeInfo ChildType => _childType ??= _meta.Types[_childTypeName];
    internal int ParentTypeId { get; }
    internal int ChildTypeId { get; }
    public LinkDirection Direction { get; internal set; }
    public bool IsQuantity { get; }
    public LLinkInfo Link => _link ??= _meta.Links[_name];
    public NamedEntityCollection<ILAttributeInfo> Attributes => _attributes ??= _meta.GetLinkAttrbiutesForTypes(ParentType.Name, ChildType.Name, _name);
  }
}