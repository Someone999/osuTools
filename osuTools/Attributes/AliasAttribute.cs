using System;

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
        /// <summary>
        /// 使用别名初始化一个AliasAttribute
        /// </summary>
        /// <param name="alias"></param>
        public AliasAttribute(string alias)
        {
            Alias = alias;
        }
    }
}
