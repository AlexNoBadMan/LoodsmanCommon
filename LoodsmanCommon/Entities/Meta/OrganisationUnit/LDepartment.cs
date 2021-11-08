using System.Data;

namespace LoodsmanCommon.Entities.Meta.OrganisationUnit
{
    public class LDepartment : LRootDepartment
    {
        public override OrganisationUnitKind Kind => OrganisationUnitKind.Department;

        public LDepartment(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
        }
    }
}