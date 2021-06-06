using osuTools.Beatmaps.HitObject;
using osuTools.Game.Modes;

namespace osuTools.Exceptions
{
    /// <summary>
    ///     当一个HitObject出现在了不该有此类型的HitObject的模式中时引发的异常
    /// </summary>
    public class IncorrectHitObjectException : osuToolsExceptionBase
    {
        /// <summary>
        ///     使用指定的信息初始化一个IncorrectHitObjectException
        /// </summary>
        /// <param name="msg"></param>
        public IncorrectHitObjectException(string msg) : base(msg)
        {
        }

        /// <summary>
        ///     使用游戏模式和打击物件的类型初始化一个IncorrectHitObjectException
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="hitObjectType"></param>
        public IncorrectHitObjectException(GameMode mode, HitObjectTypes hitObjectType) : base(
            $"模式{mode.ModeName}无法使用类型为\"{hitObjectType}\"的HitObject")
        {
        }

        /// <summary>
        ///     使用游戏模式，打击物件的类型以及指定的信息初始化一个IncorrectHitObjectException
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="hitObjectType"></param>
        /// <param name="msg"></param>
        public IncorrectHitObjectException(GameMode mode, HitObjectTypes hitObjectType, string msg) : base(
            $"模式{mode.ModeName}无法使用类型为\"{hitObjectType}\"的HitObject.\n 附加信息:" + msg)
        {
        }
    }
}