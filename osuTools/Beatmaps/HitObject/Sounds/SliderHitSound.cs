namespace osuTools.Beatmaps.HitObject.Sounds
{
    /// <summary>
    ///     滑条在特定时候的音效
    /// </summary>
    public class SliderHitSound
    {
        /// <summary>
        ///     初始化一个空的SliderHitSound
        /// </summary>
        public SliderHitSound()
        {
        }

        /// <summary>
        ///     使用<seealso cref="HitSounds" />和<seealso cref="EdgeSound" />构造一个SliderHitSound
        /// </summary>
        /// <param name="hitSound"></param>
        /// <param name="edgeSound"></param>
        public SliderHitSound(HitSounds hitSound, EdgeSound edgeSound)
        {
            HitSound = hitSound;
            Sound = edgeSound;
        }

        /// <summary>
        ///     使用<seealso cref="HitSounds" />构造一个SliderHitSound
        /// </summary>
        /// <param name="hitSound"></param>
        public SliderHitSound(HitSounds hitSound)
        {
            HitSound = hitSound;
        }

        /// <summary>
        ///     使用<seealso cref="HitSounds" />和两个<seealso cref="SampleSets" />构造一个SliderHitSound
        /// </summary>
        /// <param name="hitSound"></param>
        /// <param name="sampleSets"></param>
        /// <param name="additionSampleSet"></param>
        public SliderHitSound(HitSounds hitSound, SampleSets sampleSets, SampleSets additionSampleSet)
        {
            HitSound = hitSound;
            Sound = new EdgeSound(sampleSets, additionSampleSet);
        }

        /// <summary>
        ///     音效类型
        /// </summary>
        public HitSounds HitSound { get; set; } = HitSounds.Normal;

        /// <summary>
        ///     音效
        /// </summary>
        public EdgeSound Sound { get; set; } = new EdgeSound(SampleSets.Default, SampleSets.Default);
    }
}