#define SYNC
using osuTools.Tools;
using osuTools.MemoryCache.Win32.Structures;
using osuTools.MemoryCache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
namespace osuTools.MemoryCache.Beatmap
{
    /// <summary>
    /// 内存谱面存储库
    /// </summary>
    public class MemoryBeatmapCollection: IMemoryCache
    {
        Dictionary<string,Beatmaps.Beatmap> _beatmaps = new Dictionary<string, Beatmaps.Beatmap>();
        public Beatmaps.Beatmap this[string md5] => _beatmaps[md5];            
        static Process _currentProcess = Process.GetCurrentProcess();
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool Enabled { get; set; } = true;
        public bool AutoGC => true;

        /// <summary>
        /// 添加谱面到缓存区
        /// </summary>
        /// <param name="md5">谱面md5</param>
        /// <param name="beatmap">谱面</param>
        public unsafe void Add(string md5, Beatmaps.Beatmap beatmap)
        {
            return;
            MemoryStatusEx status = new MemoryStatusEx();
            status.Length = (uint)sizeof(MemoryStatusEx);
            Win32.Api.Win32Api.GlobalMemoryStatusEx(out status);
            if(Marshal.GetLastWin32Error() != 0)
            {
                OutputHelper.Output("[osuTools::BeatmapCache] Failed to query memory usage. Operation aborted.");
                return;
            }
            else
            {
                double memPercent = (double)status.AvailablePhysicalMemory / status.TotalPhysicalMemory;
                
                if(memPercent > 0.95)
                {
                    Enabled = false;
                    Clear();
                    GC.Collect();
                }
                else
                {
                    Enabled = true;
                }
            }
            if (Enabled)
            {
                _beatmaps.Add(md5, beatmap);
            }
        }
        /// <summary>
        /// 从缓存区移除指定谱面
        /// </summary>
        /// <param name="md5">谱面md5</param>
        /// <returns></returns>
        public bool Remove(string md5)
        {
            return _beatmaps.Remove(md5);
        }
        /// <summary>
        /// 清除缓存区所有谱面
        /// </summary>
        public void Clear()
        {
            _beatmaps.Clear();
        }
        /// <summary>
        /// 缓存区是否包含指定谱面
        /// </summary>
        /// <param name="md5">谱面md5</param>
        /// <returns></returns>
        public bool ContainsBeatmap(string md5) => _beatmaps.ContainsKey(md5);

        /// <summary>
        /// 返回缓存区所有的谱面信息，格式： {Md5}:{Beatmap}
        /// </summary>
        /// <returns></returns>
        public string GetCacheString()
        {
            StringBuilder builder = new StringBuilder();
            foreach(var item in _beatmaps)
            {
                builder.AppendLine($"{item.Key}:{item.Value}");
            }
            return builder.ToString();
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public int GetCacheSize() => _beatmaps.Count;
        
    }
}
