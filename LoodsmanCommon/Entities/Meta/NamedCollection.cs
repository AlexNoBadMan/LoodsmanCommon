using System;
using System.Collections.Generic;
using System.Data;

namespace LoodsmanCommon.Entities.Meta
{
    public class NamedCollection<TValue> : MetaItemCollection<string, TValue> where TValue : INamedEntity
    {
        private readonly Func<DataRowCollection> _dataFactory;
        private readonly Func<DataRow, TValue> _creator;

        internal NamedCollection(Func<DataRowCollection> dataFactory, Func<DataRow, TValue> creator)
        {
            _dataFactory = dataFactory;
            _creator = creator;
        }
        //protected abstract TValue CreateEntity(DataRow dataRow);
        //protected abstract DataRowCollection GetMetadata();

        protected virtual void Init()
        {
            var dataRows = _dataFactory();
            _items ??= new Dictionary<string, TValue>(dataRows.Count);
            foreach (DataRow item in dataRows)
            {
                var lEntity = _creator(item);
                _items.Add(lEntity.Name, lEntity);
            }
        }
        
        public override void Refresh()
        {
            _items.Clear();
            Init();
        }
    }
}