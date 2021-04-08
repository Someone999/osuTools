using System;
using System.Collections.Generic;
using System.IO;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Modes;
using osuTools.OsuDB;

namespace osuTools.Replay
{
    public partial class Replay
    {
        private short C300g, C300, C200, C100, C50, Cmiss, maxco;
        private string d;
        private readonly FileInfo Info;
        private readonly bool infonovalue;
        private byte mode, per, flag;
        private List<OsuGameMod> mods = new List<OsuGameMod>();
        private readonly BinaryReader r;

        /// <summary>
        ///     使用录像文件构造一个Replay对象
        /// </summary>
        /// <param name="Dir"></param>
        public Replay(string Dir)
        {
            {
                r = new BinaryReader(File.OpenRead(Dir));
            }
            Info = new FileInfo(Dir);
            Read();
        }

        /// <summary>
        ///     使用录像文件的二进制流构造一个Replay对象，并指定录像的全路径
        /// </summary>
        /// <param name="b"></param>
        /// <param name="Dir"></param>
        public Replay(BinaryReader b, string Dir)
        {
            if (b is null)
                throw new NullReferenceException();
            r = b;
            Info = new FileInfo(Dir);
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
            r = b;
            Info = null;
            infonovalue = true;
            Read();
        }

        /// <summary>
        ///     录像对应的游玩记录使用Mods
        /// </summary>
        public IReadOnlyList<OsuGameMod> Mods => mods.AsReadOnly();

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
                if (infonovalue)
                    throw new FileInfoHasNoValue("当前的构造函数没有为FileInfo赋值");
                return Info;
            }
        }

        private void Read()
        {
            var lfbar = "";
            mode = r.ReadByte();
            Mode = (OsuGameMode) mode;
            OsuVersion = r.ReadInt32();
            flag = r.ReadByte();
            BeatmapMD5 = r.ReadString();
            flag = r.ReadByte();
            Player = r.ReadString();
            flag = r.ReadByte();
            ReplayMD5 = r.ReadString();
            C300 = r.ReadInt16();
            C100 = r.ReadInt16();
            C50 = r.ReadInt16();
            C300g = r.ReadInt16();
            C200 = r.ReadInt16();
            Cmiss = r.ReadInt16();
            Accuracy = GameMode.FromLegacyMode(Mode).AccuracyCalc(
                new ScoreInfo
                {
                    c300g = c300g,
                    c300 = c300,
                    c200 = c200,
                    c100 = c100,
                    c50 = c50,
                    cMiss = cMiss
                });
            AccuracyStr = Accuracy.ToString("p");
            Score = r.ReadInt32();
            maxco = r.ReadInt16();
            per = r.ReadByte();
            if (per == 1)
                Perfect = true;
            else
                Perfect = false;
            mods = HitObjectTools.GetGenericTypesByInt<OsuGameMod>(r.ReadInt32());
            if (r.ReadByte() == 0x0b)
                lfbar = r.ReadString();
            PlayTime = new DateTime(r.ReadInt64());
            var datalen = r.ReadInt32();
            var data = r.ReadBytes(datalen);
            AdditionalData = new AdditionalRepalyData(data, datalen, lfbar);
            r.ReadDouble();
        }

        public static bool operator ==(Replay replay, OsuScoreInfo score)
        {
            return replay.ReplayMD5 == score.ReplayMD5;
        }

        public static bool operator !=(Replay replay, OsuScoreInfo score)
        {
            return replay.ReplayMD5 != score.ReplayMD5;
        }

        public static bool operator ==(OsuScoreInfo score, Replay replay)
        {
            return replay.ReplayMD5 == score.ReplayMD5;
        }

        public static bool operator !=(OsuScoreInfo score, Replay replay)
        {
            return replay.ReplayMD5 != score.ReplayMD5;
        }

        private double AccCalculater(OsuGameMode mode)
        {
            double a300 = 1, a200 = 2.0 / 3, a100 = 1.0 / 3, a50 = 1.0 / 6, a150 = 1.0 / 2;
            var ManiaAllHit = C300g + C300 + C200 + C100 + C50 + cMiss;
            var OsuAllHit = C300 + C100 + C50 + Cmiss;
            var CtbAllHit = C50 + C100 + C300 + Cmiss + C200;
            var TaikoAllHit = C300 + C100 + Cmiss;
            double ma300c = c300 + c300g,
                a300c = c300,
                a200c = c200 * a200,
                a100c = c100 * a100,
                a50c = c50 * a50,
                a150c = c100 * a150;
            double OsuValue = a300c + a100c + a50c,
                TaikoValue = a300c + a150c,
                CtbValue = c300 + c50 + c100,
                ManiaValue = ma300c + a200c + a100c + a50c,
                InvalidValue = -1;
            if (Mode == OsuGameMode.Osu) return OsuValue / OsuAllHit;
            if (Mode == OsuGameMode.Catch) return CtbValue / CtbAllHit;
            if (Mode == OsuGameMode.Taiko) return TaikoValue / TaikoAllHit;
            if (Mode == OsuGameMode.Mania) return ManiaValue / ManiaAllHit;
            if (Mode == OsuGameMode.Unkonwn) return InvalidValue;
            return InvalidValue;
        }
    }
}