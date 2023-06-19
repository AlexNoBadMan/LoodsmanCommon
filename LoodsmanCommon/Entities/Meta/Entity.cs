namespace LoodsmanCommon
{
  public abstract class Entity : IEntity, INamedEntity
  {
    internal Entity(int id, string name)
    {
      Id = id;
      Name = name;
    }

    /// <summary> Индентификатор. </summary>
    public int Id { get; }

    /// <summary> Наименование. </summary>
    public string Name { get; }
  }
}