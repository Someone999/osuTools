using System;

namespace osuTools.Unused
{
    [Serializable]
    [Obsolete("This calss had been replaced by enum.", true)]
    internal class OsuGameStatusA
    {
        public static OsuGameStatusA
            Eiditing = new OsuGameStatusA("Eiditing"),
            Idle = new OsuGameStatusA("Idle"),
            Lobby = new OsuGameStatusA("Lobby"),
            MatchSetup = new OsuGameStatusA("MatchSetup"),
            ProcessNotFound = new OsuGameStatusA("NoFoundProcess"),
            Playing = new OsuGameStatusA("Playing"),
            Rank = new OsuGameStatusA("Rank"),
            SelectSong = new OsuGameStatusA("SelectSong"),
            Unknown = new OsuGameStatusA("Unknown");

        public OsuGameStatusA(string s)
        {
            if (s != "Eiditing" && s != "Idle" && s != "MatchSetup" && s != "NoFoundProcess" && s != "Playing" &&
                s != "Rank" && s != "SelectSong") s = "Unknown";
            Status = s;
        }

        public string Status { get; }

        public static bool operator ==(OsuGameStatusA g, OsuGameStatusA s)
        {
            return g.Status == s.Status || g.Status.Contains(s.Status);
        }

        public static bool operator !=(OsuGameStatusA g, OsuGameStatusA s)
        {
            return g.Status != s.Status || !g.Status.Contains(s.Status);
        }

        public override bool Equals(object obj)
        {
            var gms = obj as OsuGameStatusA;
            if (gms != null && gms.Status == Status)
                return true;
            return false;
        }

        public override string ToString()
        {
            return Status;
        }

        public OsuGameStatus ToEnum()
        {
            return this == Eiditing ? OsuGameStatus.Editing :
                this == Idle ? OsuGameStatus.Idle :
                this == Lobby ? OsuGameStatus.Lobby :
                this == MatchSetup ? OsuGameStatus.MatchSetup :
                this == ProcessNotFound ? OsuGameStatus.ProcessNotFound :
                this == Playing ? OsuGameStatus.Playing :
                this == Rank ? OsuGameStatus.Rank :
                this == SelectSong ? OsuGameStatus.SelectSong :
                this == Unknown ? OsuGameStatus.Unkonwn : OsuGameStatus.Unkonwn;
        }

        public static bool operator ==(OsuGameStatusA g, OsuGameStatus c)
        {
            return g.ToEnum() == c;
        }

        public static bool operator !=(OsuGameStatusA g, OsuGameStatus c)
        {
            return g.ToEnum() != c;
        }

        public static bool operator ==(OsuGameStatus c, OsuGameStatusA g)
        {
            return g.ToEnum() == c;
        }

        public static bool operator !=(OsuGameStatus c, OsuGameStatusA g)
        {
            return g.ToEnum() != c;
        }
    }

    [Serializable]
    [Obsolete("This calss had been replaced by enum.", true)]
    internal class OsuGameModeA
    {
        public static OsuGameModeA Mania = new OsuGameModeA("Mania");
        public static OsuGameModeA Catch = new OsuGameModeA("CatchTheBeat");
        public static OsuGameModeA Osu = new OsuGameModeA("Osu");
        public static OsuGameModeA Taiko = new OsuGameModeA("Taiko");
        public static OsuGameModeA Unknown = new OsuGameModeA("Unknown");
        public static OsuGameModeA unDefined = new OsuGameModeA("Undefined");
        private int modei;

        public OsuGameModeA(int mode)
        {
            switch (mode)
            {
                case 0:
                    Mode = Osu.ToString();
                    modei = mode;
                    ;
                    break;
                case 1:
                    Mode = Taiko.ToString();
                    modei = mode;
                    break;
                case 2:
                    Mode = Catch.ToString();
                    modei = mode;
                    break;
                case 3:
                    Mode = Mania.ToString();
                    modei = mode;
                    break;
                default:
                    Mode = Unknown.ToString();
                    modei = 4;
                    break;
            }
        }

        public OsuGameModeA(OsuGameMode mode)
        {
            switch (mode)
            {
                case OsuGameMode.Osu:
                    Mode = Osu.ToString();
                    modei = (int) mode;
                    ;
                    break;
                case OsuGameMode.Taiko:
                    Mode = Taiko.ToString();
                    modei = (int) mode;
                    break;
                case OsuGameMode.Catch:
                    Mode = Catch.ToString();
                    modei = (int) mode;
                    break;
                case OsuGameMode.Mania:
                    Mode = Mania.ToString();
                    modei = (int) mode;
                    break;
                default:
                    Mode = Unknown.ToString();
                    modei = 4;
                    break;
            }
        }

        public OsuGameModeA(string c)
        {
            if (c != "Mania" && c != "CatchTheBeat" && c != "Osu" && c != "Taiko")
            {
                if (c == "Mania") modei = 3;
                if (c == "CatchTheBeat") modei = 2;
                if (c == "Taiko") modei = 1;
                if (c == "Osu") modei = 0;
                c = "Unknown";
            }

            Mode = c;
        }

        public string Mode { get; }

        public static bool operator ==(OsuGameModeA g, OsuGameModeA c)
        {
            return g.Mode == c.Mode || g.Mode.Contains(c.Mode);
        }

        public static bool operator !=(OsuGameModeA g, OsuGameModeA c)
        {
            return g.Mode != c.Mode || !g.Mode.Contains(c.Mode);
        }

