using System.Data;


namespace LoodsmanCommon.Entities
{
    public abstract class Entity
    {
        public int Id { get; }
        public string Name { get; }

        public Entity(DataRow dataRow, string nameField = "_NAME")
        {
            Id = (int)dataRow["_ID"];
            Name = dataRow[nameField] as string;
        }
    }
}