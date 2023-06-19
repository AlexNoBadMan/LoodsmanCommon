using Loodsman;
using PDMObjects;
using System;
using System.Data;

namespace LoodsmanCommon
{
  public interface ILObject
  {
    ILObject Parent { get; set; }
    int Id { get; set; }
    LTypeInfo Type { get; set; }
    string Product { get; set; }
    string Version { get; set; }
    LStateInfo State { get; set; }
    bool IsDocument { get; }
    PDMAccessLevels AccessLevel { get; set; }
    PDMLockLevels LockLevel { get; set; }
    NamedEntityCollection<LObjectAttribute> Attributes { get; }
    EntityCollection<LFile> Files { get; }
    LUser Creator { get; }
    DateTime Created { get; }
  }

  public class LObject : ILObject
  {
    private NamedEntityCollection<LObjectAttribute> _attributes;
    private LStateInfo _state;
    private readonly ILoodsmanProxy _proxy;
    private EntityCollection<LFile> _files;
    private CreationInfo _creationInfo;

    public LObject(DataRow dataRow, ILoodsmanProxy proxy) : this(proxy, dataRow["_TYPE"] as string, dataRow["_STATE"] as string)
    {
      Id = dataRow.ID_VERSION();
      Product = dataRow.PRODUCT();
      Version = dataRow.VERSION();
      AccessLevel = dataRow.ACCESSLEVEL();
      LockLevel = dataRow.LOCKED();
    }

    public LObject(IPluginCall pc, ILoodsmanProxy proxy) : this(proxy, pc.stType, pc.Selected.StateName)
    {
      Id = pc.IdVersion;
      Product = pc.stProduct;
      Version = pc.stVersion;
      AccessLevel = pc.Selected.AccessLevel;
      LockLevel = pc.Selected.LockLevel;
      Parent = pc.ParentObject is IPDMObject ? new LObject(pc.ParentObject, proxy) : null;
    }

    public LObject(IPDMObject obj, ILoodsmanProxy proxy) : this(proxy, obj.TypeName, obj.StateName)
    {
      Id = obj.ID;
      Product = obj.Name;
      Version = obj.Version;
      AccessLevel = obj.AccessLevel;
      LockLevel = obj.LockLevel;
      Parent = obj.Parent is IPDMLink link ? new LObject(link.ParentObject, proxy) : null;
    }

    internal LObject(ILoodsmanProxy proxy, LTypeInfo type, LStateInfo state)
    {
      _proxy = proxy;
      Type = type;
      Version = Type.IsVersioned ? Constants.DEFAULT_NEW_VERSION : Constants.DEFAULT_NEW_NO_VERSION;
      _state = state;
    }

    private LObject(ILoodsmanProxy proxy, string typeName, string stateName) :
        this(proxy, proxy.Meta.Types[typeName], proxy.Meta.States[stateName])
    { }

    public ILObject Parent { get; set; }

    public int Id { get; set; }

    public LTypeInfo Type { get; set; }

    public string Product { get; set; }

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

    public NamedEntityCollection<LObjectAttribute> Attributes => _attributes ??= new NamedEntityCollection<LObjectAttribute>(() => _proxy.GetAttributes(this), 10);

    public EntityCollection<LFile> Files => _files ??= new EntityCollection<LFile>(() => _proxy.GetFiles(this));

    public LUser Creator => (_creationInfo ??= _proxy.GetCreationInfo(Id)).Creator;

    public DateTime Created => (_creationInfo ??= _proxy.GetCreationInfo(Id)).Created;

  }
}
