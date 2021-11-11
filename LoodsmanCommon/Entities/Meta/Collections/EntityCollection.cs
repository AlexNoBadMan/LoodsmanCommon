using System;
using System.Collections.Generic;

namespace LoodsmanCommon.Entities.Meta.Collections
{
    public class EntityCollection<TValue> : MetaItemCollection<int, TValue> where TValue : IEntity
    {
        private readonly Func<IEnumerable<TValue>> _dataFactory;

        internal EntityCollection(Func<IEnumerable<TValue>> dataFactory, int capacity = 0) : base(capacity)
        {
            _dataFactory = dataFactory;
            Init();
        }

        protected virtual void Init()
        {
            var dataRows = _dataFactory();
            foreach (var lEntity in dataRows)
            {
                _items.Add(lEntity.Id, lEntity);
            }
        }
        
        public override void Refresh()
        {
            _items.Clear();
            Init();
        }
    }
}