using Ascon.Plm.Loodsman.PluginSDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LoodsmanCommon.Entities.Meta;
using LoodsmanCommon.Entities.Meta.OrganisationUnit;

namespace LoodsmanCommon
{
    public interface ILoodsmanMeta
    {
        IReadOnlyList<LType> Types { get; }
        IReadOnlyList<LLink> Links { get; }
        IReadOnlyList<LState> States { get; }
        IReadOnlyList<LAttribute> Attributes { get; }
        IReadOnlyList<LProxyUseCase> ProxyUseCases { get; }
        IReadOnlyList<LLinkInfoBetweenTypes> LinksInfoBetweenTypes { get; }
        IReadOnlyList<LMeasure> Measures { get; }
        //IReadOnlyList<LMeasureUnit> MeasuresUnits { get; }
        IReadOnlyList<LUser> Users { get; }
        LProxyUseCase GetProxyUseCase(string parentType, string childDocumentType, string extension);
        void Clear();
    }

    internal class LoodsmanMeta : ILoodsmanMeta
    {
        private IReadOnlyList<LType> _types;
        private IReadOnlyList<LLink> _links;
        private IReadOnlyList<LState> _states;
        private IReadOnlyList<LAttribute> _attributes;
        private IReadOnlyList<LProxyUseCase> _proxyUseCases;
        private IReadOnlyList<LLinkInfoBetweenTypes> _linksInfoBetweenTypes;
        private IReadOnlyList<LMeasure> _measures;
        //private IReadOnlyList<LMeasureUnit> _measuresUnits;
        private IReadOnlyList<LUser> _users;
        private readonly INetPluginCall _iNetPC;

        public IReadOnlyList<LType> Types => _types ??= _iNetPC.Native_GetTypeListEx().Select(x => new LType(_iNetPC, x, Attributes, States)).ToReadOnlyList();
        public IReadOnlyList<LLink> Links => _links ??= _iNetPC.Native_GetLinkList().Select(x => new LLink(x)).ToReadOnlyList();
        public IReadOnlyList<LState> States => _states ??= _iNetPC.Native_GetStateList().Select(x => new LState(x)).ToReadOnlyList();
        public IReadOnlyList<LAttribute> Attributes => _attributes ??= _iNetPC.Native_GetAttributeList().Select(x => new LAttribute(x)).ToReadOnlyList();
        public IReadOnlyList<LProxyUseCase> ProxyUseCases => _proxyUseCases ??= _iNetPC.Native_GetProxyUseCases().Select(x => new LProxyUseCase(x)).ToReadOnlyList();
        public IReadOnlyList<LLinkInfoBetweenTypes> LinksInfoBetweenTypes => _linksInfoBetweenTypes ??= GetLinksInfoBetweenTypes(_iNetPC.Native_GetLinkListEx()).ToReadOnlyList();
        public IReadOnlyList<LMeasure> Measures => _measures ??= _iNetPC.Native_GetFromBO_Nature().Select(x => new LMeasure(_iNetPC, x)).ToReadOnlyList();
        //public IReadOnlyList<LMeasureUnit> MeasuresUnits => _measuresUnits ??= Measures.SelectMany(x => x.Units).ToReadOnlyList();
        public IReadOnlyList<LUser> Users => _users ??= _iNetPC.Native_GetUserList().Select(x => new LUser(x)).ToReadOnlyList();
        public IReadOnlyList<LRootDepartment> RootDepartments { get; }

        public LoodsmanMeta(INetPluginCall iNetPC)
        {
            _iNetPC = iNetPC;
            //var treeObjects = iNetPC.Native_WFGetAddressBookTree(-1, 5, -1).Select(x => GetOrganisationUnit(x)).ToList();
            //treeObjects.ForEach(x => x.Children = treeObjects.Where(o => x.Id == o.ParentId));
            //var root = treeObjects.FirstOrDefault(x => x.ParentId == 0);
            //var treeObjectsList = new List<LOrganisationUnit>();
            //foreach (DataRow dataRow in iNetPC.Native_WFGetRoleTree(GetRoleTreeMode.Mode1).Rows)
            //{
            //    var treeObject = GetOrganisationUnit(dataRow);
            //    var parentId = dataRow["_PARENT"] as int? ?? 0;
            //    if (treeObjectsList.Find(x => x.Id == parentId) is LOrganisationUnit parent)
            //    {
            //        treeObject.Parent = parent;
            //        parent.Children = parent.Children.Append(treeObject);
            //    }
            //    treeObjectsList.Add(treeObject);
            //}
            //var rootList = treeObjectsList.LastOrDefault(x => x.ParentId == 0);
        }

        //private LOrganisationUnit GetOrganisationUnit(DataRow dataRow)
        //{
        //    var unitKind = (OrganisationUnitKind)dataRow["_TYPE"];
        //    if (unitKind == OrganisationUnitKind.Department)
        //        return new LDepartment(dataRow);
        //    else if (unitKind == OrganisationUnitKind.Position)
        //        return new LPosition(dataRow);
        //    else if (unitKind == OrganisationUnitKind.RootDepartment)
        //        return new LRootDepartment(dataRow);
        //    else 
        //        return new LPosition(dataRow);
        //}

        private static IEnumerable<LLinkInfoBetweenTypes> GetLinksInfoBetweenTypes(DataTable dataTable)
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
            var previousLinkInfo = new LLinkInfoBetweenTypes();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var currentLinkInfo = new LLinkInfoBetweenTypes(dataRow);
                if (previousLinkInfo.Id == currentLinkInfo.Id && previousLinkInfo.TypeId1 == currentLinkInfo.TypeId1 && previousLinkInfo.TypeId2 == currentLinkInfo.TypeId2)
                {
                    previousLinkInfo.Direction = LinkDirection.ForwardAndBackward;
                }
                else
                {
                    previousLinkInfo = currentLinkInfo;
                    yield return currentLinkInfo;
                }
            }
        }

        public LProxyUseCase GetProxyUseCase(string parentType, string childDocumentType, string extension)
        {
            return ProxyUseCases.FirstOrDefault(x => x.TypeName == parentType &&
                                                     x.DocumentType == childDocumentType &&
                                                     x.Extension.IndexOf(extension, StringComparison.OrdinalIgnoreCase) >= 0);
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
        }
    }
}