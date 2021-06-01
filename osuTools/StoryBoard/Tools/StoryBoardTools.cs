using osuTools.StoryBoard.Commands;
using osuTools.StoryBoard.Commands.Interface;
using osuTools.StoryBoard.Enums;

namespace osuTools.StoryBoard.Tools
{
    /// <summary>
    ///     StoryBoard的字符串转换工具
    /// </summary>
    public static class StoryBoardTools
    {
        /// <summary>
        ///     通过正确的字符串获取事件类型
        /// </summary>
        /// <param name="storyBoardEvent">事件的字符串</param>
        /// <returns>事件类型</returns>
        public static StoryBoardEvent GetEventByString(string storyBoardEvent)
        {
            if (storyBoardEvent.Length > 3)
                storyBoardEvent = storyBoardEvent.Split(',')[0];
            if (storyBoardEvent == "F") return StoryBoardEvent.Fade;
            if (storyBoardEvent == "M") return StoryBoardEvent.Move;
            if (storyBoardEvent == "MX") return StoryBoardEvent.MoveX;
            if (storyBoardEvent == "MY") return StoryBoardEvent.MoveY;
            if (storyBoardEvent == "S") return StoryBoardEvent.Scale;
            if (storyBoardEvent == "V") return StoryBoardEvent.VectorScale;
            if (storyBoardEvent == "R") return StoryBoardEvent.Rotate;
            if (storyBoardEvent == "C") return StoryBoardEvent.Color;
            if (storyBoardEvent == "L") return StoryBoardEvent.Loop;
            if (storyBoardEvent == "T") return StoryBoardEvent.Trigger;
            if (storyBoardEvent == "P") return StoryBoardEvent.Parameter;
            return StoryBoardEvent.None;
        }
        /// <summary>
        /// 通过正确的字符串获取事件的类对象
        /// </summary>
        /// <param name="storyBoardEvent">正确的字符串</param>
        /// <returns>事件对应的类的对象</returns>

        public static IStoryBoardSubCommand GetEventClassByString(string storyBoardEvent)
        {
            if (storyBoardEvent.Length > 3)
                storyBoardEvent = storyBoardEvent.Split(',')[0];
            if (storyBoardEvent == "F") return new Fade();
            if (storyBoardEvent == "M") return new Move();
            if (storyBoardEvent == "MX") return new MoveX();
            if (storyBoardEvent == "MY") return new MoveY();
            if (storyBoardEvent == "S") return new Scale();
            if (storyBoardEvent == "V") return new VectorScale();
            if (storyBoardEvent == "R") return new Rotate();
            if (storyBoardEvent == "C") return new Color();
            if (storyBoardEvent == "L") return new Loop();
            if (storyBoardEvent == "T") return new Trigger();
            if (storyBoardEvent == "P") return new Parameter();
            return null;
        }

        /// <summary>
        ///     通过字符串获取位置
        /// </summary>
        /// <param name="storyBoardOrigin"></param>
        /// <returns></returns>
        public static StoryBoardOrigin GetOriginByString(string storyBoardOrigin)
        {
            if (storyBoardOrigin == "BottomCentre") return StoryBoardOrigin.BottomCentre;
            if (storyBoardOrigin == "BottomLeft") return StoryBoardOrigin.BottomLeft;
            if (storyBoardOrigin == "BottomRight") return StoryBoardOrigin.BottomRight;
            if (storyBoardOrigin == "Centre") return StoryBoardOrigin.Centre;
            if (storyBoardOrigin == "CentreLeft") return StoryBoardOrigin.CentreLeft;
            if (storyBoardOrigin == "CentreRight") return StoryBoardOrigin.CentreRight;
            if (storyBoardOrigin == "Custom") return StoryBoardOrigin.Custom;
            if (storyBoardOrigin == "TopCentre") return StoryBoardOrigin.TopCentre;
            if (storyBoardOrigin == "TopLeft") return StoryBoardOrigin.TopLeft;
            if (storyBoardOrigin == "TopRight") return StoryBoardOrigin.TopRight;
            return StoryBoardOrigin.Unknown;
        }

