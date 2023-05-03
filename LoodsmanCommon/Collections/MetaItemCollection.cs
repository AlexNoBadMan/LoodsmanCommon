using System.Collections;
using System.Collections.Generic;

namespace LoodsmanCommon
{
  public abstract class MetaItemCollection<TKey, TValue> : IEnumerable<TValue>, IReadOnlyCollection<TValue>, IReadOnlyDictionary<TKey, TValue>
  {
    protected Dictionary<TKey, TValue> _items;

    public TValue this[TKey key] { get => _items[key]; internal set => _items[key] = value; }

    public int Count => _items.Count;

    public IEnumerable<TKey> Keys => _items.Keys;

    public IEnumerable<TValue> Values => _items.Values;

    public MetaItemCollection() : this(0) { }

    public MetaItemCollection(int capacity)
    {
      _items = new Dictionary<TKey, TValue>(capacity);
    }

    public bool ContainsKey(TKey key)
    {
      return _items.ContainsKey(key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      return _items.TryGetValue(key, out value);
    }

    public IEnumerator<TValue> GetEnumerator()
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

    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
      return _items.GetEnumerator();
    }

    public abstract void Refresh();
  }
}
