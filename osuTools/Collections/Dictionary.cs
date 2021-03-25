using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuTools.Collections
{
    public class Dictionary<TKey,TValue>:IDictionary<TKey,TValue>
    {
        private List<TKey> _keys = new List<TKey>();
        private List<TValue> _values = new List<TValue>();
        internal List<KeyValuePair<TKey, TValue>> _pairs = new List<KeyValuePair<TKey, TValue>>();
        private int _len;
        /// <summary>
        /// 字典存储的键
        /// </summary>
        public ICollection<TKey> Keys => _keys.ToArray();

        /// <summary>
        /// 字典存储的值
        /// </summary>
        public ICollection<TValue> Values => _values.ToArray();

        public virtual bool ContainsKey(TKey key)
        {
            foreach (var k in _keys)
            {
                if (k.Equals(key))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 向字典中添加元素，如果要添加的键已存在或者键为null时会引发异常
        /// </summary>
        /// <param name="key">要添加的键</param>
        /// <param name="val">要添加的值</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"></exception>

        public virtual void Add(TKey key, TValue val)
        {
            if(key == null)
            {
                throw new ArgumentNullException(nameof(key),"key的值不能为null");
            }
            _keys.Add(key);
            _values.Add(val);
            _pairs.Add(new KeyValuePair<TKey, TValue>(key,val));
            _len++;
        }
        /// <summary>
        /// 将一个键值对添加到字典，要添加的键已存在或者键为null时会引发异常
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key,item.Value);
        }
        ///<inheritdoc/>
        public virtual bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException();
            for (int i = 0; i < _keys.Count; i++)
            {
                if (_keys[i].Equals(key))
                {
                    _keys.RemoveAt(i);
                    _values.RemoveAt(i);
                    _pairs.RemoveAt(i);
                    _len--;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取一个键在Keys中的位置
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>位置索引，从0开始</returns>
        protected virtual int IndexOfKey(TKey key)
        {
            int index = -1;
            for (int i = 0; i < _len; i++)
            {
                if (_keys[i].Equals(key))
                    index = i;
            }
            return index;
        }
        ///<inheritdoc/>
        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            int index = IndexOfKey(key);
            if (index != -1)
            {
                value = _values[index];
                return true;
            }
            value = default;
            return false;
        }
        ///<inheritdoc/>
        public virtual bool Contains(KeyValuePair<TKey, TValue> item) => IndexOfKey(item.Key) != -1;
        ///<inheritdoc/>
        public virtual void Clear()
        {
            _keys.Clear();
            _values.Clear();
            _pairs.Clear();
            _len = 0;
        }
        ///<inheritdoc/>
        public virtual TValue this[TKey key]
        {
            get
            {
                TryGetValue(key,out var val);
                return val;
            }
            set
            {
                int i = IndexOfKey(key);
                if (i!=-1)
                    _values[i] = value;
                else
                    Add(key, value);
            }
        }
        ///<inheritdoc/>
        public virtual bool Remove(KeyValuePair<TKey, TValue> item)
        {
            
            foreach (var pair in _pairs)
            {
                if (pair.Key.Equals(item.Key) && pair.Value.Equals(item.Value))
                {
                    _pairs.Remove(item);
                    _keys.Remove(item.Key);
                    _values.Remove(item.Value);
                    return true;
                }
            }
            return false;
        }
        ///<inheritdoc/>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Array.Copy(_pairs.ToArray(), array, _len);
        }
        ///<inheritdoc/>
        public bool IsReadOnly => false;
        ///<inheritdoc/>
        public int Count => _len;
        ///<inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new DictionaryEnumerator<TKey, TValue>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
