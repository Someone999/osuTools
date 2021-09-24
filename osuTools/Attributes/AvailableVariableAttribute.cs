using System;

namespace osuTools.Attributes
{

    /// <summary>
    ///     说明被标记的元素会出现在可用变量的列表中
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AvailableVariableAttribute : Attribute
    {
        /// <summary>
        ///     使用变量名和多语言的标签名初始化一个AvailableVariableAttribute对象
        /// </summary>
        /// <param name="varname">变量名</param>
        /// <param name="languageElementName">语言标签名</param>
        /// <param name="alias">变量的别名</param>
        public AvailableVariableAttribute(string varname, string languageElementName,string[] alias = null)
        {
            VariableName = varname;
            LanguageElementName = languageElementName;
            Alias = alias;
        }

        /// <summary>
        ///     变量名
        /// </summary>
        public string VariableName { get; internal set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string LanguageElementName { get; internal set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string[] Alias { get; internal set; }

        /// <summary>
        /// 是否有别名
        /// </summary>
        public bool HasAlias => !(Alias is null) && Alias.Length > 0;
    }
}