using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Graphics.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// Represents the segment for <see cref="RangeAreaSeries"/>.
    /// </summary>
    public class RangeAreaSegment : AreaSegment, IMarkerDependentSegment
    {
        #region Fields

        private PathF? path;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data point values from the series that are bound with y1(High) for the segment.
        /// </summary>
        internal double[]? HighValues { get; set; }

        /// <summary>
        /// Gets the data point values from the series that are bound with y2(Low) for the segment.
        /// </summary>
        internal double[]? LowValues { get;  set; }

        internal List<float>? HighStrokePoints { get; set; }

        internal List<float>? LowStrokePoints { get; set; }

        internal int LabelIndex { get; set; }


        #endregion

        #region Methods

        #region Protected Methods

        /// <inheritdoc/>
        protected internal override void Draw(ICanvas canvas)
        {
            if (Empty)
            {
                return;
            }

            canvas.Alpha = Opacity;

            if (HasStroke)
            {
                canvas.StrokeSize = (float)StrokeWidth;
                canvas.StrokeColor = Stroke.ToColor();

                if (StrokeDashArray != null)
                {
                    canvas.StrokeDashPattern = StrokeDashArray.ToFloatArray();
                }
            }
            
            DrawPath(canvas, FillPoints, HighStrokePoints, LowStrokePoints);
        }

        /// <inheritdoc/>
        protected internal override void OnLayout()
        {
            if (XValues == null)
            {
                return;
            }

            this.CalculateInteriorPoints();

            if (HasStroke)
            {
                this.CalculateStrokePoints();
            }
        }

        #endregion

        #region Internal Methods

        internal void SetData(IList xVals, IList highVals, IList lowVals)
        {
            if (Series is CartesianSeries series)
            {
                var count = xVals.Count;
                this.XValues = new double[count];
                this.HighValues = new double[count];
                this.LowValues = new double[count];

                xVals.CopyTo(this.XValues, 0);
                highVals.CopyTo(this.HighValues, 0);
                lowVals.CopyTo(this.LowValues, 0);

                var high = HighValues.Min();
                var low = LowValues.Min();
                var yMin = high > low ? low : high;
                high = HighValues.Max();
                low = LowValues.Max();
                var yMax = high > low ? high : low;

                series.XRange += new DoubleRange(XValues.Min(), XValues.Max());
                series.YRange += new DoubleRange(yMin, yMax);
            }   
        }

        internal override int GetDataPointIndex(float x, float y)
        {
            if (Series != null && Series.SeriesContainsPoint(new PointF(x, y)))
            {
                return Series.Segments.IndexOf(this);
            }
            
            return -1;
        }

        internal override void OnDataLabelLayout()
        {
            if(Series is RangeAreaSeries series && series.LabelTemplate is not null)
            {
                var dataLabelSettings = series.DataLabelSettings;
                ChartDataLabelStyle labelStyle = dataLabelSettings.LabelStyle;

                if (XValues == null || HighValues == null || LowValues == null || series.ActualData == null) return;

                for (int i = 0; i < series.PointsCount; i++)
                {
                    int highIndex = 0;
                    int lowIndex = 0;

                    Index = i;

                    for (int j = 0; j < 2; j++)
                    {                       
                        if (j == 0)
                        {
                            highIndex = (2 * i) + 1;

                            double x = XValues[i];
                            double y = HighValues[i];

                            InVisibleRange = series.IsDataInVisibleRange(x, y);
                            LabelContent = dataLabelSettings.GetLabelContent(HighValues[i]);
                            LabelIndex = highIndex - 1;

                            if (DataLabels != null && DataLabels.Count > highIndex - 1)
                            {
                                var dataLabel = DataLabels[highIndex - 1];

                                dataLabel.LabelStyle = labelStyle;
                                dataLabel.Index = i;
                                dataLabel.Item = series.ActualData[i];
                                dataLabel.Label = LabelContent ?? string.Empty;

                                if (!InVisibleRange || IsEmpty)
                                {
                                    LabelPositionPoint = new PointF(float.NaN, float.NaN);
                                }
                                else
                                {
                                    series.CalculateDataPointPosition(i, ref x, ref y);
                                    PointF labelPoint = new PointF((float)x, (float)y);
                                    LabelPositionPoint = dataLabelSettings.CalculateDataLabelPoint(series, this, labelPoint, labelStyle, "HighType");
                                }

                                dataLabel.XPosition = LabelPositionPoint.X;
                                dataLabel.YPosition = LabelPositionPoint.Y;
                            }
                        }
                        else
                        {
                            lowIndex = highIndex + 1;

                            double x = XValues[i];
                            double y = LowValues[i];

                            InVisibleRange = series.IsDataInVisibleRange(x, y);
                            LabelContent = dataLabelSettings.GetLabelContent(LowValues[i]);
                            LabelIndex = lowIndex - 1;

                            if (DataLabels != null && DataLabels.Count > lowIndex - 1)
                            {
                                var dataLabel = DataLabels[lowIndex - 1];

                                dataLabel.LabelStyle = labelStyle;
                                dataLabel.Index = i;
                                dataLabel.Item = series.ActualData[i];
                                dataLabel.Label = LabelContent ?? string.Empty;

                                if (!InVisibleRange || IsEmpty)
                                {
                                    LabelPositionPoint = new PointF(float.NaN, float.NaN);
                                }
                                else
                                {
                                    series.CalculateDataPointPosition(i, ref x, ref y);
                                    PointF labelPoint = new PointF((float)x, (float)y);
                                    LabelPositionPoint = dataLabelSettings.CalculateDataLabelPoint(series, this, labelPoint, labelStyle, "LowType");
                                }

                                dataLabel.XPosition = LabelPositionPoint.X;
                                dataLabel.YPosition = LabelPositionPoint.Y;
                            }
                        }   
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private void DrawPath(ICanvas canvas, IList<float>? fillPoints, IList<float>? highstrokePoints, IList<float>? lowstrokePoints)
        {
            if (Series == null)
            {
                return;
            }

            path = new PathF();

            float animationValue = Series.AnimationValue;
            bool isDynamicAnimation = Series.CanAnimate() && Series.XRange.Equals(Series.PreviousXRange) && PreviousFillPoints != null && fillPoints != null && fillPoints.Count == PreviousFillPoints.Count;

            if (Series.CanAnimate() && !isDynamicAnimation)
            {
                AnimateSeriesClipRect(canvas, animationValue);
            }

            if (fillPoints != null)
            {
                for (int i = 0; i < fillPoints.Count; i++)
                {
                    var x = fillPoints[i];
                    var y = fillPoints[i + 1];

                    if (isDynamicAnimation && PreviousFillPoints != null)
                    {
                        x = GetDynamicAnimationValue(animationValue, x, PreviousFillPoints[i], x);
                        y = GetDynamicAnimationValue(animationValue, y, PreviousFillPoints[i + 1], y);
                    }

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

            if (HasStroke && highstrokePoints != null && lowstrokePoints != null)
            {
                DrawStroke(canvas, animationValue, isDynamicAnimation, highstrokePoints);
                DrawStroke(canvas, animationValue, isDynamicAnimation, lowstrokePoints);
            }
        }

        private void DrawStroke(ICanvas canvas, float animationValue, bool isDynamicAnimation, IList<float> strokePoints)
        {
            path = new PathF();

            for (int i = 0; i < strokePoints.Count; i++)
            {
                var x = strokePoints[i];
                var y = strokePoints[i + 1];

                if (isDynamicAnimation && PreviousStrokePoints != null)
                {
                    x = GetDynamicAnimationValue(animationValue, x, PreviousStrokePoints[i], x);
                    y = GetDynamicAnimationValue(animationValue, y, PreviousStrokePoints[i + 1], y);
                }

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

        private void CalculateInteriorPoints()
        {
            float startX, startY;

            if (Series is CartesianSeries cartesian && cartesian.ActualXAxis != null && cartesian.ActualYAxis != null && XValues != null && HighValues != null && LowValues != null)
            {
                var count = HighValues.Length;
                this.FillPoints = new List<float>();
                var start = cartesian.ActualYAxis.VisibleRange.Start;
                double xValue = this.XValues[0], lowValue = LowValues[0], highValue;
                startX = cartesian.TransformToVisibleX(xValue, lowValue);
                startY = cartesian.TransformToVisibleY(xValue, lowValue);
                FillPoints.Add(startX);
                FillPoints.Add(startY);

                for (int i = 0; i < count; i++)
                {
                    xValue = this.XValues[i];
                    highValue = this.HighValues[i];
                    FillPoints.Add(cartesian.TransformToVisibleX(xValue, highValue));
                    FillPoints.Add(cartesian.TransformToVisibleY(xValue, highValue));
                }

                for (int i = count - 1; i > 0; i--)
                {
                    xValue = this.XValues[i];
                    lowValue = this.LowValues[i];
                    FillPoints.Add(cartesian.TransformToVisibleX(xValue, lowValue));
                    FillPoints.Add(cartesian.TransformToVisibleY(xValue, lowValue));
                }

                FillPoints.Add(startX);
                FillPoints.Add(startY);
            }
        }

        private void CalculateStrokePoints()
        {
            float startX, startY;

            if (Series is CartesianSeries cartesian && cartesian.ActualXAxis != null && cartesian.ActualYAxis != null && XValues != null && HighValues != null && LowValues != null)
            {
                var count = HighValues.Length;
                this.HighStrokePoints = new List<float>();
                this.LowStrokePoints = new List<float>();
                var start = cartesian.ActualYAxis.VisibleRange.Start;
                double xValue = this.XValues[0], lowValue = LowValues[0], highValue = HighValues[0];
                startX = cartesian.TransformToVisibleX(xValue, highValue);
                startY = cartesian.TransformToVisibleY(xValue, highValue);
                HighStrokePoints.Add(startX);
                HighStrokePoints.Add(startY);

                for (int i = 0; i < count; i++)
                {
                    xValue = this.XValues[i];
                    highValue = this.HighValues[i];
                    HighStrokePoints.Add(cartesian.TransformToVisibleX(xValue, highValue));
                    HighStrokePoints.Add(cartesian.TransformToVisibleY(xValue, highValue));
                }

                xValue = this.XValues[0];
                startX = cartesian.TransformToVisibleX(xValue, lowValue);
                startY = cartesian.TransformToVisibleY(xValue, lowValue);
                LowStrokePoints.Add(startX);
                LowStrokePoints.Add(startY);

                for (int i = 0; i < count; i++)
                {
                    xValue = this.XValues[i];
                    lowValue = this.LowValues[i];
                    LowStrokePoints.Add(cartesian.TransformToVisibleX(xValue, lowValue));
                    LowStrokePoints.Add(cartesian.TransformToVisibleY(xValue, lowValue));
                }
            }
        }

        Rect IMarkerDependentSegment.GetMarkerRect(double markerWidth, double markerHeight, int tooltipIndex)
        {
            Rect rect = new Rect();

            if (Series != null && FillPoints != null)
            {
                if (XValues?.Length > tooltipIndex)
                {
                    var xIndex = (2 * tooltipIndex) + 2;
                    rect = new Rect(FillPoints[xIndex] - (markerWidth / 2), FillPoints[xIndex + 1] - (markerHeight / 2), markerWidth, markerHeight);
                }
            }

            return rect;
        }

        void IMarkerDependentSegment.DrawMarker(ICanvas canvas)
        {
            if (Series is IMarkerDependent series && FillPoints != null)
            {
                var marker = series.MarkerSettings;
                var fill = marker.Fill;
                var type = marker.Type;
                
                for (int i = 2; i < FillPoints.Count; i += 2)
                {
                    var rect = new Rect(FillPoints[i] - (marker.Width / 2), FillPoints[i + 1] - (marker.Height / 2), marker.Width, marker.Height);

                    canvas.SetFillPaint(fill == default(Brush) ? this.Fill : fill, rect);

                    series.DrawMarker(canvas, Index, type, rect);
                }
            }
        }
        #endregion

        #endregion
    }
}
