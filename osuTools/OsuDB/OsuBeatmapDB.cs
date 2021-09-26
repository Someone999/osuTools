using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using osuTools.Game.Modes;
using osuTools.GameInfo;
using osuTools.MD5Tools;
using osuTools.OsuDB.Beatmap;
using osuTools.OsuDB.Interface;

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
            Md5 = GetMd5();
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
            Md5 = GetMd5();
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
        public MD5String Md5 { get; internal set; }

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

        private MD5String GetMd5()
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
            //osu!版本
            //osu! version.
            Manifest.Version = GetInt32();
            //谱面文件夹数目
            //The count of the beatmap folder.
            Manifest.FolderCount = GetInt32();
            //账户是否解锁，仅在账户被锁定或封禁时为false.
            //Is account unlocked.Only be false when the account is locked or banned in any way
            Manifest.AccountUnlocked = GetBoolean();
            //账户解锁的时间
            //Date the account will be unlocked
            Manifest.AccountUnlockTime = new DateTime(GetInt64());
            //玩家名称
            //The nickname of the current logon account.
            Manifest.PlayerName = GetString();
            //谱面的数目
            //Number of the beatmap.
            Manifest.NumberOfBeatmap = GetInt32();
            readmanifest = true;
        }

        private OsuBeatmap ReadBeatmap()
        {
            var osustars = new Dictionary<int, double>();
            var taikostars = new Dictionary<int, double>();
            var ctbstars = new Dictionary<int, double>();
            var maniastars = new Dictionary<int, double>();
            if (Manifest.Version < 20191106)
            {
                //谱面条目的大小
                //The size of the entry of the beatmap in byte
                GetInt32();
            }
            var beatmap = new OsuBeatmap
            {
                //曲目的作者
                //Artist of the audio of the beatmap
                Artist = GetString(),
                //曲目作者的原语言
                //"Artist" in native language
                ArtistUnicode = GetString(),
                //谱面的标题
                //Title of the beatmap
                Title = GetString(),
                //谱面标题的原语言
                //"Title" in native language
                TitleUnicode = GetString(),
                //谱面的作者
                //Author of the beatmap
                Creator = GetString(),
                //谱面的难度
                //Difficulty of beatmap
                Difficulty = GetString(),
                //谱面音频文件的名字，可能指示不存在的文件
                //The audio file name of the beatmap. Not existing file may be indicated.
                AudioFileName = GetString(),
                //谱面的Md5
                //Md5 hash of the beatmap
                Md5 = GetString(),
                //谱面文件的名称
                //The name of beatmap file
                FileName = GetString()
            };
            try
            {
                //谱面的状态（Ranked，Loved，Pending等）
                //Status of beatmap like Ranked, Loved, Pending, etc.
                beatmap.BeatmapStatus = (OsuBeatmapStatus) Enum.Parse(typeof(OsuBeatmapStatus), GetByte().ToString());
            }
            catch
            {
                //谱面状态读取失败时为未知状态
                //The unknown will be assigned when failed to read
                beatmap.BeatmapStatus = OsuBeatmapStatus.Unknown;
            }
            //圈圈的数量
            //The number of HitCircle
            beatmap.HitCircle = GetInt16();
            //滑条的数量
            //The number of slider
            beatmap.Slider = GetInt16();
            //转盘的数量
            //The number of spinners.
            beatmap.Spinner = GetInt16();
            //谱面上一次修改的时间，单位为Tick
            //The time when the last modification of the beatmap in Ticks
            beatmap.LastModificationTime = new DateTime(GetInt64());
            //谱面的缩圈速度
            //The speed of the narrow of the approaching circle
            beatmap.ApproachRate = GetSingle();
            //谱面的圈圈的大小
            //The size of the notes of the beatmap
            beatmap.CircleSize = GetSingle();
            //谱面游玩时掉血的速度和回血的难度
            //The speed of HP reducing and the difficulty of HP raising
            beatmap.HpDrain = GetSingle();
            //谱面的综合难度（如判定难度）
            //Overall difficulty of beatmap like HitWindow.
            beatmap.OverallDifficulty = GetSingle();
            //滑条速度
            //Slider velocity
            GetDouble();




            if (Manifest.Version >= 20140609)
            {
                //获取std模式的Mod<->难度星级的键值对
                //Get the key-value pairs of Mod and Stars in std mode
                GetModStarsPair(reader, osustars, GetInt32());

                //获取Taiko模式的Mod<->难度星级的键值对
                //Get the key-value pairs of Mod and Stars in Taiko mode
                GetModStarsPair(reader,taikostars,GetInt32());

                //获取CTB模式的Mod<->难度星级的键值对
                //Get the key-value pairs of Mod and Stars in CTB mode
                GetModStarsPair(reader,ctbstars,GetInt32());

                //获取Mania模式的Mod<->难度星级的键值对
                //Get the key-value pairs of Mod and Stars in Mania mode
                GetModStarsPair(reader,maniastars,GetInt32());
            }

            //谱面最后一个打击物件的偏移
            //The offset of the last HitObject
            beatmap.DrainTime = TimeSpan.FromSeconds(GetInt32());
            //谱面的总长度
            //The total time of beatmap
            beatmap.TotalTime = TimeSpan.FromMilliseconds(GetInt32());
            //谱面预览点的偏移
            //The offset of the preview point
            beatmap.PreviewPoint = TimeSpan.FromMilliseconds(GetInt32());
            //获取谱面OsuTimingPoint
            //Get the OsuTimingPoints of the beatmap.
            GetTimingPoints(reader,beatmap.TimingPoints, GetInt32());
            //谱面的Id
            //Id of the beatmap
            beatmap.BeatmapId = GetInt32();
            //谱面集的Id
            //Id of beatmap set
            beatmap.BeatmapSetId = GetInt32();
            //谱面的帖子Id
            //The id of the thread of the beatmap
            beatmap.ThreadId = GetInt32();
            //谱面在std模式的通过数
            //Grade achieved at the beatmap in std mode
            GetByte();
            //谱面在Taiko模式的通过数
            //Grade achieved at the beatmap in Taiko mode
            GetByte();
            //谱面在CTB模式的通过数
            //Grade achieved at the beatmap in CTB mode
            GetByte();
            //谱面在Mania模式的通过数
            //Grade achieved at the beatmap in Mania mode
            GetByte();
            //本地的谱面偏移
            //Local beatmap offset
            GetInt16();
            //堆叠系数
            //Stack leniency
            GetSingle();
            //游戏模式
            //Game mode
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
            //谱面曲目的来源
            //The source of the audio of the beatmap
            beatmap.Source = GetString();
            //谱面的标签
            //The tags of the beatmap
            beatmap.Tags = GetString();
            //在线存储的偏移量
            //The offset stored at online
            GetInt16();
            //歌曲标题使用的字体
            //The font used for the title of the beatmap
            GetString();
            //这个谱面是否没玩过
            //Is thie beatmap unplayed
            GetBoolean();
            //谱面上次的游玩时间
            //Last time when the beatmap played
            GetInt64();
            //谱面是否为osz2
            //Is the beatmap osz2
            GetBoolean();
            //谱面所在的文件夹名
            //The folder name that the beatmap in
            beatmap.FolderName = GetString();
            //谱面上次与osu!仓库核对时的时间
            //Last time when beatmap was checked against osu! repository
            GetInt64();
            //是否不使用谱面的音效
            //Are the sounds of the beatmap ignored
            GetBoolean();
            //是否不使用谱面的皮肤
            //Is the skin of the beatmap ignored
            GetBoolean();
            //是否禁用了StoryBoard
            //Is the StoryBoard of the beatmap disabled
            GetBoolean();
            //是否禁用了视频
            //Is the vedio of the beatmap disabled
            GetBoolean();
            //是否改动可视化设置
            //If visual overrided
            GetBoolean();
            if (Manifest.Version < 20140609)
            {
                //未知用途，可能不存在
                //Unknown usage. May be null
                GetInt16();
            }
            //疑为上次修改时间
            //May be last modification time
            GetInt32();
            //Mania的下落速度
            //Scrolling speed of mania mode
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
                var newBeatmap = ReadBeatmap();
                if (newBeatmap.Title != "" && newBeatmap.Artist != "")
                    beatmaps.Add(newBeatmap);
            }
            Beatmaps = beatmaps;
            reader.Close();
        }

        private void GetModStarsPair(BinaryReader reader, Dictionary<int, double> dict,int countToRead)
        {
            for (var i = 0; i < countToRead; i++)
            {
                //分隔符
                //Separator
                GetByte();
                //Mod
                var mod = GetInt32();
                //分隔符
                //Separator
                GetByte();
                //难度星级
                //Stars
                var stars = GetDouble();
                dict.Add(mod, stars);
            }
        }

        private void GetTimingPoints(BinaryReader reader, List<OsuBeatmapTimingPoint> timingPotins, int countToRead)
        {
            for (var i = 0; i < countToRead; i++)
            {
                //该OsuTimingPoint的BPM
                //The BPM of this OsuTimingPoint
                var bpm = GetDouble();
                //该OsuTimingPoint的偏移
                //The offset of this OsuTimingPoint
                var offset = GetDouble();
                //这个OsuTimingPoint是否为继承（是否为绿线）
                //If this OsuTimingPoint inherited (Is this line a green line)
                var inherit = GetBoolean();
                timingPotins.Add(new OsuBeatmapTimingPoint(bpm, offset, inherit));
            }
        }

    }
}