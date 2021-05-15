using osuTools.Beatmaps.HitObject;
using osuTools.Exceptions;

namespace osuTools.StoryBoard
{
    /// <summary>
    /// 表示一个动画
    /// </summary>
    public class Animation : IStoryBoardAnimation
    {
        /// <inheritdoc />
        public StoryBoardOrigin Origin { get; set; }
        /// <inheritdoc />
        public StoryBoardLayer Layer { get; set; }
        /// <inheritdoc />
        public OsuPixel Position { get; set; }
        /// <inheritdoc />
        public StoryBoardResourceType ResourceType { get; } = StoryBoardResourceType.Animation;
        /// <inheritdoc />
        public string DataIdentifier { get; } = "Animation";
        /// <inheritdoc />
        public string Path { get; set; } = "";
        /// <inheritdoc />
        public int Offset { get; set; } = -1;
        /// <inheritdoc />
        public int ExcpectLength { get; set; } = 9;
        /// <inheritdoc />
        public double FrameCount { get; set; }
        /// <inheritdoc />
        public double FrameDelay { get; set; }
        /// <inheritdoc />
        public StoryBoardAnimationLoopType LoopType { get; set; }
        /// <inheritdoc />
        public void Parse(string dataline)
        {
            if (!dataline.StartsWith("Animation,")) throw new FailToParseException("该行的数据不适用。");

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
            FrameCount = double.Parse(data[6]);
            FrameDelay = double.Parse(data[7]);
            suc = int.TryParse(data[8], out var loopType);
            if (!suc)
                LoopType = StoryBoardTools.GetLoopTypeByString(data[8]);
            else
                LoopType = (StoryBoardAnimationLoopType) loopType;
        }

        public override string ToString()
        {
            return
                $"Type:{ResourceType} File:{Path} Layer:{Layer} Offset:{Offset} x:{Position.x} y={Position.y} FrameCount:{FrameCount} LoopType:{LoopType}";
        }
    }
}