using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace LoodsmanCommon
{
  public class LUser : LOrganisationUnit
  {
    private LOrganisationUnit _parent;
    private readonly int _mainpositionId;
    private readonly ILoodsmanMeta _meta;

    /// <summary>
    /// Возвращает основную должность пользователя.
    /// </summary>
    public override LOrganisationUnit Parent
    {
      get => _parent == null && _mainpositionId > 0 ? (_parent = _meta.OrganisationUnits[_mainpositionId]) : _parent;

      internal set => _parent = value;
    }

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

    internal LUser(ILoodsmanMeta meta, DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
    {
      _meta = meta;
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
      _mainpositionId = dataRow["_MAINPOSITION_ID"] as int? ?? 0;
    }
  }
}