        public static bool operator ==(OsuGameModeA g, OsuGameMode c)
        {
            if (g.ToEnum() == c)
                return true;
            return false;
        }

        public static bool operator !=(OsuGameModeA g, OsuGameMode c)
        {
            if (g.ToEnum() != c)
                return true;
            return false;
        }

        public static bool operator ==(OsuGameMode c, OsuGameModeA g)
        {
            if (g.ToEnum() == c)
                return true;
            return false;
        }

        public static bool operator !=(OsuGameMode c, OsuGameModeA g)
        {
            if (g.ToEnum() != c)
                return true;
            return false;
        }

        public override string ToString()
        {
            return Mode;
        }

        public override bool Equals(object obj)
        {
            var gm = obj as OsuGameModeA;
            if (gm != null && Mode == gm.Mode)
                return true;
            return false;
        }

        public OsuGameMode ToEnum()
        {
            if (Mode == "Mania") modei = 3;
            if (Mode == "CatchTheBeat") modei = 2;
            if (Mode == "Taiko") modei = 1;
            if (Mode == "Osu") modei = 0;
            if (Mode == "Unknow") modei = 4;
            return (OsuGameMode) modei;
        }
    }

    internal class UnusedMethod
    {
        private void Method1()
        {
            /*longmod = mods.Name;
            mod = mods.ShortName;
            modsinfo = mods;
            IntMods = (int)mods.Mod;
            //mo = new GMMod(mods.Name);
            unRanked = mods.HasMod(ModsInfo.Mods.AutoPilot) && mods.HasMod(ModsInfo.Mods.Autoplay) && mods.HasMod(ModsInfo.Mods.Relax) && mods.HasMod(ModsInfo.Mods.ScoreV2);
            Ranked = !unRanked;
            canfail = !mods.HasMod(ModsInfo.Mods.NoFail) && !mods.HasMod(ModsInfo.Mods.AutoPilot) && !mods.HasMod(ModsInfo.Mods.SpunOut) && !mods.HasMod(ModsInfo.Mods.Autoplay) && !mods.HasMod(ModsInfo.Mods.Relax);
            ManiaRanked = !mods.HasMod(ModsInfo.Mods.KeyCoop) && !mods.HasMod(ModsInfo.Mods.HardRock) && !mods.HasMod(ModsInfo.Mods.Random) && Ranked;*/
        }

        private void MethodPartial1()
        {
            /*var realtype = HitObjectTypes.Unknown;
            var types = HitObjectTools.GetGenericTypesByInt<HitObjectTypes>(int.Parse(comasp[3]));

            if (types.Contains(HitObjectTypes.HitCircle))
            {
                realtype = HitObjectTypes.HitCircle;
            }
            if (types.Contains(HitObjectTypes.Slider))
            {
                realtype = HitObjectTypes.Slider;
            }
            if (types.Contains(HitObjectTypes.Spinner))
            {
                realtype = HitObjectTypes.Spinner;
            }
            if (types.Contains(HitObjectTypes.ManiaHold))
            {
                realtype = HitObjectTypes.ManiaHold;
            }
            if (Mode == OsuGameMode.Taiko)
            {
                realtype =
                types.Contains(HitObjectTypes.Slider) ? HitObjectTypes.Slider :
                types.Contains(HitObjectTypes.Spinner) ? HitObjectTypes.Spinner :
                HitObjectTypes.Unknown;
                if (realtype == HitObjectTypes.Unknown)
                {
                    var hitSounds = HitObject.HitObjectTools.GetGenericTypesByInt<HitSounds>(int.Parse(comasp[4]));
                    if (hitSounds.Count == 1)
                    {
                        if (hitSounds[0] == HitSounds.Finish)
                        {
                            realtype = HitObjectTypes.LargeTaikoRedHit;
                        }
                    }
                    if (hitSounds.Contains(HitSounds.Clap) || hitSounds.Contains(HitSounds.Whistle))
                    {
                        realtype = HitObjectTypes.TaikoBlueHit;
                        if (hitSounds.Contains(HitSounds.Finish))
                        {
                            realtype = HitObjectTypes.LargeTaikoBlueHit;
                        }
                    }
                    else if (hitSounds.Contains(HitSounds.Normal))
                    {
                        realtype = HitObjectTypes.TaikoRedHit;
                        if (hitSounds.Contains(HitSounds.Finish))
                        {
                            realtype = HitObjectTypes.LargeTaikoRedHit;
                        }
                    }
                }
            }
            var hit = HitObject.HitObjectTools.GetHitObjectClass(Mode, realtype);
            if (hit == null)
            {
                //try
                {
                    Debug.WriteLine("无法从这一行数据中读取HitObject\n" + $"谱面模式:{Mode} 未能识别的类型:{realtype}");
                    IO.CurrentIO.WriteColor("无法从这一行数据中读取HitObject\n" + $"谱面模式:{Mode} 未能识别的类型:{realtype}", ConsoleColor.Red);
                    continue;
                    //throw new FailToParseException("无法从这一行数据中读取HitObject",new ArgumentException($"数据无效:{str}"));
                }
                /*catch(FailToParseException)
                {
                    Debug.WriteLine("无法从这一行数据中读取HitObject\n" + $"数据无效:{ str}");
                    IO.CurrentIO.WriteColor("无法从这一行数据中读取HitObject\n" + $"数据无效:{str}", ConsoleColor.Red);
                    continue;
                }*/
        }
    }
}