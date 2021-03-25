using System;
using System.Collections.Generic;

namespace osuTools.Collections
{
    public class CloneableList<T>:List<T>,ICloneable where T:ICloneable
    {
        public object Clone()
        {
            CloneableList<T> cloneableList = new CloneableList<T>();
            ForEach(item=>cloneableList.Add((T)item?.Clone()));
            return cloneableList;
        }

        public ObservableList<T> ToObservableList(bool useDeepCopy)
        {
            return useDeepCopy ? new ObservableList<T>((CloneableList<T>)Clone()) : new ObservableList<T>(this);
        }

        public List<T> ToList(bool useDeepCopy)
        {
            return useDeepCopy ? new List<T>((CloneableList<T>)Clone()) : new List<T>(this);
        }
    }
}
