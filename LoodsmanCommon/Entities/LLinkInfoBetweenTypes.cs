using System.Data;

namespace LoodsmanCommon.Entities
{
    public enum LinkDirection
    {
        Forward = 1,
        Backward = -1,
        ForwardAndBackward = 0
    }

    public class LLinkInfoBetweenTypes
    {
        public int Id { get; }
        public string Name { get; }
        public string InverseName { get; }
        public int TypeId1 { get; }
        public string TypeName1 { get; }
        public int TypeId2 { get; }
        public string TypeName2 { get; }
        public bool IsVertical { get; }
        public LinkDirection Direction { get; internal set; }
        public bool IsQuantity { get; }

        public LLinkInfoBetweenTypes(DataRow dataRow)
        {
            Id = (int)dataRow["_ID_LINKTYPE"];
            Name = dataRow["_LINKTYPE"] as string;
            InverseName = dataRow["_INVERSENAME"] as string;
            TypeId1 = (int)dataRow["_TYPE_ID_1"];
            TypeName1 = dataRow["_TYPE_NAME_1"] as string;
            TypeId2 = (int)dataRow["_TYPE_ID_2"];
            TypeName2 = dataRow["_TYPE_NAME_2"] as string;
            IsVertical = (short)dataRow["_LINKKIND"] == 0;
            Direction = (LinkDirection)dataRow["_DIRECTION"];
            IsQuantity = (short)dataRow["_IS_QUANTITY"] == 1;
        }
        internal LLinkInfoBetweenTypes()
        {

        }
    }
}