using System.Data;

namespace LoodsmanCommon
{
  public class LMeasureUnit : INamedEntity
  {
    public LMeasure ParentMeasure { get; }
    public string Guid { get; }
    public string Name { get; }
    public bool IsBase { get; }

    internal LMeasureUnit(LMeasure measure, DataRow dataRow)
    {
      ParentMeasure = measure;

      Guid = dataRow.ID_UNIT();
      Name = dataRow.NAME();
      IsBase = dataRow.BASICUNIT();
    }
  }
}
