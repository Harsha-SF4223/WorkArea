using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// Represents a range column series in .NET MAUI Cartesian chart [<see cref="SfCartesianChart"/>].
    /// </summary>
    /// <remarks>
    /// <para>To render a series, create an instance of <see cref="RangeColumnSeries"/> class, and add it to the <see cref="SfCartesianChart.Series"/> collection.</para>
    /// <para> <b>EnableTooltip - </b> A tooltip displays information while tapping or mouse hovering above a segment. To display the tooltip on a chart, you need to set the <see cref="ChartSeries.EnableTooltip"/> property as <b>true</b> in <see cref="RangeColumnSeries"/> class, and also refer <seealso cref="ChartBase.TooltipBehavior"/> property.</para>
    /// <para> <b>Data Label - </b> Data labels are used to display values related to a chart segment. To render the data labels, you need to set the <see cref="ChartSeries.ShowDataLabels"/> property as <b>true</b> in <see cref="RangeColumnSeries"/> class. To customize the chart data labels alignment, placement, and label styles, you need to create an instance of <see cref="CartesianDataLabelSettings"/> and set to the <see cref="CartesianSeries.DataLabelSettings"/> property.</para>
    /// <para> <b>Animation - </b> To animate the series, set <b>True</b> to the <see cref="ChartSeries.EnableAnimation"/> property.</para>
    /// <para> <b>LegendIcon - </b> To customize the legend icon using the <see cref="ChartSeries.LegendIcon"/> property.</para>
    /// <para> Range column series do not yet support trackball behavior.</para>
    /// </remarks>
    /// <example>
    /// # [Xaml](#tab/tabid-1)
    /// <code><![CDATA[
    ///     <chart:SfCartesianChart>
    ///
    ///           <chart:SfCartesianChart.XAxes>
    ///               <chart:NumericalAxis/>
    ///           </chart:SfCartesianChart.XAxes>
    ///
    ///           <chart:SfCartesianChart.YAxes>
    ///               <chart:NumericalAxis/>
    ///           </chart:SfCartesianChart.YAxes>
    ///
    ///           <chart:SfCartesianChart.Series>
    ///               <chart:RangeColumnSeries
    ///                   ItemsSource="{Binding Data}"
    ///                   XBindingPath="XValue"
    ///                   High="HighValue"
    ///                   Low="LowValue"/>
    ///           </chart:SfCartesianChart.Series>  
    ///           
    ///     </chart:SfCartesianChart>
    /// ]]></code>
    /// # [C#](#tab/tabid-2)
    /// <code><![CDATA[
    ///     SfCartesianChart chart = new SfCartesianChart();
    ///     
    ///     NumericalAxis xAxis = new NumericalAxis();
    ///     NumericalAxis yAxis = new NumericalAxis();
    ///     
    ///     chart.XAxes.Add(xAxis);
    ///     chart.YAxes.Add(yAxis);
    ///     
    ///     ViewModel viewModel = new ViewModel();
    /// 
    ///     RangeColumnSeries series = new RangeColumnSeries();
    ///     series.ItemsSource = viewModel.Data;
    ///     series.XBindingPath = "XValue";
    ///     series.High = "HighValue";
    ///     series.Low = "LowValue";
    ///     chart.Series.Add(series);
    ///     
    /// ]]></code>
    /// # [ViewModel](#tab/tabid-3)
    /// <code><![CDATA[
    ///     public ObservableCollection<Model> Data { get; set; }
    /// 
    ///     public ViewModel()
    ///     {
    ///        Data = new ObservableCollection<Model>();
    ///        Data.Add(new Model() { XValue = Value 1, High = 3, Low = 6 });
    ///        Data.Add(new Model() { XValue = Value 2, High = 3, Low = 7 });
    ///        Data.Add(new Model() { XValue = Value 3, High = 4,Low = 10 });
    ///        Data.Add(new Model() { XValue = Value 4, High = 6,Low = 13 });
    ///        Data.Add(new Model() { XValue = Value 5, High = 9,Low = 17 });
    ///     }
    /// ]]></code>
    /// ***
    /// </example>
    /// <seealso cref="RangeColumnSegment"/>
    public class RangeColumnSeries : RangeSeriesBase, IDrawCustomLegendIcon
    {
        #region BindableProperty Registration

        /// <summary>
        /// Identifies the <see cref="Spacing"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SpacingProperty =
              BindableProperty.Create(nameof(Spacing), typeof(double), typeof(RangeColumnSeries), 0.0d, BindingMode.Default, null, OnSpacingChanged);

        /// <summary>
        /// Identifies the <see cref="CornerRadius"/> bindable property.
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(RangeColumnSeries), null, BindingMode.Default, null, OnCornerRadiusChanged);

        /// <summary>
        /// Identifies the <see cref="Width"/> bindable property.
        /// </summary>
        public static readonly BindableProperty WidthProperty =
           BindableProperty.Create(nameof(Width), typeof(double), typeof(ColumnSeries), 0.8d, BindingMode.Default, null, OnWidthChanged);

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value to indicate space between the segments across the series.
        /// </summary>
        /// <value>It accepts values between 0 and 1, and its default is 0.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-4)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:RangeColumnSeries ItemsSource="{Binding Data}"
        ///                                   XBindingPath="XValue"
        ///                                   High ="HighValue"
        ///                                   Low ="LowValue"
        ///                                   Spacing = "0.3"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]></code>
        /// # [C#](#tab/tabid-5)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///     // Eliminated for simplicity
        ///
        ///     RangeColumnSeries rangeColumnSeries = new RangeColumnSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           High ="HighValue",
        ///           Low ="LowValue",
        ///           Spacing = 0.3,
        ///     };
        ///     
        ///     chart.Series.Add(rangeColumnSeries);
        ///
        /// ]]></code>
        /// ***
        /// </example>
        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the rounded corners for range column segments.
        /// </summary>
        ///  <value>It accepts <see cref="Microsoft.Maui.CornerRadius"/> value, the default is null</value>
        /// <example>
        /// # [Xaml](#tab/tabid-4)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:RangeColumnSeries ItemsSource="{Binding Data}"
        ///                                   XBindingPath="XValue"
        ///                                   High ="HighValue"
        ///                                   Low ="LowValue"
        ///                                   CornerRadius="5"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]></code>
        /// # [C#](#tab/tabid-5)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///     // Eliminated for simplicity
        ///
        ///     RangeColumnSeries rangeColumnSeries = new RangeColumnSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           High ="HighValue",
        ///           Low ="LowValue",
        ///           CornerRadius = new CornerRadius(5)
        ///     };
        ///     
        ///     chart.Series.Add(rangeColumnSeries);
        ///
        /// ]]></code>
        /// ***
        /// </example>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value to change the width of the range column segment.
        /// </summary>
        /// <value>It accepts values between 0 and 1, and its default is 0.8.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-4)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:RangeColumnSeries ItemsSource="{Binding Data}"
        ///                                   XBindingPath="XValue"
        ///                                   High ="HighValue"
        ///                                   Low ="LowValue"
        ///                                   Width="0.7"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]></code>
        /// # [C#](#tab/tabid-5)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///     // Eliminated for simplicity
        ///
        ///     RangeColumnSeries rangeColumnSeries = new RangeColumnSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           High ="HighValue",
        ///           Low ="LowValue",
        ///           Width=0.7,
        ///     };
        ///     
        ///     chart.Series.Add(rangeColumnSeries);
        ///
        /// ]]></code>
        /// ***
        /// </example>
        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        #endregion

        #region Internal override properties

        internal override bool IsSideBySide
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Methods

        #region Interface Implementation

        void IDrawCustomLegendIcon.DrawSeriesLegend(ICanvas canvas, RectF rect, Brush fillColor, bool isSaveState)
        {
            if (isSaveState)
            {
                canvas.SaveState();
            }

            RectF innerRect1 = new(1, 4, 2, 8);
            canvas.SetFillPaint(fillColor, innerRect1);
            canvas.FillRectangle(innerRect1);

            RectF innerRect2 = new(5, 0, 2, 8);
            canvas.SetFillPaint(fillColor, innerRect2);
            canvas.FillRectangle(innerRect2);

            RectF innerRect3 = new(9, 3, 2, 7);
            canvas.SetFillPaint(fillColor, innerRect3);
            canvas.FillRectangle(innerRect3);

            if (isSaveState)
            {
                canvas.RestoreState();
            }
        }

        #endregion

        #region Protected Override Methods

        /// <inheritdoc/>
        protected override ChartSegment CreateSegment()
        {
            return new RangeColumnSegment();
        }

        #endregion

        #region Internal Methods

        internal override void GenerateSegments(SeriesView seriesView)
        {
            var xValues = GetXValues();
            if (IsGrouped && (IsIndexed || xValues == null))
            {
                for (var i = 0; i < PointsCount; i++)
                {
                    if (i < Segments.Count && Segments[i] is RangeColumnSegment)
                    {
                        ((RangeColumnSegment)Segments[i]).SetData(new[] { i + SbsInfo.Start, i + SbsInfo.End, HighValues[i], LowValues[i], i });
                    }
                    else
                    {
                        CreateSegment(seriesView, new[] { i + SbsInfo.Start, i + SbsInfo.End, HighValues[i], LowValues[i], i }, i);
                    }
                }
            }
            else
            {
                if (xValues != null)
                {
                    for (var i = 0; i < PointsCount; i++)
                    {
                        var x = xValues[i];
                        if (i < Segments.Count && Segments[i] is RangeColumnSegment)
                        {
                            ((RangeColumnSegment)Segments[i]).SetData(new[] { x + SbsInfo.Start, x + SbsInfo.End, HighValues[i], LowValues[i], x });
                        }
                        else
                        {
                            CreateSegment(seriesView, new[] { x + SbsInfo.Start, x + SbsInfo.End, HighValues[i], LowValues[i], x }, i);
                        }
                    }
                }
            }
        }

        internal override double GetActualWidth()
        {
            return Width;
        }

        internal override double GetActualSpacing()
        {
            return Spacing;
        }

        internal override TooltipInfo? GetTooltipInfo(ChartTooltipBehavior tooltipBehavior, float x, float y)
        {
            if (ChartArea == null || SeriesYValues == null)
                return null;

            var tooltipInfo = base.GetTooltipInfo(tooltipBehavior, x, y);

            if (tooltipInfo != null)
            {
                var index = tooltipInfo.Index;

                IList<double> lowValue = SeriesYValues[1];
                double lowValueContent = Convert.ToDouble(lowValue[index]);

                if (ChartArea.IsTransposed)
                {
                    var segment = (ChartSegment)Segments[index];
                    tooltipInfo.X = segment.SegmentBounds.Center.X;
                    tooltipInfo.Y = segment.SegmentBounds.Top;
                }

                if (!double.IsNaN(lowValueContent))
                {
                   tooltipInfo.Text += "/" + lowValueContent.ToString("#.##");
                }

                return tooltipInfo;
            }

            return null;
        }

        internal override DataTemplate? GetDefaultTooltipTemplate(TooltipInfo info)
        {
            var texts = info.Text.Split('/');
            DataTemplate template = new DataTemplate(() =>
            {
                Grid grid = new Grid()
                {
                    RowDefinitions =
                    {
                        new RowDefinition{Height=GridLength.Auto },
                    },
                };

                grid.Add(RangeColumnSeries.GetTooltipContent(texts[0], texts[1], info), 0, 1);

                return grid;
            });

            return template;
        }

        internal override void GenerateTrackballPointInfo(List<object> nearestDataPoints, List<TrackballPointInfo> PointInfos, ref bool isSidebySide)
        {
            var xValues = GetXValues();
            float xPosition = 0f;
            float yPosition = 0f;
            if (nearestDataPoints != null && ActualData != null && xValues != null && SeriesYValues != null)
            {
                IList<double> yValues = SeriesYValues[0];
                IList<double> yValues1 = SeriesYValues[1];
                foreach (object point in nearestDataPoints)
                {
                    int index = ActualData.IndexOf(point);
                    var xValue = xValues[index];
                    double yValue = yValues[index];
                    double yValue1 = yValues1[index];
                     string label = string.Format("{0} : {1:#.##}\n{2} : {3:#.##}", SfCartesianChartResources.High, yValue, SfCartesianChartResources.Low, yValue1);

                    if (IsSideBySide)
                    {
                        isSidebySide = true;
                        double xMidVal = xValue + SbsInfo.Start + ((SbsInfo.End - SbsInfo.Start) / 2);
                        xPosition = TransformToVisibleX(xMidVal, yValue);
                        yPosition = TransformToVisibleY(xMidVal, yValue);
                    }

                    TrackballPointInfo? chartPointInfo = CreateTrackballPointInfo(xPosition, yPosition, label, point);

                    if (chartPointInfo != null)
                    {
#if ANDROID
                        Size contentSize = ChartUtils.GetLabelSize(label, chartPointInfo.TooltipHelper);
                        chartPointInfo.GroupLabelSize = contentSize;
#endif
                        chartPointInfo.XValue = xValue;
                        PointInfos.Add(chartPointInfo);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private static StackLayout GetTooltipContent(string highValue,string lowValue, TooltipInfo info)
        {
            var layout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Fill,
            };
            
            Label highLabel = new Label()
            {
                Text = SfCartesianChartResources.High + " : " + highValue,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = info.TextColor,
                Margin = info.Margin,
                Background = info.Background,
                FontAttributes = info.FontAttributes,
                FontSize = info.FontSize,
            };
            
            Label lowLabel = new Label
            {
                Text = SfCartesianChartResources.Low + "  : " + lowValue,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = info.TextColor,
                Margin = info.Margin,
                Background = info.Background,
                FontAttributes = info.FontAttributes,
                FontSize = info.FontSize,
            };

            layout.Children.Add(highLabel);
            layout.Children.Add(lowLabel);

            return layout;
        }

        private void CreateSegment(SeriesView seriesView, double[] values, int index)
        {
            var rangeColumn = CreateSegment() as RangeColumnSegment;
            if (rangeColumn != null)
            {
                rangeColumn.Series = this;
                rangeColumn.SeriesView = seriesView;
                rangeColumn.SetData(values);
                rangeColumn.Index = index;
                rangeColumn.Item = ActualData?[index];
                InitiateDataLabels(rangeColumn);
                Segments.Add(rangeColumn);

                if (OldSegments != null && OldSegments.Count > 0 && OldSegments.Count > index)
                {
                    var oldSegment = OldSegments[index] as RangeColumnSegment;
                    if (oldSegment != null)
                        rangeColumn.SetPreviousData(new[] { oldSegment.Y1, oldSegment.Y2 });
                }
            }
        }

        private static void OnWidthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is RangeColumnSeries rangeColumn)
            {
                rangeColumn.UpdateSbsSeries();
            }
        }

        private static void OnSpacingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is RangeColumnSeries rangeColumn)
            {
                rangeColumn.UpdateSbsSeries();
            }
        }

        private static void OnCornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is RangeColumnSeries rangeColumn)
            {
                rangeColumn.InvalidateSeries();
            }
        }

        #endregion

        #endregion
    }
}
