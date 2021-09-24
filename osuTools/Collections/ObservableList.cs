using System;
using System.Collections;
using System.Collections.Generic;

namespace osuTools.Collections
{
    /// <summary>
    /// 在添加、删除、插入元素和清空列表时触发事件的列表
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class ObservableList<T> : IList<T>
    {
        private T[] _arr;
        private int _len, _capacity;
        /// <summary>
        /// 为OnAddItem提供参数
        /// </summary>
        /// <param name="item">被添加的元素</param>
        public delegate void AddItemEventHandler(T item);
        /// <summary>
        /// 为OnRemoveItem提供参数
        /// </summary>
        /// <param name="item">要删除的元素</param>
        /// <param name="success">删除是否成功</param>

        public delegate void RemoveItemEventHandler(T item,bool success);
        /// <summary>
        /// 为OnInsertItem提供参数
        /// </summary>
        /// <param name="item">要添加的元素</param>
        /// <param name="index">被添加到的位置</param>

        public delegate void InsertItemEventHandler(T item, int index);
        /// <summary>
        /// 为OnClear提供事件处理器
        /// </summary>
        public delegate void ClearItemEventHandler();
        /// <summary>
        /// 添加元素时触发的事件
        /// </summary>

        public event AddItemEventHandler OnAdd = item => { };
        /// <summary>
        /// 移除元素时触发的事件
        /// </summary>
        public event RemoveItemEventHandler OnRemove = (item,suc) => { };
        /// <summary>
        /// 插入元素时触发的事件
        /// </summary>
        public event InsertItemEventHandler OnInsert = (item, index) => { };
        /// <summary>
        /// 清空列表时触发的事件
        /// </summary>
        public event ClearItemEventHandler OnClear = () => { };
        /// <summary>
        /// 将一个<see cref="IEnumerator{T}"/>中的内容添加到这个列表
        /// </summary>
        /// <param name="collection">要添加的集合</param>
        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }
        /// <summary>
        /// 对集合中的每个元素执行操作
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="skipNull">是否跳过null</param>
        public void ForEach(Action<T> action, bool skipNull = false)
        {
            foreach (var item in this)
            {
                if (skipNull && item == null)
                    continue;
                action.Invoke(item);
            }
        }
        int IndexProcessor(int len,int index)
        {
            index = index < 0 ? len + index : index;
            if (index > len - 1 || index < 0)
                throw new IndexOutOfRangeException("Index的值超出范围。");
            return index;
        }
        void EnsureCapacity(int size, bool forced)
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
        /// <summary>
        /// 初始化一个长度为0的ObservableList
        /// </summary>
        public ObservableList()
        {
            _arr = new T[0];
        }
       /// <summary>
       /// 使用另一个集合的元素填充列表
       /// </summary>
       /// <param name="collection">集合</param>
        public ObservableList(IEnumerable<T> collection) => AddRange(collection);
        ///<inheritdoc/>
        public void Add(T item)
        {
            EnsureCapacity(_len + 1, false);
            _arr[_len++] = item;
            OnAdd(item);
        }
        ///<inheritdoc/>
        public bool Remove(T item)
        {
            int itemHash = -1;
            bool suc = false;
            if (item != null)
               itemHash = item.GetHashCode();
            for (int i = 0; i < _len; i++)
            {
                if (item == null)
                {
                    if (_arr[i] == null)
                    {
                        _arr[i] = default;
                        Array.Copy(_arr, i + 1, _arr, i, _len - i - 1);
                        _arr[--_len] = default;
                        suc = true;
                    }
                }
                else if(_arr[i].GetHashCode() == itemHash)
                    if (_arr[i].Equals(item))
                    {
                        _arr[i] = default;
                        Array.Copy(_arr, i + 1, _arr, i, _len - i - 1);
                        _arr[--_len] = default;
                        suc = true;
                    }
            }

            OnRemove(item,suc);
            return suc;
        }
        ///<inheritdoc/>
        public int IndexOf(T item)
        {
            for (int i = 0; i < _len; i++)
            {
                if (item == null)
                {
                    if (_arr[i] == null)
                        return i;
                }
                else if (_arr[i].GetHashCode() == item.GetHashCode())
                    if (_arr[i].Equals(item))
                        return i;
            }
            return -1;
        }
        ///<inheritdoc/>
        public void RemoveAt(int index)
        {
            index = IndexProcessor(_len,index);
            OnRemove(_arr[index],true);
            _arr[index] = default;
            Array.Copy(_arr, index + 1, _arr, index, _len - index - 1);
            _arr[--_len] = default;
        }
        ///<inheritdoc/>
        public void Insert(int index,T item)
        {

            index = IndexProcessor(_len, index);
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
            OnInsert?.Invoke(item, index);
            _len++;
        }
        ///<inheritdoc/>

        public T this[int index]
        {
            get
            {

                index = IndexProcessor(_len, index);
                return _arr[index];
            }
            set
            {
                index = IndexProcessor(_len, index);
                _arr[index] = value;
            }
        }
        ///<inheritdoc/>

        public void Clear()
        {
            Array.Clear(_arr, 0, _len);
            OnClear();
            _len = 0;
        }
        ///<inheritdoc/>
        public bool Contains(T item)
        {
            for (int i = 0; i < _len; i++)
            {
                if (item == null)
                {
                    if (_arr[i] == null)
                        return true;
                }
                else if (_arr[i].GetHashCode() == item.GetHashCode())
                    if (_arr[i].Equals(item))
                        return true;
            }
            return false;
        }
        ///<inheritdoc/>
        public void CopyTo(T[] arr, int index)
        {
            Array.Copy(_arr,arr,_len);
        }
        ///<inheritdoc/>
        public int Count => _len;
        ///<inheritdoc/>
        public bool IsReadOnly => true;
        ///<inheritdoc/>
        public IEnumerator<T> GetEnumerator() => new ObservableListEnumerator<T>(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
