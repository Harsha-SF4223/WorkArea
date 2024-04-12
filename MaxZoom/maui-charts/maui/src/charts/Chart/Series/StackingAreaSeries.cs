using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// The <see cref="StackingAreaSeries"/> is a collection of data points, where the areas are stacked on top of each other.
    /// </summary>
    /// <remarks>
    /// <para>To render a series, create an instance of <see cref="StackingAreaSeries"/> class, and add it to the <see cref="SfCartesianChart.Series"/> collection.</para>
    /// 
    /// <para>It provides options for <see cref="ChartSeries.Fill"/>, <see cref="ChartSeries.PaletteBrushes"/>, <see cref="XYDataSeries.StrokeWidth"/>, <see cref="StackingSeriesBase.Stroke"/>, <see cref="StackingSeriesBase.StrokeDashArray"/>, and <see cref="ChartSeries.Opacity"/> to customize the appearance.</para>
    /// 
    /// <para> <b>EnableTooltip - </b> A tooltip displays information while tapping or mouse hovering above a segment. To display the tooltip on a chart, you need to set the <see cref="ChartSeries.EnableTooltip"/> property as <b>true</b> in <see cref="StackingAreaSeries"/> class, and also refer <seealso cref="ChartBase.TooltipBehavior"/> property.</para>
    /// <para> <b>Data Label - </b> Data labels are used to display values related to a chart segment. To render the data labels, you need to set the <see cref="ChartSeries.ShowDataLabels"/> property as <b>true</b> in <see cref="StackingAreaSeries"/> class. To customize the chart data labels placement, and label styles, you need to create an instance of <see cref="CartesianDataLabelSettings"/> and set to the <see cref="CartesianSeries.DataLabelSettings"/> property.</para>
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
    ///               <chart:StackingAreaSeries
    ///                   ItemsSource="{Binding Data}"
    ///                   XBindingPath="XValue"
    ///                   YBindingPath="YValue"/>
    ///                   
    ///               <chart:StackingAreaSeries
    ///                   ItemsSource="{Binding Data_1}"
    ///                   XBindingPath="XValue"
    ///                   YBindingPath="YValue"/>
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
    ///     StackingAreaSeries series = new StackingAreaSeries();
    ///     series.ItemsSource = viewModel.Data;
    ///     series.XBindingPath = "XValue";
    ///     series.YBindingPath = "YValue";
    ///     
    ///     StackingAreaSeries series_1 = new StackingAreaSeries();
    ///     series.ItemsSource = viewModel.Data_1;
    ///     series.XBindingPath = "XValue";
    ///     series.YBindingPath = "YValue";
    ///     
    ///     chart.Series.Add(series);
    ///     chart.Series.Add(series_1);
    ///     
    /// ]]></code>
    /// # [ViewModel](#tab/tabid-3)
    /// <code><![CDATA[
    ///     public ObservableCollection<Model> Data { get; set; }
    ///     public ObservableCollection<Model> Data_1 { get; set; }
    /// 
    ///     public ViewModel()
    ///     {
    ///        Data = new ObservableCollection<Model>();
    ///        Data.Add(new Model() { XValue = 10, YValue = 100 });
    ///        Data.Add(new Model() { XValue = 20, YValue = 150 });
    ///        Data.Add(new Model() { XValue = 30, YValue = 110 });
    ///        Data.Add(new Model() { XValue = 40, YValue = 230 });
    ///        
    ///        Data_1 = new ObservableCollection<Model>();
    ///        Data.Add(new Model() { XValue = 50, YValue = 250 });
    ///        Data.Add(new Model() { XValue = 60, YValue = 270 });
    ///        Data.Add(new Model() { XValue = 70, YValue = 280 });
    ///        Data.Add(new Model() { XValue = 80, YValue = 310 });
    ///     }
    /// ]]></code>
    /// ***
    /// </example>
    /// <seealso cref="StackingAreaSegment"/>
    public class StackingAreaSeries : StackingSeriesBase, IDrawCustomLegendIcon
    {
        #region Properties

        #region Bindable Properties

        /// <summary>
        ///  Identifies the <see cref="ShowMarkers"/> bindable property
        /// </summary>
        internal static readonly BindableProperty ShowMarkersProperty = ChartMarker.ShowMarkersProperty;

        /// <summary>
        ///  Identifies the <see cref="MarkerSettings"/> bindable property
        /// </summary>
        internal static readonly BindableProperty MarkerSettingsProperty = ChartMarker.MarkerSettingsProperty;

        #endregion

        #region Public Properties

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
        ///          <chart:StackingAreaSeries ItemsSource="{Binding Data}"
        ///                            XBindingPath="XValue"
        ///                            YBindingPath="YValue"
        ///                            ShowMarkers="True"/>
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
        ///     StackingAreaSeries series = new StackingAreaSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           YBindingPath = "YValue",
        ///           ShowMarkers= true,
        ///     };
        ///     
        ///     chart.Series.Add(series);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        internal bool ShowMarkers
        {
            get { return (bool)GetValue(ShowMarkersProperty); }
            set { SetValue(ShowMarkersProperty, value); }
        }

        /// <summary>
        /// Gets or sets the option to customize the series markers.
        /// </summary>
        /// <value>It accepts <see cref="ChartMarkerSettings"/>.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-28)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:StackingAreaSeries ItemsSource="{Binding Data}"
        ///                            XBindingPath="XValue"
        ///                            YBindingPath="YValue"
        ///                            ShowMarkers="True">
        ///               <chart:StackingAreaSeries.MarkerSettings>
        ///                     <chart:ChartMarkerSettings Fill="Red" Height="15" Width="15" />
        ///               </chart:StackingAreaSeries.MarkerSettings>
        ///          </chart:StackingAreaSeries>
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
        ///     StackingAreaSeries series = new StackingAreaSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           YBindingPath = "YValue",
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
        internal ChartMarkerSettings MarkerSettings
        {
            get { return (ChartMarkerSettings)GetValue(MarkerSettingsProperty); }
            set { SetValue(MarkerSettingsProperty, value); }
        }

        #endregion

        #endregion

        #region Interface Implementation

        void IDrawCustomLegendIcon.DrawSeriesLegend(ICanvas canvas, RectF rect, Brush fillColor, bool isSaveState)
        {
            if (isSaveState)
            {
                canvas.SaveState();
            }

            var pathF = new PathF();
            pathF.MoveTo(0, 7);
            pathF.LineTo(4, 0);
            pathF.LineTo(7, 7);
            pathF.LineTo(12, 5);
            pathF.LineTo(12, 12);
            pathF.LineTo(0, 12);
            pathF.LineTo(0, 7);
            pathF.Close();
            canvas.FillPath(pathF);

            if (isSaveState)
            {
                canvas.RestoreState();
            }
        }

        #endregion 

        #region Methods

        #region Public Methods

        /// <summary>
        /// Method to get the index of data point.
        /// </summary>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <returns></returns>

        public override int GetDataPointIndex(float pointX, float pointY)
        {
            if (Chart != null)
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

                List<PointF> segPoints = new List<PointF>();

                foreach (StackingAreaSegment segment in Segments)
                {
                    var points = segment.FillPoints;
                    if (points != null)
                    {
                        for (int i = 0; i < points.Count; i += 2)
                        {
                            if (i + 1 < points.Count)
                            {
                                float x = points[i];
                                float y = points[i + 1];
                                segPoints.Add(new PointF(x, y));
                            }
                        }
                    }
                }

                float xPos = pointX - (float)AreaBounds.Left;
                float yPos = pointY - (float)AreaBounds.Top;

                if (ChartUtils.IsAreaContains(segPoints, xPos, yPos))
                {
                    return tooltipIndex;
                }
            }

            return -1;
        }

        #endregion

        #region Internal Methods

        internal override void GenerateSegments(SeriesView seriesView)
        {
            var actualXValues = GetXValues();

            if (actualXValues == null || actualXValues.Count == 0)
            {
                return;
            }

            List<double>? xValues = null, yEnds = null, yStarts = null;
            List<object>? items = null;

            if (BottomValues == null)
            {
                ChartArea?.UpdateStackingSeries();
            }

            if (BottomValues == null)
            {
                return;
            }

            for (int i = 0; i < PointsCount; i++)
            {
                var yStartValues = BottomValues;
                var yEndValues = TopValues;

                if (!double.IsNaN(YValues[i]))
                {
                    if (xValues == null)
                    {
                        xValues = new List<double>();
                        yEnds = new List<double>();
                        yStarts = new List<double>();
                        items = new List<object>();
                    }

                    xValues.Add(actualXValues[i]);
                    yEnds?.Add(yEndValues![i]);
                    yStarts?.Add(yStartValues![i]);
                    items?.Add(ActualData![i]);
                }

                if (double.IsNaN(YValues[i]) || i == PointsCount - 1)
                {
                    if (xValues != null)
                    {
                        var segment = CreateSegment() as StackingAreaSegment;
                        if (segment != null)
                        {
                            segment.Series = this;
                            segment.SeriesView = seriesView;
                            if (yStarts != null && yEnds != null)
                                segment.SetData(xValues, yEnds, yStarts);
                            segment.Item = items;
                            InitiateDataLabels(segment);
                            Segments.Add(segment);
                        }

                        yEnds = xValues = yStarts = null;
                        items = null;
                    }

                    if (double.IsNaN(YValues[i]))
                    {
                        xValues = new List<double> { actualXValues[i] };
                        yStarts = new List<double> { YValues[i] };
                        yEnds = new List<double> { YValues[i] };
                        items = new List<object> { ActualData![i] };

                        var segment = CreateSegment() as StackingAreaSegment;
                        segment!.Series = this;
                        segment.Item = items;
                        segment.SetData(xValues, yEnds, yStarts);
                        yEnds = xValues = yStarts = null;
                        items = null;
                    }
                }
            }
        }

        internal override void InitiateDataLabels(ChartSegment segment)
        {
            for (int i = 0; i < PointsCount; i++)
            {
                var datalabel = new ChartDataLabel();
                segment.DataLabels.Add(datalabel);
                DataLabels.Add(datalabel);
            }
        }

        internal override void UpdateRange()
        {
            bool isStacking100 = this is StackingArea100Series;
            double yStart = YRange.Start;
            double yEnd = YRange.End;

            if (yStart > 0)
            {
                yStart = 0;
            }

            if (yEnd < 0)
            {
                yEnd = 0;
            }

            if (isStacking100)
            {
                yStart = yStart <= -100 ? -100 : yStart;
                yEnd = yEnd >= 100 ? 100 : yEnd;
            }

            YRange = new DoubleRange(yStart, yEnd);
            base.UpdateRange();
        }

        internal override bool IsIndividualSegment()
        {
            return false;
        }

        internal override TooltipInfo? GetTooltipInfo(ChartTooltipBehavior tooltipBehavior, float x, float y)
        {
            if (Segments == null) return null;

            int index = SeriesContainsPoint(new PointF(x, y)) ? TooltipDataPointIndex : -1;

            if (index < 0 || ItemsSource == null || ActualData == null || ActualXAxis == null
                || ActualYAxis == null || SeriesYValues == null)
            {
                return null;
            }

            var xValues = GetXValues();

            if (xValues == null || ChartArea == null || TopValues == null) return null;

            object dataPoint = ActualData[index];
            double xValue = xValues[index];
            IList<double> yValues = SeriesYValues[0];
            double content = Convert.ToDouble(yValues[index]);
            double yValue = TopValues[index];
            float xPosition = TransformToVisibleX(xValue, yValue);

            if (!double.IsNaN(xPosition) && !double.IsNaN(yValue) && !double.IsNaN(content))
            {
                float yPosition = TransformToVisibleY(xValue, yValue);

                RectF seriesBounds = AreaBounds;
                seriesBounds = new RectF(0, 0, seriesBounds.Width, seriesBounds.Height);
                yPosition = seriesBounds.Top < yPosition ? yPosition : seriesBounds.Top;
                yPosition = seriesBounds.Bottom > yPosition ? yPosition : seriesBounds.Bottom;
                xPosition = seriesBounds.Left < xPosition ? xPosition : seriesBounds.Left;
                xPosition = seriesBounds.Right > xPosition ? xPosition : seriesBounds.Right;

                TooltipInfo tooltipInfo = new TooltipInfo(this);
                tooltipInfo.X = xPosition;
                tooltipInfo.Y = yPosition;
                tooltipInfo.Index = index;
                tooltipInfo.Margin = tooltipBehavior.Margin;
                tooltipInfo.TextColor = tooltipBehavior.TextColor;
                tooltipInfo.FontFamily = tooltipBehavior.FontFamily;
                tooltipInfo.FontSize = tooltipBehavior.FontSize;
                tooltipInfo.FontAttributes = tooltipBehavior.FontAttributes;
                tooltipInfo.Background = tooltipBehavior.Background;
                tooltipInfo.Text = content.ToString();
                tooltipInfo.Item = dataPoint;

                return tooltipInfo;
            }

            return null;
        }

        internal override void GenerateTrackballPointInfo(List<object> nearestDataPoints, List<TrackballPointInfo> pointInfos, ref bool isSidebySide)
        {
            var xValues = GetXValues();

            if (nearestDataPoints != null && ActualData != null && xValues != null && SeriesYValues != null && TopValues != null)
            {
                IList<double> topValues = TopValues;
                IList<double> yvalues = SeriesYValues[0];

                foreach (object point in nearestDataPoints)
                {
                    int index = ActualData.IndexOf(point);
                    var xValue = xValues[index];
                    double topValue = topValues[index];
                    double yvalue = yvalues[index];
                    string label = yvalue.ToString();
                    var xPoint = TransformToVisibleX(xValue, topValue);
                    var yPoint = TransformToVisibleY(xValue, topValue);

                    TrackballPointInfo? chartPointInfo = CreateTrackballPointInfo(xPoint, yPoint, label, point);

                    if (chartPointInfo != null)
                    {
                        chartPointInfo.XValue = xValue;
                        pointInfos.Add(chartPointInfo);
                    }
                }
            }
        }

        internal override bool SeriesContainsPoint(PointF point)
        {
            return base.SeriesContainsPoint(point);
        }

        internal override PointF GetDataLabelPosition(ChartSegment dataLabel, SizeF labelSize, PointF labelPosition, float padding)
        {
            return DataLabelSettings.GetLabelPositionForAreaSeries(this, dataLabel, labelSize, labelPosition, padding);
        }

        internal override void DrawDataLabels(ICanvas canvas)
        {
            var dataLabelSettings = DataLabelSettings;

            List<double> xvalues = GetXValues()!;

            IList<double> actualYValues = YValues;

            if (dataLabelSettings == null || Segments == null || Segments.Count <= 0)
            {
                return;
            }

            ChartDataLabelStyle labelStyle = DataLabelSettings.LabelStyle;

            foreach (StackingAreaSegment dataLabel in Segments)
            {
                if (dataLabel == null || dataLabel.XValues == null || dataLabel.BottomValues == null || dataLabel.TopValues == null)
                {
                    return;
                }

                for (int i = 0; i < PointsCount; i++)
                {
                    double x = xvalues[i];
                    double y = TopValues![i];

                    if (double.IsNaN(y))
                    {
                        continue;
                    }

                    CalculateDataPointPosition(i, ref x, ref y);
                    PointF labelPoint = new PointF((float)x, (float)y);
                    dataLabel.LabelContent = GetLabelContent(actualYValues[i], SumOfValues(this.YValues));
                    dataLabel.LabelPositionPoint = dataLabelSettings.CalculateDataLabelPoint(this, dataLabel, labelPoint, labelStyle);
                    UpdateDataLabelAppearance(canvas, dataLabel, dataLabelSettings, labelStyle);
                }
            }
        }

        internal override void UpdateLegendItemToggle()
        {
            if (ChartArea != null)
            {
                var visibleSeries = ChartArea.VisibleSeries;
                
                if (visibleSeries == null) return;

                foreach (var chartSeries in visibleSeries)
                {
                    chartSeries.SegmentsCreated = false;
                }
            }  
        }

        internal override void OnDetachedToChart()
        {
            if (ChartArea != null)
            {
                var visibleSeries = ChartArea.VisibleSeries;

                if (visibleSeries == null) return;

                foreach (var chartSeries in visibleSeries)
                {
                    chartSeries.SegmentsCreated = false;
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// This method is used to create the segment for the series.
        /// </summary>
        protected override ChartSegment? CreateSegment()
        {
            return new StackingAreaSegment();
        }

        #endregion       

        #endregion
    }
}
