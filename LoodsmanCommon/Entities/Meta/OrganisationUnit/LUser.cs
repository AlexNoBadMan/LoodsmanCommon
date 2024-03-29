﻿using System;
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
    private readonly string _noteValue;
    private IReadOnlyList<string> _note;

    internal LUser(ILoodsmanMeta meta, DataRow dataRow) : base(dataRow)
    {
      _meta = meta;
      IsAdmin = dataRow.IS_ADMIN();
      IsWinUser = dataRow.WINUSER();
      FullName = dataRow.FULLNAME();
      Mail = dataRow.MAIL();
      Phone = dataRow.PHONE();
      Skype = dataRow.SKYPE();
      IM = dataRow.IM();
      WebPage = dataRow.WEBPAGE();
      _noteValue = dataRow.NOTE();
      State = dataRow.STATUS();
      WorkDir = dataRow.WORKDIR();
      FileDir = dataRow.FILEDIR();
      //_PROFILE_ID
      //_IS_MISSING_USER 
      //_PATH  
      //_PROFILE   
      _mainpositionId = dataRow.MAINPOSITION_ID();
    }

    /// <summary> Возвращает основную должность пользователя. </summary>
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
    public IReadOnlyList<string> Note => _note ??= new ReadOnlyCollection<string>(_noteValue.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)); 
    public UserState State { get; }
    public string WorkDir { get; internal set; }
    public string FileDir { get; internal set; }
    public override OrganisationUnitKind Kind => OrganisationUnitKind.User;
  }
}
