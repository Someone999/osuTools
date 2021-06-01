using osuTools.Beatmaps.HitObject;
using osuTools.Exceptions;
using osuTools.StoryBoard.Enums;
using osuTools.StoryBoard.Interfaces;
using osuTools.StoryBoard.Tools;

namespace osuTools.StoryBoard.Objects
{
    /// <summary>
    ///     StoryBoard中，可移动的图片
    /// </summary>
    public class Sprite : IStoryBoardImage
    {
        /// <summary>
        ///     StoryBoard的初步位置
        /// </summary>
        public StoryBoardOrigin Origin { get; set; }

        /// <summary>
        ///     图层
        /// </summary>
        public StoryBoardLayer Layer { get; set; }

        /// <summary>
        ///     相对于Origin的位置
        /// </summary>
        public OsuPixel Position { get; set; }

        /// <summary>
        ///     资源类型
        /// </summary>
        public StoryBoardResourceType ResourceType { get; } = StoryBoardResourceType.Sprite;

        /// <summary>
        ///     数据的特征字符
        /// </summary>
        public string DataIdentifier { get; } = "Sprite";

        /// <summary>
        ///     资源的相对路径
        /// </summary>
        public string Path { get; set; } = "";

        /// <summary>
        ///     相对于开始时间的播放时间
        /// </summary>
        public int Offset { get; set; } = -1;

        /// <summary>
        ///     数据的预计项数
        /// </summary>
        public int ExcpectLength { get; set; } = 6;

        /// <summary>
        ///     使用字符串构造一个Sprite对象
        /// </summary>
        /// <param name="dataline"></param>
        public void Parse(string dataline)
        {
            if (!dataline.StartsWith("Sprite,")) throw new FailToParseException("该行的数据不适用。");

            var data = dataline.Split(',');
            var sprite = data[0];
            var suc = int.TryParse(data[1], out var layer);
            if (!suc)
            {
                Layer = StoryBoardTools.GetLayerByString(data[1]);
            }
            else
            {
                Layer = (StoryBoardLayer) layer;
            }

            suc = int.TryParse(data[2], out var origin);
            if (!suc)
                Origin = StoryBoardTools.GetOriginByString(data[2]);
            else
                Origin = (StoryBoardOrigin) origin;
            Path = data[3].Trim('\"');
            Position = new OsuPixel(double.Parse(data[4]), double.Parse(data[5]));
        }

        /// <summary>
        ///     描述一个Sprite对象的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Type:{ResourceType} File:{Path} Layer:{Layer} Offset:{Offset} x:{Position.x} y={Position.y}";
        }
    }
}