using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LoodsmanCommon
{
  public abstract class LOrganisationUnit : Entity
  {
    internal LOrganisationUnit(DataRow dataRow) : base(dataRow.ID(), dataRow.NAME()) 
    {
      Children = Enumerable.Empty<LOrganisationUnit>();
    }

    public virtual LOrganisationUnit Parent { get; internal set; }
    public abstract OrganisationUnitKind Kind { get; }
    public virtual IEnumerable<LOrganisationUnit> Children { get; internal set; }

    public IEnumerable<LOrganisationUnit> Ancestors()
    {
      for (var n = Parent; n != null; n = n.Parent)
        yield return n;
    }

    public IEnumerable<LOrganisationUnit> AncestorsAndSelf()
    {
      for (var n = this; n != null; n = n.Parent)
        yield return n;
    }

    public IEnumerable<LOrganisationUnit> DescendantsAndSelf()
    {
      return TreeTraversal.PreOrder(Children, n => n.Children);
    }

    public IEnumerable<LOrganisationUnit> Descendants()
    {
      return TreeTraversal.PreOrder(this, n => n.Children);
    }
  }
}