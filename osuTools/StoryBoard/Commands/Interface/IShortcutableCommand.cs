using System.Collections.Generic;

namespace osuTools.StoryBoard.Commands.Interface
{
    /// <summary>
    ///     表示一个可缩写的命令
    /// </summary>
    public interface IShortcutableCommand
    {
        /// <summary>
        ///     可缩写命令中包含的变换
        /// </summary>
        List<ITranslation> Translations { get; }
    }
}