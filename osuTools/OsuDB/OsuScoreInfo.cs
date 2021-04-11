using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using osuTools.Beatmaps.HitObject;
using osuTools.Exceptions;
using osuTools.Online;

namespace osuTools.OsuDB
{
    /// <summary>
    ///     scores.db中存储的成绩
    /// </summary>
    public class OsuScoreInfo : SortByScore, IOsuDbData
    {
        internal List<OsuGameMod> mods = new List<OsuGameMod>();
        private readonly long pt;

        /// <summary>
        ///     使用特定的数据来构造一个ScoreDBData对象
        /// </summary>
        /// <param name="mode">游戏模式</param>
        /// <param name="ver">游戏版本</param>
        /// <param name="bmd5">谱面的MD5</param>
        /// <param name="name">玩家名</param>
        /// <param name="rmd5">回放的MD5</param>
        /// <param name="c300">300的数量</param>
        /// <param name="c100">100的数量</param>
        /// <param name="c50">50的数量</param>
        /// <param name="c300g">激或彩300的数量</param>
        /// <param name="c200">喝或200的数量</param>
        /// <param name="cmiss">Miss的数量</param>
        /// <param name="score">分数</param>
        /// <param name="maxcombo">最大连击</param>
        /// <param name="per">是否为Perfect</param>
        /// <param name="mods">使用了的Mod的整数形式</param>
        /// <param name="empty">一个必须为空的字符串</param>
        /// <param name="playtime">游玩的时间，以Tick为单位</param>
        /// <param name="verify">一个值必须为-1的整数</param>
        /// <param name="scoreid">ScoreID</param>
        /// <param name="acc">准确度</param>
        public OsuScoreInfo(OsuGameMode mode, int ver, string bmd5, string name, string rmd5, short c300, short c100,
            short c50, short c300g, short c200, short cmiss, int score, short maxcombo, bool per, int mods,
            string empty, long playtime, int verify, long scoreid, double acc)
        {
            Mode = mode;
            //System.Windows.Forms.MessageBox.Show(Mode.ToString());
            GameVersion = ver;
            BeatmapMD5 = bmd5;
            ReplayMD5 = rmd5;
            PlayerName = name;
            this.c300g = c300g;
            this.c300 = c300;
            this.c200 = c200;
            this.c100 = c100;
            this.c50 = c50;
            cMiss = cmiss;
            Score = score;
            MaxCombo = maxcombo;
            Perfect = per;
            this.mods = HitObjectTools.GetGenericTypesByInt<OsuGameMod>(mods);
            pt = playtime;
            PlayTime = new DateTime(pt);
            if (verify != -1) throw new FailToParseException("验证失败");
            ScoreID = scoreid;
            Debug.Assert(c300 + c100 + c50 + cMiss != 0);
            Accuracy = AccCalc(mode);
            //System.Windows.Forms.MessageBox.Show(Score.ToString());
        }

        /// <summary>
        ///     游戏版本
        /// </summary>
        public int GameVersion { get; }

        /// <summary>
        ///     游戏模式
        /// </summary>
        public OsuGameMode Mode { get; }

        /// <summary>
        ///     谱面的MD5
        /// </summary>
        public string BeatmapMD5 { get; }

        /// <summary>
        ///     玩家名
        /// </summary>
        public string PlayerName { get; }

        /// <summary>
        ///     回放的MD5
        /// </summary>
        public string ReplayMD5 { get; }

        /// <summary>
        ///     激或彩300的数量
        /// </summary>
        public short c300g { get; }

        /// <summary>
        ///     300的数量
        /// </summary>
        public short c300 { get; }

        /// <summary>
        ///     喝或200的数量
        /// </summary>
        public short c200 { get; }

        /// <summary>
        ///     100的数量
        /// </summary>
        public short c100 { get; }

        /// <summary>
        ///     50的数量
        /// </summary>
        public short c50 { get; }

        /// <summary>
        ///     Miss的数量
        /// </summary>
        public short cMiss { get; }

        /// <summary>
        ///     分数
        /// </summary>
        public override int Score { get; }

        /// <summary>
        ///     最大连击
        /// </summary>
        public short MaxCombo { get; }

        /// <summary>
        ///     是否达成Perfect判定
        /// </summary>
        public bool Perfect { get; }

        /// <summary>
        ///     本次游戏使用的Mod
        /// </summary>
        public IReadOnlyList<OsuGameMod> Mods => mods.AsReadOnly();

        /// <summary>
        ///     游玩时间
        /// </summary>
        public DateTime PlayTime { get; }

        /// <summary>
        ///     分数ID
        /// </summary>
        public long ScoreID { get; }

        /// <summary>
        ///     准确度
        /// </summary>
        public double Accuracy { get; }

        /// <summary>
        ///     确定指定的对象是否等于当前对象。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is OsuScoreInfo info)
            {
                return info.ReplayMD5 == ReplayMD5 && info.BeatmapMD5 == BeatmapMD5;
            }

            return obj.Equals(this);
        }

        /// <summary>
        ///     默认哈希函数
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ReplayMD5.GetHashCode();
        }
        /// <summary>
        /// 获取MD5与成绩BeatmapMD5相同的谱面
        /// </summary>
        /// <returns>
        /// 成功返回相应谱面，失败返回null
        /// </returns>
        public OsuBeatmap GetOsuBeatmap()
        {
            try
            {
                return new OsuBeatmapDB().Beatmaps.FindByMd5(BeatmapMD5);
            }
            catch (BeatmapNotFoundException)
            {
                return null;
            }

        }

        private double AccCalc(OsuGameMode mode)
        {
            switch (mode)
            {
                case OsuGameMode.Osu:
                    return (c300 + c100 * (1.0 / 3.0) + c50 * (1.0 / 6)) / (c300 + c100 + c50 + cMiss);
                case OsuGameMode.Taiko: return (c300 + c100 * 0.5) / (c300 + c100 + cMiss);
                case OsuGameMode.Catch: return (double) (c300 + c100 + c50) / (c300 + c100 + c50 + c200 + cMiss);
                case OsuGameMode.Mania:
                    return (c300g + c300 + c200 * (2 / 3.0) + c100 * (1 / 3.0) + c50 * 1 / 6.0) /
                           (c300g + c300 + c200 + c100 + c50 + cMiss);
                default: return -1;
            }
        }

        /// <summary>
        ///     使用ReplayMD5判断两个成绩是否为同一个成绩
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(OsuScoreInfo a, OsuScoreInfo b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        /// <summary>
        ///     使用时间判断两个成绩是否为同一个成绩
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(OsuScoreInfo a, OsuScoreInfo b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return !a.Equals(b);
        }
    }
}