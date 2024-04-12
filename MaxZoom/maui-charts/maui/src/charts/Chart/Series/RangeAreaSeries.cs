using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// The <see cref="RangeAreaSeries"/> is a chart type to represent data as filled areas between two ranges, typically depicting a range of values or uncertainty.
    /// </summary>
    /// <remarks>
    /// <para>To render a series, create an instance of <see cref="RangeAreaSeries"/> class, and add it to the <see cref="SfCartesianChart.Series"/> collection.</para>
    /// <para> <b>EnableTooltip - </b> A tooltip displays information while tapping or mouse hovering above a segment. To display the tooltip on a chart, you need to set the <see cref="ChartSeries.EnableTooltip"/> property as <b>true</b> in <see cref="RangeAreaSeries"/> class, and also refer <seealso cref="ChartBase.TooltipBehavior"/> property.</para>
    /// <para> <b>Data Label - </b> Data labels are used to display values related to a chart segment. To render the data labels, you need to set the <see cref="ChartSeries.ShowDataLabels"/> property as <b>true</b> in <see cref="RangeAreaSeries"/> class. To customize the chart data labels alignment, placement, and label styles, you need to create an instance of <see cref="CartesianDataLabelSettings"/> and set to the <see cref="CartesianSeries.DataLabelSettings"/> property.</para>
    /// <para> <b>Animation - </b> To animate the series, set <b>True</b> to the <see cref="ChartSeries.EnableAnimation"/> property.</para>
    /// <para> <b>LegendIcon - </b> To customize the legend icon using the <see cref="ChartSeries.LegendIcon"/> property.</para>
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
    ///               <chart:RangeAreaSeries
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
    ///     RangeAreaSeries series = new RangeAreaSeries();
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
    ///        Data.Add(new Model() { XValue = Value 1, HighValue = 3, LowValue = 6 });
    ///        Data.Add(new Model() { XValue = Value 2, HighValue = 3, LowValue = 7 });
    ///        Data.Add(new Model() { XValue = Value 3, HighValue = 4, LowValue = 10 });
    ///        Data.Add(new Model() { XValue = Value 4, HighValue = 6, LowValue = 13 });
    ///        Data.Add(new Model() { XValue = Value 5, HighValue = 9, LowValue = 17 });
    ///     }
    /// ]]></code>
    /// ***
    /// </example>
    /// <seealso cref="RangeAreaSegment"/>
    public class RangeAreaSeries : RangeSeriesBase, IMarkerDependent
    {
        #region Properties

        #region Bindable Properties

        /// <summary>
        /// Identifies the <see cref="StrokeDashArray"/> bindable property.
        /// </summary>
        internal static readonly new BindableProperty StrokeDashArrayProperty =
           BindableProperty.Create(nameof(StrokeDashArray), typeof(DoubleCollection), typeof(RangeAreaSeries), null, BindingMode.Default, null, OnStrokeDashArrayPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="ShowMarkers"/> bindable property.
        /// </summary>        
        public static readonly BindableProperty ShowMarkersProperty = ChartMarker.ShowMarkersProperty;

        /// <summary>
        /// Identifies the <see cref="MarkerSettings"/> bindable property.
        /// </summary>        
        public static readonly BindableProperty MarkerSettingsProperty = ChartMarker.MarkerSettingsProperty;

        #endregion

        #region  Public Properties

        /// <summary>
        /// Gets or sets the stroke dash array to customize the appearance of the series border.
        /// </summary>
        /// <value>It accepts the <see cref="DoubleCollection"/> value, and the default value is null.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-28)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:RangeAreaSeries   ItemsSource="{Binding Data}"
        ///                                   XBindingPath="XValue"
        ///                                   High ="HighValue"
        ///                                   Low ="LowValue"
        ///                                   StrokeDashArray="5,3"
        ///                                   Stroke = "Red" />
        ///
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-29)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///     // Eliminated for simplicity
        ///
        ///     DoubleCollection doubleCollection = new DoubleCollection();
        ///     doubleCollection.Add(5);
        ///     doubleCollection.Add(3);
        ///     RangeAreaSeries series = new RangeAreaSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           High ="HighValue",
        ///           Low ="LowValue",
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
        public new DoubleCollection StrokeDashArray
        {
            get { return (DoubleCollection)GetValue(StrokeDashArrayProperty); }
            set { SetValue(StrokeDashArrayProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value indicating whether to show markers for the series data point.
        /// </summary>
        /// <value>It accepts boolean values and its default value is false.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-28)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:RangeAreaSeries ItemsSource="{Binding Data}"
        ///                                 XBindingPath="XValue"
        ///                                 High="HighValue"
        ///                                 Low="LowValue"
        ///                                 ShowMarkers="True"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-29)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///     // Eliminated for simplicity
        ///
        ///     RangeAreaSeries series = new RangeAreaSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           HighValue = "HighValue",
        ///           LowValue = "LowValue",
        ///           ShowMarkers= true,
        ///     };
        ///     
        ///     chart.Series.Add(series);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public bool ShowMarkers
        {
            get { return (bool)GetValue(ShowMarkersProperty); }
            set { SetValue(ShowMarkersProperty, value); }
        }

        /// <summary>
        /// Gets or sets the option for customize the series markers.
        /// </summary>
        /// <value>It accepts <see cref="ChartMarkerSettings"/>.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-28)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:RangeAreaSeries ItemsSource="{Binding Data}"
        ///                            XBindingPath="XValue"
        ///                            High="HighValue"
        ///                            Low="LowValue"
        ///                            ShowMarkers="True">
        ///               <chart:RangeAreaSeries.MarkerSettings>
        ///                     <chart:ChartMarkerSettings Fill="Red" Height="15" Width="15" />
        ///               </chart:RangeAreaSeries.MarkerSettings>
        ///          </chart:RangeAreaSeries>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-29)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///     // Eliminated for simplicity
        ///
        ///    ChartMarkerSettings chartMarkerSettings = new ChartMarkerSettings()
        ///    {
        ///        Fill = new SolidColorBrush(Colors.Red),
        ///        Height = 15,
        ///        Width = 15,
        ///    };
        ///     RangeAreaSeries series = new RangeAreaSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           High = "HighValue",
        ///           Low = "LowValue",
        ///           ShowMarkers= true,
        ///           MarkerSettings= chartMarkerSettings,
        ///     };
        ///     
        ///     chart.Series.Add(series);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public ChartMarkerSettings MarkerSettings
        {
            get { return (ChartMarkerSettings)GetValue(MarkerSettingsProperty); }
            set { SetValue(MarkerSettingsProperty, value); }
        }
        #endregion

        #region Internal Properties

        internal override bool IsMultipleYPathRequired => true;

        #endregion

        #endregion

        #region Methods

        #region Internal Methods

        internal override void SetDashArray(ChartSegment segment)
        {
            segment.StrokeDashArray = StrokeDashArray;
        }

        internal override void GenerateSegments(SeriesView seriesView)
        {
            var actualXValues = GetXValues();

            if (actualXValues == null)
            {
                return;
            }

            List<double>? xValues = null, highValues = null, lowValues = null;
            List<object>? items = null;

            for (int i = 0; i < PointsCount; i++)
            {
                if (!double.IsNaN(HighValues[i]) && !double.IsNaN(LowValues[i]))
                {
                    if (xValues == null)
                    {
                        xValues = new List<double>();
                        highValues = new List<double>();
                        lowValues = new List<double>();
                        items = new List<object>();
                    }

                    xValues.Add(actualXValues[i]);
                    highValues?.Add(HighValues[i]);
                    lowValues?.Add(LowValues[i]);
                    items?.Add(ActualData![i]);
                }

                if (double.IsNaN(HighValues[i]) || double.IsNaN(LowValues[i]) || i == PointsCount - 1)
                {
                    if (xValues != null)
                    {
                        if(CreateSegment() is RangeAreaSegment segment)
                        {
                            segment.Series = this;

                            if (highValues != null && lowValues != null)
                            {
                                segment.SetData(xValues, highValues, lowValues);
                            }

                            segment.Item = items;
                            InitiateDataLabels(segment);
                            Segments.Add(segment);
                        }

                        xValues = lowValues = highValues = null;
                        items = null;
                    }

                    if (double.IsNaN(HighValues[i]) || double.IsNaN(LowValues[i]))
                    {
                        xValues = new List<double> { actualXValues[i] };
                        highValues = new List<double> { HighValues[i] };
                        lowValues = new List<double> { LowValues[i] };
                        items = new List<object> { ActualData![i] };

                        if(CreateSegment() is RangeAreaSegment segment)
                        {
                            segment.Series = this;
                            segment.Item = items;
                            segment.SetData(xValues, highValues, lowValues);
                        }

                        xValues = highValues = lowValues = null;
                        items = null;
                    }
                }
            }
        }

        internal override TooltipInfo? GetTooltipInfo(ChartTooltipBehavior tooltipBehavior, float x, float y)
        {
            if (ChartArea == null || SeriesYValues == null)
            {
                return null;
            }

            var tooltipInfo = base.GetTooltipInfo(tooltipBehavior, x, y);

            if (tooltipInfo != null)
            {
                var index = tooltipInfo.Index;
                IList<double> lowValue = SeriesYValues[1];
                double lowValueContent = Convert.ToDouble(lowValue[index]);

                if (double.IsNaN(lowValueContent))
                {
                    return null;
                }
                else if (!double.IsNaN(lowValueContent))
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
                
                grid.Add(RangeAreaSeries.GetTooltipContent(texts[0], texts[1], info), 0, 1);
                return grid;
            });
            
            return template;
        }

        internal override void InitiateDataLabels(ChartSegment segment)
        {
            for (int i = 0; i < PointsCount; i++)
            {
               for(int j =0; j < 2; j++)
                {
                    var datalabel = new ChartDataLabel();
                    segment.DataLabels.Add(datalabel);
                    DataLabels.Add(datalabel);
                }
            }
        }

        internal override SizeF GetLabelTemplateSize(ChartSegment segment)
        {
            var rangeAreaSegment = segment as RangeAreaSegment;

            if (LabelTemplateView != null && LabelTemplateView.Count > rangeAreaSegment?.LabelIndex && LabelTemplateView[rangeAreaSegment.LabelIndex] is DataLabelItemView templateView)
            {
                if (templateView != null && templateView.ContentView is View content)
                {
                    if (!content.DesiredSize.IsZero)
                    {
                        return content.DesiredSize;
                    }

                    var desiredSize = (Size)templateView.Measure(double.PositiveInfinity, double.PositiveInfinity);

                    if (desiredSize.IsZero)
                        return (Size)content.Measure(double.PositiveInfinity, double.PositiveInfinity);

                    return desiredSize;
                }
            }

            return SizeF.Zero;
        }

        internal override void DrawDataLabels(ICanvas canvas)
        {
            var dataLabelSettings = DataLabelSettings;

            if (dataLabelSettings == null || Segments == null || Segments.Count <= 0)
            {
                return;
            }

            ChartDataLabelStyle labelStyle = DataLabelSettings.LabelStyle;

            foreach (RangeAreaSegment dataLabel in Segments)
            {
                if (dataLabel == null || dataLabel.XValues == null || dataLabel.HighValues == null || dataLabel.LowValues == null)
                {
                    return;
                }

                for (int i = 0; i < dataLabel.XValues.Length; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (j == 0)
                        {
                            double x = dataLabel.XValues[i];
                            double y = dataLabel.HighValues[i];

                            if (double.IsNaN(y) || !IsDataInVisibleRange(x, y))
                            {
                                continue;
                            }

                            CalculateDataPointPosition(i, ref x, ref y);
                            PointF labelPoint = new PointF((float)x, (float)y);
                            dataLabel.LabelContent = GetLabelContent(dataLabel.HighValues[i], SumOfValues(this.HighValues));
                            dataLabel.LabelPositionPoint = dataLabelSettings.CalculateDataLabelPoint(this, dataLabel, labelPoint, dataLabelSettings.LabelStyle, "HighType");
                            UpdateDataLabelAppearance(canvas, dataLabel, dataLabelSettings, labelStyle);
                        }
                        else
                        {
                            double x = dataLabel.XValues[i];
                            double y = dataLabel.LowValues[i];

                            if (double.IsNaN(y) || !IsDataInVisibleRange(x, y))
                            {
                                continue;
                            }

                            CalculateDataPointPosition(i, ref x, ref y);
                            PointF labelPoint = new PointF((float)x, (float)y);
                            dataLabel.LabelContent = GetLabelContent(dataLabel.LowValues[i], SumOfValues(this.LowValues));
                            dataLabel.LabelPositionPoint = dataLabelSettings.CalculateDataLabelPoint(this, dataLabel, labelPoint, dataLabelSettings.LabelStyle, "LowType");
                            UpdateDataLabelAppearance(canvas, dataLabel, dataLabelSettings, labelStyle);
                        }
                    }

                }
            }
        }

        internal override void GenerateTrackballPointInfo(List<object> nearestDataPoints, List<TrackballPointInfo> pointInfos, ref bool isSidebySide)
        {

        }

        internal override void CalculateDataPointPosition(int index, ref double x, ref double y)
        {
            if (ChartArea == null)
            {
                return;
            }

            var xCal = x;
            var yCal = y;

            if (ActualYAxis != null && ActualXAxis != null && !double.IsNaN(yCal))
            {
                y = ChartArea.IsTransposed ? ActualXAxis.ValueToPoint(xCal) : ActualYAxis.ValueToPoint(yCal);
            }
            
            if (ActualXAxis != null && ActualYAxis != null && !double.IsNaN(x))
            {
                x = ChartArea.IsTransposed ? ActualYAxis.ValueToPoint(yCal) : ActualXAxis.ValueToPoint(xCal);
            }
        }
        #endregion

        #region Private Methods

        private static StackLayout GetTooltipContent(string highValue, string lowValue, TooltipInfo info)
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

        void IMarkerDependent.InvalidateDrawable()
        {
            InvalidateSeries();
        }
        
        bool needToAnimateMarker;

        bool IMarkerDependent.NeedToAnimateMarker { get => needToAnimateMarker; set => needToAnimateMarker = this.EnableAnimation; }

        ChartMarkerSettings IMarkerDependent.MarkerSettings => MarkerSettings ?? new ChartMarkerSettings();

        void IMarkerDependent.DrawMarker(ICanvas canvas, int index, ShapeType type, Rect rect) => this.DrawMarker(canvas, index, type, rect);

        #endregion

        #region Protected Methods

        /// <summary>
        /// Creates the Range Area segment.
        /// </summary>
        protected override ChartSegment? CreateSegment()
        {
            return new RangeAreaSegment();
        }

        /// <summary>
        /// Calls when drawing markers.
        /// </summary>
        protected virtual void DrawMarker(ICanvas canvas, int index, ShapeType type, Rect rect)
        {
            if (this is IMarkerDependent markerDependent)
            {
                canvas.DrawShape(rect, shapeType: type, markerDependent.MarkerSettings.HasBorder, false);
            }
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override int GetDataPointIndex(float pointX, float pointY)
        {
            var dataPoint = FindNearestChartPoint(pointX, pointY);

            if (dataPoint == null || ActualData == null)
            {
                return -1;
            }

            var tooltipIndex = ActualData.IndexOf(dataPoint);

            if (tooltipIndex < 0)
            {
                return -1;
            }

            double highValue = HighValues[tooltipIndex];
            double lowValue = LowValues[tooltipIndex];

            if (double.IsNaN(highValue) && double.IsNaN(lowValue))
            {
                return -1;
            }

            PointF visiblePoint = new PointF();
            List<PointF> segPoints = new List<PointF>();
            var xValues = GetXValues();

            if (xValues != null)
            {
                double xValue = xValues[tooltipIndex];
                var visibleX = TransformToVisibleX(xValue, 0);

                if ((tooltipIndex == ActualData.Count - 1) || (pointX - (float)AreaBounds.Left) < visibleX)
                {
                    xValue = xValues[tooltipIndex];
                    double y1Value = HighValues[tooltipIndex];
                    double y2Value = LowValues[tooltipIndex];
                    visiblePoint.X = TransformToVisibleX(xValue, y1Value);
                    visiblePoint.Y = TransformToVisibleY(xValue, y1Value);
                    segPoints.Add(new PointF(visiblePoint.X, visiblePoint.Y));
                    visiblePoint.X = TransformToVisibleX(xValue, y2Value);
                    visiblePoint.Y = TransformToVisibleY(xValue, y2Value);
                    segPoints.Add(new PointF(visiblePoint.X, visiblePoint.Y));
                }
                else
                {
                    xValue = xValues[tooltipIndex + 1];
                    double y1Value = HighValues[tooltipIndex+1];
                    double y2Value = LowValues[tooltipIndex+1];
                    visiblePoint.X = TransformToVisibleX(xValue, y1Value);
                    visiblePoint.Y = TransformToVisibleY(xValue, y1Value);
                    segPoints.Add(new PointF(visiblePoint.X, visiblePoint.Y));
                    visiblePoint.X = TransformToVisibleX(xValue, y2Value);
                    visiblePoint.Y = TransformToVisibleY(xValue, y2Value);
                    segPoints.Add(new PointF(visiblePoint.X, visiblePoint.Y));
                }

                if (((pointX - (float)AreaBounds.Left) < visibleX) && tooltipIndex != 0)
                {
                    xValue = xValues[tooltipIndex - 1];
                    double y1Value = HighValues[tooltipIndex - 1];
                    double y2Value = LowValues[tooltipIndex - 1];
                    visiblePoint.X = TransformToVisibleX(xValue, y2Value);
                    visiblePoint.Y = TransformToVisibleY(xValue, y2Value);
                    segPoints.Add(new PointF(visiblePoint.X, visiblePoint.Y));
                    visiblePoint.X = TransformToVisibleX(xValue, y1Value);
                    visiblePoint.Y = TransformToVisibleY(xValue, y1Value);
                    segPoints.Add(new PointF(visiblePoint.X, visiblePoint.Y));
                }
                else if (((pointX - (float)AreaBounds.Left) > visibleX) || tooltipIndex == 0)
                {
                    xValue = xValues[tooltipIndex];
                    double y1Value = HighValues[tooltipIndex];
                    double y2Value = LowValues[tooltipIndex];
                    visiblePoint.X = TransformToVisibleX(xValue, y2Value);
                    visiblePoint.Y = TransformToVisibleY(xValue, y2Value);
                    segPoints.Add(new PointF(visiblePoint.X, visiblePoint.Y));
                    visiblePoint.X = TransformToVisibleX(xValue, y1Value);
                    visiblePoint.Y = TransformToVisibleY(xValue, y1Value);
                    segPoints.Add(new PointF(visiblePoint.X, visiblePoint.Y));
                }
            }

            if (ChartUtils.IsPathContains(segPoints, pointX - (float)AreaBounds.Left, pointY - (float)AreaBounds.Top))
            {
                return tooltipIndex;
            }

            return -1;
        }

        private static void OnStrokeDashArrayPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is RangeAreaSeries series)
            {
                series.UpdateDashArray();
                series.InvalidateSeries();
            }
        }
        #endregion

        #endregion   
    }
}
