namespace osuTools.Replay
{
    /// <summary>
    ///     生命值图像，一个时间，生命值的键值对
    /// </summary>
    public class LifeBarGraph
    {
        private readonly double hp = -1;
        private readonly int offset = -1;
        private readonly string orgstr;

        /// <summary>
        ///     构造一个空的LifeBarGraph对象
        /// </summary>
        public LifeBarGraph()
        {
        }

        /// <summary>
        ///     将字符串解析成一个LifeBarGraph对象
        /// </summary>
        /// <param name="pair"></param>
        public LifeBarGraph(string pair)
        {
            orgstr = pair;
            var data = orgstr.Split(',');
            if (data.Length < 2) return;
            var HP = data[0];
            var Offset = data[1];
            double.TryParse(HP, out hp);
            int.TryParse(Offset, out offset);
        }

        public double HP => hp;
        public int Offset => offset;
    }
}