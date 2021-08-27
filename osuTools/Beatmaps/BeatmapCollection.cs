using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using osuTools.Exceptions;
using osuTools.Game.Modes;
using osuTools.GameInfo;
using osuTools.OsuDB;

namespace osuTools.Beatmaps
{
    namespace Beatmaps
    {
        /// <summary>
        ///     谱面的集合
        /// </summary>
        public class BeatmapCollection
        {
            /// <summary>
            ///     指定搜索结果是否应该包含搜索条件指定的谱面
            /// </summary>
            public enum BeatmapFindOption
            {
                /// <summary>
                ///     应包含
                /// </summary>
                Contains,

                /// <summary>
                ///     不应包含
                /// </summary>
                NotContains
            }

            /// <summary>
            ///     谱面添加选项
            /// </summary>
            public enum BeatmapSearchOption
            {
                /// <summary>
                ///     添加所有谱面
                /// </summary>
                AllBeatmaps = 0,

                /// <summary>
                ///     只添加搜索到的文件夹中的第一个谱面
                /// </summary>
                OnlyTheFirstBeatmap
            }

            private readonly List<Beatmap> _beatmaps = new List<Beatmap>();
            private readonly List<string> _sinfo = new List<string>();

            /// <summary>
            ///     将<see cref="OsuDB.OsuBeatmapCollection" />的信息转移到BeatmapCollection中
            /// </summary>
            /// <param name="c"></param>
            public BeatmapCollection(OsuBeatmapCollection c)
            {
                foreach (var beatmap in c)
                    _beatmaps.Add(new Beatmap(beatmap));
            }

            /// <summary>
            ///     创建一个空的BeatmapCollection
            /// </summary>
            public BeatmapCollection()
            {
                _beatmaps = new List<Beatmap>();
            }

            /// <summary>
            ///     存储的谱面
            /// </summary>
            public IReadOnlyList<Beatmap> Beatmaps
            {
                get
                {
                    _beatmaps.Sort(SortFunc);
                    return _beatmaps;
                }
            }

            /// <summary>
            ///     获取谱面的简易信息
            /// </summary>
            public IReadOnlyList<string> SongInfo => _sinfo.AsReadOnly();

            /// <summary>
            ///     使用整数索引从列表获取Beatmap
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            public Beatmap this[int x] => _beatmaps[x];

            /// <summary>
            ///     将谱面列表的信息保存到文件
            /// </summary>
            /// <param name="fileName"></param>
            public void Save(string fileName = ".\\beatmaplist\\list.txt")
            {
                var dirsplit = fileName.Split('\\');
                var dir = fileName.Replace(dirsplit.Last(), "");
                var info = new string[_beatmaps.Count];
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                for (var i = 0; i < _beatmaps.Count; i++) info[i] = $"{_beatmaps[i].BeatmapId}?{_beatmaps[i].FullPath}";
                File.WriteAllLines(fileName, info);
            }

            /// <summary>
            ///     从文件读取信息
            /// </summary>
            /// <param name="fileName"></param>
            /// <returns></returns>
            public static BeatmapCollection ReadFromFile(string fileName = ".\\beatmaplist\\list.txt")
            {
                var oinfo = new OsuInfo();
                var c = new BeatmapCollection();
                if (!File.Exists(fileName))
                {
                    var names = fileName.Split('\\');
                    var name = names[names.Length - 1];
                    Directory.CreateDirectory(fileName.Replace(name, ""));
                    File.Create(fileName).Close();
                }

                var info = File.ReadAllLines(fileName);
                if (info.Length < 1)
                {
                    MessageBox.Show("文件中不包含任何谱面信息，将重新搜索。");
                    c = GetAllBeatmaps(oinfo.BeatmapDirectory, BeatmapSearchOption.AllBeatmaps);
                }

                if (c._beatmaps.Count == 0)
                    foreach (var beatmap in info)
                    {
                        var beatmapdir = beatmap.Split('?')[1];
                        if (!File.Exists(beatmapdir))
                        {
                            MessageBox.Show($"找不到谱面\"{beatmapdir}\"，即将重新寻找谱面。");
                            return GetAllBeatmaps(oinfo.BeatmapDirectory, BeatmapSearchOption.AllBeatmaps);
                        }

                        var tmp = new Beatmap(beatmapdir);
                        if (!tmp.NotValid)
                            c._beatmaps.Add(tmp);
                    }

                return c;
            }

