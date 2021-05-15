using System;
using System.Collections.Generic;

namespace osuTools.Collections
{
    /// <summary>
    /// 可克隆的更改可通知的列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CloneableObservableList<T>:ObservableList<T> where T:ICloneable
    {
        /// <summary>
        /// 将一个集合的元素原样添加到列表的初始化方法
        /// </summary>
        /// <param name="collection"></param>
        public CloneableObservableList(IEnumerable<T> collection):base(collection)
        {
        }
        /// <summary>
        /// 初始化一个空列表
        /// </summary>
        public CloneableObservableList()
        {
        }
        /// <summary>
        /// 克隆该列表，将会深复制所有元素
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            CloneableList<T> cloneableList = new CloneableList<T>();
            ForEach(item => cloneableList.Add((T)item?.Clone()));
            return cloneableList;
        }
    }
}
