using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace LoodsmanCommon.Entities.Meta
{
    public class LAttribute : Entity
    {
        public AttributeType Type { get; }
        public string DefaultValue { get; }
        public IReadOnlyList<string> ListValue { get; }
        public bool OnlyIsItems { get; }
        public bool IsSystem { get; }
        public bool IsMeasured => Type == AttributeType.Int || Type == AttributeType.Double;

        internal LAttribute(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            Type = (AttributeType)dataRow["_ATTRTYPE"];
            DefaultValue = dataRow["_DEFAULT"] as string;
            ListValue = new ReadOnlyCollection<string>(dataRow["_LIST"].ToString().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            OnlyIsItems = (int)dataRow["_ONLYLISTITEMS"] == 1;
            //Метод GetInfoAboutType возвращает поле _ONLYLISTITEMS как short и это вызывает исключение при приведении типов
            IsSystem = (short)dataRow["_SYSTEM"] == 1;
        }
    }
}