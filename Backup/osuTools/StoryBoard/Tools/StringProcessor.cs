namespace osuTools.StoryBoard.Tools
{
    /// <summary>
    /// 初步处理StoryBoard命令的类
    /// </summary>
    public class StringProcessor
    {
        /// <summary>
        /// 原始字符串
        /// </summary>
        public string OrignalString { get; }
        /// <summary>
        /// 处理后的字符串
        /// </summary>
        public string ProcessedString { get; private set; }
        /// <summary>
        /// 命令前空格的数量(命令的层级)
        /// </summary>
        public int SpaceNum { get; private set; }
        /// <summary>
        /// 处理命令。命令会被赋值给ProcessedString和返回。处理时不会改变原字符串。
        /// </summary>
        /// <returns>处理后的命令</returns>
        public virtual string Process()
        {
            string x = "";
            x += OrignalString;
            while (x.StartsWith(" "))
            {
                x = x.Remove(0, 1);
                SpaceNum++;
            }
            ProcessedString = x;
            return ProcessedString;
        }
        /// <summary>
        /// 使用原始字符串初始化StringProcessor
        /// </summary>
        /// <param name="oriStr">原始字符串</param>
        public StringProcessor(string oriStr)
        {
            OrignalString += oriStr;
        }
    }
}