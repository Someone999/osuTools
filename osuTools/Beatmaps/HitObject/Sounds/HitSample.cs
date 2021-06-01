namespace osuTools.Beatmaps.HitObject.Sounds
{
    /// <summary>
    ///     自定义音效
    /// </summary>
    public class HitSample
    {
        /// <summary>
        ///     构建一个空的HitSample对象
        /// </summary>
        public HitSample()
        {
        }

        /// <summary>
        ///     使用两种音效，编号，音量，和文件名构造一个HitSample对象
        /// </summary>
        /// <param name="normalSet"></param>
        /// <param name="addtionSet"></param>
        /// <param name="index"></param>
        /// <param name="volume"></param>
        /// <param name="fileName"></param>
        public HitSample(SampleSets normalSet, SampleSets addtionSet, int index, int volume, string fileName)
        {
            NormalSet = normalSet;
            AdditionSet = addtionSet;
            Index = index;
            Volume = volume;
            FileName = !string.IsNullOrEmpty(fileName) ? fileName : "";
        }

        /// <summary>
        ///     将字符串解析成HitSample对象
        /// </summary>
        /// <param name="data"></param>
        public HitSample(string data)
        {
            var fileName = "";
            var datas = data.Split(':');
            if (data.Length > 0)
                NormalSet = (SampleSets) int.Parse(datas[0]);
            if (datas.Length > 1)
                AdditionSet = (SampleSets) int.Parse(datas[1]);
            if (datas.Length > 2)
                Index = int.Parse(datas[2]);
            if (datas.Length > 3)
                Volume = int.Parse(datas[3]);
            if (datas.Length > 4)
                fileName = datas[4];
            FileName = string.IsNullOrEmpty(fileName) ? "" : fileName;
        }

        /// <summary>
        ///     普通预设音效
        /// </summary>
        public SampleSets NormalSet { get; set; } = SampleSets.Default;

        /// <summary>
        ///     附加预设音效
        /// </summary>
        public SampleSets AdditionSet { get; set; } = SampleSets.Default;

        /// <summary>
        ///     编号
        /// </summary>
        public int Index { get; }

        /// <summary>
        ///     音量
        /// </summary>
        public int Volume { get; }

        /// <summary>
        ///     文件名
        /// </summary>
        public string FileName { get; } = "";

        /// <summary>
        ///     将HitSample对象转换成字符串再转化为osu文件中的格式
        /// </summary>
        /// <returns></returns>
        public string GetData()
        {
            return $"{(int) NormalSet}:{(int) AdditionSet}:{Index}:{Volume}:{FileName}";
        }
    }
}