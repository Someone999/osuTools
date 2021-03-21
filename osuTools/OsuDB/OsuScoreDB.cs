using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace osuTools.OsuDB
{
    /// <summary>
    ///     从scores.db中读取成绩。
    /// </summary>
    public class OsuScoreDB : IOsuDB
    {
        private readonly int beatmapnum;
        private readonly BinaryReader reader;
        private readonly List<OsuScoreInfo> Score = new List<OsuScoreInfo>();

        /// <summary>
        /// 从score.db中获取数据
        /// </summary>
        public OsuScoreDB()
        {
            var info = new OsuInfo();
            var dbfile = info.OsuDirectory + "scores.db";
            var stream = File.OpenRead(dbfile);
            reader = new BinaryReader(stream);
            Manifest.Version = reader.ReadInt32();
            beatmapnum = reader.ReadInt32();
            try
            {
                Read();
            }
            catch (Exception e)
            {
                Console.WriteLine($"读取时发生错误，请检查文件格式是否正确: {e.Message}");
            }
        }
        /// <summary>
        /// 从指定的文件中读取数据
        /// </summary>
        /// <param name="dbPath">文件的绝对路径或相对于osu!游戏文件夹的路径</param>
        public OsuScoreDB(string dbPath)
        {
            if (!File.Exists(dbPath))
                dbPath = Path.Combine(new OsuInfo().OsuDirectory , dbPath);
            var stream = File.OpenRead(dbPath);
            reader = new BinaryReader(stream);
            Manifest.Version = reader.ReadInt32();
            beatmapnum = reader.ReadInt32();
            try
            {
                Read();
            }
            catch (Exception e)
            {
                Console.WriteLine($"读取时发生错误，请检查文件格式是否正确: {e.Message}");
            }

        }

        /// <summary>
        ///     存储的分数
        /// </summary>
        public IReadOnlyList<OsuScoreInfo> Scores => Score.AsReadOnly();

        /// <summary>
        ///     scores,db中的头部数据
        /// </summary>
        public ScoreManifest Manifest { get; internal set; } = new ScoreManifest(-1);

        /// <summary>
        ///     从scores.db中读取
        /// </summary>
        public void Read()
        {
            var x = 0;
            for (var i = 0; i < beatmapnum; i++)
            {
                var curmd5 = "";
                curmd5 = GetString();
                var scorenum = reader.ReadInt32();
                for (var j = 0; j < scorenum; j++)
                {
                    var mode = (OsuGameMode) GetByte();
                    var ver = GetInt32();
                    var beatmapmd5 = GetString();
                    var playername = GetString();
                    var replaymd5 = GetString();
                    var c300 = GetShort();
                    var c100 = GetShort();
                    var c50 = GetShort();
                    var c300g = GetShort();
                    var c200 = GetShort();
                    var cmiss = GetShort();
                    var score = GetInt32();
                    var maxcombo = GetShort();
                    var per = GetBool();
                    var mods = GetInt32();
                    var emp = GetEmptyString();
                    var timestamp = GetInt64();
                    var veri = GetInt32();
                    var onlineid = GetInt64();
                    if (c300 + c100 + c50 + cmiss != 0)
                    {
                        var newscore = new OsuScoreInfo(mode, ver, beatmapmd5, playername, replaymd5, c300, c100, c50,
                            c300g, c200, cmiss, score, maxcombo, per, mods, emp, timestamp, veri, onlineid,
                            0);
                        if (Score.Count > 0)
                        {
                            if (newscore.PlayTime != Score.Last().PlayTime)
                                Score.Add(newscore);
                        }
                        else
                        {
                            Score.Add(newscore);
                        }
                    }
                    else
                    {
                        x++;
                    }
                }
            }
        }

        private bool IsString()
        {
            reader.ReadByte();
            return true;

            //else if (reader.ReadByte() == 0x0b) return true;
            // else throw new osuToolsException.FailToParse("无法读取字符串。");
        }

        private DialogResult msgbox(object o)
        {
            return MessageBox.Show(o.ToString());
        }

        private string GetString()
        {
            if (IsString()) return reader.ReadString();
            return string.Empty;
        }

        private short GetShort()
        {
            var v = reader.ReadInt16();
            // msgbox(v);
            return v;
        }

        private int GetInt32()
        {
            var v = reader.ReadInt32();
            // msgbox(v);
            return v;
        }

        private long GetInt64()
        {
            var v = reader.ReadInt64();
            // msgbox(v);
            return v;
        }

        private byte GetByte()
        {
            var v = reader.ReadByte();
            //msgbox(v);
            return v;
        }

        private bool GetBool()
        {
            var v = reader.ReadBoolean();
            //msgbox(v);
            return v;
        }

        private double GetDouble()
        {
            var v = reader.ReadDouble();
            //msgbox(v);
            return v;
        }

        private string GetEmptyString()
        {
            var b = reader.ReadByte();
            if (b == 0x0b) return reader.ReadString();
            //System.Windows.Forms.MessageBox.Show(reader.ReadByte().ToString());
            return "";
        }
    }
}