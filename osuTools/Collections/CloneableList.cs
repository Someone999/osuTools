using System;
using System.Collections.Generic;

namespace osuTools.Collections
{
    /// <summary>
    /// 可进行克隆的列表
    /// </summary>
    /// <typeparam name="T">要存放的数据类型，类型必须实现ICloneable</typeparam>
    public class CloneableList<T>:List<T>,ICloneable where T:ICloneable
    {
        /// <summary>
        /// 克隆列表
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            CloneableList<T> cloneableList = new CloneableList<T>();
            ForEach(item=>cloneableList.Add((T)item?.Clone()));
            return cloneableList;
        }
        /// <summary>
        /// 将列表转换成<seealso cref="ObservableList{T}"/>
        /// </summary>
        /// <param name="useDeepCopy">是否使用深复制</param>
        /// <returns></returns>
        public ObservableList<T> ToObservableList(bool useDeepCopy)
        {
            return useDeepCopy ? new ObservableList<T>((CloneableList<T>)Clone()) : new ObservableList<T>(this);
        }
        /// <summary>
        /// 将克隆列表转换为普通列表
        /// </summary>
        /// <param name="useDeepCopy">是否使用深复制</param>
        /// <returns></returns>
        public List<T> ToList(bool useDeepCopy)
        {
            return useDeepCopy ? new List<T>((CloneableList<T>)Clone()) : new List<T>(this);
        }
    }
}
