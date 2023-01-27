using System.Data;

namespace LoodsmanCommon
{
    public class LMainDepartment : LOrganisationUnit
    {
        public string Code { get; }
        public override OrganisationUnitKind Kind => OrganisationUnitKind.MainDepartment;

        public LMainDepartment(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            Code = dataRow["_CODE"] as string;
        }
    }
}