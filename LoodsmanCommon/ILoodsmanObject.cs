using Loodsman;
using PDMObjects;
using System.Data;

namespace LoodsmanCommon
{
    public interface ILoodsmanObject
    {
        ILoodsmanObject Parent { get; set; }
        int Id { get; set; }
        string Type { get; set; }
        string Product { get; set; }
        string Version { get; set; }
        string State { get; set; }
        bool IsDocument { get; set; }
    }

    internal class LoodsmanObject : ILoodsmanObject
    {
        public ILoodsmanObject Parent { get; set; }
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

        public LoodsmanObject(IPluginCall pc)
        {
            Id = pc.IdVersion;
            Type = pc.stType;
            Product = pc.stProduct;
            Version = pc.stVersion;
            State = pc.Selected.StateName;
            IsDocument = pc.Selected.IsDocument;
            if (pc.ParentObject != null)
                Parent = new LoodsmanObject(pc.ParentObject);
        }

        public LoodsmanObject(IPDMObject obj)
        {
            Id = obj.ID;
            Type = obj.TypeName;
            Product = obj.Name;
            Version = obj.Version;
            State = obj.StateName;
            IsDocument = obj.IsDocument;
        }

        public LoodsmanObject()
        {

        }
    }
}
