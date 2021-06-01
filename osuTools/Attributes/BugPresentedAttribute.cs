using System;

namespace osuTools.Attributes
{
    /// <summary>
    ///     说明被标记的元素中存在已知的Bug
    /// </summary>
    public class BugPresentedAttribute : Attribute
    {
        /// <summary>
        ///     使用Bug的描述初始化一个BugPresentedAttribute
        /// </summary>
        /// <param name="description">bug的描述</param>
        public BugPresentedAttribute(string description)
        {
            Description = description;
        }

        /// <summary>
        ///     Bug的描述
        /// </summary>
        public string Description { get; set; }
    }
}