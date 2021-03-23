using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuTools.PerformanceCalculator.Catch
{
    public class CloneableList<T>:List<T>,ICloneable where T:ICloneable
    {
        public object Clone()
        {
            CloneableList<T> cloneableList = new CloneableList<T>();
            ForEach(item=>cloneableList.Add((T)item?.Clone()));
            return cloneableList;
        }
    }
}
