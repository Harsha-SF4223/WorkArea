using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Graphics.Internals;

namespace Syncfusion.Maui.Charts
{
    internal class DataLabelView : SfDrawableView
    {
        readonly ChartPlotArea chartPlotArea;

        public DataLabelView(ChartPlotArea plotArea)
        {
            chartPlotArea = plotArea;
        }

        protected override void OnDraw(ICanvas canvas, RectF dirtyRect)
        {
            var visibleSeries = chartPlotArea.VisibleSeries;
            canvas.SaveState();
            canvas.ClipRectangle(dirtyRect);
            
            if (visibleSeries != null)
            {
                foreach (var series in visibleSeries)
                {
                    if (series.IsVisible && !series.CanAnimate() && series.ShowDataLabels && series.Segments.Count > 0 && series.LabelTemplate == null)
                    {
                        canvas.SaveState();

                        if (series.NeedToAnimateDataLabel)
                        {
                            canvas.Alpha = series.AnimationValue;
                        }

                        series.DrawDataLabels(canvas);
                        canvas.RestoreState();
                    }

                    if(series is CircularSeries circularSeries && circularSeries.LabelTemplate != null)
                    {
                        circularSeries.UpdateDataLabelPositions(canvas);
                    }
                }
            }

            canvas.RestoreState();
        }
    }

    internal class PyramidDataLabelsView : SfDrawableView
    {
        internal readonly IPyramidChartDependent chart;

        public PyramidDataLabelsView(IPyramidChartDependent chart)
        {
            this.chart = chart;
        }

        protected override void OnDraw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();
            chart.DrawDataLabels(canvas, dirtyRect);
            canvas.RestoreState();
        }
    }

    internal class PolarDataLabelView : SfDrawableView
    {
        internal readonly PolarChartArea chartArea;

        public PolarDataLabelView(PolarChartArea chartArea)
        {
            this.chartArea = chartArea;
        }

        protected override void OnDraw(ICanvas canvas, RectF dirtyRect)
        {
            var visibleSeries = chartArea.VisibleSeries;
            canvas.SaveState();
            canvas.ClipRectangle(dirtyRect);

            if (visibleSeries != null)
            {
                foreach (var series in visibleSeries)
                {
                    if (series.IsVisible && !series.CanAnimate() && series.ShowDataLabels && series.Segments.Count > 0 && series.LabelTemplate == null)
                    {
                        canvas.SaveState();

                        if (series.NeedToAnimateDataLabel)
                        {
                            canvas.Alpha = series.AnimationValue;
                        }

                        series.DrawDataLabels(canvas);
                        canvas.RestoreState();
                    }
                }
            }

            canvas.RestoreState();
        }
    }
}