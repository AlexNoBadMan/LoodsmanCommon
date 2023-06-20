namespace LoodsmanCommon
{
  public interface ILAttributeInfoOwner<T> : IEntity where T : ILAttributeInfo
  {
    NamedEntityCollection<T> Attributes { get; }
  }
}
