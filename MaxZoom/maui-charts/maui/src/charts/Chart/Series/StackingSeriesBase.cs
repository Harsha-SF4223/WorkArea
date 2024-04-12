using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// StackingSeriesBase is a class that serves as the base for stacking column series.
    /// </summary>
    public abstract class StackingSeriesBase : XYDataSeries
    {
        #region Bindable Properties

        /// <summary>
        ///  Identifies the <see cref="GroupingLabel"/> bindable property.
        /// </summary>        
        public static readonly BindableProperty GroupingLabelProperty =
            BindableProperty.Create(
                nameof(GroupingLabel),
                typeof(string),
                typeof(StackingSeriesBase), string.Empty, BindingMode.Default, propertyChanged: OnGroupingLabelChanged);
        
        /// <summary>
        ///  Identifies the <see cref="Stroke"/> bindable property.
        /// </summary>
        public static readonly BindableProperty StrokeProperty =
            BindableProperty.Create(
                nameof(Stroke),
                typeof(Brush),
                typeof(StackingSeriesBase),
                null,
                BindingMode.Default,
                null,
                OnStrokeColorChanged);

        /// <summary>
        ///  Identifies the <see cref="StrokeDashArray"/> bindable property 
        /// </summary>
        public static readonly BindableProperty StrokeDashArrayProperty =
            BindableProperty.Create(
                nameof(StrokeDashArray),
                typeof(DoubleCollection),
                typeof(StackingSeriesBase),
                null,
                BindingMode.Default,
                null,
                OnStrokeDashArrayPropertyChanged);


        #endregion

        #region Public properties

        /// <summary>
        /// This property allows for the grouping of series in a stacked chart.
        /// </summary>
        /// <value>it accept string values and its default value is "chart-default".</value>
        /// <example>
        /// # [Xaml](#tab/tabid-xaml)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///  <chart:StackingColumnSeries ItemsSource="{Binding Data}"
        ///                              XBindingPath="XValue"
        ///                              YBindingPath="YValue"
        ///                              GroupingLabel="GroupOne"/>
        ///                              
        ///  <chart:StackingColumnSeries ItemsSource="{Binding Data1}"
        ///                              XBindingPath="XValue"
        ///                              YBindingPath="YValue"
        ///                              GroupingLabel="GroupTwo"/>
        ///                              
        ///  <chart:StackingColumnSeries ItemsSource="{Binding Data2}"
        ///                              XBindingPath="XValue"
        ///                              YBindingPath="YValue"
        ///                              GroupingLabel="GroupOne"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]></code>
        /// # [C#](#tab/tabid-csharp)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///     // Eliminated for simplicity
        ///
        ///     StackingColumnSeries stackingSeries = new StackingColumnSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           YBindingPath = "YValue",
        ///           GroupingLabel="GroupOne"
        ///     };
        ///     StackingColumnSeries stackingSeries = new StackingColumnSeries()
        ///     {
        ///           ItemsSource = viewModel.Data1,
        ///           XBindingPath = "XValue",
        ///           YBindingPath = "YValue",
        ///           GroupingLabel="GroupTwo"
        ///     };
        ///     StackingColumnSeries stackingSeries = new StackingColumnSeries()
        ///     {
        ///           ItemsSource = viewModel.Data1,
        ///           XBindingPath = "XValue",
        ///           YBindingPath = "YValue",
        ///           GroupingLabel="GroupOne"
        ///     };
        ///     
        ///     chart.Series.Add(stackingSeries);
        ///
        /// ]]></code>
        /// ***
        /// </example>
        public string GroupingLabel
        {
            get { return (string)GetValue(GroupingLabelProperty); }
            set { SetValue(GroupingLabelProperty, value); }
        }

        /// <summary>
        ///  Gets or sets a value to customize the stroke appearance.
        /// </summary>
        /// <value>It accepts <see cref="Brush"/> values and its default value is null.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-6)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:StackingAreaSeries ItemsSource="{Binding Data}"
        ///                            XBindingPath="XValue"
        ///                            YBindingPath="YValue"
        ///                            Stroke = "Red" />
        ///
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-7)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///     // Eliminated for simplicity
        ///     
        ///     StackingAreaSeries series = new StackingAreaSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           YBindingPath = "YValue",
        ///           Stroke = new SolidColorBrush(Colors.Red),
        ///     };
        ///     
        ///     chart.Series.Add(series);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        /// <summary>
        ///  Gets or sets the stroke dash array to customize the appearance of the stroke.
        /// </summary>
        /// <value>It accepts the <see cref="DoubleCollection"/> value and the default value is null.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-6)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:StackingAreaSeries ItemsSource="{Binding Data}"
        ///                            XBindingPath="XValue"
        ///                            YBindingPath="YValue"
        ///                            StrokeDashArray="5,3"
        ///                            Stroke = "Red" />
        ///
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-7)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///     // Eliminated for simplicity
        ///
        ///     DoubleCollection doubleCollection = new DoubleCollection();
        ///     doubleCollection.Add(5);
        ///     doubleCollection.Add(3);
        ///     StackingAreaSeries series = new StackingAreaSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           YBindingPath = "YValue",
        ///           StrokeDashArray = doubleCollection,
        ///           Stroke = new SolidColorBrush(Colors.Red),
        ///     };
        ///     
        ///     chart.Series.Add(series);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public DoubleCollection StrokeDashArray
        {
            get { return (DoubleCollection)GetValue(StrokeDashArrayProperty); }
            set { SetValue(StrokeDashArrayProperty, value); }
        }

        #endregion

        #region Internal Properties

        internal IList<double>? TopValues { get; set; }

        internal IList<double>? BottomValues { get; set; }

        #endregion

        #region Methods

        #region Private Methods

        private static void OnStrokeColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is StackingSeriesBase series)
            {
                series.UpdateStrokeColor();
                series.InvalidateSeries();
            }
        }

        private static void OnStrokeDashArrayPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var series = bindable as StackingSeriesBase;
            if (series != null)
            {
                series.UpdateDashArray();
                series.InvalidateSeries();
            }
        }

        private static void OnGroupingLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            StackingSeriesBase? series = bindable as StackingSeriesBase;

            if (series != null && series.ChartArea != null)
            {
                series.RefreshSeries();
            }
        }
       
        private void RefreshSeries()
        {
            if (ChartArea == null || ChartArea.Series == null)
            {
                return;
            }

            foreach (var series in ChartArea.Series.OfType<StackingSeriesBase>())
            {
                series.SegmentsCreated = false;
            }
            ChartArea.NeedsRelayout = true;
            ChartArea.SideBySideSeriesPosition = null;
            ChartArea.ScheduleUpdateArea();
        }

        #endregion

        #region Internal Methods

        internal override void SetStrokeColor(ChartSegment segment)
        {
            segment.Stroke = Stroke;
        }

        internal override void SetDashArray(ChartSegment segment)
        {
            segment.StrokeDashArray = StrokeDashArray;
        }

        #endregion

        #endregion

    }
}
