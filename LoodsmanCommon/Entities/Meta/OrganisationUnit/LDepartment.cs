using System.Data;

namespace LoodsmanCommon.Entities.Meta.OrganisationUnit
{
    public class LDepartment : LOrganisationUnit
    {
        public string Code { get; }
        public override OrganisationUnitKind Kind => OrganisationUnitKind.Department;

        public LDepartment(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            Code = dataRow["_CODE"] as string;
        }
    }
}