            private bool NotNull(object b)
            {
                return b != null;
            }

            /* public BeatmapCollection FindEx(string artist = null, string title = null, string creator = null, string version = null, string tag = null, string source = null)
             {
                 BeatmapCollection result = new BeatmapCollection();

                 return result;
             }*/
            /// <summary>
            ///     使用关键词搜索谱面，可指定包括与不包括
            /// </summary>
            /// <param name="keyWord"></param>
            /// <param name="option"></param>
            /// <returns></returns>
            public BeatmapCollection Find(string keyWord, BeatmapFindOption option = BeatmapFindOption.Contains)
            {
                var b = new BeatmapCollection();
                var keyword = keyWord.ToUpper();
                foreach (var beatmap in _beatmaps)
                {
                    var allinfo = beatmap.ToString().ToUpper() + " " + beatmap.Source.ToUpper() + " " +
                                  beatmap.Tags.ToUpper() + " " + beatmap.Creator.ToUpper() + " " +
                                  beatmap.Maker.ToUpper();

                    if (option == BeatmapFindOption.Contains)
                    {
                        if (keyword.StartsWith("${") && keyword.EndsWith("}"))
                        {
                            var newkeyword = keyword.Trim('$', '}', '{');
                            if (beatmap.Title.ToUpper() == newkeyword || beatmap.TitleUnicode.ToUpper() == newkeyword ||
                                beatmap.Artist.ToUpper() == newkeyword ||
                                beatmap.ArtistUnicode.ToUpper() == newkeyword ||
                                beatmap.Maker.ToUpper() == newkeyword || beatmap.Creator.ToUpper() == newkeyword ||
                                beatmap.Tags.ToUpper() == newkeyword || beatmap.Source.ToUpper() == newkeyword ||
                                beatmap.Difficulty.ToUpper() == newkeyword)
                                if (!b.Contains(beatmap))
                                    b.Add(beatmap);
                        }
                        else if (allinfo.Contains(keyword))
                        {
                            b.Add(beatmap);
                        }
                    }

                    if (option == BeatmapFindOption.NotContains)
                    {
                        if (keyword.StartsWith("${") && keyword.EndsWith("}"))
                        {
                            var newkeyw = keyword.Trim('$', '}', '{');
                            if (beatmap.Title.ToUpper() != newkeyw && beatmap.TitleUnicode.ToUpper() != newkeyw &&
                                beatmap.Artist.ToUpper() != newkeyw && beatmap.ArtistUnicode.ToUpper() != newkeyw &&
                                beatmap.Maker.ToUpper() != newkeyw && beatmap.Creator.ToUpper() != newkeyw &&
                                beatmap.Tags.ToUpper() != newkeyw && beatmap.Source.ToUpper() != newkeyw &&
                                beatmap.Difficulty.ToUpper() != newkeyw)
                                if (!b.Contains(beatmap))
                                    b.Add(beatmap);
                        }
                        else if (!allinfo.Contains(keyword))
                        {
                            b.Add(beatmap);
                        }
                    }
                }

                if (b.Beatmaps.Count == 0) throw new BeatmapNotFoundException("找不到指定的谱面");
                return b;
            }

            /// <summary>
            ///     使用BeatmapID搜索谱面
            /// </summary>
            /// <param name="beatmapId"></param>
            /// <returns></returns>
            public Beatmap Find(int beatmapId)
            {
                if (beatmapId != -1)
                    foreach (var beatmap in _beatmaps)
                        if (beatmap.BeatmapId == beatmapId)
                            return beatmap;
                return null;
            }

            private int SortFunc(Beatmap a, Beatmap b)
            {
                var ret = String.Compare(a.Title, b.Title, StringComparison.OrdinalIgnoreCase);
                return ret > 0 ? 1 : ret == 0 ? 0 : -1;
            }

