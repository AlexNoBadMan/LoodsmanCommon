using System.Linq;

namespace LoodsmanCommon
{
  public class LLink : LAttributeOwner
  {
    private readonly ILoodsmanProxy _proxy;
    private LLinkInfoBetweenTypes _linkInfo;
    private readonly string _measureId;
    private readonly string _unitId;

    public LLink(ILoodsmanProxy proxy, int id, string name, ILObject parent, ILObject child, double maxQuantity, double minQuantity, string measureId, string unitId) : base(id, name)
    {
      _proxy = proxy;
      Parent = parent;
      Child = child;
      MaxQuantity = maxQuantity;
      MinQuantity = minQuantity;
      _measureId = measureId;
      _unitId = unitId;
    }

    public ILObject Parent { get; }
    public ILObject Child { get; }
    public double MaxQuantity { get; }
    public double MinQuantity { get; }
    public LLinkInfoBetweenTypes LinkInfo => _linkInfo ??= 
      _proxy.Meta.LinksInfoBetweenTypes.First(x => Name == x.Link.Name && x.ParentType.Id == Parent.Type.Id && x.ChildType.Id == Child.Type.Id);

    public override void UpdateAttribute(string name, object value, LMeasureUnit unit)
    {
      _proxy.UpLinkAttrValueById(Id, name, value, unit);
    }

    protected override NamedEntityCollection<ILAttribute> GetAttributes()
    {
      return null;
    }
  }
}
