namespace osuTools.PerformanceCalculator.Catch
{
    public class MKeyValuePair<TKey,TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public MKeyValuePair(TKey key,TValue val)
        {
            Key = key;
            Value = val;
        }
    }
}