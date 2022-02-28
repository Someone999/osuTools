using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimePPDisplayer;
using RealTimePPDisplayer.Displayer;

namespace osuTools
{
    /// <summary>
    /// Smooth wrapper
    /// </summary>
    public class RtppdInfo : DisplayerBase
    {
        private PPTuple _tuple, _refTuple;
        /// <summary>
        /// Smoothed Pp
        /// </summary>
        public PPTuple SmoothPP => _tuple;
        /// <inheritdoc/>
        public override void FixedDisplay(double time) => _tuple = SmoothMath.SmoothDampPPTuple(_tuple, Pp, ref _refTuple, 0.033);
    }
}
