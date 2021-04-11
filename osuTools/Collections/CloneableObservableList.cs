using System;
using System.Collections.Generic;

namespace osuTools.Collections
{
    public class CloneableObservableList<T>:ObservableList<T> where T:ICloneable
    {
        public CloneableObservableList(IEnumerable<T> collection):base(collection)
        {
        }

        public CloneableObservableList()
        {
        }
        public object Clone()
        {
            CloneableList<T> cloneableList = new CloneableList<T>();
            ForEach(item => cloneableList.Add((T)item?.Clone()));
            return cloneableList;
        }
    }
}
