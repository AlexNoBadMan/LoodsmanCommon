using System;
using System.Data;

namespace LoodsmanCommon
{
  public class LFile : Entity
  {
    private readonly ILoodsmanProxy _proxy;
    private string _path = null;

    internal LFile(ILoodsmanProxy proxy, ILObject owner, DataRow dataRow) : base(dataRow.ID_FILE(), dataRow.NAME())
    {
      _proxy = proxy;
      Owner = owner;
      RelativeFolderPath = dataRow.LOCALNAME();
      Size = dataRow.SIZE();
      CRC = dataRow.CRC();
      Created = dataRow.DATEOFCREATE();
      Modified = dataRow.MODIFIED();
    }

    public ILObject Owner { get; }
    public string RelativeFolderPath { get; }
    public long Size { get; }
    public long CRC { get; }
    public DateTime Created { get; }
    public DateTime Modified { get; }
    private string Path => _path ??= _proxy.GetFile(Owner, Name, RelativeFolderPath) ?? string.Empty;

    public string GetFile(bool mount = true)
    {
      return Path;
    }
  }
}