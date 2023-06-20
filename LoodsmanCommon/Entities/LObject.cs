using Loodsman;
using PDMObjects;
using System;
using System.Data;

namespace LoodsmanCommon
{
  public class LObject : LAttributeOwner, ILObject
  {
    #region Поля
    private LStateInfo _state;
    private readonly ILoodsmanProxy _proxy;
    private EntityCollection<LFile> _files;
    private CreationInfo _creationInfo;
    #endregion

    #region Конструкторы
    public LObject(DataRow dataRow, ILoodsmanProxy proxy) : this(proxy, dataRow.ID_VERSION(), dataRow.PRODUCT(), dataRow.TYPE(), dataRow.STATE())
    {
      Version = dataRow.VERSION();
      AccessLevel = dataRow.ACCESSLEVEL();
      LockLevel = dataRow.LOCKED();
    }

    public LObject(IPluginCall pc, ILoodsmanProxy proxy) : this(proxy, pc.IdVersion, pc.stProduct, pc.stType, pc.Selected.StateName)
    {
      Version = pc.stVersion;
      AccessLevel = pc.Selected.AccessLevel;
      LockLevel = pc.Selected.LockLevel;
      Parent = pc.ParentObject is IPDMObject ? new LObject(pc.ParentObject, proxy) : null;
    }

    public LObject(IPDMObject obj, ILoodsmanProxy proxy) : this(proxy, obj.ID, obj.Name, obj.TypeName, obj.StateName)
    {
      Version = obj.Version;
      AccessLevel = obj.AccessLevel;
      LockLevel = obj.LockLevel;
      Parent = obj.Parent is IPDMLink link ? new LObject(link.ParentObject, proxy) : null;
    }

    internal LObject(ILoodsmanProxy proxy, int id, string name, LTypeInfo type, LStateInfo state) : base(id, name)
    {
      _proxy = proxy;
      Type = type;
      Version = Type.IsVersioned ? Constants.DEFAULT_NEW_VERSION : Constants.DEFAULT_NEW_NO_VERSION;
      _state = state;
    }

    private LObject(ILoodsmanProxy proxy, int id, string name, string typeName, string stateName) :
        this(proxy, id, name, proxy.Meta.Types[typeName], proxy.Meta.States[stateName])
    { }
    #endregion

    #region Свойства
    public ILObject Parent { get; set; }
    public LTypeInfo Type { get; set; }
    public string Version { get; set; }
    public LStateInfo State
    {
      get => _state;
      set
      {
        if (_state == value)
          return;

        _proxy.UpdateState(Id, value);
        _state = value;
      }
    }

    public bool IsDocument => Type.IsDocument;
    public PDMAccessLevels AccessLevel { get; set; }
    public PDMLockLevels LockLevel { get; set; }
    public EntityCollection<LFile> Files => _files ??= new EntityCollection<LFile>(() => _proxy.GetFiles(this));
    public LUser Creator => (_creationInfo ??= _proxy.GetCreationInfo(Id)).Creator;
    public DateTime Created => (_creationInfo ??= _proxy.GetCreationInfo(Id)).Created;
    #endregion

    #region Методы
    public override void UpdateAttribute(string name, object value, LMeasureUnit unit)
    {
      _proxy.UpAttrValueById(Id, name, value, unit);
    }

    protected override NamedEntityCollection<ILAttribute> GetAttributes() => new NamedEntityCollection<ILAttribute>(() => _proxy.GetAttributes(this), 10);
    #endregion
  }
}
