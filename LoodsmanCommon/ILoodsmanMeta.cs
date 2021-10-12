using Ascon.Plm.Loodsman.PluginSDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LoodsmanCommon.Entities.Meta;
using LoodsmanCommon.Extensions;

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
        //Используем массивы т.к. нужны простые списки и нет необходимости расширять данные
        private LType[] _types;
        private LLink[] _links;
        private LState[] _states;
        private LAttribute[] _attributes;
        private LProxyUseCase[] _proxyUseCases;
        private LLinkInfoBetweenTypes[] _linksInfoBetweenTypes;
        private readonly INetPluginCall _iNetPC;

        public IEnumerable<LType> Types => _types ??= _iNetPC.Native_GetTypeListEx().GetRows().Select(x => new LType(_iNetPC, x, Attributes, States)).ToArray();
        public IEnumerable<LLink> Links => _links ??= _iNetPC.Native_GetLinkList().GetRows().Select(x => new LLink(x)).ToArray();
        public IEnumerable<LState> States => _states ??= _iNetPC.Native_GetStateList().GetRows().Select(x => new LState(x)).ToArray();
        public IEnumerable<LAttribute> Attributes => _attributes ??= _iNetPC.Native_GetAttributeList().GetRows().Select(x => new LAttribute(x)).ToArray();
        public IEnumerable<LProxyUseCase> ProxyUseCases => _proxyUseCases ??= _iNetPC.Native_GetProxyUseCases().GetRows().Select(x => new LProxyUseCase(x)).ToArray();
        public IEnumerable<LLinkInfoBetweenTypes> LinksInfoBetweenTypes => _linksInfoBetweenTypes ??= GetLinksInfoBetweenTypes(_iNetPC.Native_GetLinkListEx()).ToArray();

        public LoodsmanMeta(INetPluginCall iNetPC)
        {
            _iNetPC = iNetPC;
        }

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
        }
    }
}