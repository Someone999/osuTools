namespace osuTools.Replays.AdditionalInfo
{
    /// <summary>
    ///     生命值图像，一个时间，生命值的键值对
    /// </summary>
    public class LifeBarGraph
    {
        private readonly double _hp = -1;
        private readonly int _offset = -1;
        private readonly string _orgstr;

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
            _orgstr = pair;
            var data = _orgstr.Split(',');
            if (data.Length < 2) return;
            var hp = data[0];
            var offset = data[1];
            double.TryParse(hp, out _hp);
            int.TryParse(offset, out _offset);
        }

        public double Hp => _hp;
        public int Offset => _offset;
    }
}