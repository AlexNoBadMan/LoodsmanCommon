﻿using System.Collections.Generic;

namespace LoodsmanCommon
{
  public class LTypeAttributeInfo : ILAttributeInfo
  {
    private readonly LAttributeInfo _lAttribute;

    internal LTypeAttributeInfo(LAttributeInfo lAttribute, bool isObligatory)
    {
      _lAttribute = lAttribute;
      IsObligatory = isObligatory;
    }

    public int Id => _lAttribute.Id;
    public string Name => _lAttribute.Name;
    public AttributeType Type => _lAttribute.Type;
    public string DefaultValue => _lAttribute.DefaultValue;
    public IEnumerable<string> ListValues => _lAttribute.ListValues;
    public bool OnlyIsItems => _lAttribute.OnlyIsItems;
    public bool IsSystem => _lAttribute.IsSystem;
    public bool IsObligatory { get; }
    public bool IsMeasured => _lAttribute.IsMeasured;

    public IEnumerable<LAttributeMeasure> Measures => _lAttribute.Measures;
  }
}
