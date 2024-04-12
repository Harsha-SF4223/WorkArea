using Syncfusion.Maui.Graphics.Internals;
using System.Collections.ObjectModel;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Core;

namespace Syncfusion.Maui.Charts
{
    internal interface IChartArea
    {
        public ChartSeriesCollection? Series { get; set; }

        public ReadOnlyObservableCollection<ChartSeries>? VisibleSeries { get; }

        public void UpdateVisibleSeries()
        {

        }
    }

    internal interface IChartPlotArea : IPlotArea
    {
        public SfDrawableView DataLabelView { get; }

        public void UpdateLegendItem(LegendItem legendItem);
    }
}