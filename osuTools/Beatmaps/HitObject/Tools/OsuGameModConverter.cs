using System.Collections.Generic;
using osuTools.Exceptions;
using osuTools.Game.Mods;

namespace osuTools.Beatmaps.HitObject.Tools
{
    /// <summary>
    /// 将int转换为<see cref="OsuGameMod"/>列表的类，你也可以使用返回ModList的ModList.FromInteger来转换
    /// A class converts an integer to a list of OsuGameMod. You can also convert the integer with ModList.FromInteger which returns a ModList.
    /// </summary>
    public class OsuGameModConverter:IntToEnumListConverter<OsuGameMod>
    {
        bool Contains(OsuGameMod allMods, OsuGameMod mod) => (allMods & mod) != 0;

        bool AllContains(OsuGameMod mods1, OsuGameMod mods2, OsuGameMod addMod) =>
            Contains(mods1, addMod) && Contains(mods2, addMod);
        
        void CheckModConflict(OsuGameMod allMods, OsuGameMod currentMod)
        {
            bool contained = Contains(allMods, currentMod);
            if (contained)
                throw new ModExsitedException($"{currentMod} has existed in list.");
            OsuGameMod timeRateConflict = OsuGameMod.DoubleTime | OsuGameMod.NightCore | OsuGameMod.HalfTime;
            OsuGameMod difficultyRateConflict = OsuGameMod.Easy | OsuGameMod.HardRock;
            OsuGameMod suddenDeathConflict = OsuGameMod.SuddenDeath | OsuGameMod.Perfect;
            OsuGameMod failsJudgeConflict = OsuGameMod.NoFail | suddenDeathConflict;
            OsuGameMod fadeConflict = OsuGameMod.Hidden | OsuGameMod.FadeIn;
            OsuGameMod keyModConflict = OsuGameMod.KeyMod;
            OsuGameMod autoConflict = OsuGameMod.AutoPlay | OsuGameMod.Cinema;
            OsuGameMod autoSuddenDeathConflict = autoConflict | suddenDeathConflict;
            OsuGameMod autoPliotConflict = OsuGameMod.AutoPilot | OsuGameMod.Relax | OsuGameMod.SpunOut | autoConflict;
            OsuGameMod relaxConflict = OsuGameMod.Relax | OsuGameMod.AutoPilot | autoConflict;
            bool conflict = AllContains(allMods, currentMod, timeRateConflict);
            conflict = conflict || AllContains(allMods, currentMod, failsJudgeConflict);
            conflict = conflict || AllContains(allMods, currentMod, fadeConflict);
            conflict = conflict || AllContains(allMods, currentMod, keyModConflict);
            conflict = conflict || AllContains(allMods, currentMod, autoSuddenDeathConflict);
            if (Contains(allMods, OsuGameMod.AutoPilot))
                conflict = conflict || AllContains(allMods, currentMod, autoPliotConflict);
            conflict = conflict || AllContains(allMods, currentMod, relaxConflict);
            conflict = conflict || AllContains(allMods, currentMod, difficultyRateConflict);
            if (conflict)
                throw new ConflictingModExistedException("One or more mods in the list conflict with the mod to add.");



        }
        /// <summary>
        /// 将int转换为<see cref="OsuGameMod"/>列表，在此转换中，maybeBestVal恒为null
        /// "maybeBestVal" will always be null in this conversion.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="maybeBestVal"></param>
        /// <returns></returns>
        public override List<OsuGameMod> Convert(int val, out OsuGameMod? maybeBestVal)
        {
            maybeBestVal = null;
            List<OsuGameMod> lst = new List<OsuGameMod>();
            if (val == 0)
                return new List<OsuGameMod> {DefaultValue};
            OsuGameMod currentMod = OsuGameMod.None;
            string bits = ToReversedBinary(val);
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i] != '1')
                    continue;
                var mod = (OsuGameMod) (1 << i);
                CheckModConflict(currentMod, mod);
                currentMod |= mod;
                lst.Add(mod);
            }
            return lst;
        }
        ///<inheritdoc/>
        public override OsuGameMod DefaultValue => OsuGameMod.None;
    }
}