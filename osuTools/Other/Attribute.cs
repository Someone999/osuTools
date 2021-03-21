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

    /// <summary>
    ///     说明被标记的元素会出现在可用变量的列表中
    /// </summary>
    public class AvailableVariableAttribute : Attribute
    {
        /// <summary>
        ///     使用变量名和多语言的标签名初始化一个AvailableVariableAttribute对象
        /// </summary>
        /// <param name="varname">变量名</param>
        /// <param name="languageElementName">语言标签名</param>
        public AvailableVariableAttribute(string varname, string languageElementName)
        {
            VariableName = varname;
            LanguageElementName = languageElementName;
        }

        /// <summary>
        ///     变量名
        /// </summary>
        public string VariableName { get; internal set; } = "";

        /// <summary>
        ///     描述
        /// </summary>
        public string LanguageElementName { get; internal set; } = "";
    }

    /// <summary>
    ///     用于WorkingInProgressAttribute的枚举，用于表示开发进程
    /// </summary>
    public enum DevelopmentStage
    {
        /// <summary>
        ///     开发刚刚开始
        /// </summary>
        AtStart,

        /// <summary>
        ///     开发中
        /// </summary>
        Developing,

        /// <summary>
        ///     开发因非技术原因暂停
        /// </summary>
        Breaked,

        /// <summary>
        ///     开发因技术原因暂停
        /// </summary>
        Stuck,

        /// <summary>
        ///     查错期
        /// </summary>
        TroubleShooting,

        /// <summary>
        ///     调试期
        /// </summary>
        Debug
    }

    /// <summary>
    ///     表示被标记的元素还处于开发期
    /// </summary>
    public class WorkingInProgressAttribute : Attribute
    {
        /// <summary>
        ///     使用<see cref="DevelopmentStage" />初始化一个WorkingInProgressAttribute
        /// </summary>
        /// <param name="stage">开发阶段</param>
        /// <param name="time">标记时间</param>
        public WorkingInProgressAttribute(DevelopmentStage stage, string time)
        {
            Stage = stage;
            MarkAt = time;
        }

        /// <summary>
        ///     开发阶段
        /// </summary>
        public DevelopmentStage Stage { get; }

        /// <summary>
        ///     标记的时间
        /// </summary>
        public string MarkAt { get; }
    }
}