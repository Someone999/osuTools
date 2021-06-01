using osuTools.Attributes;
using osuTools.Beatmaps.BreakTime;
using osuTools.Beatmaps.HitObject;
using osuTools.MD5Tools;

namespace osuTools.Beatmaps
{
    public partial class Beatmap
        {
            /// <summary>
            ///     谱面对应音频文件的全路径
            /// </summary>
            [AvailableVariable("Beatmap.FullAudioFileName", "LANG_VAR_FULLAUFILENAME")]
            public string FullAudioFileName { get; } = "";

            /// <summary>
            ///     谱面对应图片文件的全路径
            /// </summary>
            [AvailableVariable("Beatmap.FullBackgroundFileName", "LANG_VAR_FULLBGFILENAME")]
            public string FullBackgroundFileName { get; } = "";

            /// <summary>
            ///     谱面对应的视频文件的全路径
            /// </summary>
            [AvailableVariable("Beatmap.FullVideoFileName", "LANG_VAR_FULLVDFILENAME")]
            public string FullVideoFileName { get; } = "";

            /// <summary>
            ///     谱面对应的音频文件名
            /// </summary>
            [AvailableVariable("Beatmap.AudioFileName", "LANG_VAR_AUDIOFILENAME")]
            public string AudioFileName { get; set; } = "";

            /// <summary>
            ///     谱面对应的视频文件名
            /// </summary>
            [AvailableVariable("Beatmap.AudioFileName", "LANG_VAR_VIDEOFILENAME")]
            public string VideoFileName { get; set; } = "";

            /// <summary>
            ///     存储谱面的文件夹的全路径
            /// </summary>
            [AvailableVariable("Beatmap.BeatmapFolder", "LANG_VAR_BEATMAPFOLDER")]
            public string BeatmapFolder => string.IsNullOrEmpty(FullPath) ? "" : FullPath.Replace(FileName, "");

            /// <summary>
            ///     谱面的MD5
            /// </summary>
            [AvailableVariable("Beatmap.MD5", "LANG_VAR_MD5")]
            public MD5String Md5 { get; } = new MD5String();

            /// <summary>
            ///     谱面的来源
            /// </summary>
            public string Source { get; set; } = "";

            /// <summary>
            ///     谱面的标签
            /// </summary>
            [AvailableVariable("Beatmap.Tags", "LANG_VAR_TAGS")]
            public string Tags { get; set; } = "";

            /// <summary>
            ///     谱面的作者
            /// </summary>
            [AvailableVariable("Beatmap.Maker", "LANG_VAR_CREATOR")]
            public string Maker { get; set; } = "";

            /// <summary>
            ///     标题
            /// </summary>
            [AvailableVariable("Beatmap.Title", "LANG_VAR_TITLE")]
            public string Title { get; set; } = "";

            /// <summary>
            ///     标题的Unicode形式
            /// </summary>
            [AvailableVariable("Beatmap.TitleUnicode", "LANG_VAR_TITLEUNICODE")]
            public string TitleUnicode { get; set; } = "";

            /// <summary>
            ///     艺术家
            /// </summary>
            [AvailableVariable("Beatmap.Artist", "LANG_VAR_ARTIST")]
            public string Artist { get; set; } = "";

            /// <summary>
            ///     艺术家的Unicode形式
            /// </summary>
            [AvailableVariable("Beatmap.ArtistUnicode", "LANG_VAR_ARTISTUNICODE")]
            public string ArtistUnicode { get; set; } = "";

            /// <summary>
            ///     谱面的作者
            /// </summary>
            [AvailableVariable("Beatmap.Creator", "LANG_VAR_CREATOR")]
            public string Creator { get; set; } = "";

            /// <summary>
            ///     谱面的难度
            /// </summary>
            [AvailableVariable("Beatmap.Difficulty", "LANG_VAR_DIFFICULTY")]
            public string Difficulty { get; set; } = "";

            /// <summary>
            ///     谱面的难度
            /// </summary>
            [AvailableVariable("Beatmap.Version", "LANG_VAR_DIFFICULTY")]
            public string Version { get; set; } = "";

            /// <summary>
            ///     谱面文件的文件名
            /// </summary>
            [AvailableVariable("Beatmap.FileName", "LANG_VAR_FILENAME")]
            public string FileName { get; } = "";

            /// <summary>
            ///     谱面文件的全路径
            /// </summary>
            [AvailableVariable("Beatmap.FullPath", "LANG_VAR_FULLPATH")]
            public string FullPath { get; } = "";

            /// <summary>
            ///     谱面文件的下载链接
            /// </summary>
            [AvailableVariable("Beatmap.DownloadLink", "LANG_VAR_DOWNLOADLINK")]
            public string DownloadLink { get; } = "";

            /// <summary>
            ///     背景文件的文件名
            /// </summary>
            [AvailableVariable("Beatmap.BackgroundFileName", "LANG_VAR_BACKGROUNDFILENAME")]
            public string BackgroundFileName { get; } = "";

            /// <summary>
            ///     谱面ID
            /// </summary>
            [AvailableVariable("Beatmap.BeatmapId", "LANG_VAR_BEATMAPID")]
            public int BeatmapId { get; set; } = -2048;

            /// <summary>
            ///     综合难度
            /// </summary>
            [AvailableVariable("Beatmap.OverallDifficulty", "LANG_VAR_OD")]
            [Alias("Beatmap.OD")]
            public double OverallDifficulty { get; set; } = -1;

            /// <summary>
            ///     掉血速度，回血难度
            /// </summary>
            [AvailableVariable("Beatmap.HPDrain", "LANG_VAR_HPDRAIN")]
            public double HpDrain { get; set; } = -1;

            /// <summary>
            ///     缩圈速度
            /// </summary>
            [AvailableVariable("Beatmap.ApproachRate", "LANG_VAR_AR")]
            [Alias("Beatmap.AR")]
            public double ApproachRate { get; set; } = -1;

            /// <summary>
            ///     圈圈大小
            /// </summary>
            [AvailableVariable("Beatmap.CircleSize", "LANG_VAR_CS")]
            [Alias("Beatmap.CS")]
            public double CircleSize { get; set; } = -1;

            /// <summary>
            ///     难度星级
            /// </summary>
            [AvailableVariable("Beatmap.Stars", "LANG_VAR_STARS")]
            public double Stars { get; set; } = -1;

            /// <summary>
            ///     谱面包含的所有HitObject
            /// </summary>
            public HitObjectCollection HitObjects
            {
                get
                {
                    if (_hitObjects == null)
                        GetHitObjects();
                    return _hitObjects;
                }
                set => _hitObjects = value;
            }

            /// <summary>
            ///     谱面中包含的所有BreakTime
            /// </summary>
            public BreakTimeCollection BreakTimes
            {
                get
                {
                    if (_breakTimes == null)
                        GetBreakTimes();
                    return _breakTimes;
                }
                set => _breakTimes = value;
            }
        }
}