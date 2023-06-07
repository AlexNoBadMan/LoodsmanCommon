using System;
using System.Data;

namespace LoodsmanCommon
{
  public class LFile : Entity
  {
    public ILObject Owner { get; }
    public string RelativePath { get; }
    public long Size { get; }
    public long CRC { get; }
    public DateTime Created { get; }
    public DateTime Modified { get; }

    internal LFile(ILObject owner, DataRow dataRow) : base((int)dataRow["_ID_FILE"], dataRow["_NAME"] as string)
    {
      Owner = owner;
      RelativePath = dataRow.LOCALNAME();
      Size = dataRow.SIZE();
      CRC = dataRow.CRC();
      Created = dataRow.DATEOFCREATE();
      Modified = dataRow.MODIFIED();
    }
  }
}
