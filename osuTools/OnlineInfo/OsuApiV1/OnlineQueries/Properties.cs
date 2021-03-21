using System;

namespace osuTools
{
    namespace Online.ApiV1
    {
        partial class OnlineBeatmap
        {
            /// <summary>
            ///     谱面集的ID
            /// </summary>
            public int BeatmapSetID => beatmapset_id;

            /// <summary>
            ///     谱面ID
            /// </summary>
            public int BeatmapID => beatmap_id;

            /// <summary>
            ///     谱面是否计入排行
            /// </summary>
            public BeatmapStatus Approved { get; private set; } = BeatmapStatus.None;

            /// <summary>
            ///     音乐长度
            /// </summary>
            public int TotalTime => total_length;

            /// <summary>
            ///     谱面长度
            /// </summary>
            public int DrainTime => hit_length;

            /// <summary>
            ///     谱面的难度标签
            /// </summary>
            public string Version { get; private set; } = "";

            /// <summary>
            ///     圈圈的大小
            /// </summary>
            public double CS => diff_size;

            /// <summary>
            ///     谱面的MD5
            /// </summary>
            public string MD5 { get; private set; } = "0";

            /// <summary>
            ///     综合难度
            /// </summary>
            public double OD => diff_overall;

            /// <summary>
            ///     缩圈速度
            /// </summary>
            public double AR => diff_approach;

            /// <summary>
            ///     掉血速度、回血难度
            /// </summary>
            public double HP => diff_drain;

            /// <summary>
            ///     谱面的圈圈数
            /// </summary>
            public int HitCircle => count_normal;

            /// <summary>
            ///     谱面的游戏模式
            /// </summary>
            public OsuGameMode Mode => (OsuGameMode) mode;

            /// <summary>
            ///     谱面的转盘数
            /// </summary>
            public int Spinner => count_spinner;

            /// <summary>
            ///     谱面的滑条数
            /// </summary>
            public int Slider => count_slider;

            /// <summary>
            ///     谱面提交的时间
            /// </summary>
            public DateTime SubmitDate
            {
                get
                {
                    DateTime dt;
                    DateTime.TryParse(submit_date, out dt);
                    return dt;
                }
            }

            /// <summary>
            ///     谱面计入排行的时间
            /// </summary>
            public DateTime ApprovedDate
            {
                get
                {
                    DateTime dt;
                    DateTime.TryParse(approved_date, out dt);
                    return dt;
                }
            }

            /// <summary>
            ///     谱面最近一次修改的日期
            /// </summary>
            public DateTime LastUpdateDate
            {
                get
                {
                    DateTime dt;
                    DateTime.TryParse(last_update, out dt);
                    return dt;
                }
            }

            /// <summary>
            ///     艺术家
            /// </summary>
            public string Artist { get; private set; } = "";

            /// <summary>
            ///     标题
            /// </summary>
            public string Title { get; private set; } = "";

            /// <summary>
            ///     谱面的创作者
            /// </summary>
            public string Creator { get; private set; } = "";

            /// <summary>
            ///     谱面的创作者的ID
            /// </summary>
            public int CreatorID => creator_id;

            /// <summary>
            ///     谱面的每分钟节拍数
            /// </summary>
            public double BPM => bpm;

            /// <summary>
            ///     谱面的来源
            /// </summary>
            public string Source { get; private set; } = "";

            /// <summary>
            ///     谱面的标签
            /// </summary>
            public string Tags { get; private set; } = "";

            /// <summary>
            ///     谱面的流派
            /// </summary>
            public Genre GenreID => (Genre) genre_id;

            /// <summary>
            ///     谱面的标签
            /// </summary>
            public Language LanguageID => (Language) language_id;

            /// <summary>
            ///     标记谱面为“喜欢”的人的数目
            /// </summary>
            public int FavoriteCount => favourite_count;

            /// <summary>
            ///     谱面的评分
            /// </summary>
            public double Rating => rating;

            /// <summary>
            ///     谱面能否下载
            /// </summary>
            public bool Downloadable
            {
                get
                {
                    if (download_unavailable == 0) return true;
                    return false;
                }
            }

            /// <summary>
            ///     谱面的音频是否可用
            /// </summary>
            public bool AudioAvailable
            {
                get
                {
                    if (audio_unavailable == 0) return true;
                    return false;
                }
            }

            /// <summary>
            ///     谱面被的次数
            /// </summary>
            public int PlayCount => playcount;

            /// <summary>
            ///     谱面被通关的次数
            /// </summary>
            public int PassCount => passcount;

            /// <summary>
            ///     谱面的总连击
            /// </summary>
            public int MaxCombo => max_combo;

            /// <summary>
            ///     谱面的定位难度
            /// </summary>
            public double AimDiff => diff_aim;

