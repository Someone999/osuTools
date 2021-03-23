﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using osuTools.OsuDB;
using osuTools.osuToolsException;

namespace osuTools
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

            private readonly List<Beatmap> beatmaps = new List<Beatmap>();
            private readonly List<string> sinfo = new List<string>();

            /// <summary>
            ///     将<see cref="OsuDB.OsuBeatmapCollection" />的信息转移到BeatmapCollection中
            /// </summary>
            /// <param name="c"></param>
            public BeatmapCollection(OsuBeatmapCollection c)
            {
                foreach (var beatmap in c)
                    beatmaps.Add(new Beatmap(beatmap));
            }

            /// <summary>
            ///     创建一个空的BeatmapCollection
            /// </summary>
            public BeatmapCollection()
            {
                beatmaps = new List<Beatmap>();
            }

            /// <summary>
            ///     存储的谱面
            /// </summary>
            public IReadOnlyList<Beatmap> Beatmaps
            {
                get
                {
                    beatmaps.Sort(sortfun);
                    return beatmaps;
                }
            }

            /// <summary>
            ///     获取谱面的简易信息
            /// </summary>
            public IReadOnlyList<string> SongInfo => sinfo.AsReadOnly();

            /// <summary>
            ///     使用整数索引从列表获取Beatmap
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            public Beatmap this[int x] => beatmaps[x];

            /// <summary>
            ///     将谱面列表的信息保存到文件
            /// </summary>
            /// <param name="FileName"></param>
            public void Save(string FileName = ".\\beatmaplist\\list.txt")
            {
                var dirsplit = FileName.Split('\\');
                var dir = FileName.Replace(dirsplit.Last(), "");
                var info = new string[beatmaps.Count];
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                for (var i = 0; i < beatmaps.Count; i++) info[i] = $"{beatmaps[i].BeatmapID}?{beatmaps[i].FullPath}";
                File.WriteAllLines(FileName, info);
            }

            /// <summary>
            ///     从文件读取信息
            /// </summary>
            /// <param name="FileName"></param>
            /// <returns></returns>
            public static BeatmapCollection ReadFromFile(string FileName = ".\\beatmaplist\\list.txt")
            {
                var oinfo = new OsuInfo();
                var c = new BeatmapCollection();
                if (!File.Exists(FileName))
                {
                    var names = FileName.Split('\\');
                    var name = names[names.Length - 1];
                    Directory.CreateDirectory(FileName.Replace(name, ""));
                    File.Create(FileName).Close();
                }

                var info = File.ReadAllLines(FileName);
                if (info.Length < 1)
                {
                    MessageBox.Show("文件中不包含任何谱面信息，将重新搜索。");
                    c = GetAllBeatmaps(oinfo.BeatmapDirectory, BeatmapSearchOption.AllBeatmaps);
                }

                if (c.beatmaps.Count == 0)
                    foreach (var beatmap in info)
                    {
                        var beatmapdir = beatmap.Split('?')[1];
                        if (!File.Exists(beatmapdir))
                        {
                            MessageBox.Show($"找不到谱面\"{beatmapdir}\"，即将重新寻找谱面。");
                            return GetAllBeatmaps(oinfo.BeatmapDirectory, BeatmapSearchOption.AllBeatmaps);
                        }

                        var tmp = new Beatmap(beatmapdir);
                        if (!tmp.notv)
                            c.beatmaps.Add(tmp);
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
            /// <param name="KeyWord"></param>
            /// <param name="option"></param>
            /// <returns></returns>
            public BeatmapCollection Find(string KeyWord, BeatmapFindOption option = BeatmapFindOption.Contains)
            {
                var b = new BeatmapCollection();
                var keyword = KeyWord.ToUpper();
                foreach (var beatmap in beatmaps)
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
            /// <param name="BeatmapID"></param>
            /// <returns></returns>
            public Beatmap Find(int BeatmapID)
            {
                if (BeatmapID != -1)
                    foreach (var beatmap in beatmaps)
                        if (beatmap.BeatmapID == BeatmapID)
                            return beatmap;
                return null;
            }

            private int sortfun(Beatmap a, Beatmap b)
            {
                var ret = string.Compare(a.Title, b.Title, true);
                return ret > 0 ? 1 : ret == 0 ? 0 : -1;
            }

            internal void Add(Beatmap b)
            {
                beatmaps.Add(b);
                sinfo.Add(b.ToString());
            }

            /// <summary>
            ///     使用MD5在数据库中搜索
            /// </summary>
            /// <param name="md5"></param>
            /// <returns></returns>
            public Beatmap FindByMD5(string md5)
            {
                foreach (var beatmap in beatmaps)
                    if (beatmap.MD5 == md5)
                        return beatmap;
                throw new BeatmapNotFoundException($"找不到MD5为{md5}的谱面。");
            }

            /// <summary>
            ///     使用指定的模式搜索谱面，可指定包含与不包含
            /// </summary>
            /// <param name="Mode"></param>
            /// <param name="option"></param>
            /// <returns></returns>
            public BeatmapCollection Find(OsuGameMode Mode, BeatmapFindOption option = BeatmapFindOption.Contains)
            {
                var bc = new BeatmapCollection();
                foreach (var b in beatmaps)
                {
                    if (option == BeatmapFindOption.Contains)
                        if (b.Mode == Mode)
                            if (!bc.Contains(b))
                                bc.Add(b);
                    if (option == BeatmapFindOption.NotContains)
                        if (b.Mode != Mode)
                            if (!bc.Contains(b))
                                bc.Add(b);
                }

                return bc;
            }

            internal void Remove(Beatmap b)
            {
                beatmaps.Remove(b);
                sinfo.Add(b.ToString());
            }

            /// <summary>
            ///     列表中是否包含指定谱面
            /// </summary>
            /// <param name="b"></param>
            /// <returns></returns>
            public bool Contains(Beatmap b)
            {
                return beatmaps.Contains(b);
            }

            /// <summary>
            ///     返回循环访问的枚举数
            /// </summary>
            /// <returns></returns>
            public IEnumerator<Beatmap> GetEnumerator()
            {
                return beatmaps.GetEnumerator();
            }

            /// <summary>
            ///     在指定的文件夹中搜索谱面，可指定谱面添加选项(<see cref="BeatmapSearchOption" />)，是否保存到文件与要保存到的文件的文件路径
            /// </summary>
            /// <param name="beatmapdir"></param>
            /// <param name="option"></param>
            /// <param name="SaveResultToFile"></param>
            /// <param name="Dir"></param>
            /// <returns></returns>
            public static BeatmapCollection GetAllBeatmaps(string beatmapdir, BeatmapSearchOption option,
                bool SaveResultToFile = true, string Dir = ".\\beatmaplist\\list.txt")
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

                        if (SaveResultToFile)
                            bc.Save(Dir);
                        return bc;
                    }

                    if (option == BeatmapSearchOption.OnlyTheFirstBeatmap)
                    {
                        var dirs = Directory.GetDirectories(beatmapdir, "*", SearchOption.AllDirectories);

                        foreach (var dir in dirs)
                        {
                            var em = Directory.GetFiles(dir + '\\', "*.osu", SearchOption.AllDirectories);
                            {
                                if (em.Count() == 0)
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

                    if (bc.Beatmaps.Count == 0)
                        throw new NoBeatmapInFolderException("未能在指定的文件夹及其子文件夹中找到任何谱面。", beatmapdir);
                    if (SaveResultToFile)
                        bc.Save(Dir);
                    return bc;
                }

                return new BeatmapCollection();
            }
        }
    }
}