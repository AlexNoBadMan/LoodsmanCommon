namespace LoodsmanCommon
{
  public interface ILAttributeOwner : ILAttributeInfoOwner<ILAttribute>
  {
    void UpdateAttribute(string name, object value, LMeasureUnit unit);
  }
}
