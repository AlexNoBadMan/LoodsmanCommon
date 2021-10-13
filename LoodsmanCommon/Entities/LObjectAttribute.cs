using Ascon.Plm.Loodsman.PluginSDK;
using LoodsmanCommon.Entities.Meta;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoodsmanCommon.Entities
{
    public class LObjectAttributes : ReadOnlyCollection<LObjectAttribute>
    {
        private readonly ILoodsmanObject _owner;
        private readonly ILoodsmanProxy _proxy;

        public LObjectAttributes(ILoodsmanObject owner, ILoodsmanProxy proxy) : base(new List<LObjectAttribute>(owner.Type.Attributes.Count))
        {
            _owner = owner;
            _proxy = proxy;
            Init();
        }

        private void Init()
        {
            foreach (var lObjectAttribute in _proxy.GetAttributes(_owner))
                Items.Add(lObjectAttribute);
        }

        public void Refresh()
        {
            Items.Clear();
            Init();
        }
    }

    public class LObjectAttribute
    {
        private readonly ILoodsmanObject _owner;
        private readonly LTypeAttribute _lTypeAttribute;

        /// <summary>
        /// Уникальный идентификатор атрибута.
        /// </summary>
        public int Id => _lTypeAttribute.Id;
        public string Name => _lTypeAttribute.Name;
        public AttributeType Type => _lTypeAttribute.Type;
        public string DefaultValue => _lTypeAttribute.DefaultValue;
        public IReadOnlyList<string> ListValue => _lTypeAttribute.ListValue;
        public bool OnlyIsItems => _lTypeAttribute.OnlyIsItems;
        public bool IsSystem => _lTypeAttribute.IsSystem;
        public bool IsObligatory => _lTypeAttribute.IsObligatory;
        public object Value { get; set; }

        public LObjectAttribute(ILoodsmanObject owner, LTypeAttribute lTypeAttribute, object value) 
        {
            _owner = owner;
            _lTypeAttribute = lTypeAttribute;
            Value = value;
        }
    }
}
