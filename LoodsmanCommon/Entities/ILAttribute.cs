using System;

namespace LoodsmanCommon
{
  public interface ILAttribute : ILAttributeInfo
  {
    bool IsChanged { get; }
    LMeasureUnit MeasureUnit { get; set; }
    object Value { get; set; }

    bool SetMeasureUnit(LMeasureUnit measureUnit, bool update);
    void SetDefaultMeasureUnit(LMeasureUnit measureUnit);
    bool SetValue(object value, bool update);
    void UpdateAttribute();

    event EventHandler AttributeChanged;
  }
}
