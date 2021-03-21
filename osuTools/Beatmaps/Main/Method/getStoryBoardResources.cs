using System.Collections.Generic;
using System.IO;
using osuTools.StoryBoard;

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
            var dirs = Directory.GetFiles($"{FullPath.Replace(FileName, "")}\\", "*.osb", SearchOption.AllDirectories);
            var map = new string[1];
            if (dirs.Length > 0)
                map = File.ReadAllLines(dirs[0]);
            else
                map = File.ReadAllLines(FullPath);
            var resources = new List<T>();
            foreach (var str in map)
            {
                var obj = new T();
                var comasp = str.Split(',');
                if (comasp.Length == obj.ExcpectLength && comasp[0] == obj.DataIdentifier)
                {
                    obj.Parse(str);
                    resources.Add(obj);
                }
                else
                {
                }
            }

            return resources;
        }

        /// <summary>
        ///     获取谱面所有的StoryBoard命令
        /// </summary>
        /// <returns>包含</returns>
        public List<IStoryBoardResource> GetStoryBoardResources()
        {
            var dirs = Directory.GetFiles($"{FullPath.Replace(FileName, "")}\\", "*.osb", SearchOption.AllDirectories);
            var map = new string[1];
            if (dirs.Length > 0)
                map = File.ReadAllLines(dirs[0]);
            else
                map = File.ReadAllLines(FullPath);
            var resources = new List<IStoryBoardResource>();
            IStoryBoardResource resource = null;
            foreach (var line in map)
            {
                var parts = line.Split(',');
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

            return resources;
        }
    }
}