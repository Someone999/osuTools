using System;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuTools.Beatmaps;

namespace osuTools
{
    namespace Online.ApiV1
    {
        /// <summary>
        ///     在线获取的谱面
        /// </summary>
        [Serializable]
        public partial class OnlineBeatmap : IEquatable<OnlineBeatmap>, IFormattable
        {
            private int _approved = -1;

            private string _approvedDate = "0-0-0 0:0:0";

            private int _audioUnavailable = 0;

            private int _beatmapId = -2;
            private int _beatmapsetId = -2;

            private double _bpm;

            private int _countNormal = -1;

            private int _countSlider = -1;

            private int _countSpinner = -1;

            private int _creatorId;

            private double _diffAim;

            private double _diffApproach = -1;

            private double _diffDrain = -1;
            private double _diffOverall = -1;
            private double _diffSize = -1;

            private double _diffSpeed;

            private double _difficultyrating;

            private int _downloadUnavailable = 0;

            private int _favouriteCount;

            private int _genreId;
            private int _hitLength = -1;
            private JObject _jobj = new JObject();

            private int _languageId;

            private string _lastUpdate = "0-0-0 0:0:0";

            private int _maxCombo;

            private int _mode = -1;

            private int _passcount;

            private int _playcount;

            private double _rating;

            private string _submitDate = "0-0-0 0:0:0";
            private int _totalLength = -1;

            /// <summary>
            ///     构造一个空的OnlineBeatmap对象
            /// </summary>
            public OnlineBeatmap()
            {
            }

            /// <summary>
            ///     使用正确的JObject填充一个OnlineBeatmap对象
            /// </summary>
            /// <param name="jobj"></param>
            public OnlineBeatmap(JObject jobj)
            {
                Parse(jobj);
            }

            bool IEquatable<OnlineBeatmap>.Equals(OnlineBeatmap other)
            {
                if (!(other is null) && Md5 == other.Md5)
                    return true;
                return false;
            }

            /// <summary>
            ///     使用一定的格式构造一个字符串
            /// </summary>
            /// <param name="format"></param>
            /// <param name="formatProvider"></param>
            /// <returns></returns>
            public string ToString(string format, IFormatProvider formatProvider)
            {
                var b = new StringBuilder(format);
                b.Replace("Artist", Artist);
                b.Replace("artist", Artist);
                b.Replace("bpm", Bpm.ToString(CultureInfo.CurrentCulture));
                b.Replace("stars", Stars.ToString(CultureInfo.CurrentCulture));
                b.Replace("mode", Mode.ToString());
                b.Replace("cs", CircleSize.ToString(CultureInfo.CurrentCulture));
                b.Replace("ar", ApproachRate.ToString(CultureInfo.CurrentCulture));
                b.Replace("hp", HpDrain.ToString(CultureInfo.CurrentCulture));
                b.Replace("od", OverallDifficulty.ToString(CultureInfo.CurrentCulture));
                b.Replace("circles", HitCircle.ToString());
                b.Replace("sliders", Slider.ToString());
                b.Replace("spiners", Spinner.ToString());
                b.Replace("creator", Creator);
                b.Replace("Title", Title);
                b.Replace("title", Title);
                b.Replace("Tags", Tags);
                b.Replace("tags", Tags);
                b.Replace("setid", BeatmapSetId.ToString());
                b.Replace("maxcombo", MaxCombo.ToString());
                b.Replace("approved", Approved.ToString());
                var total = TimeSpan.FromSeconds(TotalTime);
                var hit = TimeSpan.FromSeconds(DrainTime);
                b.Replace("length", $"{total.TotalMinutes}:{total.Seconds}");
                b.Replace("hitlength", $"{hit.TotalMinutes}:{hit.Seconds}");
                b.Replace("version", Version);
                b.Replace("md5", Md5);
                b.Replace("id", BeatmapId.ToString());
                return b.ToString();
            }

            /// <summary>
            ///     将对象序列化
            /// </summary>
            /// <param name="file"></param>
            public void Serialize(string file)
            {
                if (string.IsNullOrEmpty(file))
                    throw new ArgumentNullException(nameof(file),"文件名不能为空。");
                var dir = Path.GetDirectoryName(file);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir ?? throw new InvalidOperationException());
                File.WriteAllText(file, _jobj.ToString());
            }

            /// <summary>
            ///     从文件反序列化
            /// </summary>
            /// <param name="file"></param>
            public void Deserialize(string file)
            {
                Parse((JObject) JsonConvert.DeserializeObject(File.ReadAllText(file)));
            }

            /// <summary>
            ///     使用BeatmapID判断两个OnlineBeatmap是否相同
            /// </summary>
            /// <param name="olb"></param>
            /// <param name="lob"></param>
            /// <returns></returns>
            public static bool operator ==(OnlineBeatmap olb, Beatmap lob)
            {
                if (lob.BeatmapId == 0 || lob.BeatmapId == -1) throw new NotSupportedException();
                if (olb.BeatmapId == lob.BeatmapId && lob.BeatmapId != 0 && lob.BeatmapId != -1) return true;
                return false;
            }

