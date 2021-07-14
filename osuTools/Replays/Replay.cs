using System;
using System.Collections.Generic;
using System.IO;
using osuTools.Beatmaps.HitObject;
using osuTools.Exceptions;
using osuTools.Game;
using osuTools.Game.Modes;
using osuTools.Game.Mods;
using osuTools.OsuDB;
using osuTools.Replays.AdditionalInfo;

namespace osuTools.Replays
{
    public partial class Replay
    {
        private short _countGeki, _count300, _countKatu, _count100, _count50, _countMiss, _maxco;
        private readonly FileInfo _info;
        private readonly bool _infonovalue;
        private byte _mode, _per;
        private readonly BinaryReader _r;

        /// <summary>
        ///     使用录像文件构造一个Replay对象
        /// </summary>
        /// <param name="dir"></param>
        public Replay(string dir)
        {
            {
                _r = new BinaryReader(File.OpenRead(dir));
            }
            _info = new FileInfo(dir);
            Read();
        }

        /// <summary>
        ///     使用录像文件的二进制流构造一个Replay对象，并指定录像的全路径
        /// </summary>
        /// <param name="b"></param>
        /// <param name="dir"></param>
        public Replay(BinaryReader b, string dir)
        {
            _r = b ?? throw new NullReferenceException();
            _info = new FileInfo(dir);
            Read();
        }

        /// <summary>
        ///     使用录像文件的二进制流构造一个Replay对象
        /// </summary>
        /// <param name="b"></param>
        public Replay(BinaryReader b)
        {
            _r = b ?? throw new NullReferenceException();
            _info = null;
            _infonovalue = true;
            Read();
        }

        /// <summary>
        ///     录像对应的游玩记录使用Mods
        /// </summary>
        public ModList Mods { get; private set; } = new ModList();

        /// <summary>
        ///     附加的录像数据
        /// </summary>
        public AdditionalRepalyData AdditionalData { get; private set; }

        /// <summary>
        ///     游玩时间
        /// </summary>
        public DateTime PlayTime { get; private set; }

        /// <summary>
        ///     录像文件的文件信息
        /// </summary>
        public FileInfo ReplayFile
        {
            get
            {
                if (_infonovalue)
                    throw new InvalidOperationException("当前的构造函数没有为FileInfo赋值");
                return _info;
            }
        }

        private void Read()
        {
            var lfbar = "";
            _mode = _r.ReadByte();
            Mode = (OsuGameMode) _mode;
            OsuVersion = _r.ReadInt32();
            _r.ReadByte();
            BeatmapMd5 = _r.ReadString();
            _r.ReadByte();
            Player = _r.ReadString();
            _r.ReadByte();
            ReplayMd5 = _r.ReadString();
            _count300 = _r.ReadInt16();
            _count100 = _r.ReadInt16();
            _count50 = _r.ReadInt16();
            _countGeki = _r.ReadInt16();
            _countKatu = _r.ReadInt16();
            _countMiss = _r.ReadInt16();
            Accuracy = GameMode.FromLegacyMode(Mode).AccuracyCalc(
                new ScoreInfo
                {
                    CountGeki = _countGeki,
                    Count300 = Count300,
                    CountKatu = CountKatu,
                    Count100 = Count100,
                    Count50 = Count50,
                    CountMiss = cMiss
                });
            AccuracyStr = Accuracy.ToString("p");
            Score = _r.ReadInt32();
            _maxco = _r.ReadInt16();
            _per = _r.ReadByte();
            Perfect = _per == 1;
            Mods = ModList.FromInteger(_r.ReadInt32(),Mode);
            if (_r.ReadByte() == 0x0b)
                lfbar = _r.ReadString();
            PlayTime = new DateTime(_r.ReadInt64());
            var datalen = _r.ReadInt32();
            var data = _r.ReadBytes(datalen);
            AdditionalData = new AdditionalRepalyData(data, datalen, lfbar);
            _r.ReadDouble();
        }
        /// <summary>
        /// 判断Replay中的所有判定信息和指定<seealso cref="OsuScoreInfo"/>中的是否相等
        /// </summary>
        /// <param name="replay"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool operator ==(Replay replay, OsuScoreInfo score)
        {
            if (score is null && replay is null)
                return true;
            if (score is null || replay is null)
                return false;
            return replay.ReplayMd5 == score.ReplayMd5;
        }
        /// <summary>
        /// 判断Replay中的所有判定信息和指定<seealso cref="OsuScoreInfo"/>中的是否相等
        /// </summary>
        /// <param name="replay"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool operator !=(Replay replay, OsuScoreInfo score)
        {
            if (score is null && replay is null)
                return false;
            if (score is null || replay is null)
                return true;
            return replay.ReplayMd5 != score.ReplayMd5;
        }
        /// <summary>
        /// 判断Replay中的所有判定信息和指定<seealso cref="OsuScoreInfo"/>中的是否相等
        /// </summary>
        /// <param name="replay"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool operator ==(OsuScoreInfo score, Replay replay)
        {
            if (score is null && replay is null)
                return false;
            if (score is null || replay is null)
                return true;
            return replay.ReplayMd5 == score.ReplayMd5;
        }
        /// <summary>
        /// 判断Replay中的所有判定信息和指定<seealso cref="OsuScoreInfo"/>中的是否相等
        /// </summary>
        /// <param name="replay"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool operator !=(OsuScoreInfo score, Replay replay)
        {
            if (score is null && replay is null)
                return false;
            if (score is null || replay is null)
                return true;
            return replay.ReplayMd5 != score.ReplayMd5;
        }
        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Replay r)
                return r.ReplayMd5 == ReplayMd5;
            return false;
        }


        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return ReplayMd5.GetHashCode();
        }
    }
}