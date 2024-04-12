using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Graphics.Internals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Syncfusion.Maui.Charts.Chart.Layouts
{
    internal class ChartZoomPanView : SfView
    {
        private int dimension = 2;
        private double yValue = 0, xValue = 0;
        private List<TrackballAxisInfo> axisPointInfos = new List<TrackballAxisInfo>();

        internal ChartZoomPanBehavior? Behavior { get; set; }

        public ChartZoomPanView()
        {
            DrawingOrder = DrawingOrder.AboveContent;
        }

        protected override void OnDraw(ICanvas canvas, RectF dirtyRect)
        {
            if (Behavior != null && Behavior.Chart is SfCartesianChart chart && Behavior.SelectionRect != Rect.Zero)
            {
                canvas.SaveState();
                DrawSelectionRect(canvas, Behavior);
                DrawSelectionElements(canvas, chart, Behavior);
                canvas.RestoreState();
            }
        }

        void Drawable()
        {

        }

        internal void DrawSelectionRect(ICanvas canvas, ChartZoomPanBehavior behavior)
        {
            var selectionRectStrokeDashArray = behavior.SelectionRectStrokeDashArray;
            var selectionRectStrokeWidth = behavior.SelectionRectStrokeWidth;
            var SelectedRect = behavior.SelectionRect;
            bool HasBorder = selectionRectStrokeWidth > 0;

            canvas.SaveState();
            if (HasBorder)
            {
                canvas.StrokeColor = behavior.SelectionRectStroke.ToColor();
                canvas.StrokeSize = (float)selectionRectStrokeWidth;

                if (selectionRectStrokeDashArray != null && selectionRectStrokeDashArray.Count > 0)
                {
                    canvas.StrokeDashPattern = selectionRectStrokeDashArray.ToFloatArray();
                }
            }

            canvas.SetFillPaint(behavior.SelectionRectFill, SelectedRect);
            canvas.DrawShape(SelectedRect, ShapeType.Rectangle, HasBorder, false);
            canvas.RestoreState();
        }

        internal void DrawSelectionElements(ICanvas canvas, SfCartesianChart chart, ChartZoomPanBehavior behavior)
        {
            Point startPoint = Point.Zero, endPoint = Point.Zero;
            TooltipPosition tooltipPosition = TooltipPosition.Auto;

            canvas.StrokeColor = chart.TrackLineStroke.ToColor();
            canvas.StrokeSize = 1;

            if (chart != null && (Math.Abs(behavior.SelectionRect.Width) > 20 || Math.Abs(behavior.SelectionRect.Height) > 20))
            {
                var area = chart.ChartArea;
                var axisLayout = area.AxisLayout;
                var xAxes = axisLayout.HorizontalAxes;
                GenerateSelectionElements(canvas, xAxes, ref startPoint, ref endPoint, ref tooltipPosition);
                var yAxes = axisLayout.VerticalAxes;
                GenerateSelectionElements(canvas, yAxes, ref startPoint, ref endPoint, ref tooltipPosition);
            }
        }

        private void GenerateSelectionElements(ICanvas canvas, ObservableCollection<ChartAxis> axes, ref Point startPoint, ref Point endPoint, ref TooltipPosition tooltipPosition)
        {
            if (Behavior == null)
            {
                return;
            }

            var SelectedRect = Behavior.SelectionRect;

            foreach (var axis in axes)
            {
                bool isOpposed = axis.IsOpposed();
                Rect actualArrangeRect = axis.ArrangeRect;
                if (axis.IsVisible && axis.ShowTrackballLabel && Behavior.IsSelectionZoomingActivated)
                {
                    if (axis.IsVertical)
                    {
                        startPoint = new Point(isOpposed ? actualArrangeRect.X : actualArrangeRect.X + actualArrangeRect.Width, SelectedRect.Top);
                        endPoint = new Point(isOpposed ? actualArrangeRect.X : actualArrangeRect.X + actualArrangeRect.Width, SelectedRect.Bottom);
                        tooltipPosition = isOpposed ? TooltipPosition.Right : TooltipPosition.Left;
                    }
                    else
                    {
                        startPoint = new Point(SelectedRect.Left, isOpposed ? actualArrangeRect.Y + actualArrangeRect.Height : actualArrangeRect.Y);
                        endPoint = new Point(SelectedRect.Right, isOpposed ? actualArrangeRect.Y + actualArrangeRect.Height : actualArrangeRect.Y);
                        tooltipPosition = isOpposed ? TooltipPosition.Top : TooltipPosition.Bottom;
                    }
                    
                    canvas.DrawLine((float)startPoint.X, (float)startPoint.Y, (float)endPoint.X, (float)endPoint.Y);

                    GenerateAxisTrackballInfos(startPoint, endPoint, tooltipPosition, axis);

                    foreach (var item in axisPointInfos)
                    {
                        item.Helper.Draw(canvas);
                    }
                }
            }
        }

        private void GenerateAxisTrackballInfos(PointF startPoint, PointF endPoint, TooltipPosition tooltipPosition, ChartAxis axis)
        {
            if (Behavior != null && Behavior.Chart is SfCartesianChart chart && chart is IChart iChart)
            {
                Rect axisRect = axis.ArrangeRect;
                var clipRect = iChart.ActualSeriesClipRect;
                axisPointInfos.Clear();
                string labelFormat = "##.#";

                if (axis.TrackballLabelStyle != null)
                {
                    labelFormat = axis.TrackballLabelStyle.LabelFormat;
                }
                else if (axis is DateTimeAxis)
                {
                    labelFormat = "MM-dd-yyyy";
                }

                if (axis.IsVertical)
                {
                    yValue = (float)clipRect.Top - axisRect.Top;
                }
                else
                {
                    xValue = (float)clipRect.Left - axisRect.Left;
                }

                TrackballAxisInfo axisPointInfo1 = new TrackballAxisInfo(axis, new TooltipHelper(Drawable) { Duration = int.MaxValue }, GetAxisLabel(axis, chart.PointToValue(axis, startPoint.X + xValue, startPoint.Y + yValue), labelFormat), startPoint.X, startPoint.Y + (float)iChart.ActualSeriesClipRect.Top);
                TrackballAxisInfo axisPointInfo2 = new TrackballAxisInfo(axis, new TooltipHelper(Drawable) { Duration = int.MaxValue }, GetAxisLabel(axis, chart.PointToValue(axis, endPoint.X + xValue, endPoint.Y + yValue), labelFormat), endPoint.X, endPoint.Y + (float)iChart.ActualSeriesClipRect.Top);

                axisPointInfo1.Helper.Position = tooltipPosition;
                axisPointInfo2.Helper.Position = tooltipPosition;

                if (axis.TrackballLabelStyle != null)
                {
                    MapChartLabelStyle(chart, axisPointInfo1.Helper, axis.TrackballLabelStyle);
                    MapChartLabelStyle(chart, axisPointInfo2.Helper, axis.TrackballLabelStyle);
                }
                
                Rect actualArrangeRect = new Rect(axis.ArrangeRect.X, axis.ArrangeRect.Y, axis.ArrangeRect.X + axis.ArrangeRect.Width, axis.ArrangeRect.Y + axis.ArrangeRect.Height);

                axisPointInfo1.Helper.Show(actualArrangeRect, new Rect(startPoint.X - 1, startPoint.Y - 1, dimension, dimension), false);
                axisPointInfo2.Helper.Show(actualArrangeRect, new Rect(endPoint.X - 1, endPoint.Y - 1, dimension, dimension), false);

               axisPointInfos.Add(axisPointInfo1);
               axisPointInfos.Add(axisPointInfo2);
            }
        }

        private static string GetAxisLabel(ChartAxis axis, double Value, string labelFormat)
        {
            string label = string.Empty;

            if (axis is CategoryAxis categoryAxis)
            {
                var currSeries = categoryAxis.GetActualSeries();
                if (currSeries != null)
                {
                    var value = (int)Math.Round(Value);

                    if(value < 0)
                    {
                        value = 0;
                    }

                    label = categoryAxis.GetLabelContent(currSeries, value, labelFormat);
                }
            }
            else if (axis is NumericalAxis)
            {
                label = Value.ToString(labelFormat);
            }
            else if (axis is LogarithmicAxis)
            {
                label = ChartAxis.GetActualLabelContent(Value, labelFormat).ToString();
            }
            else if (axis is DateTimeAxis datetimeAxis)
            {
                string format;
                if (labelFormat != null)
                {
                    format = labelFormat;
                }
                else
                {
                    format = datetimeAxis.GetSpecificFormatedLabel(datetimeAxis.ActualIntervalType);
                }

                label = ChartAxis.GetFormattedAxisLabel(format, Value);
            }
            else
            {
                label = ChartAxis.GetActualLabelContent(Value, labelFormat);
            }

            return label;
        }

        private void MapChartLabelStyle(SfCartesianChart CartesianChart,TooltipHelper helper, ChartLabelStyle chartLabelStyle)
        {
            var background = chartLabelStyle.Background;
            helper.FontAttributes = chartLabelStyle.FontAttributes;
            helper.FontFamily = chartLabelStyle.FontFamily;
            helper.FontSize = chartLabelStyle.FontSize;
            helper.Padding = chartLabelStyle.Margin;
            helper.Stroke = chartLabelStyle.Stroke;
            helper.StrokeWidth = (float)chartLabelStyle.StrokeWidth;
            helper.Background = chartLabelStyle.Background;

            if (!chartLabelStyle.IsTextColorUpdated)
            {
                var fontColor = background == default(Brush) || background.ToColor() == Colors.Transparent ?
                        CartesianChart.GetTextColorBasedOnChartBackground() :
                        ChartUtils.GetContrastColor((background as SolidColorBrush).ToColor());
                helper.TextColor = fontColor;
            }
            else
            {
                helper.TextColor = chartLabelStyle.TextColor;
            }
        }
    }
}
