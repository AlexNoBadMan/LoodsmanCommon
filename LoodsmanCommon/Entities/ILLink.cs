namespace LoodsmanCommon
{
  public interface ILLink : IEntity, INamedEntity, ILAttributeOwner
  {
    ILObject Parent { get; }
    ILObject Child { get; }
    LLinkInfoBetweenTypes LinkInfo { get; }
    double MaxQuantity { get; }
    double MinQuantity { get; }
  }
}