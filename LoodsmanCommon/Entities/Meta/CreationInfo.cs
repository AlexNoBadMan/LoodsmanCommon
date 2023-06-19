using System;

namespace LoodsmanCommon
{
  public class CreationInfo
  {
    public DateTime Created { get; internal set; }
    public LUser Creator { get; internal set; }
  }
}
