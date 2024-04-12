using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Graphics.Internals;
using System;
using System.Collections.Generic;

namespace Syncfusion.Maui.Charts
{
    internal class PolarGridLineLayout : SfDrawableView
    {
        #region Fields

        private Size desiredSize;
        private float totalSpokes = 0;
        private readonly PolarChartArea area;
        private ChartAxis? xAxis => area.GetPrimaryAxis();
        private ChartAxis? yAxis => area.GetSecondaryAxis();
        private bool isRadar
        {
            get
            {
                if (area != null)
                {
                    return (area.PolarChart.GridLineType == PolarChartGridLineType.Polygon);
                }

                return false;
            }
        }
       
        #endregion

        #region Constructor

        public PolarGridLineLayout(PolarChartArea polarArea)
        {
            area = polarArea;
        }

        #endregion

        #region Methods

        private void OnDraw(ICanvas canvas)
        {
            if (area != null)
            {
                canvas.SaveState();
                canvas.Translate(0, 0);
                if (xAxis != null)
                {
                    totalSpokes = xAxis.VisibleLabels.Count;
                    DrawPrimaryAxisGridLine(xAxis, canvas);
                }

                if (yAxis != null)
                {
                    DrawSecondaryAxisGridLine(yAxis, canvas);
                }

                canvas.RestoreState();
            }
        }

        public SizeF Measure(SizeF availableSize)
        {
            desiredSize = new SizeF(availableSize.Width, availableSize.Height);
            return availableSize;
        }


        #region Protected Method

        override protected void OnDraw(ICanvas canvas, RectF dirtyRect)
        {
            base.OnDraw(canvas, dirtyRect);
            OnDraw(canvas);
        }

        #endregion

        #region Private Methods

        private void DrawPrimaryAxisGridLine(ChartAxis axis, ICanvas canvas)        
        {
            var gridLineStyle = axis.MajorGridLineStyle ?? GetDefaultGridLineStyle();
            if (!axis.CanDrawMajorGridLines())
            {
                return;
            }
            else
            {
                canvas.StrokeSize = (float)gridLineStyle.StrokeWidth;
                canvas.StrokeColor = gridLineStyle.Stroke.ToColor();
                if (gridLineStyle.StrokeDashArray != null)
                {
                    canvas.StrokeDashPattern = gridLineStyle.StrokeDashArray.ToFloatArray();
                }
            }

            float angle = 360 / totalSpokes;
            float radius = float.NaN;
            if (yAxis != null)
            {
                radius = (float)yAxis.ComputedDesiredSize.Height;
            }

            for (int i = 0; i < totalSpokes; i++)
            {
                Point pointF = area.PolarAngleToPoint(axis, radius, i * angle);
                canvas.DrawLine((float)(desiredSize.Width / 2), (float)(desiredSize.Height / 2), (float)pointF.X, (float)pointF.Y);
            }
        }

        private void DrawSecondaryAxisGridLine(ChartAxis axis, ICanvas canvas)
        {
            List<double> tickPositions = axis.TickPositions;
            if (tickPositions.Count > 0)
            {
                var rangeAxisBase = axis as RangeAxisBase;
                float top = (float)(axis.RenderedRect.Top - area.PlotAreaMargin.Top);
                float bottom = (float)(axis.RenderedRect.Bottom - area.PlotAreaMargin.Top);
                float height = bottom - top;
                var gridLineStyle = axis.MajorGridLineStyle ?? GetDefaultGridLineStyle();

                if (axis.CanDrawMajorGridLines())
                {
                    foreach (var position in tickPositions)
                    {
                        float value = (float)axis.ValueToCoefficient(position);
                        float y = height * (1f - value);
                        if (area.Series!.Count > 0 && !(isRadar))
                        {
                            DrawPolarAxisGridLine(canvas, (float)(desiredSize.Width / 2), (float)(desiredSize.Height / 2), y, gridLineStyle);
                        }
                        else
                        {
                            DrawRadarAxisGridLine(canvas, axis, y, gridLineStyle);
                        }
                    }
                }

                if (axis.SmallTickRequired && rangeAxisBase != null && rangeAxisBase.CanDrawMinorGridLines())
                {
                    List<double> smallTicks = rangeAxisBase.SmallTickPoints;
                    var minorGridLineStyle = rangeAxisBase.MinorGridLineStyle ?? GetDefaultGridLineStyle();
                    foreach (var pos in smallTicks)
                    {
                        float value = (float)axis.ValueToCoefficient(pos);
                        float y = height * (1f - value);
                        if (area.Series!.Count > 0 && !(isRadar))
                        {
                            DrawPolarAxisGridLine(canvas, (float)(desiredSize.Width / 2), (float)(desiredSize.Height / 2), y, minorGridLineStyle);
                        }
                        else
                        {
                            DrawRadarAxisGridLine(canvas, axis, y, minorGridLineStyle);
                        }
                    }
                }
            }
        }

        private void DrawPolarAxisGridLine(ICanvas canvas, float centerX, float centerY, float radius, ChartLineStyle gridLineStyle)
        {
            var x = centerX - radius;
            var y = centerY - radius;
            var size = radius * 2;

            if (gridLineStyle.CanDraw() && gridLineStyle.Stroke != null)
            {
                canvas.StrokeSize = (float)gridLineStyle.StrokeWidth;
                canvas.StrokeColor = gridLineStyle.Stroke.ToColor();
                if (gridLineStyle.StrokeDashArray != null)
                {
                    canvas.StrokeDashPattern = gridLineStyle.StrokeDashArray.ToFloatArray();
                }
            }

            canvas.DrawEllipse(x, y, size, size);
        }

        private void DrawRadarAxisGridLine(ICanvas canvas, ChartAxis axis, float y, ChartLineStyle gridLineStyle)
        {
            float angleTrack;
            int j = 1, totalCircle;
            var chart = area.PolarChart;
            if (chart.PrimaryAxis is CategoryAxis || chart.PrimaryAxis is DateTimeAxis)
            {
                if (area.VisibleSeries!.Count == 0 && chart.Series.Count > 0)
                {
                    totalCircle = chart.Series[0].PointsCount;
                }
                else
                {
                    totalCircle = chart.PrimaryAxis.VisibleLabels.Count; 
                }
            }
            else
            {
                totalCircle = chart.PrimaryAxis.VisibleLabels.Count - 1;
            }

            if (totalCircle == 0)
            {
                totalCircle = 1;
            }

            angleTrack = 360 / totalCircle;
            PathF radarPath = new PathF();

            Point startPointF = area.PolarAngleToPoint(axis, y, 0);
            radarPath.MoveTo((float)startPointF.X, (float)startPointF.Y);

            while (j < totalCircle)
            {
                Point midPointF = area.PolarAngleToPoint(axis, y, j * angleTrack);
                radarPath.LineTo((float)midPointF.X, (float)midPointF.Y);
                j++;
            }

            if (gridLineStyle.CanDraw())
            {
                canvas.StrokeSize = (float)gridLineStyle.StrokeWidth;
                canvas.StrokeColor = gridLineStyle.Stroke.ToColor();
                if (gridLineStyle.StrokeDashArray != null)
                {
                    canvas.StrokeDashPattern = gridLineStyle.StrokeDashArray.ToFloatArray();
                }
            }

            radarPath.Close();
            canvas.DrawPath(radarPath);
        }

        private ChartLineStyle GetDefaultGridLineStyle()
        {
            return new ChartLineStyle() { Stroke = new SolidColorBrush(Color.FromArgb("#E7E0EC")), StrokeWidth = 1 };
        }

        #endregion 

        #endregion
    }
}