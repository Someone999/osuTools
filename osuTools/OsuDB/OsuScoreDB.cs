using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using osuTools.Game.Modes;
using osuTools.GameInfo;

namespace osuTools.OsuDB
{
    /// <summary>
    ///     从scores.db中读取成绩。
    /// </summary>
    public class OsuScoreDb : IOsuDb
    {
        private readonly int _beatmapnum;
        private readonly BinaryReader _reader;
        private readonly List<OsuScoreInfo> _score = new List<OsuScoreInfo>();

        /// <summary>
        /// 从score.db中获取数据
        /// </summary>
        public OsuScoreDb()
        {
            var info = new OsuInfo();
            var dbfile = info.OsuDirectory + "scores.db";
            var stream = File.OpenRead(dbfile);
            _reader = new BinaryReader(stream);
            Manifest.Version = _reader.ReadInt32();
            _beatmapnum = _reader.ReadInt32();
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
        public OsuScoreDb(string dbPath)
        {
            if (!File.Exists(dbPath))
                dbPath = Path.Combine(new OsuInfo().OsuDirectory , dbPath);
            var stream = File.OpenRead(dbPath);
            _reader = new BinaryReader(stream);
            Manifest.Version = _reader.ReadInt32();
            _beatmapnum = _reader.ReadInt32();
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
        public IReadOnlyList<OsuScoreInfo> Scores => _score.AsReadOnly();

        /// <summary>
        ///     scores,db中的头部数据
        /// </summary>
        public ScoreManifest Manifest { get; internal set; } = new ScoreManifest(-1);

        /// <summary>
        ///     从scores.db中读取
        /// </summary>
        public void Read()
        {
            for (var i = 0; i < _beatmapnum; i++)
            {
                GetString();
                var scorenum = _reader.ReadInt32();
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
                    var c300G = GetShort();
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
                            c300G, c200, cmiss, score, maxcombo, per, mods, emp, timestamp, veri, onlineid);
                        if (_score.Count > 0)
                        {
                            if (newscore.PlayTime != _score.Last().PlayTime)
                                _score.Add(newscore);
                        }
                        else
                        {
                            _score.Add(newscore);
                        }
                    }
                }
            }
        }

        private bool IsString()
        {
            _reader.ReadByte();
            return true;
        }

        private string GetString()
        {
            if (IsString()) return _reader.ReadString();
            return string.Empty;
        }

        private short GetShort()
        {
            var v = _reader.ReadInt16();
            // msgbox(v);
            return v;
        }

        private int GetInt32()
        {
            var v = _reader.ReadInt32();
            // msgbox(v);
            return v;
        }

        private long GetInt64()
        {
            var v = _reader.ReadInt64();
            // msgbox(v);
            return v;
        }

        private byte GetByte()
        {
            var v = _reader.ReadByte();
            //msgbox(v);
            return v;
        }

        private bool GetBool()
        {
            var v = _reader.ReadBoolean();
            //msgbox(v);
            return v;
        }

/*
        private double GetDouble()
        {
            var v = _reader.ReadDouble();
            //msgbox(v);
            return v;
        }
*/

        private string GetEmptyString()
        {
            var b = _reader.ReadByte();
            if (b == 0x0b) return _reader.ReadString();
            return "";
        }
    }
}