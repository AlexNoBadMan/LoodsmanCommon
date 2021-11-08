using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace LoodsmanCommon.Entities.Meta.OrganisationUnit
{
    public class LUser : LOrganisationUnit
    {
        private int? _mainpositionId;
        public LPosition MainPosition { get; }
        public bool IsAdmin { get; }
        public bool IsWinUser { get; }
        public string FullName { get; }
        public string Mail { get; }
        public string Phone { get; }
        public string Skype { get; }
        public string IM { get; }
        public string WebPage { get; }
        public IReadOnlyList<string> Note { get; }
        public UserState State { get; }
        public string WorkDir { get; internal set; }
        public string FileDir { get; internal set; }
        public override OrganisationUnitKind Kind => OrganisationUnitKind.User;

        internal LUser(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            IsAdmin = (int)dataRow["_IS_ADMIN"] == 1;
            IsWinUser = (short)dataRow["_WINUSER"] == 1; 
            FullName = dataRow["_FULLNAME"] as string;
            Mail = dataRow["_MAIL"] as string;
            Phone = dataRow["_PHONE"] as string;
            Skype = dataRow["_SKYPE"] as string;
            IM = dataRow["_IM"] as string;
            WebPage = dataRow["_WEBPAGE"] as string;
            Note = new ReadOnlyCollection<string>(dataRow["_NOTE"].ToString().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            State = (UserState)dataRow["_STATUS"];
            WorkDir = dataRow["_WORKDIR"] as string;
            FileDir = dataRow["_FILEDIR"] as string;
            //_PROFILE_ID
            //_IS_MISSING_USER 
            //_PATH  
            //_PROFILE   
            _mainpositionId = dataRow["_MAINPOSITION_ID"] as int?;
        }
    }
}
