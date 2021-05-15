using osuTools.Exceptions;

namespace osuTools.StoryBoard
{
    /// <summary>
    ///     包含一个要在StoryBoard播放音频的参数
    /// </summary>
    public class Audio : IStoryBoardResource
    {
        /// <summary>
        ///     使用确定的参数构造一个StoryBoardAudioResource的对象
        /// </summary>
        /// <param name="time">与开始位置的间隔时间</param>
        /// <param name="layer">StoryBoard的图层</param>
        /// <param name="fileName">文件名</param>
        /// <param name="volum">音量，值为百分比</param>
        public Audio(int time, StoryBoardLayer layer, string fileName, int volum)
        {
            Path = fileName;
            Offset = time;
            Layer = layer;
            Volume = volum;
        }

        /// <summary>
        ///     创建一个空白的StoryBoardAudioResource对象
        /// </summary>
        public Audio()
        {
        }

        /// <summary>
        ///     StoryBoard的图层
        /// </summary>
        public StoryBoardLayer Layer { get; set; } = StoryBoardLayer.None;

        /// <summary>
        ///     音量，以百分比为单位
        /// </summary>
        public int Volume { get; set; } = -1;
        /// <inheritdoc />
        public int ExcpectLength { get; } = 5;
        /// <inheritdoc />
        public string DataIdentifier { get; } = "Sample";
        /// <inheritdoc />
        public StoryBoardResourceType ResourceType { get; } = StoryBoardResourceType.Audio;

        /// <summary>
        ///     完整文件名
        /// </summary>
        public string Path { get; set; } = "";

        /// <summary>
        ///     音频开始播放的时间
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        ///     使用格式正确的字符串填充一个StoryBoardAudioResource对象
        /// </summary>
        /// <param name="dataline">包含数据的字符串</param>
        public void Parse(string dataline)
        {
            if (!dataline.StartsWith("Sample,")) throw new FailToParseException("该行的数据不适用。");

            var data = dataline.Split(',');
            var sample = data[0];
            Offset = int.Parse(data[1]);
            Layer = (StoryBoardLayer) int.Parse(data[2]);
            Path = data[3].Trim('\"');
            Volume = int.Parse(data[4]);
        }

        public override string ToString()
        {
            return $"Type:{ResourceType} File:{Path} SartTime:{Offset}";
        }
    }
}