using Loodsman;
using PDMObjects;
using System;
using System.Data;
using System.Linq;

namespace LoodsmanCommon
{
    public interface ILObject
    {
        ILObject Parent { get; set; }
        int Id { get; set; }
        LType Type { get; set; }
        string Product { get; set; }
        string Version { get; set; }
        LStateInfo State { get; set; }
        bool IsDocument { get; }
        PDMAccessLevels AccessLevel { get; set; }
        PDMLockLevels LockLevel { get; set; }
        NamedEntityCollection<LAttribute> Attributes { get; }
        EntityCollection<LFile> Files { get; }
        LUser Creator { get; }
        DateTime Created { get; }
    }

    public class LObject : ILObject
    {
        private NamedEntityCollection<LAttribute> _attributes;
        private LStateInfo _state;
        private readonly ILoodsmanProxy _proxy;
        private DateTime? _created;
        private LUser _creator;
        private EntityCollection<LFile> _files;

        public ILObject Parent { get; set; }
        
        public int Id { get; set; }
        
        public LType Type { get; set; }
        
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
        
        public NamedEntityCollection<LAttribute> Attributes => _attributes ??= new NamedEntityCollection<LAttribute>(() => _proxy.GetAttributes(this), 10);
       
        public EntityCollection<LFile> Files => _files ??= IsDocument ?
            new EntityCollection<LFile>(() => _proxy.INetPC.Native_GetInfoAboutVersion(Id, GetInfoAboutVersionMode.Mode7).Select(x => new LFile(this, x))) :
            new EntityCollection<LFile>(() => Enumerable.Empty<LFile>());

        public LUser Creator => _creator ??= InitCreationInfo().creator;
       
        public DateTime Created => _created ??= InitCreationInfo().created;

        internal LObject(ILoodsmanProxy proxy, LType type, LStateInfo state)
        {
            _proxy = proxy;
            Type = type;
            Version = Type.IsVersioned ? Constants.DEFAULT_NEW_VERSION : Constants.DEFAULT_NEW_NO_VERSION;
            _state = state;
        }

        private LObject(ILoodsmanProxy proxy, string typeName, string stateName) :
            this(proxy, proxy.Meta.Types[typeName], proxy.Meta.States[stateName])
        { }

        public LObject(DataRow dataRow, ILoodsmanProxy proxy) : this(proxy, dataRow["_TYPE"] as string, dataRow["_STATE"] as string)
        {
            Id = (int)dataRow["_ID_VERSION"];
            Product = dataRow["_PRODUCT"] as string;
            Version = dataRow["_VERSION"] as string;
            //IsDocument = (short)dataRow["_DOCUMENT"] == 1;
            AccessLevel = (PDMAccessLevels)dataRow["_ACCESSLEVEL"];
            LockLevel = (PDMLockLevels)dataRow.GetValueOrDefault<int>("_LOCKED");
        }

        public LObject(IPluginCall pc, ILoodsmanProxy proxy) : this(proxy, pc.stType, pc.Selected.StateName)
        {
            Id = pc.IdVersion;
            Product = pc.stProduct;
            Version = pc.stVersion;
            AccessLevel = pc.Selected.AccessLevel;
            LockLevel = pc.Selected.LockLevel;
            //IsDocument = pc.Selected.IsDocument;
            Parent = pc.ParentObject is IPDMObject ? new LObject(pc.ParentObject, proxy) : null;
        }

        public LObject(IPDMObject obj, ILoodsmanProxy proxy) : this(proxy, obj.TypeName, obj.StateName)
        {
            Id = obj.ID;
            Product = obj.Name;
            Version = obj.Version;
            AccessLevel = obj.AccessLevel;
            LockLevel = obj.LockLevel;
            //IsDocument = obj.IsDocument;
            Parent = obj.Parent is IPDMLink link ? new LObject(link.ParentObject, proxy) : null;
        }

        private (DateTime created, LUser creator) InitCreationInfo()
        {
            if (_created == null && _creator == null)//В Лоцмане допустимо чтобы владелец(_creator) не был указан
            {
                var dtCreationInfo = _proxy.INetPC.Native_GetInfoAboutVersion(Id, GetInfoAboutVersionMode.Mode13).Rows[0];
                _creator = _proxy.Meta.Users.TryGetValue(dtCreationInfo["_NAME"] as string, out var lUser) ? lUser : null;
                _created = dtCreationInfo["_DATEOFCREATE"] as DateTime? ?? DateTime.MinValue;
            }
            return ((DateTime)_created, _creator);
        }
    }
}
