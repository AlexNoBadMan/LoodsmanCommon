using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoodsmanCommon
{
    public interface ILoodsmanObject
    {
        int Id { get; set; }
        string Type { get; set; }
        string Product { get; set; }
        string Version { get; set; }
        string State { get; set; }
        bool IsDocument { get; set; }
    }

    internal class LoodsmanObject : ILoodsmanObject
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Product { get; set; }
        public string Version { get; set; }
        public string State { get; set; }
        public bool IsDocument { get; set; }

        public LoodsmanObject(DataRow dataRow)
        {
            Id = (int)dataRow["_ID_VERSION"];
            Type = dataRow["_TYPE"] as string;
            Product = dataRow["_PRODUCT"] as string;
            Version = dataRow["_VERSION"] as string;
            State = dataRow["_STATE"] as string;
            IsDocument = (short)dataRow["_DOCUMENT"] == 1;
        }

        public LoodsmanObject()
        {

        }
    }
}
