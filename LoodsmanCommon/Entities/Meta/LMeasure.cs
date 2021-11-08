using Ascon.Plm.Loodsman.PluginSDK;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LoodsmanCommon.Entities.Meta
{
    public class LMeasure : INamedEntity
    {
        private NamedCollection<LMeasureUnit> _units;
        private readonly INetPluginCall _iNetPC;

        public string Guid { get; }
        public string Name { get; }
        public NamedCollection<LMeasureUnit> Units => _units ??= new NamedCollection<LMeasureUnit>(() => _iNetPC.Native_GetMUnitList(Guid).Rows, x => new LMeasureUnit(this, x));

        internal LMeasure(INetPluginCall iNetPC, DataRow dataRow)
        {
            _iNetPC = iNetPC;
            Guid = dataRow["_GUID"] as string;
            Name = dataRow["_DISPLAY"] as string;
        }
    }
}
