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

    public int Id => _lTypeAttribute.Id;
    public string Name => _lTypeAttribute.Name;
    public AttributeType Type => _lTypeAttribute.Type;
    public string DefaultValue => _lTypeAttribute.DefaultValue;
    public IReadOnlyList<string> ListValue => _lTypeAttribute.ListValue;
    public bool OnlyIsItems => _lTypeAttribute.OnlyIsItems;
    public bool IsSystem => _lTypeAttribute.IsSystem;
    public bool IsObligatory => _lTypeAttribute.IsObligatory;
    public bool IsMeasured => _lTypeAttribute.IsMeasured;

    public LMeasureUnit MeasureUnit
    {
      get
      {
        if (!IsMeasured)
          return null;

        if (_measureUnit is null && !string.IsNullOrEmpty(_measureId))
          _measureUnit = _proxy.Meta.Measures.Values.FirstOrDefault(x => x.Guid == _measureId).Units.Values.FirstOrDefault(x => x.Guid == _unitId);
        // _proxy.Meta.Measures[_measureId].Units[_unitId]; // Ключ для _proxy.Meta.Measures и Units, имя а не guid, необходимо переделать под guid.
        return _measureUnit;
      }
      set
      {
        if (!IsMeasured || _measureUnit == value)
          return;

        var isEmptyValue = _value is null;
        var valueToConvert = isEmptyValue ? 0 : (double)_value;
        var convertedValue = _proxy.ConverseValue(valueToConvert, _measureUnit, value);//Пробуем произвести конвертацию, для проверки корректности операции
        _measureUnit = value;
        if (!isEmptyValue)
        {
          _value = Type == AttributeType.Integer ? (int)convertedValue : (object)convertedValue;
          UpdateAttribute();
        }
      }
    }

    public object Value
    {
      get => _value;
      set
      {
        _value = value;
        UpdateAttribute();
      }
    }

    internal LAttribute(ILoodsmanProxy proxy, ILObject owner, LTypeAttribute lTypeAttribute, object value, string measureId, string unitId)
    {
      _proxy = proxy;
      _owner = owner;
      _lTypeAttribute = lTypeAttribute;
      _value = _lTypeAttribute.Type == AttributeType.DateTime && DateTime.TryParse(value as string, out var dateTime) ? dateTime : value;
      _measureId = measureId;
      _unitId = unitId;
    }

    private void UpdateAttribute()
    {
      var value = Value;
      if (Type == AttributeType.DateTime && Value is DateTime dateTime && dateTime.TimeOfDay == TimeSpan.Zero)
      {
        value = dateTime.ToShortDateString();
      }

      _proxy.UpAttrValueById(_owner.Id, Name, value, MeasureUnit);
    }
  }
}
