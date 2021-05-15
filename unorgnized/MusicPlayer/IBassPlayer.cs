using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuTools.MusicPlayer
{
    /// <summary>
    /// 媒体状态
    /// </summary>
    public enum MediaState
    {
        /// <summary>
        /// 已打开文件
        /// </summary>
        Open = 0,
        /// <summary>
        /// 播放
        /// </summary>
        Play = 1,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause = 2,
        /// <summary>
        /// 停止
        /// </summary>
        Stop = 3,
        /// <summary>
        /// 关闭
        /// </summary>
        Close = 4,
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 5
    }
    /// <summary>
    /// 表示一个播放器
    /// </summary>
    public interface IPlayer:IDisposable
    {
        /// <summary>
        /// 媒体源
        /// </summary>
        Uri Source { get; }
        /// <summary>
        /// 播放位置
        /// </summary>
        TimeSpan Position { get; }
        /// <summary>
        /// 媒体总时长
        /// </summary>
        TimeSpan Duration { get; }
        /// <summary>
        /// 媒体状态
        /// </summary>
        MediaState State { get; }
        /// <summary>
        /// 加载媒体
        /// </summary>
        /// <param name="file"></param>
        void Load(string file);
        /// <summary>
        /// 暂停
        /// </summary>
        void Pause();
        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="atuoReset">播放完毕后是否自动回到开头</param>
        void Play(bool atuoReset);
        /// <summary>
        /// 停止
        /// </summary>
        void Stop();
    }
}
