using System.Collections.Generic;
using System.IO;
using System.Linq;
using osuTools.StoryBoard.Commands.Interface;
using osuTools.StoryBoard.Interfaces;
using osuTools.StoryBoard.Objects;
using osuTools.StoryBoard.Tools;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        /// <summary>
        ///     获取指定类型的StoryBoard的命令
        /// </summary>
        /// <typeparam name="T">要获取的命令的资源类型</typeparam>
        /// <returns>包含指定资源信息的列表</returns>
        public List<T> GetStoryBoardResources<T>() where T : IStoryBoardResource, new()
        {
            GetStoryBoardResources();
            var resources = new List<T>();
            foreach (var resource in _sbResources)
            {
                if(resource.GetType() == typeof(T))
                    resources.Add((T)resource);
            }
            return resources;
        }

        private List<IStoryBoardResource> _sbResources;
        /// <summary>
        /// StoryBoard中所有的资源
        /// </summary>
        public List<IStoryBoardResource> StoryBoardResources
        {
            get => _sbResources ?? GetStoryBoardResources();
            set => _sbResources = value;
        }
        List<IStoryBoardResource> GetStoryBoardResources()
        {
            if (!(_sbResources is null))
                return _sbResources;
            if (string.IsNullOrEmpty(FullPath))
                return new List<IStoryBoardResource>();
            string fileName = FileName.Substring(0, FileName.Length - 4);
            var dirs = Directory.GetFiles($"{FullPath.Replace(FileName, "")}\\", $"{fileName + ".osb"}", SearchOption.AllDirectories);
            List<string> map = new List<string>();
            if(dirs.Length > 0)
                map.AddRange(File.ReadAllLines(dirs.Length > 0 ? dirs[0] : FullPath));
            map.AddRange(File.ReadAllLines(FullPath));
            var resources = new List<IStoryBoardResource>();
            foreach (var line in map)
            {
                var parts = line.Split(',');
                IStoryBoardResource resource;
                if (parts[0] == "Sprite")
                {
                    resource = new Sprite();
                    resource.Parse(line);
                    resources.Add(resource);
                }

                if (parts[0] == "Animation")
                {
                    resource = new Animation();
                    resource.Parse(line);
                    resources.Add(resource);
                }

                if (parts[0] == "Sample")
                {
                    resource = new Audio();
                    resource.Parse(line);
                    resources.Add(resource);
                }
            }

            return _sbResources = resources;
        }

        private List<IStoryBoardCommand> _sbCommands;
        /// <summary>
        /// 所有可用的StoryBoard命令，如果命令分散存在于谱面文件和osb文件，可能会不完整
        /// </summary>
        public List<IStoryBoardCommand> StoryBoardCommands
        {
            get => _sbCommands ?? GetStoryBoardCommands();
            set => _sbCommands = value;
        }
        List<IStoryBoardCommand> GetStoryBoardCommands()
        {
            var commandList = new List<IStoryBoardCommand>();
            if (string.IsNullOrEmpty(FullPath))
                return new List<IStoryBoardCommand>();
            if (!(_sbCommands is null))
                return _sbCommands;
            string fileName = FileName.Substring(0, FileName.Length - 4);
            var dirs = Directory.GetFiles($"{FullPath.Replace(FileName, "")}\\", $"{fileName + ".osb"}", SearchOption.AllDirectories);
            
            StoryBoardCommandParser parser = new StoryBoardCommandParser(FullPath);
            if (dirs.Length == 1)
            {
                StoryBoardCommandParser beatmapStoryBoardCommandParser = new StoryBoardCommandParser(dirs[0]);
                commandList.AddRange(beatmapStoryBoardCommandParser.Parse());
            }
            commandList.AddRange(parser.Parse());
            return _sbCommands = commandList;
        }
    }
}