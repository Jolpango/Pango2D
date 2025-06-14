using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Core
{
    public class Registry<TKey, TValue>
    {
        protected readonly Dictionary<TKey, TValue> entries = new();

        public void Add(TKey key, TValue value) => entries[key] = value;

        public TValue Get(TKey key)
        {
            if (!entries.TryGetValue(key, out var value))
                throw new KeyNotFoundException($"'{key}' not found in registry.");
            return value;
        }

        public bool TryGet(TKey key, out TValue value) => entries.TryGetValue(key, out value);

        public bool Contains(TKey key) => entries.ContainsKey(key);

        public bool Remove(TKey key) => entries.Remove(key);

        public void Clear() => entries.Clear();
    }

}
