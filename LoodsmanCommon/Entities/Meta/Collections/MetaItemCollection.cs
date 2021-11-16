using System.Collections;
using System.Collections.Generic;

namespace LoodsmanCommon.Entities.Meta.Collections
{
    public abstract class MetaItemCollection<TKey, TValue> : IEnumerable<TValue>, IReadOnlyCollection<TValue>
    {
        protected Dictionary<TKey, TValue> _items;

        public TValue this[TKey key] { get => _items[key]; internal set => _items[key] = value; }

        public int Count => _items.Count;

        public MetaItemCollection() : this (0) { }

        public MetaItemCollection(int capacity)
        {
            _items = new Dictionary<TKey, TValue>(capacity);
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            foreach (var item in _items)
            {
                yield return item.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in _items)
            {
                yield return item.Value;
            }
        }

        public abstract void Refresh();
    }
}
