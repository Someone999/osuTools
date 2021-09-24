using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace osuTools.Collections
{
    /// <summary>
    /// 线程安全的列表
    /// </summary>
    /// <typeparam name="T">元素的类型</typeparam>
    public class ConcurrentList<T>:IList<T>
    {
        class ConcurrentListEnumerator:IEnumerator<T>
        {
            private readonly ConcurrentList<T> _innerList;
            private int _cur = -1;
            public ConcurrentListEnumerator(ConcurrentList<T> list)
            {
                _innerList = list;
            }

            public bool MoveNext() => ++_cur > _innerList.Count;

            public void Reset() => _cur = -1;
            public T Current => _cur == -1 ? throw new InvalidOperationException() : _innerList[_cur];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
        private T[] _arr;
        private int _len, _capacity;
        private readonly object _lockObj = new object();
        void EnsureCapacity(int size, bool forced)
        {
            lock (_lockObj)
            {
                int extentTo = forced ? size : size + _len;
                if (forced || size + _len > _capacity)
                {
                    int newCapacity;
                    T[] oldArr = _arr;
                    T[] newArr = new T[newCapacity = forced ? size : _capacity == 0 ? 4 : _capacity * 2];
                    Array.Copy(oldArr, newArr, _len);
                    _arr = newArr;
                    _capacity = newCapacity;
                }
            }
        }
        /// <summary>
        /// 初始化一个ConcurrentList
        /// </summary>
        public ConcurrentList() : this(0)
        {
            _capacity = 0;
        }
        /// <summary>
        /// 使用指定的初始容量初始化一个ConcurrentList
        /// </summary>
        /// <param name="size"></param>
        public ConcurrentList(int size)
        {
            lock (_lockObj)
            {
                _arr = new T[size];
                _capacity = 0;
            }
        }
        /// <summary>
        /// 使用指定的<seealso cref="IEnumerable{T}"/>填充ConcurrentList
        /// </summary>
        /// <param name="collection"></param>
        public ConcurrentList(IEnumerable<T> collection)
        {
            lock (_lockObj)
            {
                if (collection is null)
                    throw new ArgumentNullException(nameof(collection));
                var c = collection as T[] ?? collection.ToArray();
                _arr = new T[c.Length];
                Array.Copy(c.ToArray(), _arr, c.Length);
                _capacity = c.Length;
            }
        }
        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() => new ConcurrentListEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <inheritdoc/>
        public void Add(T item)
        {
            lock (_lockObj)
            {
                EnsureCapacity(_len + 1,false);
                _arr[_len++] = item;
            }
        }
        /// <inheritdoc/>
        public void Clear()
        {
            lock (_lockObj)
            {
                _arr = new T[_len];
                _len = 0;
            }
        }
        /// <inheritdoc/>
        public bool Contains(T item)
        {
            lock (_lockObj)
            {
                IEqualityComparer comparer = EqualityComparer<T>.Default;
                if (item is IEqualityComparer equalityComparer)
                {
                    comparer = equalityComparer;
                }

                foreach (var obj in _arr)
                {
                    if (comparer.Equals(obj, item))
                        return true;
                }

                return false;
            }
        }

        int IndexConverter(int oriIndex)
        {
            if (oriIndex < 0)
            {
                if (_arr.Length - oriIndex < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                return _arr.Length - oriIndex;
            }

            return oriIndex > _arr.Length ? throw new IndexOutOfRangeException() : oriIndex;
        }
        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_lockObj)
            {
                IndexConverter(arrayIndex);
                if (_len - arrayIndex < 0)
                    throw new IndexOutOfRangeException();
                var objs = _arr.Skip(arrayIndex).Take(_len - arrayIndex).ToArray();
                Array.Copy(objs, array, _len - arrayIndex);
            }
        }
        /// <inheritdoc/>
        public bool Remove(T item)
        {
            IEqualityComparer comparer = EqualityComparer<T>.Default;
            if (item is IEqualityComparer equalityComparer)
            {
                comparer = equalityComparer;
            }

            int delIndex = -1;
            for(int i = 0; i<_len;i++)
            {
                if (comparer.Equals(_arr[i], item))
                {
                    delIndex = i;
                }
            }

            if (delIndex == -1)
            {
                return false;
            }
            RemoveAt(delIndex);
            return true;

        }
        /// <inheritdoc/>
        public int Count => _len;
        /// <inheritdoc/>
        public bool IsReadOnly => false;
        /// <inheritdoc/>
        public int IndexOf(T item)
        {
            lock (_lockObj)
            {
                IEqualityComparer comparer = EqualityComparer<T>.Default;
                if (item is IEqualityComparer equalityComparer)
                {
                    comparer = equalityComparer;
                }
                for (int i = 0; i < _len; i++)
                {
                    if (comparer.Equals(_arr[i], item))
                    {
                        return i;
                    }
                }

                return -1;
            }
        }
        /// <inheritdoc/>
        public void Insert(int index, T item)
        {
            lock (_lockObj)
            {
                
                int insertIndex = index;
                if (insertIndex != _arr.Length - 1)
                {
                    EnsureCapacity(_len + 2, false);
                    Array.Copy(_arr, insertIndex, _arr, insertIndex + 1, _len - insertIndex);
                    _arr[insertIndex] = item;
                    _len++;
                }
                else
                {
                    Add(item);
                }
            }
        }
        /// <inheritdoc/>

        public void RemoveAt(int index)
        {
            int delIndex = IndexConverter(index);
            _arr[delIndex] = default;
            int startIndex = delIndex + 1;
            if (delIndex != _arr.Length - 1)
            {
                Array.Copy(_arr, startIndex, _arr, delIndex, _arr.Length - delIndex - 1);
            }
            _arr[IndexConverter(-1)] = default;
            _len--;

        }
       ///<inheritdoc/>>
        public T this[int index]
        {
            get
            {
                lock (_lockObj)
                {
                    return _arr[IndexConverter(index)];
                }
            }
            set
            {
                lock (_lockObj)
                {
                    _arr[IndexConverter(index)] = value;
                }
            }
        }
    }
}
