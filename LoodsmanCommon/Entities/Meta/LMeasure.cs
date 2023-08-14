using Ascon.Plm.Loodsman.PluginSDK;
using System.Data;
using System.Linq;

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
    public NamedEntityCollection<LMeasureUnit> Units => _units ??= 
      new NamedEntityCollection<LMeasureUnit>(() => _iNetPC.Native_GetMUnitList(Guid).Select(x => new LMeasureUnit(this, x)).OrderBy(x => x.Name));
  }
}