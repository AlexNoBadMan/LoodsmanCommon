using Ascon.Plm.Loodsman.PluginSDK;
using System.Data;


namespace LoodsmanCommon.Entities.Meta
{
    //internal sealed class LStateCollection : NamedMetaItemCollection<LState>
    //{
    //    private readonly INetPluginCall _iNetPC;
    //    internal LStateCollection(INetPluginCall iNetPC)
    //    {
    //        _iNetPC = iNetPC;
    //        Init();
    //    }

    //    protected override LState CreateEntity(DataRow dataRow) => new LState(dataRow);
    //    protected override DataRowCollection GetMetadata() => _iNetPC.Native_GetStateList().Rows;
    //}

    public class LState : EntityIcon
    {
        internal LState(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {

        }
    }
}