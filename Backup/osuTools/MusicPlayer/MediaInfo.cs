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
        /// <summary>
        /// 使用全路径，文件名和附加信息创建一个MediaInfo，后两个参数可选
        /// </summary>
        /// <param name="path">全路径</param>
        /// <param name="showName">可选，默认为去掉扩展名的文件名</param>
        /// <param name="additionalInfo">可选，默认为null</param>
        public MediaInfo(string path,string showName = "",object additionalInfo = null)
        {
            Path = path;
            ShowName = string.IsNullOrEmpty(showName) ? FileName : showName;
            FileName = System.IO.Path.GetFileName(path);
            AdditionalInfoObject = additionalInfo;
        }
        ///<inheritdoc/>
        public override string ToString()
        {
            return ShowName;
        }
    }
}
