using System.Data;
using System.Linq;
using System.Collections.Generic;


namespace LoodsmanCommon.Entities
{
    public class LType : EntityIcon
    {
        public LAttribute KeyAttr { get; }
        public bool IsDocument { get; }
        public bool Versioned { get; }
        public LState DefaultState { get; }
        public bool CanBeProject { get; }
        public bool CanCreate { get; }

        public LType(DataRow dataRow, IEnumerable<LAttribute> attributes, IEnumerable<LState> states, string nameField = "_NAME") : base(dataRow, nameField)
        {
            KeyAttr = attributes.FirstOrDefault(a => a.Name == dataRow["_ATTRNAME"] as string);
            IsDocument = (int)dataRow["_DOCUMENT"] == 1;
            Versioned = (int)dataRow["_NOVERSIONS"] == 0;
            DefaultState = states.FirstOrDefault(a => a.Name == dataRow["_DEFAULTSTATE"] as string);
            CanBeProject = (int)dataRow["_CANBEPROJECT"] == 1;
            CanCreate = (int)dataRow["_CANCREATE"] == 1;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}