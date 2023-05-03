using System.Data;

namespace LoodsmanCommon
{

  public class LPosition : LOrganisationUnit
  {
    //private readonly int _parentId;
    public string Code { get; }
    public bool IsChief { get; }
    public override OrganisationUnitKind Kind => OrganisationUnitKind.Position;

    public LPosition(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
    {
      //_parentId = (int)dataRow["_PARENT"];
      Code = dataRow["_CODE"] as string;
      IsChief = (short)dataRow["_LEADER"] == 1;
    }
  }
}