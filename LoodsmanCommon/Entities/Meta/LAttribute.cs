using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace LoodsmanCommon.Entities.Meta
{
    public class LAttribute : Entity
    {
        public AttributeType Type { get; }
        public string DefaultValue { get; }
        public List<string> ListValue { get; }
        public bool OnlyIsItems { get; }
        public bool IsSystem { get; }

        public LAttribute(int id, string name, AttributeType type, string defaultValue, List<string> listValue, bool onlyIsItems, bool isSystem) : base(id, name)
        {
            Type = type;
            DefaultValue = defaultValue;
            ListValue = listValue;
            OnlyIsItems = onlyIsItems;
            IsSystem = isSystem;
        }

        internal LAttribute(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            Type = (AttributeType)dataRow["_ATTRTYPE"];
            DefaultValue = dataRow["_DEFAULT"] as string;
            ListValue = dataRow["_LIST"].ToString().Split(new char[2] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            OnlyIsItems = (int)dataRow["_ONLYLISTITEMS"] == 1;
            IsSystem = (short)dataRow["_SYSTEM"] == 1;
        }
    }
}