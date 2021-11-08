using System.Data;

namespace LoodsmanCommon.Entities.Meta.OrganisationUnit
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
            IsChief = dataRow["_ISCHIEF"] as int? == 1;
            Code = dataRow["_CODE"] as string;
            IsChief = (int)dataRow["_ISCHIEF"] == 1;
        }
    }
}