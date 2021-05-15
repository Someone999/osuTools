using System;
using System.Collections.Generic;
using System.IO;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Modes;
using osuTools.Game.Mods;
using osuTools.OsuDB;
using osuTools.Replays.AdditionalInfo;
using osuTools.Replays.Exception;

namespace osuTools.Replays
{
    public partial class Replay
    {
        private short _c300g, _c300, _c200, _c100, _c50, _cmiss, _maxco;
        private readonly FileInfo _info;
        private readonly bool _infonovalue;
        private byte _mode, _per, _flag;
        private List<OsuGameMod> _mods = new List<OsuGameMod>();
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
            if (b is null)
                throw new NullReferenceException();
            _r = b;
            _info = null;
            _infonovalue = true;
            Read();
        }

        /// <summary>
        ///     录像对应的游玩记录使用Mods
        /// </summary>
        public IReadOnlyList<OsuGameMod> Mods => _mods.AsReadOnly();

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
                    throw new FileInfoHasNoValue("当前的构造函数没有为FileInfo赋值");
                return _info;
            }
        }

        private void Read()
        {
            var lfbar = "";
            _mode = _r.ReadByte();
            Mode = (OsuGameMode) _mode;
            OsuVersion = _r.ReadInt32();
            _flag = _r.ReadByte();
            BeatmapMd5 = _r.ReadString();
            _flag = _r.ReadByte();
            Player = _r.ReadString();
            _flag = _r.ReadByte();
            ReplayMd5 = _r.ReadString();
            _c300 = _r.ReadInt16();
            _c100 = _r.ReadInt16();
            _c50 = _r.ReadInt16();
            _c300g = _r.ReadInt16();
            _c200 = _r.ReadInt16();
            _cmiss = _r.ReadInt16();
            Accuracy = GameMode.FromLegacyMode(Mode).AccuracyCalc(
                new ScoreInfo
                {
                    CountGeki = _c300g,
                    Count300 = c300,
                    CountKatu = c200,
                    Count100 = c100,
                    Count50 = c50,
                    CountMiss = cMiss
                });
            AccuracyStr = Accuracy.ToString("p");
            Score = _r.ReadInt32();
            _maxco = _r.ReadInt16();
            _per = _r.ReadByte();
            Perfect = _per == 1;
            _mods = HitObjectTools.GetGenericTypesByInt<OsuGameMod>(_r.ReadInt32());
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
            return replay.ReplayMd5 == score.ReplayMD5;
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
            return replay.ReplayMd5 != score.ReplayMD5;
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
            return replay.ReplayMd5 == score.ReplayMD5;
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
            return replay.ReplayMd5 != score.ReplayMD5;
        }
        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Replay r)
                return r.ReplayMd5 == ReplayMd5;
            return false;
        }



        private double AccCalculater(OsuGameMode mode)
        {
            double a200 = 2.0 / 3, a100 = 1.0 / 3, a50 = 1.0 / 6, a150 = 1.0 / 2;
            var maniaAllHit = _c300g + _c300 + _c200 + _c100 + _c50 + cMiss;
            var osuAllHit = _c300 + _c100 + _c50 + _cmiss;
            var ctbAllHit = _c50 + _c100 + _c300 + _cmiss + _c200;
            var taikoAllHit = _c300 + _c100 + _cmiss;
            double ma300C = c300 + _c300g,
                a300C = c300,
                a200C = c200 * a200,
                a100C = c100 * a100,
                a50C = c50 * a50,
                a150C = c100 * a150;
            double osuValue = a300C + a100C + a50C,
                taikoValue = a300C + a150C,
                ctbValue = c300 + c50 + c100,
                maniaValue = ma300C + a200C + a100C + a50C,
                invalidValue = -1;
            if (Mode == OsuGameMode.Osu) return osuValue / osuAllHit;
            if (Mode == OsuGameMode.Catch) return ctbValue / ctbAllHit;
            if (Mode == OsuGameMode.Taiko) return taikoValue / taikoAllHit;
            if (Mode == OsuGameMode.Mania) return maniaValue / maniaAllHit;
            if (Mode == OsuGameMode.Unkonwn) return invalidValue;
            return invalidValue;
        }
        ///<inheritdoc/>
        public override int GetHashCode()
        {
            return ReplayMd5.GetHashCode();
        }
    }
}