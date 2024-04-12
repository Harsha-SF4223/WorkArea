using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Graphics.Internals;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// Represents a segment for the HistogramSeries in a chart, displaying a vertical bar.
    /// </summary>
    public class HistogramSegment : ColumnSegment
    {

        #region Internal properties

        internal double HistrogramLabelPosition { get; set; }
        internal double PointsCount { get; set; }
        

        #endregion

        #region Internal Methods

        internal override void OnDataLabelLayout()
        {
            CalculateDataLabelPosition(HistrogramLabelPosition, PointsCount, PointsCount);
        }

        internal override void SetData(double[] values)
        {
            base.SetData(values);
            if (Series is HistogramSeries series)
                series.XRange += new DoubleRange(values[0], values[1]);
        }

        #endregion

        #region Protected Internal Methods

        /// <summary>
        /// Draws the histogram segment on the specified canvas.
        /// </summary>
        /// <param name="canvas"></param>
        protected internal override void Draw(ICanvas canvas)
        {
            if (Series is not HistogramSeries series || series.ActualXAxis == null)
                return;

            if (series.CanAnimate())
                Layout();

            if (!float.IsNaN(Left) && !float.IsNaN(Top) && !float.IsNaN(Right) && !float.IsNaN(Bottom))
            {
                canvas.StrokeSize = (float)StrokeWidth;
                canvas.StrokeColor = Stroke.ToColor();
                canvas.Alpha = Opacity;
                var rect = new Rect() { Left = Left, Top = Top, Right = Right, Bottom = Bottom };
                canvas.SetFillPaint(Fill, rect);
                canvas.FillRectangle(rect);

                if (HasStroke)
                    canvas.DrawRectangle(rect);
            }
        }

        #endregion
    }

    internal class DistributionSegment : ILineDrawing
    {
        #region Fields

        private HistogramSeries histogramSeries;
        private bool enableAntiAliasing = false;
        private Brush? stroke;
        private double strokeWidth;
        private float opacity = 1;
        private DoubleCollection? strokeDashArray;

        #endregion

        #region Internal Properties

        internal float[]? DrawPoints { get; set; }

        #endregion

        #region Interface Implementation

        Color ILineDrawing.Stroke
        {
            get
            {
                if (stroke is SolidColorBrush brush)
                    return brush.Color;
                else
                    return Colors.Black;
            }
            set => stroke = new SolidColorBrush(value);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        double ILineDrawing.StrokeWidth
        {
            get => strokeWidth;
            set => strokeWidth = value;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        bool ILineDrawing.EnableAntiAliasing
        {
            get => enableAntiAliasing;
            set => enableAntiAliasing = value;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        float ILineDrawing.Opacity
        {
            get => opacity;
            set => opacity = value;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        DoubleCollection? ILineDrawing.StrokeDashArray
        {
            get => strokeDashArray;
            set => strokeDashArray = value;
        }

        #endregion

        #region Constructor

        internal DistributionSegment(HistogramSeries series)
        {
            histogramSeries = series;
        }

        #endregion

        #region Internal Methods

        internal void UpdateCurveStyle()
        {
            if (histogramSeries.CurveStyle is ChartLineStyle chartLineStyle)
            {
                stroke = chartLineStyle.Stroke;
                strokeWidth = chartLineStyle.StrokeWidth;
                strokeDashArray = chartLineStyle.StrokeDashArray;
            }
        }

        internal void OnDraw(ICanvas canvas)
        {
            if (histogramSeries.ShowNormalDistributionCurve && !histogramSeries.CanAnimate())
            {
                canvas.DrawLines(DrawPoints!, this);
            }
        }

        #endregion
    }
}