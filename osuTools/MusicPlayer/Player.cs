using System;
using System.IO;
using System.Threading;
using System.Timers;
using ManagedBass;
using Timer = System.Timers.Timer;

namespace osuTools.MusicPlayer
{
    /// <summary>
    /// 基于Bass的音乐播放器
    /// </summary>
    public class BassMusicPlayer:IPlayer
    {
        private int _bassHandle;
        /// <summary>
        /// 播放状态
        /// </summary>
        public MediaState State { get; private set; }
        /// <summary>
        /// 媒体源
        /// </summary>
        public Uri Source { get; private set; }
        private TimeSpan _position;

        /// <summary>
        /// 播放位置
        /// </summary>
        public TimeSpan Position
        {
            get => _position;
            set
            {
                
                if(Bass.ChannelSetPosition(_bassHandle, Bass.ChannelSeconds2Bytes(_bassHandle,value.TotalSeconds)))
                    _position = value;
            }
        }
        private TimeSpan _oldPosition;
        private readonly Timer _updateTimer = new Timer();
        private float _freqMultiple, _audioFreq = -1;
        /// <summary>
        /// 媒体时长
        /// </summary>
        public TimeSpan Duration { get; private set; }
        /// <summary>
        /// 平滑变速
        /// </summary>
        /// <param name="target">目标的速度倍率</param>
        /// <param name="millisec">时限</param>
        public void ChangeSpeed(float target,int millisec)
        {
            if(Bass.ChannelSlideAttribute(_bassHandle, ChannelAttribute.Frequency, target * _audioFreq, millisec))
                _freqMultiple = target;
            if (WaitForSlide)
            {
               Thread.Sleep(millisec);
            }
        }
        /// <summary>
        /// 速率
        /// </summary>
        public float SpeedRatio
        {
            get => _freqMultiple;
            set
            {
                if (!_isInSlide)
                {
                    if (Bass.ChannelSetAttribute(_bassHandle, ChannelAttribute.Frequency, value * _audioFreq))
                        _freqMultiple = value;
                }
            }
        }
        /// <summary>
        /// 当前使用的设备
        /// </summary>
        public DeviceInfo CurrentDevice
        {
            get
            {
                DeviceInfo deviceinfo = default;
                if (_bassHandle != 0)
                {
                    var device = Bass.ChannelGetDevice(_bassHandle);
                    deviceinfo = Bass.GetDeviceInfo(device);
                }
                return deviceinfo;
            }
        }
        /// <summary>
        /// 为<seealso cref="OnMediaOpen"/>提供事件处理器
        /// </summary>
        /// <param name="info">媒体信息</param>
        public delegate void MediaOpenEventHandler(MediaInfo info);
        /// <summary>
        ///  为<seealso cref="OnMediaFailed"/>提供事件处理器
        /// </summary>
        /// <param name="state">失败的操作</param>
        /// <param name="info">媒体</param>
        /// <param name="error">错误信息</param>
        public delegate void MediaFailedEventHandler(MediaState state,MediaInfo info,Errors error);
        /// <summary>
        /// 为<seealso cref="OnPositionChanged"/>事件提供处理器
        /// </summary>
        /// <param name="oldPosition">上一个播放位置</param>
        /// <param name="newPosition">当前位置</param>
        public delegate void PositionChangedEventHandler(TimeSpan oldPosition, TimeSpan newPosition);
        /// <summary>
        /// 为<seealso cref="OnMediaStateChanged"/>提供事件处理器
        /// </summary>
        /// <param name="oldState">旧状态</param>
        /// <param name="newState">新状态</param>
        public delegate void MediaStateChangedEventHandler(MediaState oldState,MediaState newState);
        /// <summary>
        /// 为<seealso cref="OnMediaEnd"/>提供事件处理器
        /// </summary>
        public delegate void MediaEndEventHandler();
        /// <summary>
        /// 媒体成功打开后触发的事件
        /// </summary>
        public event MediaOpenEventHandler OnMediaOpen;
        /// <summary>
        /// 对媒体进行操作失败后触发的事件
        /// </summary>
        public event MediaFailedEventHandler OnMediaFailed;
        /// <summary>
        /// 媒体位置更改时触发的事件
        /// </summary>
        public event PositionChangedEventHandler OnPositionChanged;
        /// <summary>
        /// 媒体状态更改时触发的事件
        /// </summary>
        public event MediaStateChangedEventHandler OnMediaStateChanged;
        /// <summary>
        /// 媒体播放结束时触发的事件
        /// </summary>
        public event MediaEndEventHandler OnMediaEnd;
        /// <summary>
        /// 是否等待ChangeSpeed的平滑过渡完成再改变媒体状态
        /// </summary>
        public bool WaitForSlide { get; set; }
        void UpdatePosition(ref TimeSpan position,ref TimeSpan oldPosition)
        {
            lock (this)
            {
                oldPosition = position;
                position = TimeSpan.FromSeconds(Bass.ChannelBytes2Seconds(_bassHandle,
                    Bass.ChannelGetPosition(_bassHandle)));
                OnPositionChanged?.Invoke(_oldPosition, _position);
            }
        }

