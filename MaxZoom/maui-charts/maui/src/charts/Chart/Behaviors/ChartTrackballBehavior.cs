using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Graphics.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.Maui.Themes;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// Enables the trackball in the <see cref="CartesianChart"/>.
    /// </summary>
    public class ChartTrackballBehavior : ChartBehavior, IMarkerDependent, IThemeElement
    {
        #region Fields

        bool longPressActive;
        private bool isAnySideBySideSeries = false;
        bool isAnyContinuesSeries = false;
        float currX = 0f;
        Point linePoint1 = default(Point);
        Point linePoint2 = default(Point);
        bool animateMarker = false;
        bool isScheduled = false;
        float trackLabelSpacing = 4f;
        private List<TrackballAxisInfo> axisPointInfos;
        TooltipHelper? groupedTooltip;
        private ChartLabelStyle actualLabelStyle;
        List<List<TrackballPointInfo>>? intersectedGroups;

        bool IMarkerDependent.NeedToAnimateMarker { get { return animateMarker; } set { animateMarker = value; } }
        ChartMarkerSettings IMarkerDependent.MarkerSettings => MarkerSettings ?? new ChartMarkerSettings() { Fill = SolidColorBrush.Black};

        #endregion

        #region Internal Properties
        internal SfCartesianChart? CartesianChart { get; set; }
        internal RectF SeriesBounds { get; set; }
        internal List<TrackballPointInfo> PointInfos { get; set; }

        internal static readonly BindableProperty TrackballBackgroundProperty = BindableProperty.Create(nameof(TrackballBackground), typeof(Brush), typeof(ChartTrackballBehavior), SolidColorBrush.Black, BindingMode.Default, null, null);
        internal Brush TrackballBackground
        {
            get { return (Brush)GetValue(TrackballBackgroundProperty); }
            set { SetValue(TrackballBackgroundProperty, value); }
        }

        internal static readonly BindableProperty TrackballFontSizeProperty = BindableProperty.Create(nameof(TrackballFontSize), typeof(double), typeof(ChartTrackballBehavior), 14.0, BindingMode.Default, null, null, null, null);
        internal double TrackballFontSize
        {
            get { return (double)GetValue(TrackballFontSizeProperty); }
            set { SetValue(TrackballFontSizeProperty, value); }
        }

        internal static readonly BindableProperty LineStrokeProperty = BindableProperty.Create(nameof(LineStroke), typeof(Brush), typeof(ChartTrackballBehavior), SolidColorBrush.Black, BindingMode.Default, null, null);
        internal Brush LineStroke
        {
            get { return (Brush)GetValue(LineStrokeProperty); }
            set { SetValue(LineStrokeProperty, value); }
        }

        internal static readonly BindableProperty LineStrokeWidthProperty = BindableProperty.Create(nameof(LineStrokeWidth), typeof(double), typeof(ChartTrackballBehavior), 1d, BindingMode.Default, null, null);
        internal double LineStrokeWidth
        {
            get { return (double)GetValue(LineStrokeWidthProperty); }
            set { SetValue(LineStrokeWidthProperty, value); }
        }

        #endregion

        #region Bindable Properties

        /// <summary>
        /// Identifies the <see cref="DisplayMode"/> bindable property.
        /// </summary>
        public static readonly BindableProperty DisplayModeProperty = BindableProperty.Create(nameof(DisplayMode), typeof(LabelDisplayMode), typeof(ChartTrackballBehavior), LabelDisplayMode.FloatAllPoints);

        /// <summary>
        /// Identifies the <see cref="LineStyle"/> bindable property.
        /// </summary>
        public static readonly BindableProperty LineStyleProperty = BindableProperty.Create(nameof(LineStyle), typeof(ChartLineStyle), typeof(ChartTrackballBehavior), propertyChanged: OnLineStyleChanged);

        /// <summary>
        /// Identifies the <see cref="LabelStyle"/> bindable property. 
        /// </summary>
        public static readonly BindableProperty LabelStyleProperty = BindableProperty.Create(nameof(LabelStyle), typeof(ChartLabelStyle), typeof(ChartTrackballBehavior), null, propertyChanged: OnLabelStyleChanged);

        /// <summary>
        /// Identifies the <see cref="ShowLine"/> bindable property. 
        /// </summary>
        public static readonly BindableProperty ShowLineProperty = BindableProperty.Create(nameof(ShowLine), typeof(bool), typeof(ChartTrackballBehavior), true);

        /// <summary>
        /// Identifies the <see cref="MarkerSettings"/> bindable property. 
        /// </summary>      
        public static readonly BindableProperty MarkerSettingsProperty = ChartMarker.MarkerSettingsProperty;

        /// <summary>
        /// Identifies the <see cref="ShowMarkers"/> bindable property. 
        /// </summary>
        public static readonly BindableProperty ShowMarkersProperty = ChartMarker.ShowMarkersProperty;


        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets display mode of trackball labels. 
        /// <para>By default, labels for all the series under the current point index value will be shown.</para>
        /// </summary>
        /// <value>Defaults to <see cref="LabelDisplayMode.FloatAllPoints"/></value>
        /// <example>
        /// # [Xaml](#tab/tabid-3)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///           <!--omitted for brevity-->
        ///
        ///           <chart:SfCartesianChart.TrackballBehavior>
        ///               <chart:ChartTrackballBehavior DisplayMode="NearestPoint"/>
        ///           </chart:SfCartesianChart.TrackballBehavior>
        ///
        ///           <chart:LineSeries ItemsSource="{Binding Data}"
        ///                             XBindingPath="XValue"
        ///                             YBindingPath="YValue"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-4)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///    // omitted for brevity
        ///    chart.TrackballBehavior = new ChartTrackballBehavior()
        ///    {
        ///        DisplayMode = LabelDisplayMode.NearestPoint,
        ///    };
        ///
        ///     LineSeries series = new LineSeries();
        ///     series.ItemsSource = viewModel.Data;
        ///     series.XBindingPath = "XValue";
        ///     series.YBindingPath = "YValue";
        ///     chart.Series.Add(series);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public LabelDisplayMode DisplayMode
        {
            get { return (LabelDisplayMode)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value to customize the appearance of the trackball line.
        /// </summary>
        /// <example>
        /// # [Xaml](#tab/tabid-3)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///           <!--omitted for brevity-->
        ///
        ///           <chart:SfCartesianChart.TrackballBehavior>
        ///               <chart:ChartTrackballBehavior>
        ///                   <chart:ChartTrackballBehavior.LineStyle>
        ///                       <chart:ChartLineStyle Stroke = "Red" StrokeWidth="2"/>
        ///                   </chart:ChartTrackballBehavior.LineStyle>
        ///        </chart:ChartTrackballBehavior>
        ///           </chart:SfCartesianChart.TrackballBehavior>
        ///
        ///           <chart:LineSeries ItemsSource="{Binding Data}"
        ///                             XBindingPath="XValue"
        ///                             YBindingPath="YValue"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-4)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///    // omitted for brevity 
        ///    var lineStyle = new ChartLineStyle()
        ///    {
        ///        Stroke = new SolidColorBrush(Colors.Red), StrokeWidth = 2,
        ///    };
        ///    chart.TrackballBehavior = new ChartTrackballBehavior()
        ///    {
        ///        LineStyle = lineStyle,
        ///    };
        ///
        ///     LineSeries series = new LineSeries();
        ///     series.ItemsSource = viewModel.Data;
        ///     series.XBindingPath = "XValue";
        ///     series.YBindingPath = "YValue";
        ///     chart.Series.Add(series);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public ChartLineStyle LineStyle
        {
            get { return (ChartLineStyle)GetValue(LineStyleProperty); }
            set { SetValue(LineStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value to customize the appearance of trackball labels. 
        /// </summary>
        /// <example>
        /// # [Xaml](#tab/tabid-3)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///           <!--omitted for brevity-->
        ///
        ///           <chart:SfCartesianChart.TrackballBehavior>
        ///               <chart:ChartTrackballBehavior>
        ///                   <chart:ChartTrackballBehavior.LabelStyle>
        ///                       <chart:ChartLabelStyle TextColor="Red" FontSize="14"/>
        ///                   </chart:ChartTrackballBehavior.LabelStyle>
        ///        </chart:ChartTrackballBehavior>
        ///           </chart:SfCartesianChart.TrackballBehavior>
        ///
        ///           <chart:LineSeries ItemsSource="{Binding Data}"
        ///                             XBindingPath="XValue"
        ///                             YBindingPath="YValue"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-4)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///    // omitted for brevity 
        ///    var labelStyle = new ChartLabelStyle()
        ///    {
        ///        TextColor = Colors.Red, FontSize = 12,
        ///    };
        ///    chart.TrackballBehavior = new ChartTrackballBehavior()
        ///    {
        ///        LabelStyle = labelStyle,
        ///    };
        ///
        ///     LineSeries series = new LineSeries();
        ///     series.ItemsSource = viewModel.Data;
        ///     series.XBindingPath = "XValue";
        ///     series.YBindingPath = "YValue";
        ///     chart.Series.Add(series);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public ChartLabelStyle LabelStyle
        {
            get { return (ChartLabelStyle)GetValue(LabelStyleProperty); }
            set { SetValue(LabelStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the option for customize the trackball markers.
        /// </summary>
        /// <example>
        /// # [Xaml](#tab/tabid-3)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///           <!--omitted for brevity-->
        ///
        ///           <chart:SfCartesianChart.TrackballBehavior>
        ///               <chart:ChartTrackballBehavior>
        ///                   <chart:ChartTrackballBehavior.MarkerSettings>
        ///                       <chart:ChartMarkerSettings Height = "10" Width="10" Fill="Red"/>
        ///                   </chart:ChartTrackballBehavior.MarkerSettings>
        ///        </chart:ChartTrackballBehavior>
        ///           </chart:SfCartesianChart.TrackballBehavior>
        ///
        ///           <chart:LineSeries ItemsSource="{Binding Data}"
        ///                             XBindingPath="XValue"
        ///                             YBindingPath="YValue"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-4)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///    // omitted for brevity 
        ///    var markerSettings = new ChartMarkerSettings()
        ///    {
        ///        Height = 10, Width = 10,
        ///        Fill = new SolidColorBrush(Colors.Red),
        ///    };
        ///    chart.TrackballBehavior = new ChartTrackballBehavior()
        ///    {
        ///        MarkerSettings = markerSettings,
        ///    };
        ///
        ///     LineSeries series = new LineSeries();
        ///     series.ItemsSource = viewModel.Data;
        ///     series.XBindingPath = "XValue";
        ///     series.YBindingPath = "YValue";
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

        /// <summary>
        /// Gets or sets the value that indicates whether to show markers for the trackball.
        /// </summary>
        /// <example>
        /// # [Xaml](#tab/tabid-3)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///           <!--omitted for brevity-->
        ///
        ///           <chart:SfCartesianChart.TrackballBehavior>
        ///               <chart:ChartTrackballBehavior ShowMarkers="False"/>
        ///           </chart:SfCartesianChart.TrackballBehavior>
        ///
        ///           <chart:LineSeries ItemsSource="{Binding Data}"
        ///                             XBindingPath="XValue"
        ///                             YBindingPath="YValue"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-4)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///    // omitted for brevity 
        ///    chart.TrackballBehavior = new ChartTrackballBehavior()
        ///    {
        ///        ShowMarkers = false,
        ///    };
        ///
        ///     LineSeries series = new LineSeries();
        ///     series.ItemsSource = viewModel.Data;
        ///     series.XBindingPath = "XValue";
        ///     series.YBindingPath = "YValue";
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
        /// Gets or sets the value that indicates whether to show the trackball line.
        /// </summary>
        /// <example>
        /// # [Xaml](#tab/tabid-3)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///           <!--omitted for brevity-->
        ///
        ///           <chart:SfCartesianChart.TrackballBehavior>
        ///               <chart:ChartTrackballBehavior ShowLine="False"/>
        ///           </chart:SfCartesianChart.TrackballBehavior>
        ///
        ///           <chart:LineSeries ItemsSource="{Binding Data}"
        ///                             XBindingPath="XValue"
        ///                             YBindingPath="YValue"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-4)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     ViewModel viewModel = new ViewModel();
        ///
        ///    // omitted for brevity 
        ///    chart.TrackballBehavior = new ChartTrackballBehavior()
        ///    {
        ///        ShowLine = false,
        ///    };
        ///
        ///     LineSeries series = new LineSeries();
        ///     series.ItemsSource = viewModel.Data;
        ///     series.XBindingPath = "XValue";
        ///     series.YBindingPath = "YValue";
        ///     chart.Series.Add(series);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public bool ShowLine
        {
            get { return (bool)GetValue(ShowLineProperty); }
            set { SetValue(ShowLineProperty, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartTrackballBehavior"/> class.
        /// </summary>
        public ChartTrackballBehavior()
        {
            ThemeElement.InitializeThemeResources(this, "SfCartesianChartTheme");
            this.SetDynamicResource(TrackballBackgroundProperty, "SfCartesianChartTrackballLabelBackground");
            this.SetDynamicResource(TrackballFontSizeProperty, "SfCartesianChartTrackballLabelTextFontSize");
            this.SetDynamicResource(LineStrokeProperty, "SfCartesianChartTrackballLineStroke");
            ShowMarkers = true;
            LineStyle = new ChartLineStyle() { Stroke = LineStroke, StrokeWidth = LineStrokeWidth };
            LabelStyle = actualLabelStyle = DefaultLabelStyle();
            MarkerSettings = new ChartMarkerSettings() {Fill = SolidColorBrush.Black };
            axisPointInfos = new List<TrackballAxisInfo>();
            PointInfos = new List<TrackballPointInfo>();
        }

        #endregion

        #region Interface Implementation

        void IThemeElement.OnControlThemeChanged(string oldTheme, string newTheme)
        {
        }

        void IThemeElement.OnCommonThemeChanged(string oldTheme, string newTheme)
        {
        }

        #endregion

        #region Internal overrided Methods
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected internal override void OnTouchMove(ChartBase chart, float pointX, float pointY)
        {
            if (DeviceType == PointerDeviceType.Mouse || (DeviceType == PointerDeviceType.Touch && longPressActive))
            {
                Show(pointX, pointY);
            }

        }

        internal override void OnLongPressActivation(IChart chart, float pointX, float pointY, GestureStatus status)
        {
           if (chart is SfCartesianChart cartesianChart)
            {
                if ((cartesianChart.ZoomPanBehavior == null || cartesianChart.ZoomPanBehavior is ChartZoomPanBehavior behavior && !behavior.IsSelectionZoomingActivated) && (status != GestureStatus.Completed && status != GestureStatus.Canceled))
                {
                    longPressActive = true;
                    Show(pointX, pointY);
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected internal override void OnTouchUp(ChartBase chart, float pointX, float pointY)
        {
            Hide();
        }

        internal override void OnTouchExit()
        {
            Hide();
        }

        internal override void OnTouchCancel(float x, float y)
        {
            Hide();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected internal override void OnTouchDown(ChartBase chart, float pointX, float pointY)
        {
            Hide();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Activate the trackball at the nearest point to the specified location.
        /// </summary>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        public void Show(float pointX, float pointY)
        {
            if (CartesianChart == null || !(CartesianChart is IChart chart) || !(CartesianChart.ChartArea is CartesianChartArea area)) return;

            SeriesBounds = area.ActualSeriesClipRect;
            if (!isScheduled)
            {
                if (chart.ActualSeriesClipRect.Contains(pointX, pointY))
                {
                    isScheduled = true;
                    GenerateTrackball(pointX, pointY);
                    isScheduled = false;
                }
                else
                {
                    Hide();
                }

                isScheduled = false;
            }
        }

        /// <summary>
        /// Hides the trackball that is visible in the chart.
        /// </summary>
        public void Hide()
        {
            PointInfos.Clear();
            longPressActive = false;
            Invalidate();
        }

        #endregion

        #region Private Methods

        private void GenerateTrackball(float pointX, float pointY)
        {
            if (CartesianChart != null && (CartesianChart is IChart chart))
            {
                PointInfos.Clear();
                isAnySideBySideSeries = false;
                isAnyContinuesSeries = false;
                var xAxes = CartesianChart.ChartArea?.XAxes;

                if (xAxes == null || xAxes.Count == 0) return;

                foreach (ChartAxis chartAxis in xAxes)
                {
                    foreach (CartesianSeries series in chartAxis.RegisteredSeries)
                    {
                        if (series.IsVisible && series.ShowTrackballLabel && series.PointsCount > 0)
                        {
                            List<object> nearestDataPoints = series.FindNearestChartPoints(pointX, pointY);
                            if (nearestDataPoints.Count > 0)
                            {
                                if (series.IsSideBySide)
                                {
                                    isAnySideBySideSeries = true;
                                }
                                else
                                {
                                    isAnyContinuesSeries = true;
                                }

                                series.GenerateTrackballPointInfo(nearestDataPoints, PointInfos, ref isAnySideBySideSeries);
                            }
                        }
                    }
                }

                UpdateTrackballPointinfos(pointX - (float)chart.ActualSeriesClipRect.Left, pointY - (float)chart.ActualSeriesClipRect.Top);

                Invalidate();
            }
        }

        /// <summary>
        /// Invokes when the trackball is moved from one data point to another. This helps to customize the trackball label and marker based on the condition.
        /// </summary>
        /// <param name="pointInfos"></param>
        protected internal virtual void LabelsGenerated(List<TrackballPointInfo> pointInfos)
        {

        }

        private void UpdateTrackballPointinfos(float pointX, float pointY)
        {
            if (CartesianChart == null || PointInfos.Count == 0) return;

            float leastX = FindLeastXValue(pointX, pointY);

            if (CartesianChart.IsTransposed)
            {
                linePoint1 = new Point(SeriesBounds.Left, leastX);
                linePoint2 = new Point(SeriesBounds.Right, leastX);
            }
            else
            {
                linePoint1 = new Point(leastX - SeriesBounds.Left, SeriesBounds.Top);
                linePoint2 = new Point(leastX - SeriesBounds.Left, SeriesBounds.Top + SeriesBounds.Height);
            }

            if (PointInfos.Count == 0)
            {
                return;
            }

            //TODO: Validate for group label.
            //if (DisplayMode == LabelDisplayMode.NearestPoint || (isAnySideBySideSeries && DisplayMode != LabelDisplayMode.GroupAllPoints))
            if (DisplayMode == LabelDisplayMode.NearestPoint || (isAnySideBySideSeries))
            {
                ValidateTrackballBehaviorForAllSeries(leastX, pointX, pointY);
            }

            foreach(var items in PointInfos)
            {
                items.LabelStyle = actualLabelStyle;
                items.MarkerSettings = ((IMarkerDependent)this).MarkerSettings;
            }

            LabelsGenerated(PointInfos);

            CartesianChart.RaiseTrackballCreatedEvent(PointInfos);

            foreach (var item in PointInfos)
            {
                CustomizeAppearance(item);
            }

            //TODO: Enable for group all points.
            //if (DisplayMode == LabelDisplayMode.GroupAllPoints)
            //{
            //    if (PointInfos.Count > 0)
            //    {
            //        string groupLabel = PointInfos[0].Label;
            //        for (int i = 1; i < PointInfos.Count; i++)
            //        {
            //            groupLabel = groupLabel + "\n" + PointInfos[i].Label;
            //        }

            //        UpdateGroupedTooltip(groupLabel);
            //    }
            //}
            //else 
            
            if (DisplayMode == LabelDisplayMode.FloatAllPoints)
            {
                if (PointInfos.Count == 0) return;
                SmartLabelAlignment(CartesianChart);
            }

            GenerateAxisTrackballInfos(leastX);
        }

        private void GenerateAxisTrackballInfos(float leastX)
        {
            axisPointInfos.Clear();

            if (CartesianChart == null) return;

            ChartAxis? previousAxis = null;

            foreach (TrackballPointInfo pointInfo in PointInfos)
            {
                ChartAxis? axis = pointInfo.Series.ActualXAxis;

                if (axis == null || !axis.ShowTrackballLabel || !axis.IsVisible || previousAxis == axis)
                {
                    continue;
                }
                previousAxis = axis;
                float x = float.NaN;
                float y = float.NaN;
                double xValue = pointInfo.XValue;
                double offset = 0;

                if (axis.AxisLineStyle != null)
                    offset = axis.AxisLineStyle.StrokeWidth / 2;

                bool isOpposed = axis.IsOpposed();

                Rect actualArrangeRect = axis.ArrangeRect;

                if (axis.IsVertical)
                {
                    y = (float)leastX - SeriesBounds.Top;
                    x = (float)(actualArrangeRect.X + offset);
                }
                else
                {
                    x = (float)(leastX - actualArrangeRect.X);
                    y = (float)(isOpposed ? actualArrangeRect.Y - offset : actualArrangeRect.Y + offset);
                }

                string labelFormat = "##.##";

                if (axis.TrackballLabelStyle != null)
                {
                    labelFormat = axis.TrackballLabelStyle.LabelFormat;
                }
                else
                {
                    //Todo: need to check all axis based on label format.
                    if (axis is DateTimeAxis)
                    {
                        //Default Label format assign it.
                        labelFormat = "MM-dd-yyyy";
                    }
                }
                
                TrackballAxisInfo axisPointInfo = new TrackballAxisInfo(axis, new TooltipHelper(Drawable) { Duration = int.MaxValue }, GetAxisLabel(axis, xValue, labelFormat), x, y);

                axisPointInfo.Helper.FontManager = CartesianChart.GetFontManager();

                var labelStyle = axis.TrackballLabelStyle ?? DefaultAxisLabelStyle(axis);

                MapChartLabelStyle(axisPointInfo.Helper , labelStyle);
                if (!CartesianChart.IsTransposed)
                    axisPointInfo.Helper.Position = isOpposed ? TooltipPosition.Top : TooltipPosition.Bottom;
                else
                    axisPointInfo.Helper.Position = isOpposed ? TooltipPosition.Right : TooltipPosition.Left;

                axisPointInfo.Helper.Show(actualArrangeRect, new Rect(x - 1, y - 1, 2, 2), false);
                axisPointInfos.Add(axisPointInfo);
            }
        }

        private void SmartLabelAlignment(SfCartesianChart CartesianChart)
        {
            if (PointInfos.Count > 0)
            {
                //TODO: Enable for group all points.
                //if (DisplayMode != LabelDisplayMode.GroupAllPoints)
                //{
                    if (CartesianChart.IsTransposed)
                    {
                        PointInfos = PointInfos.OrderBy(item => item.X).ToList();
                    }
                    else
                    {
                        PointInfos = PointInfos.OrderBy(item => item.Y).ToList();
                    }
                //}
            }

            List<TrackballPointInfo> tempTrackballInfo = new List<TrackballPointInfo>(PointInfos);
            intersectedGroups = new List<List<TrackballPointInfo>>();
            List<TrackballPointInfo> tempIntersectedLabels = new List<TrackballPointInfo>();
            tempIntersectedLabels.Add(tempTrackballInfo[0]);
            for (int i = 0; i < tempTrackballInfo.Count - 1; i++)
            {
                Rect rect1 = tempTrackballInfo[i].TooltipHelper.TooltipRect;
                Rect rect2 = tempTrackballInfo[i + 1].TooltipHelper.TooltipRect;
                if (rect1.IntersectsWith(rect2))
                {
                    tempIntersectedLabels.Add(tempTrackballInfo[i + 1]);
                }
                else
                {
                    intersectedGroups.Add(new List<TrackballPointInfo>(tempIntersectedLabels));
                    tempIntersectedLabels.Clear();
                    tempIntersectedLabels.Add(tempTrackballInfo[i + 1]);
                }
            }

            if (tempIntersectedLabels.Count > 0)
            {
                intersectedGroups.Add(new List<TrackballPointInfo>(tempIntersectedLabels));
                tempIntersectedLabels.Clear();
            }

            UpdatePositionforIntersectedLabels();
        }

        private void UpdatePositionforIntersectedLabels()
        {
            if (CartesianChart is IChart chart && intersectedGroups != null)
            {
                var prevRect = Rect.Zero;
                foreach (var intersectedGroupLabels in intersectedGroups)
                {
                    if (!CartesianChart.IsTransposed)
                    {
                        HandleMultipleGroupLabels(intersectedGroupLabels, chart, ref prevRect);
                    }
                    else
                    {
                        HandleTransposedMultipleGroupLabels(intersectedGroupLabels, chart, ref prevRect);
                    }
                }
            }
        }

        // Method to handle multiple group labels
        private void HandleMultipleGroupLabels(List<TrackballPointInfo> intersectedGroupLabels, IChart chart, ref Rect prevRect)
        {
            var info = intersectedGroupLabels[0];
            var infoTooltipRect = info.TooltipHelper.TooltipRect;
            double tempYValue = info.Y;
            double intersectedGroupLabelsCount = intersectedGroupLabels.Count;
            double halfHeight = ((infoTooltipRect.Height * intersectedGroupLabelsCount)
                                + trackLabelSpacing * (intersectedGroupLabelsCount - 1)) / 2 - infoTooltipRect.Height / 2;
            if (info.Y - halfHeight <= 0)
            {
                tempYValue = tempYValue + halfHeight - info.Y + trackLabelSpacing;
            }
            
            for (int i = 0; i < intersectedGroupLabelsCount; i++)
            {
                var rect = intersectedGroupLabels[i].TooltipHelper.TooltipRect;
                if(intersectedGroupLabelsCount > 1)
                {
                    rect.Y = tempYValue - halfHeight + (i * (rect.Height + trackLabelSpacing));
                }
                if (rect.IntersectsWith(prevRect) || (rect.Y < prevRect.Y))
                {
                    rect.Y = prevRect.Y + prevRect.Height + trackLabelSpacing;
                }

                if (rect.Y + rect.Height > chart.ActualSeriesClipRect.Height)
                {
                    HandleOverflowingLabel(intersectedGroupLabels);
                }
                else
                {
                    intersectedGroupLabels[i].TooltipHelper.TooltipRect = rect;
                    prevRect = rect;
                }
            }
        }

        private void HandleTransposedMultipleGroupLabels(List<TrackballPointInfo> intersectedGroupLabels, IChart chart, ref Rect transposedPrevRect)
        {
            var info = intersectedGroupLabels[0];
            var infoTooltipRect = info.TooltipHelper.TooltipRect;
            double tempXValue = info.X;
            double intersectedGroupLabelsCount = intersectedGroupLabels.Count;
            double halfWidth = ((infoTooltipRect.Width * intersectedGroupLabelsCount) 
                               + trackLabelSpacing * (intersectedGroupLabelsCount - 1)) / 2 - infoTooltipRect.Width / 2;
            if (info.X - halfWidth <= 0)
            {
                tempXValue = tempXValue + halfWidth - info.X + trackLabelSpacing;
            }
            for (int i = 0; i < intersectedGroupLabelsCount; i++)
            {
                var rect = intersectedGroupLabels[i].TooltipHelper.TooltipRect;
                if (intersectedGroupLabelsCount > 1)
                {
                    rect.X = tempXValue - halfWidth + (i * (rect.Width + trackLabelSpacing));
                }
                if (rect.IntersectsWith(transposedPrevRect))
                {
                    rect.X = transposedPrevRect.X + transposedPrevRect.Width + trackLabelSpacing;
                }

                if (rect.X + rect.Width > chart.ActualSeriesClipRect.Width)
                {
                    HandleOverflowingLabel(intersectedGroupLabels);
                }
                else
                {
                    intersectedGroupLabels[i].TooltipHelper.TooltipRect = rect;
                    transposedPrevRect = rect;
                }
            }
        }
        
        // Method to handle overflowing label
        private void HandleOverflowingLabel(List<TrackballPointInfo> intersectedGroupLabels)
        {
            if (CartesianChart is IChart chart && intersectedGroups != null)
            {
                var chartActualClipRect = chart.ActualSeriesClipRect;
                var bottomPrevRect = Rect.Zero;
                var rect = Rect.Zero;
                bool isAlignmentStarted = false;

                if (intersectedGroupLabels.Count > 1)
                {
                    for (int j = intersectedGroups.Count - 1; j >= 0; j--)
                    {
                        var newIntersectedGroups = intersectedGroups[j];
                        for (int k = newIntersectedGroups.Count - 1; k >= 0; k--)
                        {
                            rect = newIntersectedGroups[k].TooltipHelper.TooltipRect;

                            if (!CartesianChart.IsTransposed)
                            {
                                var rectArranged = new Rect(bottomPrevRect.X, bottomPrevRect.Y, chartActualClipRect.Width - bottomPrevRect.X, chartActualClipRect.Height - bottomPrevRect.Y);
                                if (rect.IntersectsWith(bottomPrevRect) || (rectArranged.Contains(rect.X, rect.Y) && isAlignmentStarted))
                                {
                                    rect.Y = bottomPrevRect.Y - trackLabelSpacing - rect.Height;
                                }
                                else if (rect.Y + rect.Height >= chartActualClipRect.Height && !isAlignmentStarted)
                                {
                                    rect.Y = chartActualClipRect.Height - rect.Height;
                                    isAlignmentStarted = true;
                                }
                            }
                            else
                            {
                                var rectArranged = new Rect(bottomPrevRect.X, 0, chartActualClipRect.Width - bottomPrevRect.X, chartActualClipRect.Height);
                               
                                if (rect.IntersectsWith(bottomPrevRect) || (rectArranged.Contains(rect.X, rect.Y) && isAlignmentStarted))
                                {
                                    rect.X = bottomPrevRect.X - trackLabelSpacing - rect.Width;
                                }
                                else if (rect.X + rect.Width >= chartActualClipRect.Width && !isAlignmentStarted)
                                {
                                    rect.X = chartActualClipRect.Width - rect.Width;
                                    isAlignmentStarted = true;
                                }
                            }
                           
                            newIntersectedGroups[k].TooltipHelper.TooltipRect = rect;
                            bottomPrevRect = rect;
                        }
                    }
                }
            }
        }

        private void MapChartLabelStyle(TooltipHelper helper, ChartLabelStyle chartLabelStyle)
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

        private void UpdateGroupedTooltip(string groupLabel)
        {
            if (CartesianChart == null) return;

            groupedTooltip = new TooltipHelper(Drawable) { FontManager = CartesianChart.GetFontManager() };
            groupedTooltip.Text = groupLabel;
            groupedTooltip.Duration = int.MaxValue;
            MapChartLabelStyle(groupedTooltip, actualLabelStyle);

            Size size = groupLabel.Measure(groupedTooltip);
            var padding = groupedTooltip.Padding;
            Size contentSize = new Size(size.Width + padding.Left + padding.Right, size.Height + padding.Top + padding.Bottom);

            if (CartesianChart.IsTransposed)
            {
                groupedTooltip.PriorityPosition = TooltipPosition.Right;
                groupedTooltip.Show(SeriesBounds, new Rect(linePoint2.X - contentSize.Width - contentSize.Width / 2, linePoint2.Y, 1, 1), false);
            }
            else
            {
                groupedTooltip.Position = TooltipPosition.Top;
                groupedTooltip.Show(SeriesBounds, new Rect(linePoint1.X, contentSize.Height + contentSize.Height / 7, 1, 1), false);
            }
        }

        private float FindLeastXValue(float pointX, float pointY)
        {
            if (CartesianChart == null) return float.NaN;

            var xAxes = CartesianChart.ChartArea?.XAxes;

            if (xAxes == null) return float.NaN;

            bool isTransposed = CartesianChart.IsTransposed;


            double startXValue = isTransposed
                                 ? SeriesBounds.Top
                                 : SeriesBounds.Left;

            double nearPointX = startXValue;

            double touchXValue = isTransposed ? pointY : pointX;
            double delta = 0;

            List<TrackballPointInfo> leastXPointsInfo = new List<TrackballPointInfo>();

            foreach (TrackballPointInfo pointInfo in PointInfos)
            {
                var axis = pointInfo.Series.ActualXAxis;
                if (axis == null) continue;

                currX = (float)(axis.ValueToPoint(pointInfo.XValue));

                var difference = Math.Abs(touchXValue - currX) - startXValue;

                if (delta == touchXValue - currX)
                {
                    leastXPointsInfo.Add(pointInfo);
                } 
                else if (difference <= Math.Abs(touchXValue - nearPointX))
                {
                    nearPointX = currX;
                    delta = touchXValue - currX;
                    leastXPointsInfo.Clear();
                    leastXPointsInfo.Add(pointInfo);
                }

            }

            var copyList = PointInfos.ToList();

            foreach (var pointInfo in copyList)
            {
                if (!leastXPointsInfo.Contains(pointInfo))
                {
                    PointInfos.Remove(pointInfo);
                }
            }

            float leastX = float.NaN;

            if (PointInfos.Count > 0)
            {
                TrackballPointInfo pointInfo = PointInfos[0];

                var axis = pointInfo.Series.ActualXAxis;
                if (axis == null) return float.NaN;

                leastX = (float)(axis.ValueToPoint(pointInfo.XValue) + startXValue);

                leastX = (float)Math.Round(leastX, 3);
            }

            return leastX;
        }

        private void ValidateTrackballBehaviorForAllSeries(double leastX, double pointX, double pointY)
        {
            if (CartesianChart == null) return;

            List<TrackballPointInfo> tempTrackballPointInfos = new List<TrackballPointInfo>(PointInfos);
            bool isTransposed = CartesianChart.IsTransposed;

            tempTrackballPointInfos = isTransposed ? tempTrackballPointInfos.OrderBy(a => a.X).ToList() : tempTrackballPointInfos.OrderBy(a => a.Y).ToList();

            foreach (TrackballPointInfo pointInfo in tempTrackballPointInfos)
            {
                CartesianSeries series = pointInfo.Series;
                ChartAxis? axis = series.ActualXAxis;
                ChartAxis? verticalAxis = series.ActualYAxis;

                if (axis == null || verticalAxis == null) return;

                double locationX = isTransposed ? pointY + SeriesBounds.Top : pointX + SeriesBounds.Left;
                double locationY = isTransposed ? pointX + SeriesBounds.Left : pointY + SeriesBounds.Top;

                double startXValue = isTransposed ? SeriesBounds.Top : SeriesBounds.Left;

                //TODO: Validate for grouped label.
                //if (series.IsSideBySide && CartesianChart.EnableSideBySideSeriesPlacement && DisplayMode != LabelDisplayMode.GroupAllPoints)
                if (series.IsSideBySide && CartesianChart.EnableSideBySideSeriesPlacement)
                {
                    DoubleRange sbsInfo = series.SbsInfo;
                    double xVal = pointInfo.XValue;
                    bool isXaxisInversed = axis.IsInversed;
                    double xStartValue = xVal + sbsInfo.Start;
                    double xEndValue = xVal + sbsInfo.End;
                    double xEnd = axis.ValueToPoint(xEndValue) + startXValue;

                    double xStart = axis.ValueToPoint(xStartValue) + startXValue;
                    bool isStartIndex = series.SideBySideIndex == 0;
                    bool isEndIndex = series.SideBySideIndex == axis.SideBySideSeriesCount - 1;

                    if (isXaxisInversed || isTransposed)
                    {
                        if (!(isXaxisInversed && isTransposed))
                        {
                            double temp = xStart;
                            xStart = xEnd;
                            xEnd = temp;
                            bool isTemp = isEndIndex;
                            isEndIndex = isStartIndex;
                            isStartIndex = isTemp;
                        }
                    }

                    if (locationX < leastX && isStartIndex)
                    {
                        if (!(locationX < xStart) && !(locationX < xEnd && locationX >= xStart))
                        {
                            RemoveTrackballInfo(pointInfo);
                        }
                    }
                    else if (locationX > leastX && isEndIndex)
                    {
                        if (!(locationX > xEnd && locationX > xStart) && !(locationX < xEnd && locationX >= xStart))
                        {
                            RemoveTrackballInfo(pointInfo);
                        }
                    }
                    else if (!(locationX < xEnd && locationX >= xStart))
                    {
                        RemoveTrackballInfo(pointInfo);
                    }
                }

                //Todo: Here need to check StackingSeries.

                if (DisplayMode != LabelDisplayMode.FloatAllPoints &&
                    ((isAnyContinuesSeries) || (!isTransposed && !CartesianChart.EnableSideBySideSeriesPlacement) ||
                     (pointInfo.X == leastX)))
                {
                    int pointInfoIndex = tempTrackballPointInfos.IndexOf(pointInfo);

                    if (pointInfoIndex < tempTrackballPointInfos.Count - 1)
                    {
                        TrackballPointInfo nextPointInfo = tempTrackballPointInfos[(pointInfoIndex + 1)];

                        if (nextPointInfo.Y == pointInfo.Y || (pointInfo.Y > locationY && pointInfoIndex == 0))
                        {
                            //Todo: When provide RangeAreaSeries, need to uncomment these codes. 
                            //if (!(series is RangeAreaSeries))
                            //{
                            //    continue;
                            //}
                        }

                        if (!(locationY < (nextPointInfo.Y - ((nextPointInfo.Y - pointInfo.Y) / 2))))
                        {
                            RemoveTrackballInfo(pointInfo);
                        }
                        else if (pointInfoIndex != 0)
                        {
                            TrackballPointInfo previousPointInfo = tempTrackballPointInfos[pointInfoIndex - 1];

                            if (locationY < (pointInfo.Y - ((pointInfo.Y - previousPointInfo.Y) / 2)))
                            {
                                RemoveTrackballInfo(pointInfo);
                            }
                        }
                    }
                    else
                    {
                        if (pointInfoIndex != 0 && pointInfoIndex == tempTrackballPointInfos.Count - 1)
                        {
                            TrackballPointInfo previousPointInfo = tempTrackballPointInfos[pointInfoIndex - 1];

                            if (locationY < previousPointInfo.Y)
                            {
                                RemoveTrackballInfo(pointInfo);
                            }

                            if (locationY < (pointInfo.Y - ((pointInfo.Y - previousPointInfo.Y) / 2)))
                            {
                                RemoveTrackballInfo(pointInfo);
                            }
                        }
                    }
                }
            }

            //Todo: When provide RangeAreaSeries, need to uncomment these codes. 
            //iOS-1812: The below code has been implemented since the trackball nearest point for rangeArea series need to show both high and low values
            //if (PointInfos.Count > 0)
            //{
            //    List<TrackballPointInfo> trackballPoints = new List<TrackballPointInfo>();

            //    foreach (TrackballPointInfo points in PointInfos)
            //    {
            //        if (points.Series is RangeAreaSeries)
            //        {
            //            foreach (TrackballPointInfo rangePoint in tempTrackballPointInfos)
            //            {
            //                if (rangePoint.Series == points.Series && rangePoint != points)
            //                {
            //                    trackballPoints.Add(rangePoint);
            //                }
            //            }
            //        }
            //    }

            //    foreach (var values in trackballPoints)
            //    {
            //        PointInfos.Add(values);
            //    }
            //}
        }

        void RemoveTrackballInfo(TrackballPointInfo pointInfo)
        {
            if (PointInfos.Contains(pointInfo))
            {
                PointInfos.Remove(pointInfo);
            }
        }

        private void CustomizeAppearance(TrackballPointInfo pointInfo)
        {
            if (CartesianChart == null) return;

            if (pointInfo != null)
            {
                pointInfo.SetTargetRect(this);

                //TODO: Validate not for grouped all points.
                //if (DisplayMode != LabelDisplayMode.GroupAllPoints)
                if(pointInfo.X >= 0 && pointInfo.Y >= 0)
                {
                    MapChartLabelStyle(pointInfo.TooltipHelper, pointInfo.LabelStyle ?? actualLabelStyle);
                    pointInfo.ShowTrackballLabel(CartesianChart, SeriesBounds);
                }
            }
        }

        private  ChartLabelStyle DefaultLabelStyle()
        {
            return new ChartLabelStyle()
            {
                FontSize = TrackballFontSize,
                Background = TrackballBackground,
                Margin = 5f, 
                LabelFormat = "#.##",
            }; 
        }

        private ChartLabelStyle DefaultAxisLabelStyle(ChartAxis axis)
        {
            return new ChartLabelStyle()
            {
                 FontSize = axis.TrackballAxisFontSize,
                 Background = axis.TrackballAxisBackground,
                 Margin = new Thickness(7, 4),
            };
        }

        // To Call Draw Method
        private void Invalidate()
        {
            if (CartesianChart != null)
            {
                CartesianChart.TrackballView.InvalidateDrawable();
            }
        }

        private static void DrawMarker(ICanvas canvas, Rect trackRect, ChartMarkerSettings chartMarkerSettings)
        {
            var settings = chartMarkerSettings;
            var fill = settings.Fill;
            var type = settings.Type;

            if (settings.HasBorder)
            {
                canvas.StrokeSize = (float)settings.StrokeWidth;
                canvas.StrokeColor = settings.Stroke.ToColor();
            }

            canvas.SetFillPaint(fill == default(Brush) ? SolidColorBrush.Transparent : fill, trackRect);
            canvas.DrawShape(trackRect, type, settings.HasBorder, false);
        }

        internal void DrawElements(ICanvas canvas, Rect dirtyRect)
        {
            if (CartesianChart == null) return;
            var rect = dirtyRect.SubtractThickness(CartesianChart.ChartArea.PlotAreaMargin);
            
            if (ShowLine)
            {
                DrawTrackballLine(canvas);
            }

            canvas.Translate((float)rect.Left, (float)rect.Top);
            DrawTrackballLabels(canvas); //Draw only if not grouped label. 
            
            //TODO: Enable for grouped all points. 
            //else
            //{
            //    if (ShowMarkers)
            //    {
            //        foreach (var info in PointInfos)
            //        {
            //            DrawMarker(canvas, info.TargetRect, info.MarkerSettings ?? ((IMarkerDependent)this).MarkerSettings);
            //        }
            //    }

            //    groupedTooltip?.Draw(canvas);
            //}

            foreach (var item in axisPointInfos)
            {
                item.Helper.Draw(canvas);
            }
        }

        private static string GetAxisLabel(ChartAxis axis, double xValue, string labelFormat)
        {
            string label = string.Empty;

            if (axis is CategoryAxis categoryAxis)
            {
                var currSeries = categoryAxis.GetActualSeries();
                if (currSeries != null)
                {
                    label = categoryAxis.GetLabelContent(currSeries, (int)Math.Round(xValue), labelFormat);

                }
            }
            else if (axis is NumericalAxis)
            {
                label = ((int)Math.Round(xValue)).ToString(labelFormat);
            }
            else if (axis is LogarithmicAxis)
            {
                label = ChartAxis.GetActualLabelContent(xValue, labelFormat).ToString();
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

                label = ChartAxis.GetFormattedAxisLabel(format, xValue);
            }
            else
            {
                label = ChartAxis.GetActualLabelContent(xValue, labelFormat);
            }

            return label;
        }

        private void DrawTrackballLabels(ICanvas canvas)
        {
            foreach (var info in PointInfos)
            {
                if (info.Y <= SeriesBounds.Bottom && info.X >= 0 && info.Y >= 0 && info.X <= SeriesBounds.Width)
                {
                    if (ShowMarkers)
                    {
                        DrawMarker(canvas, info.TargetRect, info.MarkerSettings ?? ((IMarkerDependent)this).MarkerSettings);
                    }
                }

                info.TooltipHelper.Draw(canvas);
            }
        }

        private void DrawTrackballLine(ICanvas canvas)
        {
            var lineStyle = LineStyle;
            if (lineStyle != null && CartesianChart != null)
            {
                if (lineStyle.StrokeDashArray != null)
                {
                    canvas.StrokeDashPattern = lineStyle.StrokeDashArray.ToFloatArray();
                }

                canvas.StrokeColor = lineStyle.Stroke.ToColor();
                canvas.StrokeSize = (float)lineStyle.StrokeWidth;

                if (!CartesianChart.IsTransposed)
                {
                    canvas.DrawLine((float)linePoint1.X + SeriesBounds.Left, (float)SeriesBounds.Top, (float)linePoint2.X + SeriesBounds.Left, (float)SeriesBounds.Bottom);
                }
                else
                {
                    canvas.DrawLine((float)SeriesBounds.Left,(float)linePoint1.Y, (float)SeriesBounds.Right, (float)linePoint2.Y );
                }
            }
        }

        #region Property Changed Methods

        private static void OnLabelStyleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ChartTrackballBehavior behavior)
            {
                if (newValue != null)
                {
                    behavior.actualLabelStyle = (ChartLabelStyle)newValue;
                }

                if (newValue != null)
                {
                    if (newValue is ChartLabelStyle style)
                    {
                        behavior.actualLabelStyle = (ChartLabelStyle)newValue;
                        SetInheritedBindingContext(style, behavior.BindingContext);
                        behavior.InitializeTrackballDynamicResource();
                    }
                }

                ChartBase.SetParent((Element?)oldValue, (Element?)newValue, behavior.Parent);
            }
        }

        private void InitializeTrackballDynamicResource()
        {
            actualLabelStyle.SetDynamicResource(ChartLabelStyle.StrokeProperty, "SfCartesianChartTrackballLabelStroke");
            actualLabelStyle.SetDynamicResource(ChartLabelStyle.TextColorProperty, "SfCartesianChartTrackballLabelTextColor");
            actualLabelStyle.SetDynamicResource(ChartLabelStyle.CornerRadiusProperty, "SfCartesianChartTrackballLabelCornerRadius");
        }

        private static void OnLineStyleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ChartTrackballBehavior behavior)
            {
                if (newValue != null)
                {
                    if (newValue is ChartLineStyle lineStyle)
                    {
                        SetInheritedBindingContext(lineStyle, behavior.BindingContext); 
                    }
                }

                ChartBase.SetParent((Element?)oldValue, (Element?)newValue, behavior.Parent);
            }
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        void IMarkerDependent.InvalidateDrawable()
        {

        }

        void IMarkerDependent.DrawMarker(ICanvas canvas, int index, ShapeType type, Rect rect)
        {

        }

        void Drawable()
        {

        }

        #endregion
    }

    /// <summary>
    /// Stores the information to generate trackball axis label
    /// </summary>
    internal class TrackballAxisInfo
    {
        public TrackballAxisInfo(ChartAxis axis, TooltipHelper helper, string label, float x, float y)
        {
            Axis = axis;
            X = x;
            Y = y;
            Helper = helper;
            helper.Text = label;
        }

        public ChartAxis Axis { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public TooltipHelper Helper { get; set; }
    }
}
