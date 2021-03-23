using System;
using osuTools.Beatmaps;
using osuTools.Game.Modes;
using osuTools.Game.Mods;

namespace osuTools
{
    namespace osuToolsException
    {
        /// <summary>
        ///     osuTools异常的基类
        /// </summary>
        public class osuToolsExceptionBase : Exception
        {
            /// <summary>
            ///     使用指定的信息初始化一个osuToolsExceptionBase
            /// </summary>
            /// <param name="msg">信息</param>
            public osuToolsExceptionBase(string msg) : base(msg)
            {
            }

            /// <summary>
            ///     使用指定的信息和一个内部异常初始化一个osuToolsExceptionBase
            /// </summary>
            /// <param name="msg"></param>
            /// <param name="innerException"></param>
            public osuToolsExceptionBase(string msg, Exception innerException) : base(msg, innerException)
            {
            }
        }

        /// <summary>
        ///     当指定的文件不是谱面文件的时候引发的异常。
        /// </summary>
        public class InvalidBeatmapFileException : osuToolsExceptionBase
        {
            /// <summary>
            ///     使用指定的信息初始化一个InvalidBeatmapFileException
            /// </summary>
            /// <param name="msg">信息</param>
            public InvalidBeatmapFileException(string msg) : base(msg)
            {
            }

            /// <summary>
            ///     使用指定的信息和内部异常初始化一个InvalidBeatmapFileException
            /// </summary>
            /// <param name="msg">信息</param>
            /// <param name="innerException" />
            public InvalidBeatmapFileException(string msg, Exception innerException) : base(msg, innerException)
            {
            }
        }

        /// <summary>
        ///     osu!api查询失败时引发的异常。
        /// </summary>
        public class OnlineQueryFailedException : osuToolsExceptionBase
        {
            /// <summary>
            ///     使用指定的信息初始化一个OnlineQueryFailedException
            /// </summary>
            /// <param name="info">信息</param>
            public OnlineQueryFailedException(string info) : base(info)
            {
            }

            /// <summary>
            ///     使用指定的信息和内部异常初始化一个OnlineQueryFailedException
            /// </summary>
            /// <param name="msg">信息</param>
            /// <param name="innerException" />
            public OnlineQueryFailedException(string msg, Exception innerException) : base(msg, innerException)
            {
            }
        }

        /// <summary>
        ///     处理osu文件时出现错误引发的异常。
        /// </summary>
        public class FailToParseException : osuToolsExceptionBase
        {
            /// <summary>
            ///     使用指定的信息初始化一个FailToParseException
            /// </summary>
            /// <param name="message">信息</param>
            public FailToParseException(string message) : base(message)
            {
            }

            /// <summary>
            ///     使用指定的信息和内部异常初始化一个FailToParseException
            /// </summary>
            /// <param name="msg">信息</param>
            /// <param name="innerException" />
            public FailToParseException(string msg, Exception innerException) : base(msg, innerException)
            {
            }
        }

        /// <summary>
        ///     在指定的文件夹中找不到有效谱面时引发的异常。
        /// </summary>
        public class NoBeatmapInFolderException : osuToolsExceptionBase
        {
            /// <summary>
            ///     使用指定的信息与文件夹初始化一个NoBeatmapInFolderException
            /// </summary>
            /// <param name="message">信息</param>
            /// <param name="folder">文件夹</param>
            public NoBeatmapInFolderException(string message, string folder) : base(message)
            {
                Folder = folder;
            }

            /// <summary>
            ///     文件夹
            /// </summary>
            public string Folder { get; }
        }

        /// <summary>
        ///     找不到与指定条件匹配的谱面时引发的异常。
        /// </summary>
        public class BeatmapNotFoundException : osuToolsExceptionBase
        {
            /// <summary>
            ///     使用指定的信息初始化一个BeatmapNotFoundException
            /// </summary>
            /// <param name="message">信息</param>
            public BeatmapNotFoundException(string message) : base(message)
            {
            }
        }

        /// <summary>
        ///     在指定的文件夹中找不到回放时引发的异常。
        /// </summary>
        public class NoReplayInFolderException : osuToolsExceptionBase
        {
            /// <summary>
            ///     使用指定的信息与文件夹初始化一个NoReplayInFolderException
            /// </summary>
            /// <param name="message">信息</param>
            /// <param name="folder">文件夹</param>
            public NoReplayInFolderException(string message, string folder) : base(message)
            {
                Folder = folder;
            }

            /// <summary>
            ///     文件夹
            /// </summary>
            public string Folder { get; }
        }

        /// <summary>
        ///     找不到与指定条件匹配的回放时引发的异常。
        /// </summary>
        public class ReplayNotFoundException : osuToolsExceptionBase
        {
            /// <summary>
            ///     使用指定的信息初始化一个ReplayNotFoundException
            /// </summary>
            /// <param name="message">信息</param>
            public ReplayNotFoundException(string message) : base(message)
            {
            }
        }

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

        /// <summary>
        ///     插件中的任何一个依赖项或插件本身初始化失败时引发的异常
        /// </summary>
        public class InitializationFailedException : osuToolsExceptionBase
        {
            /// <summary>
            ///     使用指定的信息初始化一个InitializationFailedException
            /// </summary>
            /// <param name="msg">信息</param>
            public InitializationFailedException(string msg) : base(msg)
            {
            }

            /// <summary>
            ///     使用指定的信息和内部异常初始化一个InitializationFailedException
            /// </summary>
            /// <param name="msg">信息</param>
            /// <param name="innerException" />
            public InitializationFailedException(string msg, Exception innerException) : base(msg, innerException)
            {
            }
        }
    }
}