using Ascon.Plm.Loodsman.PluginSDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LoodsmanCommon.Entities;


namespace LoodsmanCommon
{
    public interface ILoodsmanMeta
    {
        IEnumerable<LType> Types { get; }
        IEnumerable<LLink> Links { get; }
        IEnumerable<LState> States { get; }
        IEnumerable<LAttribute> Attributes { get; }
        IEnumerable<LProxyUseCase> ProxyUseCases { get; }
        IEnumerable<LLinkInfoBetweenTypes> LinksInfoBetweenTypes { get; }
        LProxyUseCase GetProxyUseCase(string parentType, string childDocumentType, string extension);
        void Clear();
    }

    internal class LoodsmanMeta : ILoodsmanMeta
    {
        private List<LType> _types;
        private List<LLink> _links;
        private List<LState> _states;
        private List<LAttribute> _attributes;
        private List<LProxyUseCase> _proxyUseCases;
        private List<LLinkInfoBetweenTypes> _linksInfoBetweenTypes;
        private readonly INetPluginCall _iNetPC;

        public IEnumerable<LType> Types
        {
            get
            {
                if (_types is null)
                {
                    _types = new List<LType>();
                    using (var dataTable = _iNetPC.GetDataTable("GetTypeListEx"))
                        _types.AddRange(dataTable.Select().Select(x => new LType(x, Attributes, States)));
                }
                return _types;
            }
        }

        public IEnumerable<LLink> Links
        {
            get
            {
                if (_links is null)
                {
                    _links = new List<LLink>();
                    using (var dataTable = _iNetPC.GetDataTable("GetLinkList"))
                        _links.AddRange(dataTable.Select().Select(x => new LLink(x)));
                }
                return _links;
            }
        }

        public IEnumerable<LState> States
        {
            get
            {
                if (_states is null)
                {
                    _states = new List<LState>();
                    using (var dataTable = _iNetPC.GetDataTable("GetStateList"))
                        _states.AddRange(dataTable.Select().Select(x => new LState(x)));
                }
                return _states;
            }
        }

        public IEnumerable<LAttribute> Attributes
        {
            get
            {
                if (_attributes is null)
                {
                    _attributes = new List<LAttribute>();
                    using (var dataTable = _iNetPC.GetDataTable("GetAttributeList2", 0))
                        _attributes.AddRange(dataTable.Select().Select(x => new LAttribute(x)));
                }
                return _attributes;
            }
        }

        public IEnumerable<LProxyUseCase> ProxyUseCases
        {
            get
            {
                if (_proxyUseCases is null)
                {
                    _proxyUseCases = new List<LProxyUseCase>();
                    using (var dataTable = _iNetPC.GetDataTable("GetProxyUseCases", 0, 0, 0))
                        _proxyUseCases.AddRange(dataTable.Select().Select(x => new LProxyUseCase(x)));
                }
                return _proxyUseCases;
            }
        }
        
        public IEnumerable<LLinkInfoBetweenTypes> LinksInfoBetweenTypes 
        { 
            get 
            {
                if (_linksInfoBetweenTypes is null)
                {
                    _linksInfoBetweenTypes = new List<LLinkInfoBetweenTypes>();
                    using (var dataTable = _iNetPC.GetDataTable("GetLinkListEx"))
                        FillLinksInfoBetweenTypes(dataTable, _linksInfoBetweenTypes);
                }
                return _linksInfoBetweenTypes; 
            } 
        }

        public LoodsmanMeta(INetPluginCall iNetPC)
        {
            _iNetPC = iNetPC;
        }

        private static void FillLinksInfoBetweenTypes(DataTable dataTable, List<LLinkInfoBetweenTypes> linkInfos)
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
                    linkInfos.Add(currentLinkInfo);
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
        }
    }
}