            /// <summary>
            ///     使用BeatmapID判断两个OnlineBeatmap是否相同
            /// </summary>
            /// <param name="olb"></param>
            /// <param name="lob"></param>
            /// <returns></returns>
            public static bool operator !=(OnlineBeatmap olb, Beatmap lob)
            {
                if (lob.BeatmapId == 0 || lob.BeatmapId == -1) throw new NotSupportedException();
                if (olb.BeatmapId == lob.BeatmapId && lob.BeatmapId != 0 && lob.BeatmapId != -1) return false;
                return true;
            }

            /// <summary>
            ///     使用BeatmapID判断OnlineBeatmap和Beatmap是否相同
            /// </summary>
            /// <param name="olb"></param>
            /// <param name="lob"></param>
            /// <returns></returns>
            public static bool operator ==(OnlineBeatmap olb, OsuRTDataProvider.BeatmapInfo.Beatmap lob)
            {
                if (lob.BeatmapID == 0 || lob.BeatmapID == -1) throw new NotSupportedException();
                if (olb.BeatmapId == lob.BeatmapID && lob.BeatmapID != 0 && lob.BeatmapID != -1) return true;
                return false;
            }

            /// <summary>
            ///     使用BeatmapID判断OnlineBeatmap和Beatmap是否相同
            /// </summary>
            /// <param name="olb"></param>
            /// <param name="lob"></param>
            /// <returns></returns>
            public static bool operator !=(OnlineBeatmap olb, OsuRTDataProvider.BeatmapInfo.Beatmap lob)
            {
                if (lob.BeatmapID == 0 || lob.BeatmapID == -1) throw new NotSupportedException();
                if (olb.BeatmapId == lob.BeatmapID && lob.BeatmapID != 0 && lob.BeatmapID != -1) return false;
                return true;
            }

            /// <summary>
            ///     该谱面对象是否为空
            /// </summary>
            /// <returns></returns>
            public bool IsEmpty()
            {
                if (Md5 == "0")
                    return true;
                return false;
            }

            private void Parse(JObject jobj)
            {
                this._jobj = jobj;
                int.TryParse(jobj["beatmapset_id"].ToString(), out _beatmapsetId);
                int.TryParse(jobj["beatmap_id"].ToString(), out _beatmapId);
                int.TryParse(jobj["approved"].ToString(), out _approved);
                int.TryParse(jobj["total_length"].ToString(), out _totalLength);
                int.TryParse(jobj["hit_length"].ToString(), out _hitLength);
                int.TryParse(jobj["mode"].ToString(), out _mode);
                int.TryParse(jobj["count_normal"].ToString(), out _countNormal);
                int.TryParse(jobj["count_slider"].ToString(), out _countSlider);
                int.TryParse(jobj["count_spinner"].ToString(), out _countSpinner);
                int.TryParse(jobj["creator_id"].ToString(), out _creatorId);
                int.TryParse(jobj["genre_id"].ToString(), out _genreId);
                int.TryParse(jobj["language_id"].ToString(), out _languageId);
                int.TryParse(jobj["playcount"].ToString(), out _playcount);
                int.TryParse(jobj["passcount"].ToString(), out _passcount);
                int.TryParse(jobj["favourite_count"].ToString(), out _favouriteCount);
                int.TryParse(jobj["max_combo"].ToString(), out _maxCombo);
                double.TryParse(jobj["diff_overall"].ToString(), out _diffOverall);
                double.TryParse(jobj["diff_approach"].ToString(), out _diffApproach);
                double.TryParse(jobj["diff_drain"].ToString(), out _diffDrain);
                double.TryParse(jobj["diff_size"].ToString(), out _diffSize);
                double.TryParse(jobj["bpm"].ToString(), out _bpm);
                double.TryParse(jobj["rating"].ToString(), out _rating);
                double.TryParse(jobj["diff_speed"].ToString(), out _diffSpeed);
                double.TryParse(jobj["diff_aim"].ToString(), out _diffAim);
                double.TryParse(jobj["difficultyrating"].ToString(), out _difficultyrating);
                Version = jobj["version"].ToString();
                Md5 = jobj["file_md5"].ToString();
                _submitDate = jobj["submit_date"].ToString();
                _lastUpdate = jobj["last_update"].ToString();
                _approvedDate = jobj["approved_date"].ToString();
                Title = jobj["title"].ToString();
                Artist = jobj["artist"].ToString();
                Creator = jobj["creator"].ToString();
                Tags = jobj["tags"].ToString();
                Source = jobj["source"].ToString();
                Approved = (BeatmapStatus) _approved;
            }

            public string ToString(string format)
            {
                return ToString(format, null);
            }

            /// <summary>
            ///     返回基础信息
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return
                    $"{Artist} - {Title} [{Version}]\nMode:{Mode}\nStars:{_difficultyrating:f2} BPM:{_bpm}\nOD:{OverallDifficulty} ApproachRate:{ApproachRate} CircleSize:{CircleSize} HPDrain:{HpDrain}";
            }

            /// <summary>
            ///     将该OnlineBeatmap转化为<see cref="Beatmaps.Beatmap" />
            /// </summary>
            /// <returns></returns>
            public Beatmap ToBeatmap()
            {
                return new Beatmap(this);
            }
        }
    }
}