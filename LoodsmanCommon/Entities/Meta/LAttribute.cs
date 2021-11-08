using Ascon.Plm.Loodsman.PluginSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace LoodsmanCommon.Entities.Meta
{

    //internal sealed class LAttributeCollection : NamedMetaItemCollection<LAttribute>
    //{
    //    private readonly INetPluginCall _iNetPC;

    //    internal LAttributeCollection(INetPluginCall iNetPC)
    //    {
    //        _iNetPC = iNetPC;
    //        Init();
    //    }

    //    protected override LAttribute CreateEntity(DataRow dataRow) => new LAttribute(dataRow);
    //    protected override DataRowCollection GetMetadata() => _iNetPC.Native_GetAttributeList().Rows;
    //}

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