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
        private T[] _objArr;
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
        /// 
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
        void Extend()
        {
            if (_len + 1 >= _capacity)
            {
                int newCapacity = _capacity == 0 ? 4 : _capacity * 2;
                T[] newArr = new T[newCapacity];
                Array.Copy(_objArr,newArr,_len);
                _objArr = newArr;
                _capacity = newCapacity;
            }
        }
        /// <summary>
        /// 初始化一个长度为0的ObservableList
        /// </summary>
        public ObservableList()
        {
            _objArr = new T[0];
        }
       /// <summary>
       /// 使用另一个集合的元素填充列表
       /// </summary>
       /// <param name="collection">集合</param>
        public ObservableList(IEnumerable<T> collection) => AddRange(collection);
        ///<inheritdoc/>
        public void Add(T item)
        {
            Extend();
            _objArr[_len++] = item;
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
                    if (_objArr[i] == null)
                    {
                        _objArr[i] = default;
                        Array.Copy(_objArr, i + 1, _objArr, i, _len - i - 1);
                        _objArr[--_len] = default;
                        suc = true;
                    }
                }
                else if(_objArr[i].GetHashCode() == itemHash)
                    if (_objArr[i].Equals(item))
                    {
                        _objArr[i] = default;
                        Array.Copy(_objArr, i + 1, _objArr, i, _len - i - 1);
                        _objArr[--_len] = default;
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
                    if (_objArr[i] == null)
                        return i;
                }
                else if (_objArr[i].GetHashCode() == item.GetHashCode())
                    if (_objArr[i].Equals(item))
                        return i;
            }
            return -1;
        }
        ///<inheritdoc/>
        public void RemoveAt(int index)
        {
            index = IndexProcessor(_len,index);
            OnRemove(_objArr[index],true);
            _objArr[index] = default;
            Array.Copy(_objArr, index + 1, _objArr, index, _len - index - 1);
            _objArr[--_len] = default;
        }
        ///<inheritdoc/>
        public void Insert(int index,T item)
        {

            index = IndexProcessor(_len, index);
            T[] before = new T[index], after = new T[_len - index];
            T[] summary = new T[_len + 1];
            int pos = 0;
            Array.Copy(_objArr, before, index);
            Array.Copy(_objArr, index, after, 0, _len - index);
            Array.Copy(before, 0, summary, pos, index);
            pos += index;
            Array.Copy(new [] {item},0, summary,pos, 1);
            pos++;
            Array.Copy(after,0, summary,pos, _len - index);
            _objArr = summary;
            OnInsert(item, index);
            _len++;
        }
        ///<inheritdoc/>

        public T this[int index]
        {
            get
            {

                index = IndexProcessor(_len, index);
                return _objArr[index];
            }
            set
            {
                index = IndexProcessor(_len, index);
                _objArr[index] = value;
            }
        }
        ///<inheritdoc/>

        public void Clear()
        {
            Array.Clear(_objArr, 0, _len);
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
                    if (_objArr[i] == null)
                        return true;
                }
                else if (_objArr[i].GetHashCode() == item.GetHashCode())
                    if (_objArr[i].Equals(item))
                        return true;
            }
            return false;
        }
        ///<inheritdoc/>
        public void CopyTo(T[] arr, int index)
        {
            Array.Copy(_objArr,arr,_len);
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
