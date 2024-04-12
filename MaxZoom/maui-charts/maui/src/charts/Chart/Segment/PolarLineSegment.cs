using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Graphics.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// Represents a segment of a polar line chart.
    /// </summary>
    public class PolarLineSegment : ChartSegment, IMarkerDependentSegment
    {
        #region Fields

        private double x1Value, y1Value, x2Value, y2Value;
        private float x1Pos = float.NaN, x2Pos = float.NaN, y1Pos = float.NaN, y2Pos = float.NaN;

        #endregion

        #region Methods

        #region Interface Implementation

        void IMarkerDependentSegment.DrawMarker(ICanvas canvas)
        {
            if (Series is IMarkerDependent series)
            {
                var marker = series.MarkerSettings;
                var fill = marker.Fill;
                var type = marker.Type;

                var rect = new Rect(x1Pos - (marker.Width / 2), y1Pos - (marker.Height / 2), marker.Width, marker.Height);

                canvas.SetFillPaint(fill == default(Brush) ? this.Fill : fill, rect);
                series.DrawMarker(canvas, Index, type, rect);
            }
        }

        Rect IMarkerDependentSegment.GetMarkerRect(double markerWidth, double markerHeight, int tooltipIndex)
        {
            Rect rect = new Rect(this.x1Pos - (markerWidth / 2), this.y1Pos - (markerHeight / 2), markerWidth, markerHeight);
            return rect;
        }

        #endregion

        #region Internal Method

        internal override int GetDataPointIndex(float x, float y)
        {
            if (Series != null)
            {
                if (IsRectContains((float)x1Value, (float)y1Value, x, y, (float)StrokeWidth))
                {
                    return Series.Segments.IndexOf(this);
                }
                else if (Series.Segments.IndexOf(this) == Series.Segments.Count - 1 && IsRectContains((float)x2Value, (float)y2Value, x, y, (float)StrokeWidth))
                {
                    return Series.Segments.IndexOf(this) + 1;
                }
            }

            return -1;
        }

        internal override void SetData(double[] values)
        {
            x1Value = values[0];
            y1Value = values[1];
            x2Value = values[2];
            y2Value = values[3];
            Empty = double.IsNaN(x1Value) || double.IsNaN(x2Value) || double.IsNaN(y1Value) || double.IsNaN(y2Value);

            if (Series!.PointsCount == 1)
            {
                this.Series.XRange = new DoubleRange(this.x1Value, this.x1Value);
                this.Series.YRange = new DoubleRange(this.y1Value, this.y1Value);
            }
            else
            {
                this.Series.XRange += new DoubleRange(this.x1Value, this.x2Value);
                this.Series.YRange += new DoubleRange(this.y1Value, this.y2Value);
            }
        }

        internal override void OnDataLabelLayout()
        {
            if(Series != null && Series.LabelTemplate != null)
            {
                CalculateDataLabelPosition(x1Value, y1Value, y1Value);
            }
        }

        internal void CalculateDataLabelPosition(double xvalue, double yvalue, double actualYValues)
        {
            var polarSeries = Series as PolarSeries;
            var dataLabelSettings = polarSeries?.DataLabelSettings;

            if (polarSeries == null || polarSeries.ChartArea == null || !polarSeries.ShowDataLabels || dataLabelSettings == null) return;

            double x = xvalue, y = yvalue;
            polarSeries.CalculateDataPointPosition(Index, ref x, ref y);
            PointF labelPoint = new PointF((float)x, (float)y);
            DataLabelXPosition = x;
            DataLabelYPosition = y;
            LabelContent = polarSeries.GetLabelContent(actualYValues, polarSeries.SumOfValues(polarSeries.YValues));
            UpdateDataLabels();     
        }

        internal override void UpdateDataLabels()
        {
            if (Series is PolarSeries series)
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

                    LabelPositionPoint = series.CalculateDataLabelPoint(this, new PointF((float)DataLabelXPosition, (float)DataLabelYPosition), datalabelSettings.LabelStyle);
                    dataLabel.XPosition = LabelPositionPoint.X;
                    dataLabel.YPosition = LabelPositionPoint.Y;
                }
            }
        }

        #endregion

        #region Protected Internal Methods

        /// <inheritdoc/>
        protected internal override void OnLayout()
        {
            var series = Series as PolarSeries;
            ChartAxis xAxis = series!.ActualXAxis!;
            if (xAxis == null)
            {
                return;
            }

            var start = Math.Floor(xAxis.VisibleRange.Start);
            var end = Math.Ceiling(xAxis.VisibleRange.End);

            if ((x1Value >= start && x1Value <= end) || (x2Value >= start && x2Value <= end) || (start >= x1Value && start <= x2Value))
            {
                PointF point1 = series.TransformVisiblePoint(x1Value, y1Value, 1);
                PointF point2 = series.TransformVisiblePoint(x2Value, y2Value, 1);

                x1Pos = point1.X;
                y1Pos = point1.Y;
                x2Pos = point2.X;
                y2Pos = point2.Y;
            }
        }

        /// <inheritdoc/>
        protected internal override void Draw(ICanvas canvas)
        {
            var series = Series as PolarSeries;
            if (series == null || SeriesView == null || Empty)
            { return; }

            float x1 = x1Pos;
            float y1 = y1Pos;
            float x2 = x2Pos;
            float y2 = y2Pos;

            if (series.CanAnimate())
            {
                var animation = series.AnimationValue;
                PointF point1 = series.TransformVisiblePoint(x1Value, y1Value, animation);
                PointF point2 = series.TransformVisiblePoint(x2Value, y2Value, animation);

                x1 = point1.X;
                y1 = point1.Y;
                x2 = point2.X;
                y2 = point2.Y;
            }

            if (StrokeDashArray != null)
            {
                canvas.StrokeDashPattern = StrokeDashArray.ToFloatArray();
            }

            canvas.StrokeSize = (float)StrokeWidth;
            canvas.StrokeColor = Fill.ToColor();
            canvas.Alpha = Opacity;

            canvas.DrawLine(x1, y1, x2, y2);
        }

        #endregion

        #endregion
    }
}