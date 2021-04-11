using osuTools.Game.Mods;

namespace osuTools.Exceptions
{
    /// <summary>
    ///     尝试向列表中添加列表中已存在的Mod时引发的异常
    /// </summary>
    public class ModExsitedException : osuToolsExceptionBase
    {
        /// <summary>
        ///     使用指定的信息初始化一个ModExsitedException
        /// </summary>
        /// <param name="msg"></param>
        public ModExsitedException(string msg = "要添加的Mod已经在列表中。") : base(msg)
        {
        }

        /// <summary>
        ///     使用已存在的Mod初始化一个ModExsitedException
        /// </summary>
        /// <param name="existedMod"></param>
        public ModExsitedException(Mod existedMod) : base($"Mod\"{existedMod.Name}\"已经在列表中。")
        {
        }
    }
}