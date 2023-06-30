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
    private readonly string _unitId;
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
      _unitId = unitId;
      //_isLoaded = false;
    }
    #endregion

    #region Свойства
    public int Id => _attribute.Id;
    public string Name => _attribute.Name;
    public AttributeType Type => _attribute.Type;
    public string DefaultValue => _attribute.DefaultValue;
    public IReadOnlyList<string> ListValues => _attribute.ListValues;
    public bool OnlyIsItems => _attribute.OnlyIsItems;
    public bool IsChanged => GetIsChanged();
    public bool IsSystem => _attribute.IsSystem;
    public bool IsObligatory => _attribute.IsObligatory;
    public bool IsMeasured => _attribute.IsMeasured;
    public IEnumerable<LAttributeMeasure> Measures => _attribute.Measures;
    public LMeasureUnit MeasureUnit { get => GetMeasureUnit(); set => SetMeasureUnit(value, true); }
    public object Value { get => GetValue(); set => SetValue(value, true); }
    #endregion

    #region Методы
    public bool SetValue(object value, bool update)
    {
      if ($"{_value}" == $"{value}")
        return false;

      _value = value;

      if (update)
        UpdateAttribute();

      AttributeChanged?.Invoke(this, EventArgs.Empty);

      return true;
    }

    public bool SetMeasureUnit(LMeasureUnit measureUnit, bool update)
    {
      if (_measureUnit == measureUnit || !IsMeasured || !Measures.Any(x => x.Units.Values.Contains(measureUnit)))
        return false;

      _measureUnit = measureUnit;

      if (update)
        UpdateAttribute();

      AttributeChanged?.Invoke(this, EventArgs.Empty);

      return true;
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

    private LMeasureUnit GetMeasureUnit()
    {
      if (!IsMeasured)
        return null;

      if (_measureUnit is null && !string.IsNullOrEmpty(_measureId))
        _measureUnit = _proxy.Meta.Measures.Values.FirstOrDefault(x => x.Guid == _measureId).Units.Values.FirstOrDefault(x => x.Guid == _unitId);
      // _proxy.Meta.Measures[_measureId].Units[_unitId]; // Ключ для _proxy.Meta.Measures и Units, имя а не guid, необходимо переделать под guid.
      return _measureUnit;
    }

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

    private bool GetIsChanged()
    {
      return $"{_originalValue}" != $"{_value}" || $"{_measureUnit?.Guid}" != _unitId;
    }
    #endregion
  }
}
