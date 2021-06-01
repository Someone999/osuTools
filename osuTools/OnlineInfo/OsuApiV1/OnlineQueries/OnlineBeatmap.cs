using System;
using osuTools.Beatmaps;
using osuTools.Game.Modes;

namespace osuTools.OnlineInfo.OsuApiV1.OnlineQueries
{
    partial class OnlineBeatmap:IBeatmap
    {
        /// <summary>
        ///     谱面集的ID
        /// </summary>
        public int BeatmapSetId => _beatmapsetId;

        /// <summary>
        ///     谱面ID
        /// </summary>
        public int BeatmapId => _beatmapId;

        /// <summary>
        ///     谱面是否计入排行
        /// </summary>
        public BeatmapStatus Approved { get; private set; } = BeatmapStatus.None;

        /// <summary>
        ///     音乐长度
        /// </summary>
        public int TotalTime => _totalLength;

        /// <summary>
        ///     谱面长度
        /// </summary>
        public int DrainTime => _hitLength;

        /// <summary>
        ///     谱面的难度标签
        /// </summary>
        public string Version { get; private set; } = "";

        /// <summary>
        ///     圈圈的大小
        /// </summary>
        public double CircleSize => _diffSize;

        /// <summary>
        ///     谱面的MD5
        /// </summary>
        public string Md5 { get; private set; } = "0";

        /// <summary>
        ///     综合难度
        /// </summary>
        public double OverallDifficulty => _diffOverall;

        /// <summary>
        ///     缩圈速度
        /// </summary>
        public double ApproachRate => _diffApproach;

        /// <summary>
        ///     掉血速度、回血难度
        /// </summary>
        public double HpDrain => _diffDrain;

        /// <summary>
        ///     谱面的圈圈数
        /// </summary>
        public int HitCircle => _countNormal;

        /// <summary>
        ///     谱面的游戏模式
        /// </summary>
        public OsuGameMode Mode => (OsuGameMode) _mode;

        /// <summary>
        ///     谱面的转盘数
        /// </summary>
        public int Spinner => _countSpinner;

        /// <summary>
        ///     谱面的滑条数
        /// </summary>
        public int Slider => _countSlider;

        /// <summary>
        ///     谱面提交的时间
        /// </summary>
        public DateTime SubmitDate
        {
            get
            {
                DateTime.TryParse(_submitDate, out var dt);
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
                DateTime.TryParse(_approvedDate, out var dt);
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
                DateTime.TryParse(_lastUpdate, out var dt);
                return dt;
            }
        }

        /// <summary>
        ///     艺术家
        /// </summary>
        public string Artist { get; private set; } = "";
        /// <summary>
        /// 艺术家名字的Unicode格式，这里只会返回Artist
        /// </summary>
        public string ArtistUnicode => Artist;
        /// <summary>
        /// 谱面标题的Unicode格式，这里只会返回Title
        /// </summary>
        public string TitleUnicode => Title;

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
        public int CreatorId => _creatorId;

        /// <summary>
        ///     谱面的每分钟节拍数
        /// </summary>
        public double Bpm => _bpm;

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
        public Genre GenreId => (Genre) _genreId;

        /// <summary>
        ///     谱面的标签
        /// </summary>
        public Language LanguageId => (Language) _languageId;

        /// <summary>
        ///     标记谱面为“喜欢”的人的数目
        /// </summary>
        public int FavoriteCount => _favouriteCount;

        /// <summary>
        ///     谱面的评分
        /// </summary>
        public double Rating => _rating;

        /// <summary>
        ///     谱面能否下载
        /// </summary>
        public bool Downloadable
        {
            get
            {
                if (_downloadUnavailable == 0) return true;
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
                if (_audioUnavailable == 0) return true;
                return false;
            }
        }

        /// <summary>
        ///     谱面被的次数
        /// </summary>
        public int PlayCount => _playcount;

        /// <summary>
        ///     谱面被通关的次数
        /// </summary>
        public int PassCount => _passcount;

        /// <summary>
        ///     谱面的总连击
        /// </summary>
        public int MaxCombo => _maxCombo;

        /// <summary>
        ///     谱面的定位难度
        /// </summary>
        public double AimDiff => _diffAim;

        /// <summary>
        ///     谱面的速度难度
        /// </summary>
        public double SpeedDiff => _diffSpeed;

        /// <summary>
        ///     谱面的难度星级
        /// </summary>
        public double Stars => _difficultyrating;
    }
}