            internal void Add(Beatmap b)
            {
                _beatmaps.Add(b);
                _sinfo.Add(b.ToString());
            }

            /// <summary>
            ///     使用MD5在数据库中搜索
            /// </summary>
            /// <param name="md5"></param>
            /// <returns></returns>
            public Beatmap FindByMd5(string md5)
            {
                foreach (var beatmap in _beatmaps)
                    if (beatmap.Md5 == md5)
                        return beatmap;
                throw new BeatmapNotFoundException($"找不到MD5为{md5}的谱面。");
            }

            /// <summary>
            ///     使用指定的模式搜索谱面，可指定包含与不包含
            /// </summary>
            /// <param name="mode"></param>
            /// <param name="option"></param>
            /// <returns></returns>
            public BeatmapCollection Find(OsuGameMode mode, BeatmapFindOption option = BeatmapFindOption.Contains)
            {
                var bc = new BeatmapCollection();
                foreach (var b in _beatmaps)
                {
                    if (option == BeatmapFindOption.Contains)
                        if (b.Mode == mode)
                            if (!bc.Contains(b))
                                bc.Add(b);
                    if (option == BeatmapFindOption.NotContains)
                        if (b.Mode != mode)
                            if (!bc.Contains(b))
                                bc.Add(b);
                }

                return bc;
            }

            internal void Remove(Beatmap b)
            {
                _beatmaps.Remove(b);
                _sinfo.Add(b.ToString());
            }

            /// <summary>
            ///     列表中是否包含指定谱面
            /// </summary>
            /// <param name="b"></param>
            /// <returns></returns>
            public bool Contains(Beatmap b)
            {
                return _beatmaps.Contains(b);
            }

            /// <summary>
            ///     返回循环访问的枚举数
            /// </summary>
            /// <returns></returns>
            public IEnumerator<Beatmap> GetEnumerator()
            {
                return _beatmaps.GetEnumerator();
            }

            /// <summary>
            ///     在指定的文件夹中搜索谱面，可指定谱面添加选项(<see cref="BeatmapSearchOption" />)，是否保存到文件与要保存到的文件的文件路径
            /// </summary>
            /// <param name="beatmapdir"></param>
            /// <param name="option"></param>
            /// <param name="saveResultToFile"></param>
            /// <param name="saveFilePath"></param>
            /// <returns></returns>
            public static BeatmapCollection GetAllBeatmaps(string beatmapdir, BeatmapSearchOption option,
                bool saveResultToFile = true, string saveFilePath = ".\\beatmaplist\\list.txt")
            {
                var bc = new BeatmapCollection();
                if (Directory.Exists(beatmapdir))
                {
                    if (option == BeatmapSearchOption.AllBeatmaps)
                    {
                        var dirs = Directory.GetFiles(beatmapdir, "*.osu", SearchOption.AllDirectories);
                        foreach (var osufile in dirs)
                        {
                            var b = new Beatmap(osufile);
                            bc.Add(b);
                        }

                        if (saveResultToFile)
                            bc.Save(saveFilePath);
                        return bc;
                    }

                    if (option == BeatmapSearchOption.OnlyTheFirstBeatmap)
                    {
                        var dirs = Directory.GetDirectories(beatmapdir, "*", SearchOption.AllDirectories);

                        foreach (var dir in dirs)
                        {
                            var em = Directory.GetFiles(dir + '\\', "*.osu", SearchOption.AllDirectories);
                            {
                                if (em.Length == 0)
                                {
                                    //throw new osuToolsException.NoBeatmapInFolder("指定的文件夹里不包含谱面。", dir);
                                }

                                bc.Add(new Beatmap(em.First()));
                            }
                        }
                    }
                    else
                    {
                        if (beatmapdir == null) throw new NullReferenceException();
                        throw new DirectoryNotFoundException();
                    }

                    if (bc.Beatmaps.Count == 0) return bc;
                    if (saveResultToFile)
                        bc.Save(saveFilePath);
                    return bc;
                }

                return new BeatmapCollection();
            }
        }
    }
}