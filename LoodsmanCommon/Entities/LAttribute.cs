using System;
using System.Linq;
using System.Collections.Generic;

namespace LoodsmanCommon
{
  public class LAttribute : IEntity, INamedEntity
  {
    private readonly ILoodsmanProxy _proxy;
    private readonly ILObject _owner;
    private readonly LTypeAttribute _lTypeAttribute;
    private readonly string _measureId;
    private readonly string _unitId;
    private LMeasureUnit _measureUnit;
    private object _value;

    internal LAttribute(ILoodsmanProxy proxy, ILObject owner, LTypeAttribute lTypeAttribute, object value, string measureId, string unitId)
    {
      _proxy = proxy;
      _owner = owner;
      _lTypeAttribute = lTypeAttribute;
      _value = _lTypeAttribute.Type == AttributeType.DateTime && DateTime.TryParse(value as string, out var dateTime) ? dateTime : value;
      _measureId = measureId;
      _unitId = unitId;
    }

    public int Id => _lTypeAttribute.Id;
    public string Name => _lTypeAttribute.Name;
    public AttributeType Type => _lTypeAttribute.Type;
    public string DefaultValue => _lTypeAttribute.DefaultValue;
    public IReadOnlyList<string> ListValue => _lTypeAttribute.ListValue;
    public bool OnlyIsItems => _lTypeAttribute.OnlyIsItems;
    public bool IsSystem => _lTypeAttribute.IsSystem;
    public bool IsObligatory => _lTypeAttribute.IsObligatory;
    public bool IsMeasured => _lTypeAttribute.IsMeasured;
    public IEnumerable<LAttributeMeasure> Measures => _lTypeAttribute.Measures;

    public LMeasureUnit MeasureUnit
    {
      get => GetMeasureUnit();
      set => SetMeasureUnit(value, true);
    }

    public object Value
    {
      get => _value;
      set => SetValue(value, true);
    }

    public bool SetValue(object value, bool update)
    {
      if ($"{_value}" != $"{value}")
        return false;

      _value = value;

      if (update)
        UpdateAttribute();

      return true;
    }

    public bool SetMeasureUnit(LMeasureUnit measureUnit, bool update)
    {
      if (_measureUnit == measureUnit || !IsMeasured || !Measures.Any(x => x.Units.Values.Contains(measureUnit)))
        return false;

      _measureUnit = measureUnit;

      if (update)
        UpdateAttribute();

      return true;
    }

    public void UpdateAttribute()
    {
      var value = Value;
      if (Type == AttributeType.DateTime && Value is DateTime dateTime && dateTime.TimeOfDay == TimeSpan.Zero)
      {
        value = dateTime.ToShortDateString();
      }

      _proxy.UpAttrValueById(_owner.Id, Name, value, MeasureUnit);
    }

    private LMeasureUnit GetMeasureUnit()
    {
      if (!IsMeasured)
        return null;

      if (_measureUnit is null && !string.IsNullOrEmpty(_measureId))
        _measureUnit = _proxy.Meta.Measures.Values.FirstOrDefault(x => x.Guid == _measureId).Units.Values.FirstOrDefault(x => x.Guid == _unitId);
      // _proxy.Meta.Measures[_measureId].Units[_unitId]; // Ключ для _proxy.Meta.Measures и Units, имя а не guid, необходимо переделать под guid.
      return _measureUnit;
    }
  }
}
