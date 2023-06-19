using System.Data;

namespace LoodsmanCommon
{
  public class LTypeInfo : EntityIcon
  {
    private readonly string _keyAttributeName;
    private readonly string _defaultStateName;
    private readonly ILoodsmanMeta _meta;
    private LAttributeInfo _keyAttribute;
    private LStateInfo _defaultState;
    private NamedEntityCollection<LTypeAttributeInfo> _attributes;

    internal LTypeInfo(ILoodsmanMeta meta, DataRow dataRow) : base(dataRow.ID(), dataRow.NAME(), dataRow.ICON())
    {
      _meta = meta;
      _keyAttributeName = dataRow.ATTRNAME();
      IsDocument = dataRow.DOCUMENT();
      IsVersioned = !dataRow.NOVERSIONS();
      _defaultStateName = dataRow.DEFAULTSTATE();
      CanBeProject = dataRow.CANBEPROJECT();
      CanCreate = dataRow.CANCREATE();
    }

    /// <summary> Ключевой атрибут типа. </summary>
    public LAttributeInfo KeyAttribute => _keyAttribute ??= _meta.Attributes[_keyAttributeName];

    /// <summary> Является ли документом. </summary>
    public bool IsDocument { get; }

    /// <summary> Является ли версионным. </summary>
    public bool IsVersioned { get; }

    /// <summary> Состояние по умолчанию. </summary>
    public LStateInfo DefaultState => _defaultState ??= _meta.States[_defaultStateName];

    /// <summary> Может ли быть проектом. </summary>
    public bool CanBeProject { get; }

    /// <summary> Может ли текущий пользователь создавать объекты данного типа. </summary>
    public bool CanCreate { get; }

    /// <summary> Список возможных атрибутов типа, включая служебные. </summary>
    public NamedEntityCollection<LTypeAttributeInfo> Attributes => _attributes ??= _meta.GetTypeAttrbiutes(Name);
  }
}