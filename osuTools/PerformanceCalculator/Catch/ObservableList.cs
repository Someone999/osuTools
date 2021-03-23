using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuTools.PerformanceCalculator.Catch
{
    public class ObservableList<T> : IList<T>
    {
        private T[] _objArr;
        private int len, capacity;

        public delegate void AddItem(T item);

        public delegate void RemoveItem(T item,bool success);

        public delegate void InsertItem(T item, int index);

        public delegate void ClearItem();

        public event AddItem OnAdd = item => { };
        public event RemoveItem OnRemove = (item,suc) => { };
        public event InsertItem OnInsert = (item, index) => { };
        public event ClearItem OnClear = () => { };

        int IndexProcessor(int len,int index)
        {
            index = index < 0 ? len + index : index;
            if (index > len - 1 || index < 0)
                throw new IndexOutOfRangeException("Index的值超出范围。");
            return index;
        }
        void extend()
        {
            if (len == capacity)
            {
                T[] newArr = new T[capacity == 0 ? 4 : capacity * 2];
                Array.Copy(_objArr,newArr,len);
                _objArr = newArr;
            }
        }
        public ObservableList()
        {
            _objArr = new T[0];
        }

        public void Add(T item)
        {
            extend();
            _objArr[len++] = item;
            OnAdd(item);
        }

        public bool Remove(T item)
        {
            int itemHash = -1;
            bool suc = false;
            if (item != null)
               itemHash = item.GetHashCode();
            for (int i = 0; i < len; i++)
            {
                if (item == null)
                {
                    if (_objArr[i] == null)
                    {
                        _objArr[i] = default;
                        Array.Copy(_objArr, i + 1, _objArr, i, len - i - 1);
                        _objArr[--len] = default;
                        suc = true;
                    }
                }
                else if(_objArr[i].GetHashCode() == itemHash)
                    if (_objArr[i].Equals(item))
                    {
                        _objArr[i] = default;
                        Array.Copy(_objArr, i + 1, _objArr, i, len - i - 1);
                        _objArr[--len] = default;
                        suc = true;
                    }
            }

            OnRemove(item,suc);
            return suc;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < len; i++)
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

        public void RemoveAt(int index)
        {
            index = IndexProcessor(len,index);
            OnRemove(_objArr[index],true);
            _objArr[index] = default;
            Array.Copy(_objArr, index + 1, _objArr, index, len - index - 1);
            _objArr[--len] = default;
        }
        public void Insert(int index,T item)
        {

            index = IndexProcessor(len, index);
            T[] before = new T[index], after = new T[len - index];
            T[] summary = new T[len + 1];
            int pos = 0;
            Array.Copy(_objArr, before, index);
            Array.Copy(_objArr, index, after, 0, len - index);
            Array.Copy(before, 0, summary, pos, index);
            pos += index;
            Array.Copy(new [] {item},0, summary,pos, 1);
            pos++;
            Array.Copy(after,0, summary,pos, len - index);
            _objArr = summary;
            OnInsert(item, index);
            len++;
        }

        public T this[int index]
        {
            get
            {

                index = IndexProcessor(len, index);
                return _objArr[index];
            }
            set
            {
                index = IndexProcessor(len, index);
                _objArr[index] = value;
            }
        }

        public void Clear()
        {
            Array.Clear(_objArr, 0, len);
            len = 0;
        }
        public bool Contains(T item)
        {
            for (int i = 0; i < len; i++)
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

        public void CopyTo(T[] arr, int index)
        {
            Array.Copy(_objArr,arr,len);
        }

        public int Count => len;
        public bool IsReadOnly => true;
        public IEnumerator<T> GetEnumerator() => new ObservableListEnumerator<T>(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
