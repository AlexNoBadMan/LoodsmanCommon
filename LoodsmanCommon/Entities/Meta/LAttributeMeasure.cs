namespace LoodsmanCommon
{
  public class LAttributeMeasure
  {
    private readonly LMeasure _measure;

    internal LAttributeMeasure(LMeasure measure, bool isDefault)
    {
      _measure = measure;
      IsDefault = isDefault;
    }

    public bool IsDefault { get; }
    public string Guid => _measure.Guid;
    public string Name => _measure.Name;
    public NamedEntityCollection<LMeasureUnit> Units => _measure.Units;
  }
}