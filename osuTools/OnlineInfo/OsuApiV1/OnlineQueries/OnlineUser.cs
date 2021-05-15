using System;

namespace osuTools.OnlineInfo.OsuApiV1.OnlineQueries
{
    partial class OnlineUser
    {
        /// <summary>
        ///     用户ID
        /// </summary>
        public int UserId => _userId;

        /// <summary>
        ///     游玩次数
        /// </summary>
        public int PlayCount => _playcount;

        /// <summary>
        ///     游玩Ranked谱面获得的分数
        /// </summary>
        public double RankedScore => _rankedScore;

        /// <summary>
        ///     游玩已提交谱面获得的分数
        /// </summary>
        public double TotalScore => _totalScore;

        /// <summary>
        ///     平均准确度
        /// </summary>
        public double Accuracy => _accuracy;

        /// <summary>
        ///     等级
        /// </summary>
        public double Level => _level;

        /// <summary>
        ///     表现分
        /// </summary>
        public double Pp => _ppRaw;

        /// <summary>
        ///     SS的数量
        /// </summary>
        public int SsCount => _countRankSs;

        /// <summary>
        ///     银色SS的数量
        /// </summary>
        public int SshCount => _countRankSsh;

        /// <summary>
        ///     S的数量
        /// </summary>
        public int SCount => _countRankS;

        /// <summary>
        ///     银色S的数量
        /// </summary>
        public int ShCount => _countRankSh;

        /// <summary>
        ///     A的数量
        /// </summary>
        public int ACount => _countRankA;

        /// <summary>
        ///     世界排名
        /// </summary>
        public int GlobalRank => _ppRank;

        /// <summary>
        ///     国内排名
        /// </summary>
        public int CountryRank => _ppCountryRank;

        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; } = "";

        /// <summary>
        ///     国籍
        /// </summary>
        public string Country { get; } = "";

        /// <summary>
        ///     注册时间
        /// </summary>
        public DateTime JoinDate => _t;

        /// <summary>
        ///     游戏时间
        /// </summary>
        public TimeSpan PlayTime => TimeSpan.FromSeconds(_totalSecondsPlayed);
    }
}