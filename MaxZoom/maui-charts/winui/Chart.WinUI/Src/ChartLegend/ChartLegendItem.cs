
namespace Syncfusion.UI.Xaml.Charts
{
    /// <summary>
    /// Represents the legend item chart series member.
    /// </summary>
    public class ChartLegendItem : LegendItem
    {
        /// <summary>
        /// Get the corresponding legend item series
        /// </summary>
        public ChartSeries Series { get; internal set; }

        internal ChartSegment Segment { get; set; }

        internal override void ToggledSegment()
        {
            if (Segment == null) return;

            if (Segment.IsSegmentVisible)
            {
                Series.ToggledLegendIndex.Add(Index);
                Segment.IsSegmentVisible = false;
            }
            else
            {
                Series.ToggledLegendIndex.Remove(Index);
                Segment.IsSegmentVisible = true;
            }
            Series.ActualArea.ScheduleUpdate();
        }

        internal void Dispose()
        {
            Segment?.Dispose();
            Segment = null;
            Item = null;
            Series?.Dispose();
            Series = null;
        }
    }

    internal class CartesianLegendItem : ChartLegendItem
    {

    }

    internal class PolarLegendItem : ChartLegendItem
    {

    }

    internal class CircularLegendItem : ChartLegendItem
    {
    }

    internal class FunnelLegendItem : ChartLegendItem
    {
    }

    internal class PyramidLegendItem : ChartLegendItem
    {
    }
}
