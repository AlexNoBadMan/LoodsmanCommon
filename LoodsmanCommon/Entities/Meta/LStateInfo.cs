using System.Data;


namespace LoodsmanCommon
{
    public class LStateInfo : EntityIcon
    {
        internal LStateInfo(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {

        }
    }
}