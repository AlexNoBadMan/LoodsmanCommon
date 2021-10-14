using Ascon.Plm.Loodsman.PluginSDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LoodsmanCommon.Entities.Meta;
using LoodsmanCommon.Extensions;
using System.Collections.ObjectModel;

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
        LProxyUseCase GetProxyUseCase(string parentType, string childDocumentType, string extension);
        void Clear();
    }

    internal class LoodsmanMeta : ILoodsmanMeta
    {
        //Используем массивы т.к. нужны простые списки и нет необходимости расширять данные
        private IReadOnlyList<LType> _types;
        private IReadOnlyList<LLink> _links;
        private IReadOnlyList<LState> _states;
        private IReadOnlyList<LAttribute> _attributes;
        private IReadOnlyList<LProxyUseCase> _proxyUseCases;
        private IReadOnlyList<LLinkInfoBetweenTypes> _linksInfoBetweenTypes;
        private IReadOnlyList<LMeasure> _measures;
        //private IReadOnlyList<LMeasureUnit> _measuresUnits;
        private readonly INetPluginCall _iNetPC;

        public IReadOnlyList<LType> Types => _types ??= _iNetPC.Native_GetTypeListEx().GetRows().Select(x => new LType(_iNetPC, x, Attributes, States)).ToReadOnlyList();
        public IReadOnlyList<LLink> Links => _links ??= _iNetPC.Native_GetLinkList().GetRows().Select(x => new LLink(x)).ToReadOnlyList();
        public IReadOnlyList<LState> States => _states ??= _iNetPC.Native_GetStateList().GetRows().Select(x => new LState(x)).ToReadOnlyList();
        public IReadOnlyList<LAttribute> Attributes => _attributes ??= _iNetPC.Native_GetAttributeList().GetRows().Select(x => new LAttribute(x)).ToReadOnlyList();
        public IReadOnlyList<LProxyUseCase> ProxyUseCases => _proxyUseCases ??= _iNetPC.Native_GetProxyUseCases().GetRows().Select(x => new LProxyUseCase(x)).ToReadOnlyList();
        public IReadOnlyList<LLinkInfoBetweenTypes> LinksInfoBetweenTypes => _linksInfoBetweenTypes ??= GetLinksInfoBetweenTypes(_iNetPC.Native_GetLinkListEx()).ToReadOnlyList();
        public IReadOnlyList<LMeasure> Measures => _measures ??= _iNetPC.Native_GetFromBO_Nature().GetRows().Select(x => new LMeasure(_iNetPC, x)).ToReadOnlyList();
        //public IReadOnlyList<LMeasureUnit> MeasuresUnits => _measuresUnits ??= Measures.SelectMany(x => x.Units).ToReadOnlyList();
        
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