﻿using System.Collections.Generic;
using System.IO;

namespace osuTools.Replays.AdditionalInfo
{
    /// <summary>
    ///     表示额外的录像数据
    /// </summary>
    public class AdditionalRepalyData
    {
        private readonly LifeBarGraphCollection _l;
        private byte[] _lzmAstream;

        /// <summary>
        ///     使用回放数据，数据长度和表示生命值图像的字符串构造一个AdditionalRepalyData对象
        /// </summary>
        /// <param name="data"></param>
        /// <param name="len"></param>
        /// <param name="lifebargraphstr"></param>
        public AdditionalRepalyData(byte[] data, int len, string lifebargraphstr)
        {
            _lzmAstream = data;
            LzmaStream.Write(data, 0, len);
            ReplayDataLength = len;
            _l = new LifeBarGraphCollection(lifebargraphstr);
        }

        /// <summary>
        ///     生命值图像的列表
        /// </summary>
        public IReadOnlyList<LifeBarGraph> LifeBarGraphData => _l.Data;

        /// <summary>
        ///     游玩回放的数据
        /// </summary>
        public MemoryStream LzmaStream { get; } = new MemoryStream();

        public int ReplayDataLength { get; }
    }
}