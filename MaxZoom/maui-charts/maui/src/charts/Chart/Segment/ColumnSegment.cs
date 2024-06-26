﻿using Microsoft.Maui.Graphics;
using Microsoft.Maui;
using System;
using Syncfusion.Maui.Graphics.Internals;
using Syncfusion.Maui.Core;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// Represents a segment of a column chart and stacked column chart.
    /// </summary>
    public partial class ColumnSegment : CartesianSegment
    {
        #region Fields

        private double x1, y1, x2, y2, xvalue, labelContent;

        #endregion

        #region Properties

        #region Public Properties

        /// <summary>
        /// Gets the left position value for the column segment.
        /// </summary>
        public float Left { get; internal set; }

        /// <summary>
        /// Gets the top position value for the column segment.
        /// </summary>
        public float Top { get; internal set; }

        /// <summary>
        /// Gets the right position value for the column segment.
        /// </summary>
        public float Right { get; internal set; }

        /// <summary>
        /// Gets the bottom position value for the column segment.
        /// </summary>
        public float Bottom { get; internal set; }

        #endregion

        #region Internal  Properties

        internal float Y1 { get; set; } = float.NaN;
        internal float Y2 { get; set; } = float.NaN;
        internal float PreviousY1 { get; set; } = float.NaN;
        internal float PreviousY2 { get; set; } = float.NaN;
      
    #endregion

    #endregion

    #region Methods

    #region Protected Methods

        /// <inheritdoc/>
        protected internal override void OnLayout()
        {
            if (Series is not CartesianSeries)
            {
                return;
            }

            Layout();
        }

        /// <inheritdoc/>
        protected internal override void Draw(ICanvas canvas)
        {
            if (Series is not CartesianSeries series || series is not ISBSDependent sBSDependent|| series.ActualXAxis == null)
            {
                return;
            }

            if (series.CanAnimate())
            {
                Layout();
            }

            if (!float.IsNaN(Left) && !float.IsNaN(Top) && !float.IsNaN(Right) && !float.IsNaN(Bottom))
            {
                canvas.StrokeSize = (float)StrokeWidth;
                canvas.StrokeColor = Stroke.ToColor();
                canvas.StrokeDashPattern = StrokeDashArray?.ToFloatArray();
                canvas.Alpha = Opacity;
                CornerRadius cornerRadius = sBSDependent.CornerRadius;

                //Drawing segment.
                var rect = new Rect() { Left = Left, Top = Top, Right = Right , Bottom = Bottom };
                var actualCrossingValue = series.ActualXAxis.ActualCrossingValue;
                
                canvas.SetFillPaint(Fill, rect);

                if (cornerRadius.TopLeft > 0 || cornerRadius.TopRight > 0 || cornerRadius.BottomLeft > 0 || cornerRadius.BottomRight > 0)
                {
                    // negative segment
                    if (y1 < (double.IsNaN(actualCrossingValue) ? 0 : actualCrossingValue))
                    {
                        canvas.FillRoundedRectangle(rect, cornerRadius.BottomLeft, cornerRadius.BottomRight, cornerRadius.TopLeft, cornerRadius.TopRight);
                    }
                    else
                    {
                        canvas.FillRoundedRectangle(rect, cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomLeft, cornerRadius.BottomRight);
                    }
                }
                else
                {
                    canvas.FillRectangle(rect);
                }

                //Drawing stroke.
                if (HasStroke)
                {
                    if (cornerRadius.TopLeft > 0 || cornerRadius.TopRight > 0 || cornerRadius.BottomLeft > 0 || cornerRadius.BottomRight > 0)
                    {
                        //negative segment stroke
                        if (y1 < (double.IsNaN(actualCrossingValue) ? 0 : actualCrossingValue))
                        {
                            canvas.DrawRoundedRectangle(rect, cornerRadius.BottomLeft, cornerRadius.BottomRight, cornerRadius.TopLeft, cornerRadius.TopRight);
                        }
                        else
                        {
                            canvas.DrawRoundedRectangle(rect, cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomLeft, cornerRadius.BottomRight);
                        }
                    }
                    else
                    {
                        canvas.DrawRectangle(rect);
                    }
                }                
            }
        }

        #endregion

        #region Internal Methods

        internal void Layout()
        {
            CartesianSeries? series = Series as CartesianSeries;
            var xAxis = series?.ActualXAxis;

            if (series == null || series.ChartArea == null || xAxis == null)
            {
                return;
            }

            var crossingValue = xAxis.ActualCrossingValue;
            var start = Math.Floor(xAxis.VisibleRange.Start);
            var end = Math.Ceiling(xAxis.VisibleRange.End);
            double y1Value = y1;
            double y2Value = y2;

            if (!double.IsNaN(crossingValue) && Series is not StackingColumnSeries)
            {
                //TODO: Ensure the better way to having this. 
                //if (seriesIndex == 0 || (seriesIndex != 0 && Series is CartesianSeries))
                y2Value = crossingValue;
            }

            if (series.CanAnimate())
            {
                float animationValue = series.AnimationValue;

                if (!series.XRange.Equals(series.PreviousXRange) || (float.IsNaN(PreviousY1) && float.IsNaN(PreviousY2)))
                {
                    y1Value *= animationValue;
                    y2Value *= animationValue;
                }
                else
                {
                    y1Value = GetColumnDynamicAnimationValue(animationValue, PreviousY1, y1);
                    y2Value = GetColumnDynamicAnimationValue(animationValue, PreviousY2, y2);
                }
            }

            Left = Top = Right = Bottom = float.NaN;

            if (x1 <= end && x2 >= start)
            {
                Left = series.TransformToVisibleX(x1, y1Value);
                Top = series.TransformToVisibleY(x1, y1Value);
                Right = series.TransformToVisibleX(x2, y2Value);
                Bottom = series.TransformToVisibleY(x2, y2Value);

                if (Left > Right)
                {
                    (Right, Left) = (Left, Right);
                }

                if (Top > Bottom)
                {
                    (Bottom, Top) = (Top, Bottom);
                }

                Y1 = (float)y1Value;
                Y2 = (float)y2Value;
            }
            else
            {
                this.Left = float.NaN;
            }

            SegmentBounds = new RectF(Left, Top, Right - Left, Bottom - Top);
        }


        internal override void SetData(double[] values)
        {
            if (Series is not CartesianSeries series)
            {
                return;
            }
            x1 = values[0];
            x2 = values[1];
            y1 = values[2];
            y2 = values[3];
            xvalue = values[4];
            labelContent = values[5];

            Empty = double.IsNaN(x1) || double.IsNaN(x2) || double.IsNaN(y1) || double.IsNaN(y2);

            series.XRange += DoubleRange.Union(xvalue);
            series.YRange += new DoubleRange(y1, y2);
        }

        internal void SetPreviousData(float[] values)
        {
            this.PreviousY1 = values[0];
            this.PreviousY2 = values[1];
        }

        internal override int GetDataPointIndex(float x, float y)
        {
            if (Series != null && SegmentBounds.Contains(x, y))
            {
                return Series.Segments.IndexOf(this);
            }

            return -1;
        }

        internal override void OnDataLabelLayout()
        {
            CalculateDataLabelPosition(xvalue, y1, labelContent);
        }

        #endregion

        #region private Methods

       
        private float GetColumnDynamicAnimationValue(float animationValue, double oldValue, double currentValue)
        {
            if (!double.IsNaN(oldValue) && !double.IsNaN(currentValue))
            {
                return (float)((currentValue > oldValue) ?
                    oldValue + ((currentValue - oldValue) * animationValue)
                    : oldValue - ((oldValue - currentValue) * animationValue));
            }
            else
            {
                return double.IsNaN(oldValue) ? (float)currentValue * animationValue : (float)(oldValue - (oldValue * animationValue));
            }
        }

        #endregion

        #endregion
    }
}
