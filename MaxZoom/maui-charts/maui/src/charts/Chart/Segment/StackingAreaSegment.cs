using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// Represents a segment of a stackingarea chart.
    /// </summary>
    public class StackingAreaSegment : AreaSegment
    {
        #region Properties

        /// <summary>
        /// Gets the bottom values.
        /// </summary>
        internal double[]? BottomValues { get; set; }

        /// <summary>
        /// Gets the top values.
        /// </summary>
        internal double[]? TopValues { get; set; }

        #endregion

        #region Methods

        #region Internal Methods

        internal void SetData(IList xValues, IList yEnds, IList yStarts)
        {
            if (Series is CartesianSeries series && series.ActualYAxis != null)
            {
                var count = xValues.Count;
                XValues = new double[count];
                BottomValues = new double[count];
                TopValues = new double[count];

                xValues.CopyTo(XValues, 0);
                yEnds.CopyTo(TopValues, 0);
                yStarts.CopyTo(BottomValues, 0);

                var yMin = TopValues.Min();
                yMin = double.IsNaN(yMin) ? TopValues.Any() ? TopValues.Where(e => !double.IsNaN(e)).DefaultIfEmpty().Min() : 0 : yMin;
                yMin = yMin == 0 ? series.ActualYAxis!.VisibleRange.Start : yMin;

                Series!.XRange += new DoubleRange(XValues.Min(), XValues.Max());
                Series.YRange += new DoubleRange(yMin, TopValues.Max());
            }
        }

        internal override int GetDataPointIndex(float x, float y)
        {
            return -1;
        }

        #endregion

        #region Protected Internal Methods
        /// <summary>
        /// 
        /// </summary>
        protected internal override void OnLayout()
        {
            if (XValues == null)
            {
                return;
            }

            CalculateInteriorPoints();

            if (HasStroke)
            {
                CalculateStrokePoints();
            }
        }

        #endregion

        #region Private Methods

        private void CalculateInteriorPoints()
        {
            if (Series is CartesianSeries cartesian && cartesian.ActualXAxis != null && XValues != null && BottomValues != null && TopValues != null)
            {
                float startX, startY;
                var count = XValues.Count();
                FillPoints = new List<float>();
                startX = Series!.TransformToVisibleX(XValues[0], BottomValues[0]);
                startY = Series.TransformToVisibleY(XValues[0], BottomValues[0]);
                FillPoints.Add(startX);
                FillPoints.Add(startY);

                for (int i = 0; i < count; i++)
                {
                    FillPoints.Add(Series.TransformToVisibleX(XValues[i], TopValues[i]));
                    FillPoints.Add(Series.TransformToVisibleY(XValues[i], TopValues[i]));
                }

                for (int i = count - 1; i > 0; i--)
                {
                    if (!double.IsNaN(BottomValues[i]))
                    {
                        FillPoints.Add(Series.TransformToVisibleX(XValues[i], BottomValues[i]));
                        FillPoints.Add(Series.TransformToVisibleY(XValues[i], BottomValues[i]));
                    }
                }

                FillPoints.Add(startX);
                FillPoints.Add(startY);
            }
        }

        private void CalculateStrokePoints()
        {
            if (Series != null && XValues != null && BottomValues != null && TopValues != null)
            {
                float x, y;
                var halfStrokeWidth = (float)StrokeWidth / 2;
                StrokePoints = new List<float>();
                double xValue = 0f, yValue;
                int i, total = Series.Segments.Count;
                yValue = total > 1 && Series.Segments.IndexOf(this) != 0 ? TopValues[0] : BottomValues[0];
                var count = XValues.Length;
                for (i = 0; i < count; i++)
                {
                    xValue = XValues[i];
                    yValue = TopValues[i];
                    x = Series.TransformToVisibleX(xValue, yValue);
                    y = Series.TransformToVisibleY(xValue, yValue);
                    StrokePoints.Add(x);
                    StrokePoints.Add(y += yValue >= 0 ? halfStrokeWidth : -halfStrokeWidth);
                }
            }
        }

        #endregion

        #endregion
    }
}
