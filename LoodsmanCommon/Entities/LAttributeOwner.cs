namespace LoodsmanCommon
{
  public abstract class LAttributeOwner : Entity, ILAttributeOwner
  {
    private NamedEntityCollection<ILAttribute> _attributes;

    public LAttributeOwner(int id, string name) : base(id, name)
    {
    }

    public NamedEntityCollection<ILAttribute> Attributes => _attributes ??= GetAttributes();
    public abstract void UpdateAttribute(string name, object value, LMeasureUnit unit);
    protected abstract NamedEntityCollection<ILAttribute> GetAttributes();
  }
}
