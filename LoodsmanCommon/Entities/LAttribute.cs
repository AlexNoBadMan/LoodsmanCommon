using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace LoodsmanCommon.Entities
{
    public enum AttributeType : short
    {
        LString = 0,
        LInt = 1,
        LDouble = 2,
        LDateTime = 3,
        LRTFText = 5,
        LImage = 6
    }

    public class LAttribute : Entity
    {
        public AttributeType AttrType { get; }
        public string Default { get; }
        public List<string> ListValue { get; }
        public bool OnlyIsItems { get; }
        public bool System { get; set; }
        public LAttribute(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            AttrType = (AttributeType)dataRow["_ATTRTYPE"];
            Default = dataRow["_DEFAULT"] as string;
            ListValue = dataRow["_LIST"].ToString().Split(new char[2] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //Encoding.Default.GetString(dataRow["_LIST"] as byte[])
            //.Split(Eol_sep, StringSplitOptions.RemoveEmptyEntries).ToList();
            OnlyIsItems = (int)dataRow["_ONLYLISTITEMS"] == 1;
            System = (short)dataRow["_SYSTEM"] == 1;
        }
    }
}