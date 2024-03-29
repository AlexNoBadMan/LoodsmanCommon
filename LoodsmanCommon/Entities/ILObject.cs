﻿using PDMObjects;
using System;

namespace LoodsmanCommon
{
  public interface ILObject : IEntity, INamedEntity, ILAttributeOwner
  {
    LTypeInfo Type { get; set; }
    string Version { get; set; }
    LStateInfo State { get; set; }
    string BOLocation { get; }
    bool IsDocument { get; }
    PDMAccessLevels AccessLevel { get; set; }
    PDMLockLevels LockLevel { get; set; }
    EntityCollection<LFile> Files { get; }
    LUser Creator { get; }
    DateTime Created { get; }
  }
}
