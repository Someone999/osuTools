﻿using System.Collections.Generic;
using System.IO;
using osuTools.osuToolsException;

namespace osuTools
{
    namespace Replay
    {
        internal static class ex
        {
            public static bool Contains(this IReadOnlyList<OsuGameMod> mods, OsuGameMod mod)
            {
                foreach (var moda in mods)
                    if (mod == moda)
                        return true;
                return false;
            }
        }

        /// <summary>
        ///     录像集合
        /// </summary>
        public class ReplayCollection
        {
            /// <summary>
            ///     指定是哪个对象的MD5
            /// </summary>
            public enum MD5Type
            {
                /// <summary>
                ///     谱面MD5
                /// </summary>
                BeatmapMD5,

                /// <summary>
                ///     录像MD5
                /// </summary>
                ReplayMD5
            }

            private readonly List<Replay> rdata = new List<Replay>();

            /// <summary>
            ///     存储的录像
            /// </summary>
            public IReadOnlyList<Replay> Replays => rdata.AsReadOnly();

            /// <summary>
            ///     使用整数索引从列表中获取谱面
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            public Replay this[int x] => Replays[x];

            internal void Add(Replay data)
            {
                rdata.Add(data);
            }

            internal void Remove(Replay data)
            {
                rdata.Remove(data);
            }

            /// <summary>
            ///     判断指定录像是否在列表中
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public bool Contains(Replay data)
            {
                return rdata.Contains(data);
            }

            /// <summary>
            ///     使用MD5搜索谱面
            /// </summary>
            /// <param name="MD5"></param>
            /// <param name="md5Type">指定谱面或录像的MD5</param>
            /// <returns></returns>
            public ReplayCollection Find(string MD5, MD5Type md5Type)
            {
                var r = new ReplayCollection();
                foreach (var replay in rdata)
                {
                    if (md5Type == MD5Type.BeatmapMD5)
                        if (replay.BeatmapMD5 == MD5)
                            r.Add(replay);
                    if (md5Type == MD5Type.ReplayMD5)
                        if (replay.ReplayMD5 == MD5)
                            r.Add(replay);
                    if (r.Replays.Count == 0) throw new ReplayNotFoundException("找不到与指定MD5匹配的录像");
                }

                return r;
            }

            /// <summary>
            ///     使用Mod匹配录像
            /// </summary>
            /// <param name="mod"></param>
            /// <returns></returns>
            public ReplayCollection Find(OsuGameMod mod)
            {
                var r = new ReplayCollection();
                foreach (var replay in rdata)
                    if (replay.Mods.Contains(mod))
                        r.rdata.Add(replay);
                return r;
            }

            /// <summary>
            ///     使用游戏模式匹配录像
            /// </summary>
            /// <param name="mode"></param>
            /// <returns></returns>
            public ReplayCollection Find(OsuGameMode mode)
            {
                var r = new ReplayCollection();
                foreach (var replay in rdata)
                    if (replay.Mode == mode)
                        r.rdata.Add(replay);
                return r;
            }

            /// <summary>
            ///     在指定文件夹中搜索录像
            /// </summary>
            /// <param name="replaydir"></param>
            /// <returns></returns>
            public static ReplayCollection GetAllReplays(string replaydir)
            {
                var rc = new ReplayCollection();
                var dirs = Directory.GetFiles(replaydir, "*.osr", SearchOption.AllDirectories);
                foreach (var osrfile in dirs)
                {
                    var stream = File.OpenRead(osrfile);
                    var bin = new BinaryReader(stream);
                    var osr = new Replay(bin, osrfile);
                    rc.Add(osr);
                }

                return rc;
            }

            /// <summary>
            ///     返回循环访问List的枚举数。
            /// </summary>
            /// <returns></returns>
            public IEnumerator<Replay> GetEnumerator()
            {
                return rdata.GetEnumerator();
            }
        }
    }
}