        /// <summary>
        ///     通过字符串获取图层
        /// </summary>
        /// <param name="storyBoardLayer"></param>
        /// <returns></returns>
        public static StoryBoardLayer GetLayerByString(string storyBoardLayer)
        {
            if (storyBoardLayer == "Foreground") return StoryBoardLayer.Foreground;
            if (storyBoardLayer == "Background") return StoryBoardLayer.Background;
            if (storyBoardLayer == "Pass") return StoryBoardLayer.Pass;
            if (storyBoardLayer == "Fail") return StoryBoardLayer.Fail;
            return StoryBoardLayer.None;
        }

        /// <summary>
        ///     通过字符串获取循环类型
        /// </summary>
        /// <param name="loopType"></param>
        /// <returns></returns>
        public static StoryBoardAnimationLoopType GetLoopTypeByString(string loopType)
        {
            if (loopType == "LoopOnce") return StoryBoardAnimationLoopType.Once;
            if (loopType == "LoopForever") return StoryBoardAnimationLoopType.Forever;
            return StoryBoardAnimationLoopType.Unknow;
        }

        /// <summary>
        ///     通过字符串获取擦除类型
        /// </summary>
        /// <param name="easing"></param>
        /// <returns></returns>
        public static StoryBoardEasing GetStoryBoardEasingByString(string easing)
        {
            if (easing == "Liner") return StoryBoardEasing.Linear;
            if (easing == "Easing Out") return StoryBoardEasing.EasingOut;
            if (easing == "Easing In") return StoryBoardEasing.EasingIn;
            if (easing == "Quad Out") return StoryBoardEasing.QuadOut;
            if (easing == "Quad In") return StoryBoardEasing.QuadIn;
            if (easing == "Quad In/Out") return StoryBoardEasing.QuadInOut;
            if (easing == "Cubic Out") return StoryBoardEasing.CubicOut;
            if (easing == "Cubic In") return StoryBoardEasing.CubicIn;
            if (easing == "Quart Out") return StoryBoardEasing.QuartOut;
            if (easing == "Quart In") return StoryBoardEasing.QuartIn;
            if (easing == "Quart In/Out") return StoryBoardEasing.QuartInOut;
            if (easing == "Quint Out") return StoryBoardEasing.QuintOut;
            if (easing == "Quint In") return StoryBoardEasing.QuintIn;
            if (easing == "Quint In/Out") return StoryBoardEasing.QuintInOut;
            if (easing == "Sine Out") return StoryBoardEasing.SineOut;
            if (easing == "Sine In") return StoryBoardEasing.SineIn;
            if (easing == "Sine In/Out") return StoryBoardEasing.SineInOut;
            if (easing == "Expo Out") return StoryBoardEasing.ExpoOut;
            if (easing == "Expo In") return StoryBoardEasing.ExpoIn;
            if (easing == "Expo In/Out") return StoryBoardEasing.ExpoInOut;
            if (easing == "Circ Out") return StoryBoardEasing.CircOut;
            if (easing == "Circ In") return StoryBoardEasing.CircIn;
            if (easing == "Circ In/Out") return StoryBoardEasing.CircInOut;
            if (easing == "Elastic Out") return StoryBoardEasing.ElasticOut;
            if (easing == "Elastic In") return StoryBoardEasing.ElasticIn;
            if (easing == "ElasticHalf Out") return StoryBoardEasing.ElasticHalfOut;
            if (easing == "ElasticQuarter Out") return StoryBoardEasing.ElasticQuarterOut;
            if (easing == "Elastic In/Out") return StoryBoardEasing.ElasticInOut;
            if (easing == "Back Out") return StoryBoardEasing.BackOut;
            if (easing == "Back In") return StoryBoardEasing.BackIn;
            if (easing == "Back In/Out") return StoryBoardEasing.BackInOut;
            if (easing == "Bounce Out") return StoryBoardEasing.BounceOut;
            if (easing == "Bounce In") return StoryBoardEasing.BounceIn;
            if (easing == "Bounce In/Out") return StoryBoardEasing.BounceInOut;
            return StoryBoardEasing.Unknown;
        }
    }
}