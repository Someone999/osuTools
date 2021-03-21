using System;
using System.Collections.Generic;
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
        ///     在线获取的谱面的集合
        /// </summary>
        public class OnlineBeatmapCollection : OnlineInfo<OnlineBeatmap>
        {
            /// <summary>
            ///     指示本次查询是否失败
            /// </summary>
            public bool Failed { get; internal set; } = false;

            /// <summary>
            ///     使用整数索引从列表获取OnlineBeatmap
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            public OnlineBeatmap this[int x]
            {
                get => Beatmaps[x];
                set => Beatmaps[x] = value;
            }

            /// <summary>
            ///     存储的<see cref="OnlineBeatmap" />
            /// </summary>
            public List<OnlineBeatmap> Beatmaps { get; } = new List<OnlineBeatmap>();

            /// <summary>
            ///     存储的谱面的数量
            /// </summary>
            public int Count => Beatmaps.Count;

            /// <summary>
            ///     通过关键词搜索谱面
            /// </summary>
            /// <param name="keyword"></param>
            /// <returns></returns>
            public OnlineBeatmapCollection Find(string keyword)
            {
                var bc = new OnlineBeatmapCollection();
                foreach (var beat in Beatmaps)
                    if (beat.ToBeatmap().ToString().Trim().ToUpper().Contains(keyword.ToUpper().Trim()) ||
                        beat.ToBeatmap().Source.Trim().ToUpper().Contains(keyword.ToUpper().Trim()) ||
                        beat.ToBeatmap().Tags.Trim().ToUpper().Contains(keyword.ToUpper().Trim()))
                        bc.Beatmaps.Add(beat);
                return bc;
            }

            /// <summary>
            ///     判断列表中是否包含指定谱面
            /// </summary>
            /// <param name="b"></param>
            /// <returns></returns>
            public bool Contains(OnlineBeatmap b)
            {
                return Beatmaps.Contains(b);
            }

            /// <summary>
            ///     返回循环访问List的枚举数
            /// </summary>
            /// <returns></returns>
            public IEnumerator<OnlineBeatmap> GetEnumerator()
            {
                return Beatmaps.GetEnumerator();
            }
        }

        /// <summary>
        ///     在线获取的谱面
        /// </summary>
        [Serializable]
        public partial class OnlineBeatmap : IEquatable<OnlineBeatmap>, IFormattable, IJsonSerilizable
        {
            private int approved = -1;

            private string approved_date = "0-0-0 0:0:0";

            private int audio_unavailable = 0;

            private int beatmap_id = -2;
            private int beatmapset_id = -2;

            private double bpm;

            private int count_normal = -1;

            private int count_slider = -1;

            private int count_spinner = -1;

            private int creator_id;

            private double diff_aim;

            private double diff_approach = -1;

            private double diff_drain = -1;
            private double diff_overall = -1;
            private double diff_size = -1;

            private double diff_speed;

            private double difficultyrating;

            private int download_unavailable = 0;

            private int favourite_count;

            private int genre_id;
            private int hit_length = -1;
            private JObject jobj = new JObject();

            private int language_id;

            private string last_update = "0-0-0 0:0:0";

            private int max_combo;

            private int mode = -1;

            private int passcount;

            private int playcount;

            private double rating;

            private string submit_date = "0-0-0 0:0:0";
            private int total_length = -1;

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
                if (MD5 == other.MD5)
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
                b.Replace("bpm", BPM.ToString());
                b.Replace("stars", Stars.ToString());
                b.Replace("mode", Mode.ToString());
                b.Replace("cs", CS.ToString());
                b.Replace("ar", AR.ToString());
                b.Replace("hp", HP.ToString());
                b.Replace("od", OD.ToString());
                b.Replace("circles", HitCircle.ToString());
                b.Replace("sliders", Slider.ToString());
                b.Replace("spiners", Spinner.ToString());
                b.Replace("creator", Creator);
                b.Replace("Title", Title);
                b.Replace("title", Title);
                b.Replace("Tags", Tags);
                b.Replace("tags", Tags);
                b.Replace("setid", BeatmapSetID.ToString());
                b.Replace("maxcombo", MaxCombo.ToString());
                b.Replace("approved", Approved.ToString());
                var total = TimeSpan.FromSeconds(TotalTime);
                var hit = TimeSpan.FromSeconds(DrainTime);
                b.Replace("length", $"{total.TotalMinutes}:{total.Seconds}");
                b.Replace("hitlength", $"{hit.TotalMinutes}:{hit.Seconds}");
                b.Replace("version", Version);
                b.Replace("md5", MD5);
                b.Replace("id", BeatmapID.ToString());
                return b.ToString();
            }

            /// <summary>
            ///     将对象序列化
            /// </summary>
            /// <param name="file"></param>
            public void Serialize(string file)
            {
                if (string.IsNullOrEmpty(file))
                    throw new ArgumentNullException("文件名不能为空。");
                var dir = Path.GetDirectoryName(file);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllText(file, jobj.ToString());
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
                if (lob.BeatmapID == 0 || lob.BeatmapID == -1) throw new NotSupportedException();
                if (olb.BeatmapID == lob.BeatmapID && lob.BeatmapID != 0 && lob.BeatmapID != -1) return true;
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
                if (lob.BeatmapID == 0 || lob.BeatmapID == -1) throw new NotSupportedException();
                if (olb.BeatmapID == lob.BeatmapID && lob.BeatmapID != 0 && lob.BeatmapID != -1) return false;
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
                if (olb.BeatmapID == lob.BeatmapID && lob.BeatmapID != 0 && lob.BeatmapID != -1) return true;
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
                if (olb.BeatmapID == lob.BeatmapID && lob.BeatmapID != 0 && lob.BeatmapID != -1) return false;
                return true;
            }

            /// <summary>
            ///     该谱面对象是否为空
            /// </summary>
            /// <returns></returns>
            public bool IsEmpty()
            {
                if (MD5 == "0")
                    return true;
                return false;
            }

            private void Parse(JObject jobj)
            {
                this.jobj = jobj;
                int.TryParse(jobj["beatmapset_id"].ToString(), out beatmapset_id);
                int.TryParse(jobj["beatmap_id"].ToString(), out beatmap_id);
                int.TryParse(jobj["approved"].ToString(), out approved);
                int.TryParse(jobj["total_length"].ToString(), out total_length);
                int.TryParse(jobj["hit_length"].ToString(), out hit_length);
                int.TryParse(jobj["mode"].ToString(), out mode);
                int.TryParse(jobj["count_normal"].ToString(), out count_normal);
                int.TryParse(jobj["count_slider"].ToString(), out count_slider);
                int.TryParse(jobj["count_spinner"].ToString(), out count_spinner);
                int.TryParse(jobj["creator_id"].ToString(), out creator_id);
                int.TryParse(jobj["genre_id"].ToString(), out genre_id);
                int.TryParse(jobj["language_id"].ToString(), out language_id);
                int.TryParse(jobj["playcount"].ToString(), out playcount);
                int.TryParse(jobj["passcount"].ToString(), out passcount);
                int.TryParse(jobj["favourite_count"].ToString(), out favourite_count);
                int.TryParse(jobj["max_combo"].ToString(), out max_combo);
                double.TryParse(jobj["diff_overall"].ToString(), out diff_overall);
                double.TryParse(jobj["diff_approach"].ToString(), out diff_approach);
                double.TryParse(jobj["diff_drain"].ToString(), out diff_drain);
                double.TryParse(jobj["diff_size"].ToString(), out diff_size);
                double.TryParse(jobj["bpm"].ToString(), out bpm);
                double.TryParse(jobj["rating"].ToString(), out rating);
                double.TryParse(jobj["diff_speed"].ToString(), out diff_speed);
                double.TryParse(jobj["diff_aim"].ToString(), out diff_aim);
                double.TryParse(jobj["difficultyrating"].ToString(), out difficultyrating);
                Version = jobj["version"].ToString();
                MD5 = jobj["file_md5"].ToString();
                submit_date = jobj["submit_date"].ToString();
                last_update = jobj["last_update"].ToString();
                approved_date = jobj["approved_date"].ToString();
                Title = jobj["title"].ToString();
                Artist = jobj["artist"].ToString();
                Creator = jobj["creator"].ToString();
                Tags = jobj["tags"].ToString();
                Source = jobj["source"].ToString();
                Approved = (BeatmapStatus) approved;
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
                    $"{Artist} - {Title} [{Version}]\nMode:{Mode}\nStars:{difficultyrating:f2} BPM:{bpm}\nOD:{OD} AR:{AR} CS:{CS} HP:{HP}";
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