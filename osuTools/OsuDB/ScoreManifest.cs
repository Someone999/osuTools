namespace osuTools.OsuDB
{
    /// <summary>
    ///     分数文件起始部位的数据
    /// </summary>
    public class ScoreManifest
    {
        /// <summary>
        ///     使用游戏版本构造一个ScoreManifest
        /// </summary>
        /// <param name="ver"></param>
        public ScoreManifest(int ver)
        {
            Version = ver;
        }

        /// <summary>
        ///     游戏版本
        /// </summary>
        public int Version { get; internal set; }
    }
}