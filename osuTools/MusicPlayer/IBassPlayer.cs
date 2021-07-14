using System;

namespace osuTools.MusicPlayer
{
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
        /// <param name="url"></param>
        void Load(string url);
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
