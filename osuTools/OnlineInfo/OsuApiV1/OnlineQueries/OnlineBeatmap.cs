using System;

namespace osuTools.Online.ApiV1
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
}