using PDMObjects;
using System;

namespace LoodsmanCommon
{
  public interface ILObject : IEntity, INamedEntity, ILAttributeOwner
  {
    ILObject Parent { get; set; }
    LTypeInfo Type { get; set; }
    string Version { get; set; }
    LStateInfo State { get; set; }
    bool IsDocument { get; }
    PDMAccessLevels AccessLevel { get; set; }
    PDMLockLevels LockLevel { get; set; }
    EntityCollection<LFile> Files { get; }
    LUser Creator { get; }
    DateTime Created { get; }
  }
}
