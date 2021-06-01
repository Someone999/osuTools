﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuTools.MusicPlayer;

namespace osuTools.Beatmaps
{

    /// <summary>
    /// 返回音频播放器对象
    /// </summary>
    public interface IHasPlayableAudio
    {
        /// <summary>
        /// 获取播放器对象
        /// </summary>
        /// <returns></returns>
        IPlayer GetAudioPlayer();
    }
}