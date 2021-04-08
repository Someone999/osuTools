namespace osuTools
{
    public partial class ORTDPWrapper
    {
        /*public class CTBDiffAttributes
        {
            public CTBDiffAttributes(OsuBeatmap b)
            {
                StarRating = b.Stars;
                ApproachRate = b.AR;
            }
            public double StarRating { get; set; }
            public double ApproachRate { get; set; }
            public int MaxCombo { get; set; }
        }
        public double Calculate(CTBDiffAttributes Attributes, List<OsuGameMod> Mods, int c300, int cTick)
        {
            var mods = Mods;

            var fruitsHit = c300;
            var totalComboHits = cTick + c300;


            // We are heavily relying on aim in catch the beat
            double value = Math.Pow(5.0 * Math.Max(1.0, Attributes.StarRating / 0.0049) - 4.0, 2.0) / 100000.0;

            // Longer maps are worth more. "Longer" means how many hits there are which can contribute to combo
            int numTotalHits = totalComboHits;

            // Longer maps are worth more
            double lengthBonus =
                0.95 + 0.4 * Math.Min(1.0, numTotalHits / 3000.0) +
                (numTotalHits > 3000 ? Math.Log10(numTotalHits / 3000.0) * 0.5 : 0.0);

            // Longer maps are worth more
            value *= lengthBonus;

            // Penalize misses exponentially. This mainly fixes tag4 maps and the likes until a per-hitobject solution is available
            value *= Math.Pow(0.97, cMiss);

            // Combo scaling
            if (Attributes.MaxCombo > 0)
                value *= Math.Min(Math.Pow(Attributes.MaxCombo, 0.8) / Math.Pow(Attributes.MaxCombo, 0.8), 1.0);

            double approachRateFactor = 1.0;
            if (Attributes.ApproachRate > 9.0)
                approachRateFactor += 0.1 * (Attributes.ApproachRate - 9.0); // 10% for each AR above 9
            else if (Attributes.ApproachRate < 8.0)
                approachRateFactor += 0.025 * (8.0 - Attributes.ApproachRate); // 2.5% for each AR below 8

            value *= approachRateFactor;

            if (mods.Any(m => m == OsuGameMod.Hidden))
                // Hiddens gives nothing on max approach rate, and more the lower it is
                value *= 1.05 + 0.075 * (10.0 - Math.Min(10.0, Attributes.ApproachRate)); // 7.5% for each AR below 10

            if (mods.Any(m => m == OsuGameMod.Flashlight))
                // Apply length bonus again if flashlight is on simply because it becomes a lot harder on longer maps.
                value *= 1.35 * lengthBonus;

            // Scale the aim value with accuracy _slightly_
            value *= Math.Pow(1, 5.5);

            // Custom multipliers for NoFail. SpunOut is not applicable.
            if (mods.Any(m => m == OsuGameMod.NoFail))
                value *= 0.90;

            return value;
        }*/
    }
}