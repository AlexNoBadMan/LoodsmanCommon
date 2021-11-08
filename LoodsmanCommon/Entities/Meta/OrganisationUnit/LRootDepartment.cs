using System.Data;

namespace LoodsmanCommon.Entities.Meta.OrganisationUnit
{
    public class LRootDepartment : LOrganisationUnit
    {
        public string Code { get; }
        public override OrganisationUnitKind Kind => OrganisationUnitKind.RootDepartment;

        public LRootDepartment(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            Code = dataRow["_CODE"] as string;
        }
    }
}