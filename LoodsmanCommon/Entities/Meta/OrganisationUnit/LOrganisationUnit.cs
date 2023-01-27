using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LoodsmanCommon
{
    public abstract class LOrganisationUnit : Entity
    {
        public virtual LOrganisationUnit Parent { get; internal set; }
        public abstract OrganisationUnitKind Kind { get; }
        public virtual IEnumerable<LOrganisationUnit> Children { get; internal set; } = Enumerable.Empty<LOrganisationUnit>();

        internal LOrganisationUnit(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
        }


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
            yield return this;
            foreach (var item in Children.SelectMany(x => x.DescendantsAndSelf()))
                yield return item;
        }

        public IEnumerable<LOrganisationUnit> Descendants()
        {
            foreach (var item in Children)
            {
                yield return item;
                foreach (var item2 in item.Descendants())
                    yield return item2;
            }
        }
    }
}