            /// <summary>
            ///     谱面的速度难度
            /// </summary>
            public double SpeedDiff => diff_speed;

            /// <summary>
            ///     谱面的难度星级
            /// </summary>
            public double Stars => difficultyrating;
        }

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

        partial class OnlineBestRecord
        {
            /// <summary>
            ///     谱面ID
            /// </summary>
            public int BeatmapID => beatmap_id;

            /// <summary>
            ///     分数ID
            /// </summary>
            public int ScoreID => score_id;

            /// <summary>
            ///     分数
            /// </summary>
            public int Score => score;

            /// <summary>
            ///     经权重计算后的pp
            /// </summary>
            public double WeightedPP { get; private set; }

            /// <summary>
            ///     原始pp
            /// </summary>
            public override double PP => pp;

            /// <summary>
            ///     最大连击
            /// </summary>
            public int MaxCombo => maxcombo;

            /// <summary>
            ///     300g或激的数量
            /// </summary>
            public int c300g => countgeki;

            /// <summary>
            ///     300的数量
            /// </summary>
            public int c300 => count300;

            /// <summary>
            ///     200或喝的数量
            /// </summary>
            public int c200 => countkatu;

            /// <summary>
            ///     100的数量
            /// </summary>
            public int c100 => count100;

            /// <summary>
            ///     50的数量
            /// </summary>
            public int c50 => count50;

            /// <summary>
            ///     Miss的数量
            /// </summary>
            public int cMiss => countmiss;

            /// <summary>
            ///     用户ID
            /// </summary>
            public int UserID => user_id;

            /// <summary>
            ///     结算评价
            /// </summary>
            public string Rank { get; }

            /// <summary>
            ///     如Std,CTB全连或Mania没有出现100，50，Miss，为true，否则为false
            /// </summary>
            public bool Perfect { get; }

            /// <summary>
            ///     游玩时间(为UTC时间)
            /// </summary>
            public DateTime GetDate => d;
        }

        public partial class OnlineScore
        {
            /// <summary>
            ///     分数ID
            /// </summary>
            public uint ScoreID => score_id;

            /// <summary>
            ///     分数
            /// </summary>
            public int Score => score;

            public override double PP => pp;

            /// <summary>
            ///     最大连击
            /// </summary>
            public int MaxCombo => maxcombo;

            /// <summary>
            ///     300g或激的数量
            /// </summary>
            public int c300g => countgeki;

            /// <summary>
            ///     300的数量
            /// </summary>
            public int c300 => count300;

            /// <summary>
            ///     200或喝的数量
            /// </summary>
            public int c200 => countkatu;

            /// <summary>
            ///     100的数量
            /// </summary>
            public int c100 => count100;

            /// <summary>
            ///     50的数量
            /// </summary>
            public int c50 => count50;

            /// <summary>
            ///     Miss的数量
            /// </summary>
            public int cMiss => countmiss;

            /// <summary>
            ///     用户ID
            /// </summary>
            public int UserID => user_id;

            /// <summary>
            ///     结算评价
            /// </summary>
            public string Rank { get; }

            /// <summary>
            ///     如Std,CTB全连或Mania没有出现100，50，Miss，为true，否则为false
            /// </summary>
            public bool Perfect { get; }

            /// <summary>
            ///     游玩时间(为UTC时间)
            /// </summary>
            public DateTime GetDate => d;

            /// <summary>
            ///     录像是否可用
            /// </summary>
            public bool ReplayAvailable { get; }
        }

        partial class RecentOnlineResult
        {
            /// <summary>
            ///     谱面ID
            /// </summary>
            public int BeatmapID => beatmap_id;

            /// <summary>
            ///     最大连击
            /// </summary>
            public int MaxCombo => maxcombo;

            /// <summary>
            ///     300g或激的数量
            /// </summary>
            public int c300g => countgeki;

            /// <summary>
            ///     300的数量
            /// </summary>
            public int c300 => count300;

            /// <summary>
            ///     200或喝的数量
            /// </summary>
            public int c200 => countkatu;

            /// <summary>
            ///     100的数量
            /// </summary>
            public int c100 => count100;

            /// <summary>
            ///     50的数量
            /// </summary>
            public int c50 => count50;

            /// <summary>
            ///     Miss的数量
            /// </summary>
            public int cMiss => countmiss;

            /// <summary>
            ///     用户ID
            /// </summary>
            public int UserID => user_id;

            /// <summary>
            ///     结算评价
            /// </summary>
            public string Rank { get; }

            /// <summary>
            ///     如Std,CTB全连或Mania没有出现100，50，Miss，为true，否则为false
            /// </summary>
            public bool Perfect { get; }

            /// <summary>
            ///     游玩时间(为UTC时间)
            /// </summary>
            public DateTime GetDate => d;
        }
    }
}