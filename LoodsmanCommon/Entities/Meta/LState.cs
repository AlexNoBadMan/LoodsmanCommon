using System.Data;


namespace LoodsmanCommon.Entities.Meta
{
    public class LState : EntityIcon
    {
        internal LState(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {

        }
    }
}