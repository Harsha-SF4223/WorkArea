using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Graphics.Internals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Syncfusion.Maui.Charts
{
    internal class PolarChartArea : AreaBase, IChartPlotArea
    {
        #region Fields
      
        private readonly AbsoluteLayout behaviorLayout;
        internal readonly AbsoluteLayout DataLabelLayout;
        private bool shouldPopulateLegendItems = true;
        private View? plotAreaBackgroundView;
        RectF actualSeriesClipRect;
        private ILegend? legend;
        private readonly ObservableCollection<ILegendItem> legendItems;
        private ChartPolarSeriesCollection? series;
        private IChart chart => PolarChart;

        #endregion

        #region Public Properties

        public override IPlotArea PlotArea => this;

        public ChartPolarSeriesCollection? Series
        {
            get
            {
                return series;
            }
            set
            {
                if (value != series)
                {
                    OnSeriesCollectionChanging();
                    series = value;
                    OnSeriesCollectionChanged();
                }
            }
        }

        public ReadOnlyObservableCollection<ChartSeries>? VisibleSeries => Series?.GetVisibleSeries();

        #endregion

        #region Internal Properties

        internal readonly SfPolarChart PolarChart;
        internal readonly PolarAxisLayoutView AxisLayout;
        internal readonly PolarGridLineLayout GridLineLayout;
        internal readonly AbsoluteLayout SeriesViews;
        internal readonly PolarDataLabelView DataLabelView;
        internal RectF ActualSeriesClipRect { get { return actualSeriesClipRect; } set { actualSeriesClipRect = value; } }
        internal Thickness PlotAreaMargin { get; set; } = Thickness.Zero;
        internal IList<Brush>? PaletteColors { get; set; }
        internal View? PlotAreaBackgroundView
        {
            get
            {
                return plotAreaBackgroundView;
            }
            set
            {
                if (plotAreaBackgroundView != null && Contains(plotAreaBackgroundView))
                {
                    plotAreaBackgroundView.RemoveBinding(AbsoluteLayout.LayoutBoundsProperty);
                    plotAreaBackgroundView.RemoveBinding(AbsoluteLayout.LayoutFlagsProperty);
                    Remove(plotAreaBackgroundView);
                }

                if (value != null)
                {
                    plotAreaBackgroundView = value;
                    AbsoluteLayout.SetLayoutBounds(plotAreaBackgroundView, new Rect(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(plotAreaBackgroundView, AbsoluteLayoutFlags.All);
                    Insert(0, plotAreaBackgroundView);
                }
            }
        }
        internal PointF PolarAxisCenter { get; set; }

        #endregion

        #region Interface Implementation

        ILegend? IPlotArea.Legend{ get { return legend; } set { if (value != legend) { legend = value; } } }
        public ReadOnlyObservableCollection<ILegendItem> LegendItems => new ReadOnlyObservableCollection<ILegendItem>(legendItems);
        private EventHandler<EventArgs>? legendItemsUpdated;
        private EventHandler<LegendItemEventArgs>? legendItemsToggled;
        event EventHandler<EventArgs> IPlotArea.LegendItemsUpdated { add { legendItemsUpdated += value; } remove { legendItemsUpdated -= value; } }
        event EventHandler<LegendItemEventArgs>? IPlotArea.LegendItemToggled { add { legendItemsToggled += value; } remove { legendItemsToggled -= value; } }
        public Rect PlotAreaBounds { get ; set ; }
        public bool ShouldPopulateLegendItems { get => shouldPopulateLegendItems; set => shouldPopulateLegendItems = value; }
        LegendHandler IPlotArea.LegendItemToggleHandler
        {
            get
            {
                return ToggleLegendItem;
            }
        }
        SfDrawableView IChartPlotArea.DataLabelView => DataLabelView;

        #endregion

        #region Constructor

        /// <summary>
        /// It helps to create chart area to hold the view for polar charts.
        /// </summary>
        /// <param name="polarChart"></param>
        public PolarChartArea(SfPolarChart polarChart)
        {
            PolarChart = polarChart;
            series = new ChartPolarSeriesCollection();
            legendItems = new ObservableCollection<ILegendItem>();
            PaletteColors = ChartColorModel.DefaultBrushes;
            GridLineLayout = new PolarGridLineLayout(this);
            SeriesViews = new AbsoluteLayout();
            AxisLayout = new PolarAxisLayoutView(this);
            DataLabelView = new PolarDataLabelView(this);
            behaviorLayout = polarChart.BehaviorLayout;
            DataLabelLayout = new AbsoluteLayout();
            AbsoluteLayout.SetLayoutBounds(GridLineLayout, new Rect(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(GridLineLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(SeriesViews, new Rect(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(SeriesViews, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(AxisLayout, new Rect(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(AxisLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(DataLabelView, new Rect(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(DataLabelView, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(behaviorLayout, new Rect(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(behaviorLayout, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(DataLabelLayout, new Rect(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(DataLabelLayout, AbsoluteLayoutFlags.All);
            Add(GridLineLayout);
            Add(SeriesViews);
            Add(DataLabelView);
            Add(AxisLayout);
            Add(behaviorLayout);
            Add(DataLabelLayout);
        }

        #endregion

        #region Methods

        #region Public Method

        public void UpdateLegendItems()
        {
            if (shouldPopulateLegendItems)
            {
                UpdateLegendItemsSource();
                shouldPopulateLegendItems = false;
                legendItemsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public void UpdateLegendItem(LegendItem legendItem)
        {
            if (PolarChart is ChartBase chart)
            {
                var color = chart.LegendStyle.TextColor;
                legendItem.TextColor = color;
                legendItem.FontSize = (float)chart.LegendStyle.FontSize;
                legendItem.DisableBrush = Color.FromRgba(color.Red, color.Green, color.Blue, 0.38);
            }
        }

        #endregion

        #region Internal Methods

        internal ChartAxis GetPrimaryAxis()
        {
            return PolarChart.PrimaryAxis;
        }

        internal ChartAxis GetSecondaryAxis()
        {
            return PolarChart.SecondaryAxis;
        }

        internal void InternalCreateSegments(ChartSeries series)
        {
            foreach (var view in SeriesViews.Children)
            {
                if (view is SeriesView seriesView && seriesView.IsVisible && series == seriesView.Series)
                {
                    seriesView.InternalCreateSegments();
                }
            }
        }

        internal void OnPaletteColorChanged()
        {
            if (Series != null)
            {
                foreach (var series in Series)
                {
                    if (series is PolarSeries polar && polar.Chart != null)
                    {
                        series.PaletteColorsChanged();
                    }
                }
            }
        }

        internal void ScheduleUpdate(bool reLayout)
        {
            NeedsRelayout = reLayout;
            ScheduleUpdateArea();
        }

        internal PointF PolarAngleToPoint(ChartAxis axis, float radius, float theta)
        {
            PointF point = new PointF();
            theta = axis.IsInversed ? -theta : theta;
            var angle = PolarChart.PolarStartAngle;
            point.X = (float)(PolarAxisCenter.X + (radius * Math.Cos((theta + angle) * (Math.PI / 180))));
            point.Y = (float)(PolarAxisCenter.Y + (radius * Math.Sin((theta + angle) * (Math.PI / 180))));
            return point;
        }

        #endregion

        #region Protected Methods

        protected override void UpdateAreaCore()
        {
            if (chart == null) return;

            chart.ResetTooltip();

            AxisLayout.AssignAxisToSeries();
            AxisLayout.LayoutAxis(AreaBounds);
            GridLineLayout.Measure(ActualSeriesClipRect.Size);

            chart.ActualSeriesClipRect = ChartUtils.GetSeriesClipRect(AreaBounds, chart.TitleHeight);

            UpdateVisibleSeries();
            AxisLayout.InvalidateDrawable();
            GridLineLayout.InvalidateDrawable();
        }

        protected void UpdateLegendItemsSource()
        {
            if (Series == null || legend == null || !legend.IsVisible)
            {
                return;
            }

            legendItems.Clear();
            int index = 0;
            foreach (PolarSeries series in Series)
            {
                if (series.IsVisibleOnLegend)
                {
                    var legendItem = new LegendItem();
                    legendItem.IconType = ChartUtils.GetShapeType(series.LegendIcon);
                    legendItem.Item = series;
                    legendItem.Source = series;
                    Brush? solidColor = series.GetFillColor(legendItem, index);
                    legendItem.IconBrush = solidColor != null ? solidColor : new SolidColorBrush(Colors.Transparent);
                    legendItem.Text = series.Label;
                    legendItem.Index = index;
                    legendItem.IsToggled = !series.IsVisible;
                    UpdateLegendItem(legendItem);
                    legendItems?.Add(legendItem);
                    index++;
                }
            }
        }

        #endregion

        #region Private Methods

        void ToggleLegendItem(ILegendItem legendItem)
        {
            if (legend != null && legend.IsVisible && legend.ToggleVisibility)
            {
                if (legendItem is LegendItem polarLegendItem && polarLegendItem.Item != null)
                {
                    if (polarLegendItem.Item is PolarSeries series)
                    {
                        series.IsVisible = !legendItem.IsToggled;
                    }
                }
            }
        }

        private void UpdateVisibleSeries()
        {
            foreach (SeriesView seriesView in SeriesViews.Children)
            {
                if (seriesView != null && seriesView.IsVisible)
                {
                    seriesView.UpdateSeries();
                }
            }

            DataLabelView?.InvalidateDrawable();
        }

        private void AddSeries(int index, object series)
        {
            var chartSeries = series as PolarSeries;
            if (chartSeries != null)
            {
                chartSeries.Chart = PolarChart;
                chartSeries.Parent = PolarChart;
                SetInheritedBindingContext(chartSeries, this.BindingContext);
                chartSeries.ChartArea = this;
                chartSeries.SegmentsCreated = false;
                var seriesView = new SeriesView(chartSeries, this);
                chartSeries.NeedToAnimateSeries = chartSeries.EnableAnimation;
                AbsoluteLayout.SetLayoutBounds(seriesView, new Rect(0, 0, 1, 1));
                AbsoluteLayout.SetLayoutFlags(seriesView, AbsoluteLayoutFlags.All);
                SeriesViews.Insert(index, seriesView);
                var labelView = chartSeries.LabelTemplateView;
                AbsoluteLayout.SetLayoutBounds(labelView, new Rect(0, 0, 1, 1));
                AbsoluteLayout.SetLayoutFlags(labelView, AbsoluteLayoutFlags.All);                      
                DataLabelLayout.Insert(index, labelView);
            }
        }

        private void RemoveSeries(int index, object series)
        {
            var chartSeries = series as ChartSeries;
            if (chartSeries != null)
            {
                chartSeries.ResetData();
                chartSeries.SegmentsCreated = false;
                chart.IsRequiredDataLabelsMeasure = true;
                DataLabelLayout.Children.RemoveAt(index);
                SeriesViews.Children.RemoveAt(index);
                chartSeries.Chart = null;
                chartSeries.Parent = null;
            }
        }

        private void ResetSeries()
        {
            foreach (SeriesView seriesView in SeriesViews)
            {
                seriesView.Series.ResetData();
            }

            SeriesViews.Clear();
        }

        private void OnSeriesCollectionChanging()
        {
            if (series != null)
            {
                series.CollectionChanged -= Series_CollectionChanged;
                ResetSeries();
            }
        }

        private void OnSeriesCollectionChanged()
        {
            if (series != null)
            {
                series.CollectionChanged += Series_CollectionChanged;

                int count = 0;
                foreach (var chartSeries in series)
                {
                    AddSeries(count, chartSeries);
                    count++;
                }

                if (legend != null)
                {
                    UpdateLegendItems();
                }

                ScheduleUpdate(true);
            }
        }

        private void Series_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            e.ApplyCollectionChanges((index, series) => AddSeries(index, series), (index, series) => RemoveSeries(index, series), ResetSeries);
            
            ShouldPopulateLegendItems = true;
            
            ScheduleUpdate(true);
        }

        #endregion 

        #endregion
    }
}