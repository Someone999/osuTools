using System.Collections.Generic;

namespace osuTools.Replays.AdditionalInfo
{
    /// <summary>
    ///     生命值图像的集合
    /// </summary>
    public class LifeBarGraphCollection
    {
        private readonly List<LifeBarGraph> _gr = new List<LifeBarGraph>();

        /// <summary>
        ///     将字符串解析成<see cref="LifeBarGraph" />
        /// </summary>
        /// <param name="s"></param>
        public LifeBarGraphCollection(string s)
        {
            GetData(s);
        }

        /// <summary>
        ///     构造一个空的LifeBarGraphCollection对象
        /// </summary>
        public LifeBarGraphCollection()
        {
        }

        /// <summary>
        ///     存储了生命值图像的集合
        /// </summary>
        public IReadOnlyList<LifeBarGraph> Data => _gr;

        private void GetData(string str)
        {
            var pair = str.Split('|');
            var i = 0;
            foreach (var value in pair)
            {
                var val = new LifeBarGraph(value);
                if (val.Offset == 0 && val.Hp == 0 && i != 0)
                    continue;
                _gr.Add(val);
                i++;
            }
        }
    }
}