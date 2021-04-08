using System;

namespace osuTools.Online.ApiV1
{
    partial class OnlineUser
    {
        /// <summary>
        ///     用户ID
        /// </summary>
        public int UserID => user_id;

        /// <summary>
        ///     游玩次数
        /// </summary>
        public int PlayCount => playcount;

        /// <summary>
        ///     游玩Ranked谱面获得的分数
        /// </summary>
        public double RankedScore => ranked_score;

        /// <summary>
        ///     游玩已提交谱面获得的分数
        /// </summary>
        public double TotalScore => total_score;

        /// <summary>
        ///     平均准确度
        /// </summary>
        public double Accuracy => accuracy;

        /// <summary>
        ///     等级
        /// </summary>
        public double Level => level;

        /// <summary>
        ///     表现分
        /// </summary>
        public double PP => pp_raw;

        /// <summary>
        ///     SS的数量
        /// </summary>
        public int SSCount => count_rank_ss;

        /// <summary>
        ///     银色SS的数量
        /// </summary>
        public int SSHCount => count_rank_ssh;

        /// <summary>
        ///     S的数量
        /// </summary>
        public int SCount => count_rank_s;

        /// <summary>
        ///     银色S的数量
        /// </summary>
        public int SHCount => count_rank_sh;

        /// <summary>
        ///     A的数量
        /// </summary>
        public int ACount => count_rank_a;

        /// <summary>
        ///     世界排名
        /// </summary>
        public int GlobalRank => pp_rank;

        /// <summary>
        ///     国内排名
        /// </summary>
        public int CountryRank => pp_country_rank;

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
        public DateTime JoinDate => t;

        /// <summary>
        ///     游戏时间
        /// </summary>
        public TimeSpan PlayTime => TimeSpan.FromSeconds(total_seconds_played);
    }
}