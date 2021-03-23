using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// 一个存储ICloneable对象的列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CloneableList<T>:List<T>,ICloneable where T:ICloneable
    {
        /// <summary>
        /// 克隆（深复制）列表
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            CloneableList<T> cloneableList = new CloneableList<T>();
            ForEach(item=>cloneableList.Add((T)item?.Clone()));
            return cloneableList;
        }
    }
}
