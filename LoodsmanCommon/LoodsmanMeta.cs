using Ascon.Plm.Loodsman.PluginSDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LoodsmanCommon
{
  internal class LoodsmanMeta : ILoodsmanMeta
  {
    private NamedEntityCollection<LTypeInfo> _types;
    private NamedEntityCollection<LLinkInfo> _links;
    private NamedEntityCollection<LStateInfo> _states;
    private NamedEntityCollection<LAttributeInfo> _attributes;
    private IReadOnlyList<LProxyUseCase> _proxyUseCases;
    private IReadOnlyList<LLinkInfoBetweenTypes> _linksInfoBetweenTypes;
    private NamedEntityCollection<LMeasure> _measures;
    private NamedEntityCollection<LUser> _users;
    private LUser _currentUser;
    private EntityCollection<LOrganisationUnit> _organisationUnits;
    private LMainDepartment _currentMainDepartment;
    private Dictionary<int, List<LAttributeMeasure>> _attributeMeasures;
    private readonly INetPluginCall _iNetPC;

    public LoodsmanMeta(INetPluginCall iNetPC)
    {
      _iNetPC = iNetPC;
    }

    public NamedEntityCollection<LTypeInfo> Types => _types ??= new NamedEntityCollection<LTypeInfo>(() => _iNetPC.Native_GetTypeListEx().Select(x => new LTypeInfo(this, x)), 300);
    public NamedEntityCollection<LLinkInfo> Links => _links ??= new NamedEntityCollection<LLinkInfo>(() => _iNetPC.Native_GetLinkList().Select(x => new LLinkInfo(x)), 100);
    public NamedEntityCollection<LStateInfo> States => _states ??= new NamedEntityCollection<LStateInfo>(() => _iNetPC.Native_GetStateList().Select(x => new LStateInfo(x)), 50);
    public NamedEntityCollection<LAttributeInfo> Attributes => _attributes ??= new NamedEntityCollection<LAttributeInfo>(() => _iNetPC.Native_GetAttributeList().Select(x => new LAttributeInfo(this, x)), 500);
    public IReadOnlyList<LProxyUseCase> ProxyUseCases => _proxyUseCases ??= _iNetPC.Native_GetProxyUseCases().Select(x => new LProxyUseCase(x)).ToReadOnlyList();
    public IReadOnlyList<LLinkInfoBetweenTypes> LinksInfoBetweenTypes => _linksInfoBetweenTypes ??= GetLinksInfoBetweenTypes(_iNetPC.Native_GetLinkListEx()).ToReadOnlyList();
    public NamedEntityCollection<LMeasure> Measures => _measures ??= new NamedEntityCollection<LMeasure>(() => _iNetPC.Native_GetFromBO_Nature().Select(x => new LMeasure(_iNetPC, x)), 45);
    public NamedEntityCollection<LUser> Users => _users ??= InitUsers();
    public LUser CurrentUser => _currentUser ??= InitCurrentUser();
    public EntityCollection<LOrganisationUnit> OrganisationUnits => _organisationUnits ??= new EntityCollection<LOrganisationUnit>(() => InitOrganisationUnits(), 150);
    public LMainDepartment CurrentMainDepartment => _currentMainDepartment ??=
        CurrentUser.Ancestors().FirstOrDefault(x => x is LMainDepartment) as LMainDepartment ??
        OrganisationUnits.Values.FirstOrDefault(x => x is LMainDepartment) as LMainDepartment;

    private Dictionary<int, List<LAttributeMeasure>> AttributeMeasures => _attributeMeasures ??= GetAttributeMeasures();

    public IEnumerable<LAttributeMeasure> GetAttributeMeasures(string name)
    {
      var id = Attributes[name].Id;
      return AttributeMeasures.TryGetValue(id, out var value) ? value : Enumerable.Empty<LAttributeMeasure>();
    }

    public LProxyUseCase GetProxyUseCase(string parentType, string childDocumentType, string extension)
    {
      return ProxyUseCases.FirstOrDefault(x => x.TypeName == parentType &&
                                               x.DocumentType == childDocumentType &&
                                               x.Extension.IndexOf(extension, StringComparison.OrdinalIgnoreCase) >= 0);
    }

    public NamedEntityCollection<LTypeAttributeInfo> GetTypeAttrbiutes(string name)
    {
      return new NamedEntityCollection<LTypeAttributeInfo>(
              () => _iNetPC.Native_GetInfoAboutType(name, GetInfoAboutTypeMode.Mode12).Select(x => new LTypeAttributeInfo(Attributes[x.NAME()], x.OBLIGATORY())),
              10);
    }

    public void Clear()
    {
      _attributes = null;
      _types = null;
      _states = null;
      _links = null;
      _proxyUseCases = null;
      _linksInfoBetweenTypes = null;
      _measures = null;
      _users = null;
      _currentUser = null;
      _organisationUnits = null;
      _currentMainDepartment = null;
    }

    private Dictionary<int, List<LAttributeMeasure>> GetAttributeMeasures()
    {
      var dictionary = new Dictionary<int, List<LAttributeMeasure>>();
      foreach (DataRow item in _iNetPC.Native_GetInfoAboutAttribute().Rows)
      {
        var id = item.ID();
        var guid = item.ID_MEASURE();
        var measure = Measures.Values.FirstOrDefault(x => x.Guid == guid);

        if (dictionary.TryGetValue(id, out var measures))
          measures.Add(new LAttributeMeasure(measure, item.DEFAULT_BOOL()));
        else
          dictionary.Add(id, new List<LAttributeMeasure>() { new LAttributeMeasure(measure, item.DEFAULT_BOOL()) });
      }

      return dictionary;
    }

    private IEnumerable<LOrganisationUnit> InitOrganisationUnits()
    {
      //var treeObjects = iNetPC.Native_WFGetRoleTree(GetRoleTreeMode.Mode1).Select(x => GetOrganisationUnit(x)).ToList();
      //treeObjects.ForEach(x => x.Children = treeObjects.Where(o => x.Id == o.ParentId));

      var dataRows = _iNetPC.Native_WFGetRoleTree(GetRoleTreeMode.Mode1).Rows;
      var orgUnits = new Dictionary<int, LOrganisationUnit>(dataRows.Count);
      foreach (DataRow dataRow1 in dataRows)
      {
        var unitKind = dataRow1.TYPE_ORG_UNIT();
        LOrganisationUnit orgUnit = unitKind switch
        {
          OrganisationUnitKind.Department => new LDepartment(dataRow1),
          OrganisationUnitKind.Position => new LPosition(dataRow1),
          OrganisationUnitKind.User => null,
          OrganisationUnitKind.MainDepartment => new LMainDepartment(dataRow1),
          _ => null
        };
        if (orgUnit == null)
          continue;

        orgUnits.Add(orgUnit.Id, orgUnit);
        yield return orgUnit;
      }

      foreach (DataRow dataRow2 in dataRows)
      {
        var parentId = dataRow2.PARENT();
        if (parentId <= 0)
          continue;

        var parent = orgUnits[parentId];
        if (dataRow2.TYPE_ORG_UNIT() != OrganisationUnitKind.User)
        {
          var treeObject = orgUnits[dataRow2.ID()];
          parent.Children = parent.Children.Append(treeObject);
          treeObject.Parent = parent;
        }
        else
        {
          //Для пользователей не заполняем Parent(родителем будет основная долность) и не добавляем в список т.к. есть список Users
          //Берём пользователей из готового списка Users
          parent.Children = parent.Children.Append(Users[dataRow2.USERNAME()]);
        }
      }
    }

    private LUser InitCurrentUser()
    {
      var userInfo = _iNetPC.Native_GetInfoAboutCurrentUser().Rows[0];
      var user = _users is null ? new LUser(this, _iNetPC.Native_WFGetUserProperties(userInfo.ID()).Rows[0]) : _users[userInfo.NAME()];
      user.WorkDir = userInfo.USERDIR();
      user.FileDir = userInfo.FILEDIR();
      return user;
    }

    private NamedEntityCollection<LUser> InitUsers()
    {
      var users = new NamedEntityCollection<LUser>(() => _iNetPC.Native_GetUserList().Select(x => new LUser(this, x)), 100);
      if (_currentUser != null)
        users[_currentUser.Name] = _currentUser;

      return users;
    }

    private IEnumerable<LLinkInfoBetweenTypes> GetLinksInfoBetweenTypes(DataTable dataTable)
    {
      /* Метод GetLinkListEx возвращает данные в которых есть дубликаты
         Например Если связь горизонтальная
         |_TYPE_ID_1|  _TYPE_NAME_1   |_TYPE_ID_2| _TYPE_NAME_2    |_ID_LINKTYPE|   _LINKTYPE    |  _INVERSENAME  |_LINKKIND|_DIRECTION|_IS_QUANTITY|
         |    59    |Сборочная единица|    59    |Сборочная единица|     19     |Взаимозаменяемые|Взаимозаменяемые|    1    |    1     |     0      |
         |    59    |Сборочная единица|    59    |Сборочная единица|     19     |Взаимозаменяемые|Взаимозаменяемые|    1    |    1     |     0      |
         |    59    |Сборочная единица|    59    |Сборочная единица|     19     |Взаимозаменяемые|Взаимозаменяемые|    1    |    -1    |     0      |
         |    59    |Сборочная единица|    59    |Сборочная единица|     19     |Взаимозаменяемые|Взаимозаменяемые|    1    |    -1    |     0      |

         Если связь вертикальная и типы могут входить друг в друга
         |_TYPE_ID_1|  _TYPE_NAME_1   |_TYPE_ID_2| _TYPE_NAME_2    |_ID_LINKTYPE|   _LINKTYPE          |  _INVERSENAME  |_LINKKIND|_DIRECTION|_IS_QUANTITY|
         |    59    |Сборочная единица|    59    |Сборочная единица|     8      |Изготавливается из ...|Для изготовления|    0    |   1      |     0      |
         |    59    |Сборочная единица|    59    |Сборочная единица|     8      |Изготавливается из ...|Для изготовления|    0    |   -1     |     0      |

          Для исключения дубликатов, если Id, TypeId1 и TypeId2 такие как у предыдущей добавленной строки, 
          то присваиваем пердыдущей позиции (уже добавленной) Direction = LinkDirection.ForwardAndBackward, не добавляя текущуюю 
      */
      LLinkInfoBetweenTypes previousLinkInfo = null;
      foreach (DataRow dataRow in dataTable.Rows)
      {
        var currentLinkInfo = new LLinkInfoBetweenTypes(this, dataRow);
        if (previousLinkInfo != null &&
            previousLinkInfo.Id == currentLinkInfo.Id && 
            previousLinkInfo.TypeId1 == currentLinkInfo.TypeId1 && 
            previousLinkInfo.TypeId2 == currentLinkInfo.TypeId2)
        {
          previousLinkInfo.Direction = LinkDirection.ForwardAndBackward;
        }
        else
        {
          yield return previousLinkInfo = currentLinkInfo;
        }
      }
    }
  }
}