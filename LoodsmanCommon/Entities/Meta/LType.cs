using System.Data;
using System.Linq;
using System.Collections.Generic;
using Ascon.Plm.Loodsman.PluginSDK;

namespace LoodsmanCommon.Entities.Meta
{

    //internal sealed class LTypeCollection : NamedMetaItemCollection<LType>
    //{
    //    private readonly INetPluginCall _iNetPC;
    //    private readonly NamedMetaItemCollection<LAttribute> _lAttributes;
    //    private readonly NamedMetaItemCollection<LState> _lStates;

    //    internal LTypeCollection(INetPluginCall iNetPC, NamedMetaItemCollection<LAttribute> lAttributes, NamedMetaItemCollection<LState> lStates)
    //    {
    //        _iNetPC = iNetPC;
    //        _lAttributes = lAttributes;
    //        _lStates = lStates;
    //        Init();
    //    }

    //    protected override LType CreateEntity(DataRow dataRow) => new LType(_iNetPC, dataRow, _lAttributes, _lStates);
    //    protected override DataRowCollection GetMetadata() => _iNetPC.Native_GetTypeListEx().Rows;
    //}
    
    public class LType : EntityIcon
    {
        private readonly INetPluginCall _iNetPC;
        private readonly NamedCollection<LAttribute> _lAttributes;
        private IReadOnlyCollection<LTypeAttribute> _attributes;

        /// <summary>
        /// Ключевой атрибут типа.
        /// </summary>
        public LAttribute KeyAttribute { get; }

        /// <summary>
        /// Является ли документом.
        /// </summary>
        public bool IsDocument { get; }

        /// <summary>
        /// Является ли версионным.
        /// </summary>
        public bool IsVersioned { get; }

        /// <summary>
        /// Состояние по умолчанию.
        /// </summary>
        public LState DefaultState { get; }

        /// <summary>
        /// Может ли быть проектом.
        /// </summary>
        public bool CanBeProject { get; }

        /// <summary>
        /// Может ли текущий пользователь создавать объекты данного типа.
        /// </summary>
        public bool CanCreate { get; }

        /// <summary>
        /// Список возможных атрибутов типа, включая служебные.
        /// </summary>
        public IReadOnlyCollection<LTypeAttribute> Attributes => _attributes ??= _iNetPC.Native_GetInfoAboutType(Name, GetInfoAboutTypeMode.Mode12)
                                                .Select(x =>
                                                {
                                                    var id = (int)x["_ID"];
                                                    return new LTypeAttribute(_lAttributes.First(a => a.Id == id), (short)x["_OBLIGATORY"] == 1);
                                                })
                                                .ToReadOnlyList();

        internal LType(INetPluginCall iNetPC, DataRow dataRow, NamedCollection<LAttribute> lAttributes, NamedCollection<LState> lStates, string nameField = "_TYPENAME") : base(dataRow, nameField)
        {
            _iNetPC = iNetPC;
            _lAttributes = lAttributes;
            KeyAttribute = _lAttributes[dataRow["_ATTRNAME"] as string];
            IsDocument = (int)dataRow["_DOCUMENT"] == 1;
            IsVersioned = (int)dataRow["_NOVERSIONS"] == 0;
            DefaultState = lStates[dataRow["_DEFAULTSTATE"] as string];
            CanBeProject = (int)dataRow["_CANBEPROJECT"] == 1;
            CanCreate = (int)dataRow["_CANCREATE"] == 1;
        }
    }
}