using osuTools.Skins.Interfaces;

namespace osuTools.Skins.Game
{
    /// <summary>
    /// 一般的皮肤声音文件
    /// </summary>
    public class GeneralSkinSound : ISkinSound
    {
        /// <summary>
        /// 使用文件名和全路径初始化GeneralSkinSound
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullPath"></param>
        public GeneralSkinSound(string fileName, string fullPath)
        {
            FileName = fileName;
            FullPath = fullPath;
        }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; internal set; }
        /// <summary>
        /// 全路径
        /// </summary>
        public string FullPath { get; internal set; }
    }
}