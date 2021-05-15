using System;

namespace osuTools.Exceptions
{
    /// <summary>
    ///     找不到皮肤文件时触发的异常
    /// </summary>
    public class SkinFileNotFoundException : Exception
    {
        /// <summary>
        ///     初始化一个SkinFileNotFoundException异常
        /// </summary>
        public SkinFileNotFoundException() : base("找不到文件。可能是该皮肤没有定义该元素或者该元素没有使用png格式的图片。")
        {
        }

        /// <summary>
        ///     使用指定的消息初始化一个SkinFileNotFoundException异常
        /// </summary>
        public SkinFileNotFoundException(string msg) : base(msg)
        {
        }
    }
}