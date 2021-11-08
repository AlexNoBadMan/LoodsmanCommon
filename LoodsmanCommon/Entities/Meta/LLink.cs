using Ascon.Plm.Loodsman.PluginSDK;
using System.Data;


namespace LoodsmanCommon.Entities.Meta
{
    //internal sealed class LLinkCollection : NamedMetaItemCollection<LLink>
    //{
    //    private readonly INetPluginCall _iNetPC;

    //    internal LLinkCollection(INetPluginCall iNetPC)
    //    {
    //        _iNetPC = iNetPC;
    //        Init();
    //    }

    //    protected override LLink CreateEntity(DataRow dataRow) => new LLink(dataRow);
    //    protected override DataRowCollection GetMetadata() => _iNetPC.Native_GetLinkList().Rows;
    //}

    public class LLink : EntityIcon
    {
        public string InverseName { get; }
        public bool VerticalLink { get; }

        internal LLink(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            InverseName = dataRow["_INVERSENAME"] as string;
            VerticalLink = (short)dataRow["_TYPE"] == 0; //Приведение к int вызвало ошибку с short проблем не возникло
            //Order = (int)dataRow["_ORDER"];
        }
    }
}