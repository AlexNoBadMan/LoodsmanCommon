using System.Data;


namespace LoodsmanCommon.Entities
{
    public class LLink : EntityIcon
    {
        public string InverseName { get; }
        public bool VerticalLink { get; }

        public LLink(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            InverseName = dataRow["_INVERSENAME"] as string;
            VerticalLink = (short)dataRow["_TYPE"] == 0; //Приведение к int вызвало ошибку с short проблем не возникло
            //Order = (int)dataRow["_ORDER"];
        }
    }
}