        void UpdateState()
        {
            lock (this)
            {
                if (Bass.LastError == Errors.OK)
                {
                    MediaState state = MediaState.Unknown;
                    if (_bassHandle != 0)
                    {
                        switch (Bass.ChannelIsActive(_bassHandle))
                        {
                            case PlaybackState.Paused:
                            case PlaybackState.Stalled:
                                state = MediaState.Pause;
                                break;
                            case PlaybackState.Playing:
                                state = MediaState.Play;
                                break;
                            case PlaybackState.Stopped:
                                state = MediaState.Stop;
                                break;
                            default:
                                state = MediaState.Unknown;
                                break;
                        }
                    }

                    if (State == MediaState.Play && state == MediaState.Stop)
                    {
                        if (_position >= Duration)
                            _position = TimeSpan.Zero;
                        OnMediaEnd?.Invoke();
                    }

                    State = state;

                    OnMediaStateChanged?.Invoke(State, state);
                }
            }

        }

        void UpdateSlideState()
        {
            _isInSlide = Bass.ChannelIsSliding(_bassHandle, ChannelAttribute.Frequency);
        }
        /// <summary>
        /// 初始化一个BassMusicPlayer
        /// </summary>
        public BassMusicPlayer()
        {
            State = MediaState.Close;
            OnMediaStateChanged?.Invoke(MediaState.Unknown, MediaState.Close);
            Bass.Init(-1, 48000, 0,IntPtr.Zero);
            _updateTimer.Elapsed += _updateTimer_Elapsed;
            _updateTimer.Interval = 1;
        }
        /// <summary>
        /// 初始化一个BassMusicPlayer并加载指定的文件
        /// </summary>
        /// <param name="url"></param>
        public BassMusicPlayer(string url)
        {
            State = MediaState.Close;
            OnMediaStateChanged?.Invoke(MediaState.Unknown, MediaState.Close);
            Bass.Init(-1, 48000, 0, IntPtr.Zero);
            _updateTimer.Elapsed += _updateTimer_Elapsed;
            _updateTimer.Interval = 1;
            Load(url);
        }

        private void _updateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                if (State == MediaState.Play)
                    UpdatePosition(ref _position, ref _oldPosition);
                UpdateState();
                UpdateSlideState();
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        public void Load(string url)
        {
            Uri tmpUri = new Uri(url);
            if (tmpUri.Scheme == "file")
                _bassHandle = Bass.CreateStream(tmpUri.LocalPath);
            else
                _bassHandle = Bass.CreateStream(tmpUri.AbsoluteUri, 0, BassFlags.Default, null);
            var errCode = Bass.LastError;
            if (errCode == Errors.OK)
            {
                Duration = TimeSpan.FromSeconds(Bass.ChannelBytes2Seconds(_bassHandle,
                    Bass.ChannelGetLength(_bassHandle)));
                State = MediaState.Open;
                Source = new Uri(url);
                Bass.ChannelGetAttribute(_bassHandle, ChannelAttribute.Frequency, out _audioFreq);
                OnMediaOpen?.Invoke(new MediaInfo(Source.AbsolutePath));
            }
            else
            {
                OnMediaFailed?.Invoke(MediaState.Open,new MediaInfo(url,Path.GetFileNameWithoutExtension(url),0),errCode);
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="restart"></param>
        public void Play(bool restart = true)
        {
            Bass.ChannelPlay(_bassHandle, restart);
            _updateTimer.Enabled = true;
            _updateTimer.AutoReset = true;
            var errCode = Bass.LastError;
            if (errCode != Errors.OK)
            {
                OnMediaFailed?.Invoke(MediaState.Play,
                    new MediaInfo(Path.GetFileNameWithoutExtension(Source.AbsolutePath)),errCode);
            }

            UpdateState();
        }

        private bool _isInSlide;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Pause()
        {
            Bass.ChannelPause(_bassHandle);
            UpdateState();
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Stop()
        {
            Bass.ChannelStop(_bassHandle);
            UpdateState();
        }
        /// <summary>
        /// 为<seealso cref="OnDispose"/>提供事件处理器
        /// </summary>
        /// <param name="currentMedia">当前媒体</param>
        public delegate void DisposedEventArgs(MediaInfo currentMedia);
        /// <summary>
        /// 当播放器被释放时触发的事件
        /// </summary>
        public event DisposedEventArgs OnDispose;
        bool _disposed;
        /// <summary>
        /// 释放播放器所用的资源
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _bassHandle = 0;
                OnDispose?.Invoke(new MediaInfo(Source.AbsolutePath));
                Bass.Free();
            }
        }
    }
}
