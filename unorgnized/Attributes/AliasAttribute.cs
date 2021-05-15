using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuTools.Attributes
{

    /// <summary>
    /// 表示被标记的属性有自定义的别名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = true)]
    public class AliasAttribute:Attribute
    {
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get;}
        public AliasAttribute(string alias)
        {
            Alias = alias;
        }
    }
}
