using Ascon.Plm.Loodsman.PluginSDK;
using LoodsmanCommon.Entities.Meta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoodsmanCommon.Entities
{
    public class LObjectAttributes : List<LObjectAttribute>
    {
        private readonly ILoodsmanObject _owner;
        private readonly ILoodsmanProxy _proxy;

        public LObjectAttributes(ILoodsmanObject owner, ILoodsmanProxy proxy)
        {
            _owner = owner;
            _proxy = proxy;
            Init();
        }

        private void Init()
        {
            //AddRange();
        }

        public void Refresh()
        {
            Clear();
            Init();
        }
    }

    public class LObjectAttribute : LAttribute
    {
        public object Value { get; set; }
        //public LObjectAttribute(, string nameField = "_NAME") : base(dataRow, nameField)
        //{

        //}
        public LObjectAttribute(int id, string name, AttributeType type, string defaultValue, List<string> listValue, bool onlyIsItems, bool isSystem, object value) : base(id, name, type, defaultValue, listValue, onlyIsItems, isSystem)
        {
            Value = value;
        }
    }
}
