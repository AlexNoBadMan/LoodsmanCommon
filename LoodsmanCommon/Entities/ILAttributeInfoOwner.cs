namespace LoodsmanCommon
{
  public interface ILAttributeInfoOwner<T> : IEntity, INamedEntity where T : ILAttributeInfo
  {
    NamedEntityCollection<T> Attributes { get; }
  }
}
