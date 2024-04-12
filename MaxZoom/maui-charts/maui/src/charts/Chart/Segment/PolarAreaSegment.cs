using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Graphics.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// Represents a segment of a polar area chart.
    /// </summary>
    public class PolarAreaSegment : ChartSegment, IMarkerDependentSegment
    {
        #region Fields

        private double[]? xValues { get; set; }
        private double[]? yValues { get; set; }
        private int pointsCount;
        private PathF? path;
        private List<float>? segmentFillPoints, segmentStrokePoints;

        #endregion

        #region Methods

        #region Interface Implementation

        void IMarkerDependentSegment.DrawMarker(ICanvas canvas)
        {
            if (Series is IMarkerDependent series && segmentFillPoints != null)
            {
                var marker = series.MarkerSettings;
                var fill = marker.Fill;
                var type = marker.Type;
                for (int i = 2; i < segmentFillPoints.Count - 3; i += 2)
                {
                    var rect = new Rect(segmentFillPoints[i] - (marker.Width / 2), segmentFillPoints[i + 1] - (marker.Height / 2), marker.Width, marker.Height);

                    canvas.SetFillPaint(fill == default(Brush) ? this.Fill : fill, rect);

                    series.DrawMarker(canvas, Index, type, rect);
                }
            }
        }

        Rect IMarkerDependentSegment.GetMarkerRect(double markerWidth, double markerHeight, int tooltipIndex)
        {
            Rect rect = new Rect();
            if (Series != null && segmentFillPoints != null)
            {
                if (xValues?.Length > tooltipIndex)
                {
                    var xIndex = (2 * tooltipIndex) + 2;
                    rect = new Rect(segmentFillPoints[xIndex] - (markerWidth / 2), segmentFillPoints[xIndex + 1] - (markerHeight / 2), markerWidth, markerHeight);
                }
            }

            return rect;
        }

        #endregion

        #region Internal Method

        internal override void SetData(IList xDatas, IList yDatas)
        {
            var series = Series as PolarSeries;

            if (series != null && series.ActualYAxis != null)
            {
                pointsCount = xDatas.Count;
                xValues = new double[pointsCount];
                yValues = new double[pointsCount];
                xDatas.CopyTo(xValues, 0);
                yDatas.CopyTo(yValues, 0);
                var yMin = yValues.Min();
                yMin = double.IsNaN(yMin) ? yValues.Any() ? yValues.Where(e => !double.IsNaN(e)).DefaultIfEmpty().Min() : 0 : yMin;
                yMin = yMin == 0 ? series.ActualYAxis.VisibleRange.Start : yMin;
                series.XRange += new DoubleRange(this.xValues.Min(), this.xValues.Max());
                series.YRange += new DoubleRange(yMin, this.yValues.Max());
            }
        }

        #endregion

        #region Protected Method

        /// <inheritdoc/>
        protected internal override void OnLayout()
        {
            if (xValues == null)
            {
                return;
            }

            segmentFillPoints = GenerateInteriorPoints(1);

            if (HasStroke)
            {
                segmentStrokePoints = GenerateStrokePoints(1);
            }
        }

        internal override void OnDataLabelLayout()
        {
            if (Series is PolarAreaSeries series && series != null && series.LabelTemplate != null)
            {
                var dataLabeSettings = series.DataLabelSettings;

                List<double> xValues = series.GetXValues()!;

                if (dataLabeSettings == null || xValues == null) return;

                ChartDataLabelStyle labelStyle = dataLabeSettings.LabelStyle;

                if (xValues == null || series.YValues == null) return;

                for (int i = 0; i < series.PointsCount; i++)
                {
                    double x = xValues[i], y = series.YValues[i];

                    if (double.IsNaN(y)) continue;

                    series.CalculateDataPointPosition(i, ref x, ref y);
                    PointF labelPoint = new PointF((float)x, (float)y);

                    Index = i;
                    LabelContent = series.GetLabelContent(series.YValues[i], series.SumOfValues(series.YValues));

                    if (DataLabels != null && DataLabels.Count > i)
                    {
                        ChartDataLabel dataLabel = DataLabels[i];

                        dataLabel.LabelStyle = dataLabeSettings.LabelStyle;
                        dataLabel.Background = dataLabeSettings.LabelStyle.Background;
                        dataLabel.Index = i;
                        dataLabel.Item = series.ActualData?[i];
                        dataLabel.Label = LabelContent is not null ? LabelContent : string.Empty;

                        LabelPositionPoint = series.CalculateDataLabelPoint(this, labelPoint, labelStyle);
                        dataLabel.XPosition = LabelPositionPoint.X;
                        dataLabel.YPosition = LabelPositionPoint.Y;
                    }
                }
            }
        }

        /// <inheritdoc/>
        protected internal override void Draw(ICanvas canvas)
        {
            if (Empty || Series == null)
            {
                return;
            }

            var fillPoints = segmentFillPoints;
            var strokePoints = segmentStrokePoints;

            if (Series.CanAnimate())
            {
                fillPoints = GenerateInteriorPoints(Series.AnimationValue);

                if (HasStroke)
                {
                    strokePoints = GenerateStrokePoints(Series.AnimationValue);
                }
            }

            canvas.Alpha = Opacity;

            if (HasStroke)
            {
                canvas.StrokeSize = (float)StrokeWidth;
                canvas.StrokeColor = Stroke.ToColor();
            }

            if (StrokeDashArray != null)
            {
                canvas.StrokeDashPattern = StrokeDashArray.ToFloatArray();
            }

            DrawPath(canvas, fillPoints, strokePoints);
        }

        #endregion

        #region Private Methods

        private void DrawPath(ICanvas canvas, List<float>? fillPoints, List<float>? strokePoints)
        {
            path = new PathF();
            if (fillPoints != null)
            {
                for (int i = 0; i < fillPoints.Count; i++)
                {
                    var x = fillPoints[i];
                    var y = fillPoints[i + 1];

                    if (i == 0)
                    {
                        path.MoveTo(x, y);
                    }
                    else
                    {
                        path.LineTo(x, y);
                    }

                    i++;
                }

                canvas.SetFillPaint(Fill, path.Bounds);
                canvas.FillPath(path);
            }

            if (HasStroke && strokePoints != null)
            {
                path = new PathF();
                for (int i = 0; i < strokePoints.Count; i++)
                {
                    var x = strokePoints[i];
                    var y = strokePoints[i + 1];
                    if (i == 0)
                    {
                        path.MoveTo(x, y);
                    }
                    else
                    {
                        path.LineTo(x, y);
                    }

                    i++;
                }

                canvas.DrawPath(path);
            }
        }

        private List<float> GenerateInteriorPoints(float animationValue)
        {
            var series = Series as PolarSeries;
            var fillPoints = new List<float>();
            if (series != null && series.ActualXAxis != null && series.ActualYAxis != null && xValues != null && yValues != null)
            {
                PointF pointF = series.TransformVisiblePoint(xValues[0], yValues[0], animationValue);
                fillPoints.Add(pointF.X);
                fillPoints.Add(pointF.Y);
                for (int i = 0; i < pointsCount; i++)
                {
                    PointF midPointF = series.TransformVisiblePoint(xValues[i], yValues[i], animationValue);
                    fillPoints.Add(midPointF.X);
                    fillPoints.Add(midPointF.Y);
                }

                if (series.IsClosed)
                {
                    PointF endPointF = series.TransformVisiblePoint(xValues[0], yValues[0], animationValue);
                    fillPoints.Add(endPointF.X);
                    fillPoints.Add(endPointF.Y);
                }
                else
                {
                    PointF endPointF = series.TransformVisiblePoint(xValues[0], series.ActualYAxis.ActualRange.Start, animationValue);
                    fillPoints.Add(endPointF.X);
                    fillPoints.Add(endPointF.Y);
                }
            }

            return fillPoints;
        }

        private List<float> GenerateStrokePoints(float animationValue)
        {
            var series = Series as PolarSeries;
            var strokePoints = new List<float>();
            if (series != null && series.ActualXAxis != null && series.ActualYAxis != null && xValues != null && yValues != null)
            {
                PointF startPoint = series.TransformVisiblePoint(xValues[0], yValues[0], animationValue);
                strokePoints.Add(startPoint.X);
                strokePoints.Add(startPoint.Y);
                for (int i = 1; i < pointsCount; i++)
                {
                    PointF midPoint = series.TransformVisiblePoint(xValues[i], yValues[i], animationValue);
                    strokePoints.Add(midPoint.X);
                    strokePoints.Add(midPoint.Y);
                }

                if (series.IsClosed)
                {
                    PointF pointF = series.TransformVisiblePoint(xValues[0], yValues[0], animationValue);
                    strokePoints.Add(pointF.X);
                    strokePoints.Add(pointF.Y);
                }
                else
                {
                    PointF pointF = series.TransformVisiblePoint(xValues[0], series.ActualYAxis.ActualRange.Start, animationValue);
                    strokePoints.Add(pointF.X);
                    strokePoints.Add(pointF.Y);
                }
            }

            return strokePoints;
        }

        #endregion

        #endregion
    }
}