using System.Data;

namespace LoodsmanCommon
{
  public class LPosition : LOrganisationUnit
  {
    internal LPosition(DataRow dataRow) : base(dataRow)
    {
      //_parentId = (int)dataRow["_PARENT"];
      Code = dataRow.CODE();
      IsChief = dataRow.LEADER();
    }

    //private readonly int _parentId;
    public string Code { get; }
    public bool IsChief { get; }
    public override OrganisationUnitKind Kind => OrganisationUnitKind.Position;
  }
}