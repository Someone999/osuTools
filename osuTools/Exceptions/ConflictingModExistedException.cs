using osuTools.Game.Mods;

namespace osuTools.Exceptions
{
    /// <summary>
    ///     列表中有任意Mod与要添加的Mod不兼容时引发的异常
    /// </summary>
    public class ConflictingModExistedException : osuToolsExceptionBase
    {
        /// <summary>
        ///     使用指定的信息初始化一个ConflictingModExistedException
        /// </summary>
        /// <param name="message"></param>
        public ConflictingModExistedException(string message = "已存在一个或多个与该Mod相冲突的Mod。") : base(message)
        {
        }

        /// <summary>
        ///     使用指定的Mod和即将引发冲突的要添加的Mod初始化一个ConflictingModExistedException
        /// </summary>
        /// <param name="exsitedMod"></param>
        /// <param name="toAdd"></param>
        public ConflictingModExistedException(Mod exsitedMod, Mod toAdd) : base(
            $"Mod\"{exsitedMod.Name}\"与Mod\"{toAdd.Name}\"不能共存。")
        {
        }
    }
}