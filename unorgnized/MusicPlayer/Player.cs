using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using ManagedBass;
using Bass = ManagedBass.Bass;
using Timer = System.Timers.Timer;

namespace osuTools.MusicPlayer
{
    public class BassMusicPlayer:IPlayer
    {
        private int _bassHandle;
        public MediaState State { get; private set; }
        public Uri Source { get; private set; }
        private TimeSpan _position;

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
        public TimeSpan Duration { get; private set; }
        public void ChangeSpeed(float target,int millisec)
        {
            if(Bass.ChannelSlideAttribute(_bassHandle, ChannelAttribute.Frequency, target * _audioFreq, millisec))
                _freqMultiple = target;
            if (WaitForSlide)
            {
               Thread.Sleep(millisec);
            }
        }

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

        public delegate void MediaOpenEventHandler(MediaInfo info);
        public delegate void MediaFailedEventHandler(MediaState state,MediaInfo info,Errors error);
        public delegate void PositionChangedEventHandler(TimeSpan oldPosion, TimeSpan newPosition);
        public delegate void MediaStateChangedEventHandler(MediaState oldState,MediaState newState);
        public delegate void MediaEndEventHandler();

        public event MediaOpenEventHandler OnMediaOpen;
        public event MediaFailedEventHandler OnMediaFailed;
        public event PositionChangedEventHandler OnPositionChanged;
        public event MediaStateChangedEventHandler OnMediaStateChanged;
        public event MediaEndEventHandler OnMediaEnd;
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

        public BassMusicPlayer()
        {
            State = MediaState.Close;
            OnMediaStateChanged?.Invoke(MediaState.Unknown, MediaState.Close);
            Bass.Init(-1, 48000, 0,IntPtr.Zero);
            _updateTimer.Elapsed += _updateTimer_Elapsed;
            _updateTimer.Interval = 1;
        }
        public BassMusicPlayer(string file)
        {
            State = MediaState.Close;
            OnMediaStateChanged?.Invoke(MediaState.Unknown, MediaState.Close);
            Bass.Init(-1, 48000, 0, IntPtr.Zero);
            _updateTimer.Elapsed += _updateTimer_Elapsed;
            _updateTimer.Interval = 1;
            Load(file);
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

        public void Load(string path)
        {
            _bassHandle = Bass.SampleLoad(path,0,0,1,0);
            var errCode = Bass.LastError;
            if (errCode == Errors.OK)
            {
                Duration = TimeSpan.FromSeconds(Bass.ChannelBytes2Seconds(_bassHandle,
                    Bass.ChannelGetLength(_bassHandle, 0)));
                State = MediaState.Open;
                Source = new Uri(path);
                Bass.ChannelGetAttribute(_bassHandle, ChannelAttribute.Frequency, out _audioFreq);
            }
            else
            {
                OnMediaFailed?.Invoke(MediaState.Open,new MediaInfo(path,Path.GetFileNameWithoutExtension(path),0),errCode);
            }
        }

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

        public void Pause()
        {
            Bass.ChannelPause(_bassHandle);
            UpdateState();
        }

        public void Stop()
        {
            Bass.ChannelStop(_bassHandle);
            UpdateState();
        }

        public delegate void DisposedEventArgs(MediaInfo currentMedia);

        public event DisposedEventArgs OnDispose;
        bool _disposed;

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
