using System;
using System.Collections.Generic;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// This class serves as event data for the <see cref="SfCartesianChart.TrackballCreated"/> Event.
    /// </summary>
    public class TrackballEventArgs : EventArgs
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="TrackballEventArgs"/> class.
        /// </summary>
        public TrackballEventArgs(List<TrackballPointInfo> chartPointsInfo)
        {
            TrackballPointsInfo = chartPointsInfo;
        }

        /// <summary>
        /// Gets the collection of trackball information that is created for each series.
        /// </summary>
        public List<TrackballPointInfo> TrackballPointsInfo { get; internal set; }
    }
}