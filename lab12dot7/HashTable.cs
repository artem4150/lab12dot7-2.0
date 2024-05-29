using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using лаба10;

namespace lab12dot7
{
    public class MyKeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public MyKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    public class HashTable<TKey, TValue>
    {
        private const int DefaultCapacity = 10;
        private MyKeyValuePair<TKey, TValue>?[] _items;
        private int _count;

        public HashTable()
        {
            _items = new MyKeyValuePair<TKey, TValue>?[DefaultCapacity];
            _count = 0;
        }

        private int GetIndex(TKey key, int length)
        {
            int hash = key.GetHashCode();
            return Math.Abs(hash % length);
        }

        private int GetIndex(TKey key)
        {
            return GetIndex(key, _items.Length);
        }

        public void Add(TKey key, TValue value)
        {
            if (_count >= _items.Length * 0.75)
            {
                Resize();
            }

            int index = GetIndex(key);
            while (_items[index] != null && !_items[index].Key.Equals(key))
            {
                index = (index + 1) % _items.Length;
            }

            _items[index] = new MyKeyValuePair<TKey, TValue>(key, value);
            _count++;
        }

        public TValue Find(TKey key)
        {
            int index = GetIndex(key);
            int startIndex = index;
            while (_items[index] != null)
            {
                if (_items[index].Key.Equals(key))
                {
                    return _items[index].Value;
                }
                index = (index + 1) % _items.Length;
                if (index == startIndex)
                {
                    break;
                }
            }
            throw new KeyNotFoundException("Key not found");
        }

        public bool Remove(TKey key)
        {
            int index = GetIndex(key);
            int startIndex = index;
            while (_items[index] != null)
            {
                if (_items[index].Key.Equals(key))
                {
                    _items[index] = null;
                    _count--;

                    int nextIndex = (index + 1) % _items.Length;
                    while (_items[nextIndex] != null)
                    {
                        if (GetIndex(_items[nextIndex].Key) == startIndex)
                        {
                            break;
                        }

                        _items[index] = _items[nextIndex];
                        _items[nextIndex] = null;

                        index = nextIndex;
                        nextIndex = (nextIndex + 1) % _items.Length;
                    }
                    return true;
                }
                index = (index + 1) % _items.Length;
                if (index == startIndex)
                {
                    break;
                }
            }
            return false;
        }

        private void Resize()
        {
            var newSize = _items.Length * 2;
            var newItems = new MyKeyValuePair<TKey, TValue>?[newSize];
            foreach (var item in _items)
            {
                if (item != null)
                {
                    int index = GetIndex(item.Key, newSize);
                    while (newItems[index] != null)
                    {
                        index = (index + 1) % newSize;
                    }
                    newItems[index] = item;
                }
            }
            _items = newItems;
        }

        public int GetCount()
        {
            return _count;
        }

        public void Print()
        {
            
            foreach (var item in _items)
            {
                if (item != null)
                {
                    Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
                }
            }
            Console.WriteLine($"Количество элементов в списке: {_count}");
        }

        
    }
}