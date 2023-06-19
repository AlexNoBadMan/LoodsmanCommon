using System.Data;

namespace LoodsmanCommon
{
  public class LDepartment : LOrganisationUnit
  {
    internal LDepartment(DataRow dataRow) : base(dataRow)
    {
      Code = dataRow.CODE();
    }

    public string Code { get; }
    public override OrganisationUnitKind Kind => OrganisationUnitKind.Department;
  }
}