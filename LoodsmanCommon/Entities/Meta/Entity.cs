using System.Data;


namespace LoodsmanCommon.Entities.Meta
{
    public abstract class Entity
    {
        public int Id { get; }
        public string Name { get; }

        public Entity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        internal Entity(DataRow dataRow, string nameField = "_NAME") : this((int)dataRow["_ID"], dataRow[nameField] as string)
        { }

    }
}