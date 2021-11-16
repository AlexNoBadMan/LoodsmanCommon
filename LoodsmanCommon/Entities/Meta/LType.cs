using System.Data;
using System.Linq;
using System.Collections.Generic;
using Ascon.Plm.Loodsman.PluginSDK;
using LoodsmanCommon.Entities.Meta.Collections;

namespace LoodsmanCommon.Entities.Meta
{
    public class LType : EntityIcon
    {
        private readonly INetPluginCall _iNetPC;
        private readonly NamedEntityCollection<LAttribute> _lAttributes;
        private NamedEntityCollection<LTypeAttribute> _attributes;

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
        public NamedEntityCollection<LTypeAttribute> Attributes => _attributes ??= new NamedEntityCollection<LTypeAttribute>(
            () => _iNetPC.Native_GetInfoAboutType(Name, GetInfoAboutTypeMode.Mode12).Select(x => new LTypeAttribute(_lAttributes[x["_NAME"] as string], (short)x["_OBLIGATORY"] == 1)),
            10);

        internal LType(INetPluginCall iNetPC, DataRow dataRow, NamedEntityCollection<LAttribute> lAttributes, NamedEntityCollection<LState> lStates, string nameField = "_TYPENAME") : base(dataRow, nameField)
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