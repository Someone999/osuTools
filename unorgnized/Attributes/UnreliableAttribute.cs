using System;

namespace osuTools.Attributes
{
    /// <summary>
    ///     说明使用被该属性标记的元素可能不会得到正确的结果
    /// </summary>
    public class UnreliableAttribute : Attribute
    {
        /// <summary>
        ///     使用指定的原因初始化一个UnreliableAttribute
        /// </summary>
        /// <param name="reason">原因</param>
        public UnreliableAttribute(string reason)
        {
            Reason = reason;
        }

        /// <summary>
        ///     原因
        /// </summary>
        public string Reason { get; set; }
    }
}