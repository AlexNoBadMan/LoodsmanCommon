using Ascon.Plm.Loodsman.PluginSDK;
using System.Data;

namespace LoodsmanCommon
{
  public class LMeasure : INamedEntity
  {
    private NamedEntityCollection<LMeasureUnit> _units;
    private readonly INetPluginCall _iNetPC;

    internal LMeasure(INetPluginCall iNetPC, DataRow dataRow)
    {
      _iNetPC = iNetPC;
      Guid = dataRow.GUID();
      Name = dataRow.DISPLAY();
    }

    public string Guid { get; }
    public string Name { get; }
    public NamedEntityCollection<LMeasureUnit> Units => _units ??= new NamedEntityCollection<LMeasureUnit>(() => _iNetPC.Native_GetMUnitList(Guid).Select(x => new LMeasureUnit(this, x)));

  }

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