using System.Data;

namespace LoodsmanCommon
{
  public class LTypeInfo : EntityIcon, ILAttributeInfoOwner<ILAttributeInfo>
  {
    private readonly string _keyAttributeName;
    private readonly string _defaultStateName;
    private readonly ILoodsmanMeta _meta;
    private LAttributeInfo _keyAttribute;
    private LStateInfo _defaultState;
    private NamedEntityCollection<ILAttributeInfo> _attributes;

    internal LTypeInfo(ILoodsmanMeta meta, DataRow dataRow) : base(dataRow.ID(), dataRow.TYPENAME(), dataRow.ICON())
    {
      _meta = meta;
      _keyAttributeName = dataRow.ATTRNAME();
      IsDocument = dataRow.DOCUMENT();
      IsVersioned = !dataRow.NOVERSIONS();
      _defaultStateName = dataRow.DEFAULTSTATE();
      CanBeProject = dataRow.CANBEPROJECT();
      CanCreate = dataRow.CANCREATE();
      NativeName = dataRow.NATIVENAME();
      ServerName = dataRow.SERVERNAME();
      IsBO = !string.IsNullOrEmpty(NativeName);
    }

    //public IReadOnlyList<LEffectivityTypeInfo> EffTypes { get; } 

    /// <summary> Возвращает ключевой атрибут типа. </summary>
    public LAttributeInfo KeyAttribute => _keyAttribute ??= _meta.Attributes[_keyAttributeName];

    /// <summary> Возвращает признак того что тип Является документом. </summary>
    public bool IsDocument { get; }

    /// <summary> Возвращает признак того что тип Является версионным. </summary>
    public bool IsVersioned { get; }

    /// <summary> Возвращает состояние по умолчанию. </summary>
    public LStateInfo DefaultState => _defaultState ??= _meta.States[_defaultStateName];

    /// <summary> Возвращает признак того что тип может быть проектом. </summary>
    public bool CanBeProject { get; }

    /// <summary> Возвращает признак того что текущий пользователь может создавать объекты данного типа. </summary>
    public bool CanCreate { get; }

    /// <summary> Возвращает имя бизнес-объекта. </summary>
    public string NativeName { get; }

    /// <summary> Возвращает COM-сервер бизнес-объекта. </summary>
    public string ServerName { get; }

    /// <summary> Возвращает признак того что тип является бизнес-объектом. </summary>
    public bool IsBO { get; }

    /// <summary> Возвращает список возможных атрибутов типа, включая служебные. </summary>
    public NamedEntityCollection<ILAttributeInfo> Attributes => _attributes ??= _meta.GetTypeAttrbiutes(Name);
  }
}