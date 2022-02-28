#define SYNC
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuTools.Tools
{
    /// <summary>
    /// 输出辅助类
    /// </summary>
    public static class OutputHelper
    {
#if SYNC
        /// <summary>
        /// 有Sync时的输出，使用<see cref="Sync.Tools.ISyncOutput.Write(string, bool, bool)"/>
        /// </summary>
        /// <param name="obj">要输出的对象或内容</param>
        /// <param name="newLine">是否另起一行</param>
        /// <param name="time">是否附加时间</param>
        public static void Output(object obj, bool newLine = true, bool time = true) => Sync.Tools.IO.CurrentIO.Write(obj.ToString(), newLine, time);
        /// <summary>
        /// 有Sync时的输出，使用<see cref="Sync.Tools.ISyncOutput.WriteColor(string,ConsoleColor, bool, bool)"/>
        /// </summary>
        /// <param name="obj">要输出的对象或内容</param>
        /// <param name="color">要输出字体的颜色</param>
        /// <param name="newLine">是否另起一行</param>
        /// <param name="time">是否附加时间</param>
        public static void OutputColor(object obj, ConsoleColor color, bool newLine = true, bool time = true) =>
            Sync.Tools.IO.CurrentIO.WriteColor(obj.ToString(), color, newLine, time);
#else
        /// <summary>
        /// 没有Sync时的输出，使用<see cref="Console.WriteLine(object)"/>
        /// </summary>
        /// <param name="obj">要输出的对象或内容</param>
        /// <param name="newLine">是否另起一行</param>
        /// <param name="time">是否附加时间</param>
        public static void Output(object obj, bool newLine = true, bool time = true)
        {
            string output = obj.ToString();
            if(time)
            {
                output = DateTime.Now.ToString("HH:mm:ss") + " " + output;
            }
            if(newLine)
            {
                output += "\n";
            }
            Console.Write(output);
        }
        /// <summary>
        /// 没有Sync时的输出，使用<see cref="Console.WriteLine(object)"/>
        /// <param name="obj">要输出的对象或内容</param>
        /// <param name="color">要输出字体的颜色</param>
        /// <param name="newLine">是否另起一行</param>
        /// <param name="time">是否附加时间</param>
        /// </summary>
        public static void OutputColor(object obj, ConsoleColor color, bool newLine = true, bool time = true)
        {
            Console.ForegroundColor = color;
            string output = obj.ToString();
            if (time)
            {
                output = DateTime.Now.ToString("HH:mm:ss") + " " + output;
            }
            if (newLine)
            {
                output += "\n";
            }
            Console.Write(output);
            Console.ForegroundColor = ConsoleColor.White;
        }
#endif
    }
}
