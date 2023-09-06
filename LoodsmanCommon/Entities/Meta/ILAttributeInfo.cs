using System.Collections.Generic;

namespace LoodsmanCommon
{
  public interface ILAttributeInfo : IEntity, INamedEntity
  {
    string DefaultValue { get; }
    bool IsMeasured { get; }
    bool IsObligatory { get; }
    bool IsSystem { get; }
    IEnumerable<string> ListValues { get; }
    IEnumerable<LAttributeMeasure> Measures { get; }
    bool OnlyIsItems { get; }
    AttributeType Type { get; }
  }
}