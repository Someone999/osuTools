namespace osuTools.MusicPlayer
{
    /// <summary>
    /// 媒体信息
    /// </summary>
    public class MediaInfo
    {
        /// <summary>
        /// 全路径
        /// </summary>
        public string Path { get; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string ShowName { get; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public object AdditionalInfoObject { get; }
        public MediaInfo(string path,string showName = "",object additionalInfo = null)
        {
            Path = path;
            ShowName = string.IsNullOrEmpty(showName) ? FileName : showName;
            FileName = System.IO.Path.GetFileName(path);
            AdditionalInfoObject = additionalInfo;
        }
        public override string ToString()
        {
            return ShowName;
        }
    }
}
