using LoodsmanCommon.Entities.Meta;
using System;
using System.Data;

namespace LoodsmanCommon.Entities
{
    public class LFile : Entity
    {
        public ILoodsmanObject Owner { get; }
        public string RelativePath { get; }
        public long Size { get; }
        public long CRC { get; }
        public DateTime Created { get; }
        public DateTime Modified { get; }

        internal LFile(ILoodsmanObject owner, DataRow dataRow) : base((int)dataRow["_ID_FILE"], dataRow["_NAME"] as string)
        {
            Owner = owner;
            RelativePath = dataRow["_LOCALNAME"] as string;
            Size = (long)dataRow["_SIZE"];
            CRC = (long)dataRow["_CRC"];
            Created = dataRow["_DATEOFCREATE"] as DateTime? ?? DateTime.MaxValue;
            Modified = dataRow["_MODIFIED"] as DateTime? ?? DateTime.MaxValue;
        }
    }
}
