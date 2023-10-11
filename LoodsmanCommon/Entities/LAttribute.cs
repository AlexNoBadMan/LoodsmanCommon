using System;
using System.Linq;
using System.Collections.Generic;

namespace LoodsmanCommon
{
  public class LAttribute : ILAttribute
  {
    #region Поля
    private readonly ILoodsmanProxy _proxy;
    private readonly ILAttributeOwner _owner;
    private readonly ILAttributeInfo _attribute;
    private readonly string _measureId;
    private string _originalUnitId;
    private string _unitId;
    private LMeasureUnit _measureUnit;
    //private bool _isLoaded;
    private object _originalValue;
    private object _value;
    public event EventHandler AttributeChanged;
    #endregion

    #region Конструктор
    internal LAttribute(ILoodsmanProxy proxy, ILAttributeOwner owner, ILAttributeInfo attribute, object value, string measureId, string unitId)
    {
      _proxy = proxy;
      _owner = owner;
      _attribute = attribute;
      _value = _attribute.Type == AttributeType.DateTime && DateTime.TryParse(value as string, out var dateTime) ? dateTime : value;
      _originalValue = _value;
      _measureId = measureId;
      _originalUnitId = unitId;
      _unitId = unitId;
      //_isLoaded = false;
    }
    #endregion

    #region Свойства
    public int Id => _attribute.Id;
    public string Name => _attribute.Name;
    public AttributeType Type => _attribute.Type;
    public string DefaultValue => _attribute.DefaultValue;
    public IEnumerable<string> ListValues => _attribute.ListValues;
    public bool OnlyIsItems => _attribute.OnlyIsItems;
    public bool IsChanged => $"{_originalValue}" != $"{_value}" || $"{MeasureUnit?.Guid}" != _originalUnitId;
    public bool IsSystem => _attribute.IsSystem;
    public bool IsObligatory => _attribute.IsObligatory;
    public bool IsMeasured => _attribute.IsMeasured;
    public IEnumerable<LAttributeMeasure> Measures => _attribute.Measures;
    public LMeasureUnit MeasureUnit { get => GetMeasureUnit(); set => SetMeasureUnit(value, true); }
    public object Value { get => GetValue(); set => SetValue(value, true); }
    #endregion

    #region Методы

    private object GetValue()
    {
      // TODO:
      //if (!_isLoaded)
      //{
      //  _isLoaded = true;
      //  if (Type == AttributeType.Text || Type == AttributeType.Image)
      //  {
      //    //INetPluginCall pc = null;
      //    //_originalValue = pc.GetAttrImageValueById; owner.Id Is LObject || is LLink ??
      //    //_value = _originalValue;
      //  }

      //}

      return _value;
    }

    public bool SetValue(object value, bool update)
    {
      if ($"{_value}" == $"{value}")
        return false;

      var oldValue = _value;
      _value = value;
      if (update)
      {
        try
        {
          UpdateAttribute();
        }
        catch
        {
          _value = oldValue;
          return false;
        }
      }

      AttributeChanged?.Invoke(this, EventArgs.Empty);
      return true;
    }

    private LMeasureUnit GetMeasureUnit()
    {
      if (!IsMeasured)
        return null;

      if (_measureUnit is null && !string.IsNullOrEmpty(_unitId))
        _measureUnit = _proxy.Meta.Measures.Values.SelectMany(x => x.Units.Values).FirstOrDefault(x => x.Guid == _unitId);
      // _proxy.Meta.Measures[_measureId].Units[_unitId]; // Ключ для _proxy.Meta.Measures и Units, имя а не guid, необходимо переделать под guid.
      return _measureUnit;
    }

    public bool SetMeasureUnit(LMeasureUnit measureUnit, bool update)
    {
      if (!CanSetMeasureUnit(measureUnit))
        return false;

      _measureUnit = measureUnit;
      _unitId = _measureUnit?.Guid;

      if (update)
        UpdateAttribute();

      AttributeChanged?.Invoke(this, EventArgs.Empty);

      return true;
    }

    public void SetDefaultMeasureUnit(LMeasureUnit measureUnit)
    {
      if (!CanSetMeasureUnit(measureUnit))
        return;

      _measureUnit = measureUnit;
      _originalUnitId = _measureUnit?.Guid;
      _unitId = _originalUnitId;
    }

    public void UpdateAttribute()
    {
      var value = Value;
      if (Type == AttributeType.DateTime && Value is DateTime dateTime && dateTime.TimeOfDay == TimeSpan.Zero)
      {
        value = dateTime.ToShortDateString();
      }

      _owner.UpdateAttribute(Name, value, MeasureUnit);
    }

    private bool CanSetMeasureUnit(LMeasureUnit measureUnit)
    {
      return _measureUnit != measureUnit && IsMeasured && (measureUnit == null || Measures.Any(x => x.Units.Values.Contains(measureUnit)));
    }

    #endregion
  }
}
