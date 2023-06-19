using System.Data;

namespace LoodsmanCommon
{
  public class LMainDepartment : LOrganisationUnit
  {
    internal LMainDepartment(DataRow dataRow) : base(dataRow)
    {
      Code = dataRow.CODE();
    }

    public string Code { get; }
    public override OrganisationUnitKind Kind => OrganisationUnitKind.MainDepartment;
  }
}