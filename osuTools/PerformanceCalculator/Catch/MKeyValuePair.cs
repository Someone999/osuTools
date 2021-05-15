namespace osuTools.PerformanceCalculator.Catch
{
    /// <summary>
    /// 键值对
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class MKeyValuePair<TKey,TValue>
    {
        /// <summary>
        /// 键
        /// </summary>
        public TKey Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public TValue Value { get; set; }
        /// <summary>
        /// 使用键和值初始化一个键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public MKeyValuePair(TKey key,TValue val)
        {
            Key = key;
            Value = val;
        }
    }
}