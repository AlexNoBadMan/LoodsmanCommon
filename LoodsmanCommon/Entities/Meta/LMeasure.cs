using Ascon.Plm.Loodsman.PluginSDK;
using LoodsmanCommon.Entities.Meta.Collections;
using System.Data;

namespace LoodsmanCommon.Entities.Meta
{
    public class LMeasure : INamedEntity
    {
        private NamedEntityCollection<LMeasureUnit> _units;
        private readonly INetPluginCall _iNetPC;

        public string Guid { get; }
        public string Name { get; }
        public NamedEntityCollection<LMeasureUnit> Units => _units ??= new NamedEntityCollection<LMeasureUnit>(() => _iNetPC.Native_GetMUnitList(Guid).Select(x => new LMeasureUnit(this, x)));

        internal LMeasure(INetPluginCall iNetPC, DataRow dataRow)
        {
            _iNetPC = iNetPC;
            Guid = dataRow["_GUID"] as string;
            Name = dataRow["_DISPLAY"] as string;
        }
    }
}
