using System.Data;

namespace LoodsmanCommon
{
    public abstract class Entity : IEntity, INamedEntity
    {
        /// <summary>
        /// Индентификатор
        /// </summary>
        public int Id { get; }
        public string Name { get; }

        internal Entity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        internal Entity(DataRow dataRow, string nameField = "_NAME") : this((int)dataRow["_ID"], dataRow[nameField] as string) { }
    }
}