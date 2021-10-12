using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoodsmanCommon.Entities.Meta
{
    public class LTypeAttribute : LAttribute
    {
        public bool IsObligatory { get; }

        public LTypeAttribute(int id, string name, AttributeType type, string defaultValue, List<string> listValue, bool onlyIsItems, bool isSystem, bool isObligatory) 
            : base(id, name, type, defaultValue, listValue, onlyIsItems, isSystem)
        {
            IsObligatory = isObligatory;
        }
            }
}
