using PDMObjects;
using System;

namespace LoodsmanCommon
{
  public interface ILObject
  {
    ILObject Parent { get; set; }
    int Id { get; set; }
    LTypeInfo Type { get; set; }
    string Product { get; set; }
    string Version { get; set; }
    LStateInfo State { get; set; }
    bool IsDocument { get; }
    PDMAccessLevels AccessLevel { get; set; }
    PDMLockLevels LockLevel { get; set; }
    NamedEntityCollection<LObjectAttribute> Attributes { get; }
    EntityCollection<LFile> Files { get; }
    LUser Creator { get; }
    DateTime Created { get; }
  }
}
