using Microsoft.Maui.Graphics;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// The CartesianSegment is the base class for the <see cref="AreaSegment"/>, <see cref="ColumnSegment"/>, <see cref="LineSegment"/>, <see cref="SplineSegment"/>, <see cref="ScatterSegment"/> and <see cref="FastLineSegment"/> classes.
    /// </summary>
    public abstract partial class CartesianSegment : ChartSegment
    {
        #region Methods

        #region Animation Methods

        internal float GetDynamicAnimationValue(float animationValue, float value, float oldValue, float newValue)
        {
            if (!double.IsNaN(oldValue) && !double.IsNaN(newValue))
            {
                return (float)((newValue > oldValue) ?
                    oldValue + ((newValue - oldValue) * animationValue)
                    : oldValue - ((oldValue - newValue) * animationValue));
            }
            else
            {
                return double.IsNaN(oldValue) ? (float)newValue : (float)(oldValue - (oldValue * animationValue));
            }
        }

        internal void AnimateSeriesClipRect(ICanvas canvas, float animationValue)
        {
            CartesianSeries? cartesianSeries = Series as CartesianSeries;

            if (cartesianSeries != null && cartesianSeries.EnableAnimation && cartesianSeries.ChartArea is CartesianChartArea chartArea)
            {
                RectF seriesClipRect = cartesianSeries.AreaBounds;

                if (chartArea.IsTransposed)
                {
                    canvas.ClipRectangle(0, seriesClipRect.Height - (seriesClipRect.Height * animationValue), seriesClipRect.Width, seriesClipRect.Height);
                }
                else
                {
                    canvas.ClipRectangle(0, 0, seriesClipRect.Right * animationValue, seriesClipRect.Bottom);
                }
            }
        }

        #endregion

        #region DataLabel Methods

        internal void CalculateDataLabelPosition(double xvalue, double yvalue, double actualYValues)
        {
            var xyDataSeries = Series as XYDataSeries;
            var dataLabelSettings = xyDataSeries?.DataLabelSettings;

            if (xyDataSeries == null || xyDataSeries.ChartArea == null || !xyDataSeries.ShowDataLabels || dataLabelSettings == null) return;

            IsEmpty = double.IsNaN(yvalue);
           
            double x = xvalue, y = xyDataSeries.GetDataLabelPositionAtIndex(Index);
            InVisibleRange = xyDataSeries.IsDataInVisibleRange(xvalue, y);
            xyDataSeries.CalculateDataPointPosition(Index, ref x, ref y);
            PointF labelPoint = new PointF((float)x, (float)y);
            DataLabelXPosition = x;
            DataLabelYPosition = y;
            LabelContent = xyDataSeries.GetLabelContent(actualYValues, xyDataSeries.SumOfValues(xyDataSeries.YValues));
            UpdateDataLabels();               
        }

        internal override void UpdateDataLabels()
        {
            if (Series is XYDataSeries series)
            {
                var datalabelSettings = series.DataLabelSettings;

                if (datalabelSettings == null)
                {
                    return;
                }

                if (DataLabels != null && DataLabels.Count > 0)
                {
                    ChartDataLabel dataLabel = DataLabels[0];

                    dataLabel.LabelStyle = datalabelSettings.LabelStyle;
                    dataLabel.Background = datalabelSettings.LabelStyle.Background;
                    dataLabel.Index = Index;
                    dataLabel.Item = Item;
                    dataLabel.Label = LabelContent is not null ? LabelContent : string.Empty;

                    LabelPositionPoint = InVisibleRange && !IsEmpty ? datalabelSettings.CalculateDataLabelPoint(series, this, new PointF((float)DataLabelXPosition, (float)DataLabelYPosition), datalabelSettings.LabelStyle) : new PointF(float.NaN, float.NaN);
                    dataLabel.XPosition = LabelPositionPoint.X;
                    dataLabel.YPosition = LabelPositionPoint.Y;
                }
            }
        }

        #endregion

        #endregion
    }
}
