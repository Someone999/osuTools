using System;
using System.Collections.Generic;
using System.Linq;

namespace osuTools.Beatmaps.HitObject.Tools
{
    /// <summary>
    /// 将整数转换为指定枚举列表的类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IntToEnumListConverter<T> where T: struct
    {
        /// <summary>
        /// 将整数转换成枚举列表
        /// </summary>
        /// <param name="val"></param>
        /// <param name="maybeBestVal"></param>
        /// <returns></returns>
        public abstract List<T> Convert(int val,out T? maybeBestVal);
        /// <summary>
        /// 将整数转换成颠倒后的二进制数
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToReversedBinary(int val) =>
            new string(System.Convert.ToString(val, 2).Reverse().ToArray());
        /// <summary>
        /// 转换失败时的默认值
        /// </summary>
        public abstract T DefaultValue { get; }
    }
}