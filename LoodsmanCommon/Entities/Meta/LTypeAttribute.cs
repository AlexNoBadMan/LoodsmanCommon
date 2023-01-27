using System.Collections.Generic;

namespace LoodsmanCommon
{
    public class LTypeAttribute : IEntity, INamedEntity
    {
        private readonly LAttributeInfo _lAttribute;

        public int Id => _lAttribute.Id;
        public string Name => _lAttribute.Name;
        public AttributeType Type => _lAttribute.Type;
        public string DefaultValue => _lAttribute.DefaultValue;
        public IReadOnlyList<string> ListValue => _lAttribute.ListValue;
        public bool OnlyIsItems => _lAttribute.OnlyIsItems;
        public bool IsSystem => _lAttribute.IsSystem;
        public bool IsObligatory { get; }
        public bool IsMeasured => _lAttribute.IsMeasured;

        internal LTypeAttribute(LAttributeInfo lAttribute, bool isObligatory)
        {
            _lAttribute = lAttribute;
            IsObligatory = isObligatory;
        }
    }
}
