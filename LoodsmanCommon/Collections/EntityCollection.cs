using System;
using System.Collections.Generic;

namespace LoodsmanCommon
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
            var data = _dataFactory();
            foreach (var lEntity in data)
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