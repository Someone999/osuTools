namespace osuTools.StoryBoard
{
    /// <summary>
    ///     表示StoryBoard可用的资源
    /// </summary>
    public interface IStoryBoardResource
    {
        /// <summary>
        ///     StoryBoard资源的种类
        /// </summary>
        StoryBoardResourceType ResourceType { get; }

        /// <summary>
        ///     StoryBoard资源的路径
        /// </summary>
        string Path { get; }

        /// <summary>
        ///     StoryBoard资源使用的时间点与开始时的偏移
        /// </summary>
        int Offset { get; }

        /// <summary>
        ///     数据的特征字符串
        /// </summary>
        string DataIdentifier { get; }

        /// <summary>
        ///     预计的数据的项数
        /// </summary>
        int ExcpectLength { get; }

        /// <summary>
        ///     将字符串解析为IStoryBoardResource
        /// </summary>
        /// <param name="data"></param>
        void Parse(string data);
    }
}