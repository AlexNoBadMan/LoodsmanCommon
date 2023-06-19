using System;
using System.Data;

namespace LoodsmanCommon
{
  public class LFile : Entity
  {
    internal LFile(ILObject owner, DataRow dataRow) : base(dataRow.ID_FILE(), dataRow.NAME())
    {
      Owner = owner;
      RelativePath = dataRow.LOCALNAME();
      Size = dataRow.SIZE();
      CRC = dataRow.CRC();
      Created = dataRow.DATEOFCREATE();
      Modified = dataRow.MODIFIED();
    }

    public ILObject Owner { get; }
    public string RelativePath { get; }
    public long Size { get; }
    public long CRC { get; }
    public DateTime Created { get; }
    public DateTime Modified { get; }
  }
}
