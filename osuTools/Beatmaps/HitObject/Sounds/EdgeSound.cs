namespace osuTools.Beatmaps.HitObject.Sounds
{
    /// <summary>
    ///     光标在滑条的三个阶段开始时播放的音效
    /// </summary>
    public class EdgeSound
    {
        /// <summary>
        ///     使用普通音效和附加音效初始化一个EdgeSound对象
        /// </summary>
        /// <param name="normalSet"></param>
        /// <param name="additionSet"></param>
        public EdgeSound(SampleSets normalSet = SampleSets.Default, SampleSets additionSet = SampleSets.Default)
        {
            NormalSet = normalSet;
            AdditionSet = additionSet;
        }

        /// <summary>
        ///     普通预设音效
        /// </summary>
        public SampleSets NormalSet { get; set; }

        /// <summary>
        ///     附加预设音效
        /// </summary>
        public SampleSets AdditionSet { get; set; } 

        /// <summary>
        ///     将EdgeSound转化为字符串后转化为osu文件格式的字符串
        /// </summary>
        /// <returns></returns>
        public string GetData()
        {
            return $"{(int) NormalSet}:{(int) AdditionSet}";
        }
    }
}