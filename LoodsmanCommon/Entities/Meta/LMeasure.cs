using Ascon.Plm.Loodsman.PluginSDK;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LoodsmanCommon.Entities.Meta
{
    public class LMeasure
    {
        private IReadOnlyList<LMeasureUnit> _units;
        private readonly INetPluginCall _iNetPC;

        public string Guid { get; }
        public string Name { get; }
        public IReadOnlyList<LMeasureUnit> Units => _units ??= _iNetPC.Native_GetMUnitList(Guid).Select(x => new LMeasureUnit(this, x)).ToReadOnlyList();

        internal LMeasure(INetPluginCall iNetPC, DataRow dataRow)
        {
            _iNetPC = iNetPC;
            Guid = dataRow["_GUID"] as string;
            Name = dataRow["_DISPLAY"] as string;
        }
    }
}
