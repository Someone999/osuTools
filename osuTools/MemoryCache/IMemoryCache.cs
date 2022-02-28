#define SYNC


namespace osuTools.MemoryCache
{
    /// <summary>
    /// 内存缓存对象
    /// </summary>
    public interface IMemoryCache
    {
        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();
        /// <summary>
        /// 输出缓存的内容
        /// </summary>
        /// <returns></returns>
        string GetCacheString();
        /// <summary>
        /// 获取缓存的大小
        /// </summary>
        /// <returns></returns>
        int GetCacheSize();
        /// <summary>
        /// 是否启用缓存
        /// </summary>
        bool Enabled { get; set; }
        /// <summary>
        /// 由插件自动调用GC
        /// </summary>
        bool AutoGC { get; }
    }
}
