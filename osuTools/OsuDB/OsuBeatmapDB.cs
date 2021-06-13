using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using osuTools.Game.Modes;
using osuTools.GameInfo;
using osuTools.MD5Tools;

namespace osuTools.OsuDB
{
    /// <summary>
    ///     通过读取osu!.db获取所有的谱面以及一些游戏相关的信息。
    /// </summary>
    public class OsuBeatmapDB : IOsuDb
    {
        private readonly string f;

        private readonly BinaryReader reader;
        private bool readmanifest;

        /// <summary>
        ///     初始化一个OsuBeatmapDB对象
        /// </summary>
        public OsuBeatmapDB()
        {
            var info = new OsuInfo();
            var file = info.OsuDirectory + "osu!.db";
            var stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            reader = new BinaryReader(stream);

            f = file;
            //System.Windows.Forms.MessageBox.Show(f);
            MD5 = GetMD5();
            //Sync.Tools.IO.CurrentIO.Write(or.CurrentMode.ToString());
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
        public OsuBeatmapDB(string dbPath)
        {
            if (!File.Exists(dbPath))
                dbPath = Path.Combine(new OsuInfo().OsuDirectory, dbPath);
            var stream = File.Open(dbPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            reader = new BinaryReader(stream);
            f = dbPath;
            MD5 = GetMD5();
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
        ///     osu!的一些信息
        /// </summary>
        public OsuManifest Manifest { get; internal set; }

        /// <summary>
        ///     从osu!.db读取到的谱面
        /// </summary>
        public OsuBeatmapCollection Beatmaps { get; internal set; }

        /// <summary>
        ///     osu!.db的MD5
        /// </summary>
        public MD5String MD5 { get; internal set; }

        /// <summary>
        ///     手动从osu!.db读取信息，这将重新写入所有信息
        /// </summary>
        public void Read()
        {
            Manifest = new OsuManifest();
            Beatmaps = new OsuBeatmapCollection();
            if (!readmanifest) ReadManifest();
            GetAllBeatmaps();
        }

        private MD5String GetMD5()
        {
            var provider = new MD5CryptoServiceProvider();
            var data = File.ReadAllBytes(f);
            provider.ComputeHash(data);
            return new MD5String(provider);
        }

        private short GetInt16()
        {
            var v = reader.ReadInt16();
            return v;
        }

        private int GetInt32()
        {
            var v = reader.ReadInt32();
            return v;
        }

        private long GetInt64()
        {
            var v = reader.ReadInt64();
            return v;
        }

        private double GetDouble()
        {
            var v = reader.ReadDouble();
            return v;
        }

        private float GetSingle()
        {
            var v = reader.ReadSingle();
            return v;
        }

        private byte GetByte()
        {
            var v = reader.ReadByte();
            return v;
        }

        private bool GetBoolean()
        {
            var v = reader.ReadBoolean();
            return v;
        }

        private string GetString()
        {
            if (reader.ReadByte() == 0x0b)
            {
                var v = reader.ReadString();
                return v;
            }

            return string.Empty;
        }

        private void ReadManifest()
        {
            Manifest.Version = GetInt32();
            Manifest.FolderCount = GetInt32();
            Manifest.AccountUnlocked = GetBoolean();
            Manifest.AccountUnlockTime = new DateTime(GetInt64());
            Manifest.PlayerName = GetString();
            Manifest.NumberOfBeatmap = GetInt32();
            readmanifest = true;
        }

        private OsuBeatmap ReadBeatmap()
        {
            var osustars = new Dictionary<int, double>();
            var taikostars = new Dictionary<int, double>();
            var ctbstars = new Dictionary<int, double>();
            var maniastars = new Dictionary<int, double>();
            var beatmap = new OsuBeatmap
            {
                Artist = GetString(),
                ArtistUnicode = GetString(),
                Title = GetString(),
                TitleUnicode = GetString(),
                Creator = GetString(),
                Difficulty = GetString(),
                AudioFileName = GetString(),
                Md5 = GetString(),
                FileName = GetString()
            };
            try
            {
                beatmap.BeatmapStatus = (OsuBeatmapStatus) Enum.Parse(typeof(OsuBeatmapStatus), GetByte().ToString());
            }
            catch
            {
                beatmap.BeatmapStatus = OsuBeatmapStatus.Unknown;
            }

            beatmap.HitCircle = GetInt16();
            beatmap.Slider = GetInt16();
            beatmap.Spinner = GetInt16();
            beatmap.LastModificationTime = new DateTime(GetInt64());
            beatmap.ApproachRate = GetSingle();
            beatmap.CircleSize = GetSingle();
            beatmap.HpDrain = GetSingle();
            beatmap.OverallDifficulty = GetSingle();
            GetDouble();
            var pac = GetInt32();
            var intlst = new List<int>();
            for (var i = 0; i < pac; i++)
            {
                GetByte();
                var intflag = GetInt32();
                GetByte();
                var doubleflag = GetDouble();
                osustars.Add(intflag, doubleflag);
            }

            pac = GetInt32();
            for (var i = 0; i < pac; i++)
            {
                GetByte();
                var intflag = GetInt32();
                GetByte();
                var doubleflag = GetDouble();
                taikostars.Add(intflag, doubleflag);
            }

            pac = GetInt32();
            for (var i = 0; i < pac; i++)
            {
                GetByte();
                var intflag = GetInt32();
                GetByte();
                var doubleflag = GetDouble();
                ctbstars.Add(intflag, doubleflag);
            }

            pac = GetInt32();
            for (var i = 0; i < pac; i++)
            {
                GetByte();
                var intflag = GetInt32();
                GetByte();
                var doubleflag = GetDouble();
                if (intlst.Contains(intflag)) throw new Exception();
                intlst.Add(intflag);
                maniastars.Add(intflag, doubleflag);
            }

            beatmap.DrainTime = TimeSpan.FromSeconds(GetInt32());
            beatmap.TotalTime = TimeSpan.FromMilliseconds(GetInt32());
            beatmap.PreviewPoint = TimeSpan.FromMilliseconds(GetInt32());
            pac = GetInt32();
            for (var i = 0; i < pac; i++)
            {
                var bpm = GetDouble();
                var offset = GetDouble();
                var inherit = GetBoolean();
                beatmap.Timepoints.Add(new OsuBeatmapTimePoint(bpm, offset, inherit));
            }

            beatmap.BeatmapId = GetInt32();
            beatmap.BeatmapSetId = GetInt32();
            beatmap.ThreadId = GetInt32();
            GetByte();
            GetByte();
            GetByte();
            GetByte();
            GetInt16();
            GetSingle();
            beatmap.Mode = (OsuGameMode) GetByte();
            if (osustars.Count == 0)
                osustars.Add(0, 0);
            if (taikostars.Count == 0)
                taikostars.Add(0, 0);
            if (ctbstars.Count == 0)
                ctbstars.Add(0, 0);
            if (maniastars.Count == 0)
                maniastars.Add(0, 0);
            beatmap.ModStarPair.SetModeDict(OsuGameMode.Osu, osustars);
            beatmap.ModStarPair.SetModeDict(OsuGameMode.Taiko, taikostars);
            beatmap.ModStarPair.SetModeDict(OsuGameMode.Catch, ctbstars);
            beatmap.ModStarPair.SetModeDict(OsuGameMode.Mania, maniastars);

            beatmap.Source = GetString();
            beatmap.Tags = GetString();
            GetInt16();
            GetString();
            GetBoolean();
            GetInt64();
            GetBoolean();
            beatmap.FolderName = GetString();
            GetInt64();
            GetBoolean();
            GetBoolean();
            GetBoolean();
            GetBoolean();
            GetBoolean();
            GetInt32();
            GetByte();
            try
            {
                beatmap.Stars = beatmap.ModStarPair[beatmap.Mode][0];
            }
            catch
            {
                beatmap.Stars = 0;
                Debug.WriteLine("Error when reading stars return beatmap with 0 star.");
                return beatmap;
            }
            return beatmap;
        }

        private void GetAllBeatmaps()
        {
            var i = Manifest.NumberOfBeatmap;
            var beatmaps = new OsuBeatmapCollection();
            for (var j = 0; j < i; j++)
            {
                var newbeatmap = ReadBeatmap();
                if (newbeatmap.Title != "" && newbeatmap.Artist != "")
                    beatmaps.Add(newbeatmap);
            }

            Beatmaps = beatmaps;
            reader.Close();
        }
    }
}