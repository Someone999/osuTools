using osuTools.Attributes;
using osuTools.Beatmaps.HitObject;

namespace osuTools
{
    namespace Beatmaps
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
            public string FullVideoFileName
            {
                get
                {
                    if (HasVideo) return fuvi;
                    return null;
                }
            }

            /// <summary>
            ///     谱面对应的音频文件名
            /// </summary>
            [AvailableVariable("Beatmap.AudioFileName", "LANG_VAR_AUDIOFILENAME")]
            public string AudioFileName
            {
                get => au;
                set => au = value;
            }

            /// <summary>
            ///     谱面对应的视频文件名
            /// </summary>
            [AvailableVariable("Beatmap.AudioFileName", "LANG_VAR_VIDEOFILENAME")]
            public string VideoFileName
            {
                get
                {
                    if (HasVideo) return vi;
                    return null;
                }
                set => vi = value;
            }

            /// <summary>
            ///     存储谱面的文件夹的全路径
            /// </summary>
            [AvailableVariable("Beatmap.BeatmapFolder", "LANG_VAR_BEATMAPFOLDER")]
            public string BeatmapFolder => FullPath.Replace(FileName, "");

            /// <summary>
            ///     谱面的MD5
            /// </summary>
            [AvailableVariable("Beatmap.MD5", "LANG_VAR_MD5")]
            public MD5String MD5 { get; } = new MD5String();

            /// <summary>
            ///     谱面的来源
            /// </summary>
            public string Source
            {
                get => sou;
                set => sou = value;
            }

            /// <summary>
            ///     谱面的标签
            /// </summary>
            [AvailableVariable("Beatmap.Tags", "LANG_VAR_TAGS")]
            public string Tags
            {
                get => tag;
                set => tag = value;
            }

            /// <summary>
            ///     谱面的作者
            /// </summary>
            [AvailableVariable("Beatmap.Maker", "LANG_VAR_CREATOR")]
            public string Maker
            {
                get => mak;
                set => mak = value;
            }

            /// <summary>
            ///     标题
            /// </summary>
            [AvailableVariable("Beatmap.Title", "LANG_VAR_TITLE")]
            public string Title
            {
                get => t;
                set => t = value;
            }

            /// <summary>
            ///     标题的Unicode形式
            /// </summary>
            [AvailableVariable("Beatmap.TitleUnicode", "LANG_VAR_TITLEUNICODE")]
            public string TitleUnicode
            {
                get => ut;
                set => ut = value;
            }

            /// <summary>
            ///     艺术家
            /// </summary>
            [AvailableVariable("Beatmap.Artist", "LANG_VAR_ARTIST")]
            public string Artist
            {
                get => a;
                set => a = value;
            }

            /// <summary>
            ///     艺术家的Unicode形式
            /// </summary>
            [AvailableVariable("Beatmap.ArtistUnicode", "LANG_VAR_ARTISTUNICODE")]
            public string ArtistUnicode
            {
                get => ua;
                set => ua = value;
            }

            /// <summary>
            ///     谱面的作者
            /// </summary>
            [AvailableVariable("Beatmap.Creator", "LANG_VAR_CREATOR")]
            public string Creator
            {
                get => c;
                set => c = value;
            }

            /// <summary>
            ///     谱面的难度
            /// </summary>
            [AvailableVariable("Beatmap.Difficulty", "LANG_VAR_DIFFICULTY")]
            public string Difficulty
            {
                get => dif;
                set => dif = value;
            }

            /// <summary>
            ///     谱面的难度
            /// </summary>
            [AvailableVariable("Beatmap.Version", "LANG_VAR_DIFFICULTY")]
            public string Version
            {
                get => ver;
                set => ver = value;
            }

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
            public int BeatmapID
            {
                get => id;
                set => id = value;
            }

            /// <summary>
            ///     综合难度
            /// </summary>
            [AvailableVariable("OD", "LANG_VAR_OD")]
            public double OD
            {
                get => od;
                set => od = value;
            }

            /// <summary>
            ///     掉血速度，回血难度
            /// </summary>
            [AvailableVariable("Beatmap.HP", "LANG_VAR_HPDRAIN")]
            public double HP
            {
                get => hp;
                set => hp = value;
            }

            /// <summary>
            ///     缩圈速度
            /// </summary>
            [AvailableVariable("AR", "LANG_VAR_AR")]
            public double AR
            {
                get => ar;
                set => ar = value;
            }

            /// <summary>
            ///     圈圈大小
            /// </summary>
            [AvailableVariable("CS", "LANG_VAR_CS")]
            public double CS
            {
                get => cs;
                set => cs = value;
            }

            /// <summary>
            ///     难度星级
            /// </summary>
            [AvailableVariable("Beatmap.Stars", "LANG_VAR_STARS")]
            public double Stars
            {
                get => stars;
                set => stars = value;
            }

            /// <summary>
            ///     谱面包含的所有HitObject
            /// </summary>
            public HitObjectCollection HitObjects
            {
                get
                {
                    if (hitObjects == null)
                        getHitObjects();
                    return hitObjects;
                }
                set => hitObjects = value;
            }

            /// <summary>
            ///     谱面中包含的所有BreakTime
            /// </summary>
            public BreakTimeCollection BreakTimes
            {
                get
                {
                    if (breakTimes == null)
                        getBreakTimes();
                    return breakTimes;
                }
                set => breakTimes = value;
            }
        }
    }
}