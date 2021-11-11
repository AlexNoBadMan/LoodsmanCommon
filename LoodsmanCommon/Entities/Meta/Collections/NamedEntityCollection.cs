using System;
using System.Collections.Generic;

namespace LoodsmanCommon.Entities.Meta.Collections
{
    public class NamedEntityCollection<TValue> : MetaItemCollection<string, TValue> where TValue : INamedEntity
    {
        private readonly Func<IEnumerable<TValue>> _dataFactory;
        internal NamedEntityCollection(Func<IEnumerable<TValue>> dataFactory, int capacity = 0) : base(capacity)
        {
            _dataFactory = dataFactory;
            Init();
        }

        protected virtual void Init()
        {
            var data = _dataFactory();
            foreach (var lEntity in data)
            {
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