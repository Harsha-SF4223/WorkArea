using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// Represents the segment for <see cref="StepAreaSeries"/>
    /// </summary>
    public class StepAreaSegment : AreaSegment, IMarkerDependentSegment
    {
        #region Methods

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

        /// <summary>
        /// Calculate interior points.
        /// </summary>
        private void CalculateInteriorPoints()
        {
            if (Series is CartesianSeries cartesian && cartesian.ActualXAxis != null && XValues != null && YValues != null)
            {
                var crossingValue = cartesian.ActualXAxis.ActualCrossingValue;
                var count = XValues.Length;
                this.FillPoints = new List<float>();
                double yValue = this.YValues[0], xValue = this.XValues[0];
                crossingValue = double.IsNaN(crossingValue) ? 0 : crossingValue;
                this.FillPoints.Add(cartesian.TransformToVisibleX(xValue, 0));
                FillPoints.Add(cartesian.TransformToVisibleY(xValue, crossingValue));

                for (int i = 0; i < count; i++)
                {
                    xValue = this.XValues[i];
                    yValue = this.YValues[i];
                    double x1Value = this.XValues[count > i + 1 ? i + 1 : i];
                    this.FillPoints.Add(cartesian.TransformToVisibleX(xValue, yValue));
                    this.FillPoints.Add(cartesian.TransformToVisibleY(xValue, yValue));
                    var midX = cartesian.TransformToVisibleX(x1Value, yValue);
                    var midY = cartesian.TransformToVisibleY(x1Value, yValue);

                    if (i == count - 1)
                    {
                        midX = cartesian.TransformToVisibleX(xValue, yValue);
                        midY = cartesian.TransformToVisibleY(xValue, yValue);
                    }

                    FillPoints.Add(midX);
                    FillPoints.Add(midY);

                }

                xValue = this.XValues[count - 1];
                this.FillPoints.Add(cartesian.TransformToVisibleX(xValue, 0));
                FillPoints.Add(cartesian.TransformToVisibleY(xValue, crossingValue));
            }
        }


        /// <summary>
        /// Calculate stroke points.
        /// </summary>
        private void CalculateStrokePoints()
        {
            if (Series is CartesianSeries cartesian && cartesian.ActualXAxis != null && XValues != null && YValues != null)
            {
                var crossingValue = cartesian.ActualXAxis.ActualCrossingValue;
                var count = XValues.Length;
                this.StrokePoints = new List<float>();

                for (int i = 0; i < count; i++)
                {
                    double xValue = this.XValues[i];
                    double yValue = this.YValues[i];
                    double x1Value = this.XValues[count > i + 1 ? i + 1 : i];
                    this.StrokePoints.Add(cartesian.TransformToVisibleX(xValue, yValue));
                    this.StrokePoints.Add(cartesian.TransformToVisibleY(xValue, yValue));
                    var midX = cartesian.TransformToVisibleX(x1Value, yValue);
                    var midY = cartesian.TransformToVisibleY(x1Value, yValue);

                    if (i == count - 1)
                    {
                        midX = cartesian.TransformToVisibleX(xValue, yValue);
                        midY = cartesian.TransformToVisibleY(xValue, yValue);
                    }

                    StrokePoints.Add(midX);
                    StrokePoints.Add(midY);

                }

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

        void IMarkerDependentSegment.DrawMarker(ICanvas canvas)
        {
            if (Series is IMarkerDependent series && FillPoints != null)
            {
                var marker = series.MarkerSettings;
                var fill = marker.Fill;
                var type = marker.Type;
                for (int i = 2; i < FillPoints.Count - 3; i += 4)
                {
                    var rect = new Rect(FillPoints[i] - (marker.Width / 2), FillPoints[i + 1] - (marker.Height / 2), marker.Width, marker.Height);

                    canvas.SetFillPaint(fill == default(Brush) ? this.Fill : fill, rect);

                    series.DrawMarker(canvas, Index, type, rect);
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
                    var xIndex = 4*(tooltipIndex) + 2;
                    rect = new Rect(FillPoints[xIndex] - (markerWidth / 2), FillPoints[xIndex + 1] - (markerHeight / 2), markerWidth, markerHeight);
                }
            }

            return rect;
        }

        #endregion
    }
}
