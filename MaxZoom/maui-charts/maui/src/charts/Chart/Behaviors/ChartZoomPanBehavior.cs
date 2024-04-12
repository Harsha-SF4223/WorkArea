using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core.Internals;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// ZoomPanBehavior enables zooming and panning operations over a cartesian chart.
    /// </summary>
    /// <remarks>
    /// To enable the zooming and panning in the chart, create an instance of <see cref="ChartZoomPanBehavior"/> and set it to the <c>ZoomPanBehavior</c> property of <see cref="SfCartesianChart"/>.
    /// </remarks>
    /// <example>
    /// # [Xaml](#tab/tabid-1)
    /// <code><![CDATA[
    ///     <chart:SfCartesianChart>
    ///
    ///           <chart:SfCartesianChart.ZoomPanBehavior>
    ///               <chart:ChartZoomPanBehavior />
    ///           </chart:SfCartesianChart.ZoomPanBehavior>
    ///           
    ///     </chart:SfCartesianChart>
    /// ]]></code>
    /// # [C#](#tab/tabid-2)
    /// <code><![CDATA[
    ///     SfCartesianChart chart = new SfCartesianChart();
    ///     
    ///     chart.ZoomPanBehavior = new ChartZoomPanBehavior();
    ///     
    /// ]]></code>
    /// ***
    /// </example>
    public partial class ChartZoomPanBehavior : ChartBehavior
    {
        #region Fields
        private GestureStatus pinchStatus = GestureStatus.Canceled;
        private bool isPinchZoomingActivated = false;
        private bool isSelectionActivated = false;
        private float cumulativeZoomLevel = 1f;
        private float startX;
        private float startY;
        private float endX;
        private float endY;
        private Rect currentSelectionRect;

#if MACCATALYST || IOS
        private bool isSingleTapActivated = false;
        private bool isDoubleTapped = false;
        private bool isScrollChanged = false;
        private bool isSelected = false;
        private Rect selectedRegionRect;
        private DateTime startTime;
        private int downEntered = 0;
        private DateTime endTime;
#else
        private bool isSelectionZoomingEnded = false;
#endif

        #endregion

        #region Bindable Properties

        /// <summary>
        /// Identifies the <see cref="EnablePinchZooming"/> bindable property.
        /// </summary>        
        public static readonly BindableProperty EnablePinchZoomingProperty = BindableProperty.Create(nameof(EnablePinchZooming), typeof(bool), typeof(ChartZoomPanBehavior), true, BindingMode.Default, null, OnEnableZoomingPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="EnablePanning"/> bindable property.
        /// </summary>        
        public static readonly BindableProperty EnablePanningProperty = BindableProperty.Create(nameof(EnablePanning), typeof(bool), typeof(ChartZoomPanBehavior), true, BindingMode.Default, null, OnEnablePanningPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="EnableDoubleTap"/> bindable property.
        /// </summary>        
        public static readonly BindableProperty EnableDoubleTapProperty = BindableProperty.Create(nameof(EnableDoubleTap), typeof(bool), typeof(ChartZoomPanBehavior), true, BindingMode.Default, null, OnEnableDoubleTapPropertyChanged);

         /// <summary>
         /// Identifies the <see cref="ZoomMode"/> bindable property.
         /// </summary>        
         public static readonly BindableProperty ZoomModeProperty = BindableProperty.Create(nameof(ZoomMode), typeof(ZoomMode), typeof(ChartZoomPanBehavior), ZoomMode.XY, BindingMode.Default, null, OnZoomModePropertyChanged);

        /// <summary>
        /// Identifies the <see cref="EnableSelectionZooming"/> bindable property.
        /// </summary> 
        public static readonly BindableProperty EnableSelectionZoomingProperty = BindableProperty.Create(nameof(EnableSelectionZooming), typeof(bool), typeof(ChartZoomPanBehavior), false, BindingMode.Default, null);

        /// <summary>
        /// Identifies the <see cref="SelectionRectStroke"/> bindable property.
        /// </summary> 
        public static readonly BindableProperty SelectionRectStrokeProperty = BindableProperty.Create(nameof(SelectionRectStroke), typeof(Brush), typeof(ChartZoomPanBehavior), null, BindingMode.Default, null);

        /// <summary>
        /// Identifies the <see cref="SelectionRectStrokeWidth"/> bindable property.
        /// </summary> 
        public static readonly BindableProperty SelectionRectStrokeWidthProperty = BindableProperty.Create(nameof(SelectionRectStrokeWidth), typeof(double), typeof(ChartZoomPanBehavior), 1d, BindingMode.Default, null);

        /// <summary>
        /// Identifies the <see cref="SelectionRectFill"/> bindable property.
        /// </summary> 
        public static readonly BindableProperty SelectionRectFillProperty = BindableProperty.Create(nameof(SelectionRectFill), typeof(Brush), typeof(ChartZoomPanBehavior), null, BindingMode.Default, null);

        /// <summary>
        /// Identifies the <see cref="SelectionRectStrokeDashArray"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SelectionRectStrokeDashArrayProperty = BindableProperty.Create(nameof(SelectionRectStrokeDashArray), typeof(DoubleCollection), typeof(ChartZoomPanBehavior), new DoubleCollection { 5, 5 }, BindingMode.Default, null);

        /// <summary>
        /// Identifies the <see cref="EnableDirectionalZooming"/> bindable property.
        /// </summary>        
        public static readonly BindableProperty EnableDirectionalZoomingProperty = BindableProperty.Create(nameof(EnableDirectionalZooming), typeof(bool), typeof(ChartZoomPanBehavior), false, BindingMode.Default, null);

        /// <summary>
        /// Identifies the <see cref="MaximumZoomLevel"/> bindable property.
        /// </summary>
        public static readonly BindableProperty MaximumZoomLevelProperty = BindableProperty.Create(nameof(MaximumZoomLevel), typeof(float), typeof(ChartZoomPanBehavior), float.NaN, BindingMode.Default, null, OnMaximumZoomLevelPropertyChanged);

        /// <summary>
        /// Gets or sets a value indicating whether the finger gesture is enabled or disabled.
        /// </summary>
        /// <value>
        /// It accepts the bool values and its default value is <c>true</c>.
        /// </value>
        /// <remarks>
        /// If this property is true, zooming is performed based on the pinch gesture of the user. If this property is false, zooming is performed based on the mouse wheel of the user.
        /// </remarks>
        /// <example>
        /// # [MainPage.xaml](#tab/tabid-3)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        ///
        ///     <!--omitted for brevity-->
        ///
        ///     <chart:SfCartesianChart.ZoomPanBehavior>
        ///        <chart:ChartZoomPanBehavior EnablePinchZooming="True"/>
        ///     </chart:SfCartesianChart.ZoomPanBehavior>
        ///
        ///     <chart:LineSeries ItemsSource="{Binding Data}"
        ///                       XBindingPath="XValue"
        ///                       YBindingPath="YValue"/>
        /// 
        /// </chart:SfCartesianChart>
        ///
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-4)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// ViewModel viewModel = new ViewModel();
        /// 
        /// // omitted for brevity
        /// chart.ZoomPanBehavior = new ChartZoomPanBehavior() 
        /// { 
        ///    EnablePinchZooming = true, 
        /// };
        /// 
        /// LineSeries series = new LineSeries()
        /// {
        ///    ItemsSource = viewModel.Data,
        ///    XBindingPath = "XValue",
        ///    YBindingPath = "YValue",
        /// };
        /// chart.Series.Add(series);
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public bool EnablePinchZooming
        {
            get { return (bool)GetValue(EnablePinchZoomingProperty); }
            set { SetValue(EnablePinchZoomingProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the panning is enabled.
        /// </summary>
        /// <value>
        /// It accepts the bool values and the default value is <c>true</c>.
        /// </value>
        /// <example>
        /// # [MainPage.xaml](#tab/tabid-9)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        ///
        ///     <!--omitted for brevity-->
        ///
        ///     <chart:SfCartesianChart.ZoomPanBehavior>
        ///        <chart:ChartZoomPanBehavior EnablePanning = "True" />
        ///     </chart:SfCartesianChart.ZoomPanBehavior>
        ///
        ///     <chart:LineSeries ItemsSource="{Binding Data}"
        ///                       XBindingPath="XValue"
        ///                       YBindingPath="YValue"/>
        /// 
        /// </chart:SfCartesianChart>
        ///
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-10)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// ViewModel viewModel = new ViewModel();
        /// 
        /// // omitted for brevity
        /// chart.ZoomPanBehavior = new ChartZoomPanBehavior() 
        /// { 
        ///    EnablePanning = true,
        /// };
        /// 
        /// LineSeries series = new LineSeries()
        /// {
        ///    ItemsSource = viewModel.Data,
        ///    XBindingPath = "XValue",
        ///    YBindingPath = "YValue",
        /// };
        /// chart.Series.Add(series);
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public bool EnablePanning
        {
            get { return (bool)GetValue(EnablePanningProperty); }
            set { SetValue(EnablePanningProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether zooming is enabled through double tapping.
        /// </summary>
        /// <value>
        /// It accepts the bool values and the default value is <c>true</c>.
        /// </value>
        /// <example>
        /// # [MainPage.xaml](#tab/tabid-9)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        ///
        ///     <!--omitted for brevity-->
        ///
        ///     <chart:SfCartesianChart.ZoomPanBehavior>
        ///        <chart:ChartZoomPanBehavior EnableDoubleTap = "True" />
        ///     </chart:SfCartesianChart.ZoomPanBehavior>
        ///
        ///     <chart:LineSeries ItemsSource="{Binding Data}"
        ///                       XBindingPath="XValue"
        ///                       YBindingPath="YValue"/>
        /// 
        /// </chart:SfCartesianChart>
        ///
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-10)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// ViewModel viewModel = new ViewModel();
        /// 
        /// // omitted for brevity
        /// chart.ZoomPanBehavior = new ChartZoomPanBehavior() 
        /// { 
        ///    EnableDoubleTap = true,
        /// };
        /// 
        /// LineSeries series = new LineSeries()
        /// {
        ///    ItemsSource = viewModel.Data,
        ///    XBindingPath = "XValue",
        ///    YBindingPath = "YValue",
        /// };
        /// chart.Series.Add(series);
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public bool EnableDoubleTap
        {
            get { return (bool)GetValue(EnableDoubleTapProperty); }
            set { SetValue(EnableDoubleTapProperty, value); }
        }

        /// <summary>
        /// Gets or sets the mode for zooming direction.
        /// </summary>
        /// <remarks>The zooming can be done both horizontally and vertically.</remarks>
        /// <value>
        /// It accepts the <see cref="ZoomMode"/> values and the default value is <see cref="ZoomMode.XY"/>.
        /// </value>
        /// <example>
        /// # [MainPage.xaml](#tab/tabid-5)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        ///
        ///     <!--omitted for brevity-->
        ///
        ///     <chart:SfCartesianChart.ZoomPanBehavior>
        ///        <chart:ChartZoomPanBehavior ZoomMode="X"/>
        ///     </chart:SfCartesianChart.ZoomPanBehavior>
        ///
        ///     <chart:LineSeries ItemsSource="{Binding Data}"
        ///                       XBindingPath="XValue"
        ///                       YBindingPath="YValue"/>
        ///          
        /// </chart:SfCartesianChart>
        ///
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-6)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// ViewModel viewModel = new ViewModel();
        /// 
        /// // omitted for brevity
        /// chart.ZoomPanBehavior = new ChartZoomPanBehavior() 
        /// { 
        ///    ZoomMode = ZoomMode.X,
        /// };
        /// 
        /// LineSeries series = new LineSeries()
        /// {
        ///    ItemsSource = viewModel.Data,
        ///    XBindingPath = "XValue",
        ///    YBindingPath = "YValue",
        /// };
        /// chart.Series.Add(series);
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public ZoomMode ZoomMode
        {
            get { return (ZoomMode)GetValue(ZoomModeProperty); }
            set { SetValue(ZoomModeProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the selection zooming is enabled.
        /// </summary>
        /// <value>
        /// It accept the bool values and the default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// To show the axis trackball label while <b>Selection Zooming</b>, you must set the <see cref="ChartAxis.ShowTrackballLabel"/> as <c>True</c>.
        /// </remarks>
        /// <example>
        /// # [MainPage.xaml](#tab/tabid-9)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        ///
        ///     <!--omitted for brevity-->
        ///
        ///     <chart:SfCartesianChart.ZoomPanBehavior>
        ///        <chart:ChartZoomPanBehavior EnableSelectionZooming = "True" />
        ///     </chart:SfCartesianChart.ZoomPanBehavior>
        ///
        ///     <chart:LineSeries ItemsSource="{Binding Data}"
        ///                       XBindingPath="XValue"
        ///                       YBindingPath="YValue"/>
        /// 
        /// </chart:SfCartesianChart>
        ///
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-10)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// ViewModel viewModel = new ViewModel();
        /// 
        /// // omitted for brevity
        /// chart.ZoomPanBehavior = new ChartZoomPanBehavior() 
        /// { 
        ///    EnableSelectionZooming = true,
        /// };
        /// 
        /// LineSeries series = new LineSeries()
        /// {
        ///    ItemsSource = viewModel.Data,
        ///    XBindingPath = "XValue",
        ///    YBindingPath = "YValue",
        /// };
        /// chart.Series.Add(series);
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public bool EnableSelectionZooming
        {
            get { return(bool)GetValue(EnableSelectionZoomingProperty);}
            set { SetValue(EnableSelectionZoomingProperty, value); }
        }

        /// <summary>
        /// Gets or sets the stroke color of the selection rectangle.
        /// </summary>
        public Brush SelectionRectStroke
        {
            get { return (Brush)GetValue(SelectionRectStrokeProperty);}
            set { SetValue(SelectionRectStrokeProperty, value);}
        }

        /// <summary>
        /// Gets or sets the stroke width of the selection rectangle.
        /// </summary>
        public double SelectionRectStrokeWidth
        {
            get { return (double)GetValue(SelectionRectStrokeWidthProperty);}
            set { SetValue(SelectionRectStrokeWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the fill color of the selection rectangle.
        /// </summary>
        public Brush SelectionRectFill
        {
            get { return (Brush)GetValue(SelectionRectFillProperty);}
            set { SetValue(SelectionRectFillProperty, value); }
        }

        /// <summary>
        /// Gets or sets the stroke dash array for the selection zooming rectangle.
        /// </summary>
        public DoubleCollection SelectionRectStrokeDashArray
        {
            get { return (DoubleCollection)GetValue(SelectionRectStrokeDashArrayProperty); }
            set { SetValue(SelectionRectStrokeDashArrayProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether zooming is enabled using the direction of the axis.
        /// </summary>
        /// <value>
        /// It accept the bool values and the default value is <c>false</c>.
        /// </value>
        /// <example>
        /// # [MainPage.xaml](#tab/tabid-9)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        ///
        ///     <!--omitted for brevity-->
        ///
        ///     <chart:SfCartesianChart.ZoomPanBehavior>
        ///        <chart:ChartZoomPanBehavior EnablePinchZooming = "True" 
        ///                                    EnableDirectionalZooming = "True"/>
        ///     </chart:SfCartesianChart.ZoomPanBehavior>
        ///
        ///     <chart:LineSeries ItemsSource="{Binding Data}"
        ///                       XBindingPath="XValue"
        ///                       YBindingPath="YValue"/>
        /// 
        /// </chart:SfCartesianChart>
        ///
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-10)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// ViewModel viewModel = new ViewModel();
        /// 
        /// // omitted for brevity
        /// chart.ZoomPanBehavior = new ChartZoomPanBehavior() 
        /// { 
        ///    EnablePinchZooming = true,
        ///    EnableDirectionalZooming = true,
        /// };
        /// 
        /// LineSeries series = new LineSeries()
        /// {
        ///    ItemsSource = viewModel.Data,
        ///    XBindingPath = "XValue",
        ///    YBindingPath = "YValue",
        /// };
        /// chart.Series.Add(series);
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public bool EnableDirectionalZooming
        {
            get { return (bool)GetValue(EnableDirectionalZoomingProperty); }
            set { SetValue(EnableDirectionalZoomingProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value that determines the maximum zoom level of the chart. 
        /// </summary>
        /// <value>This property takes the float value.</value>
        /// <example>
        /// # [MainPage.xaml](#tab/tabid-9)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        ///
        ///     <!--omitted for brevity-->
        ///
        ///     <chart:SfCartesianChart.ZoomPanBehavior>
        ///        <chart:ChartZoomPanBehavior EnablePinchZooming = "True" 
        ///                                    MaximumZoomLevel="2"/>
        ///     </chart:SfCartesianChart.ZoomPanBehavior>
        ///
        ///     <chart:LineSeries ItemsSource="{Binding Data}"
        ///                       XBindingPath="XValue"
        ///                       YBindingPath="YValue"/>
        /// 
        /// </chart:SfCartesianChart>
        ///
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-10)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// ViewModel viewModel = new ViewModel();
        /// 
        /// // omitted for brevity
        /// chart.ZoomPanBehavior = new ChartZoomPanBehavior() 
        /// { 
        ///    EnablePinchZooming = true,
        ///    MaximumZoomLevel=2,
        /// };
        /// 
        /// LineSeries series = new LineSeries()
        /// {
        ///    ItemsSource = viewModel.Data,
        ///    XBindingPath = "XValue",
        ///    YBindingPath = "YValue",
        /// };
        /// chart.Series.Add(series);
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public float MaximumZoomLevel
        {
            get { return (float)GetValue(MaximumZoomLevelProperty); }
            set { SetValue(MaximumZoomLevelProperty, value); }
        }

        #endregion

        #region Internal Properties

        internal SfCartesianChart? Chart { get; set; }
        internal bool IsSelectionZoomingActivated { get; set; } = false;
        internal Rect SelectionRect { get; set; }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartZoomPanBehavior"/> class.
        /// </summary>
        public ChartZoomPanBehavior()
        {
            SelectionRectStroke = new SolidColorBrush(Color.FromArgb("#6200EE"));
            SelectionRectFill = new SolidColorBrush(Color.FromArgb("#146200EE"));
        }
        
        #endregion

        #region Methods

        #region Internal Methods

        internal override void SetTouchHandled(IChart chart)
        {
#if MONOANDROID || WINDOWS
            var cartesianChart = chart as SfCartesianChart;
            if (cartesianChart != null && EnablePanning)
            {
                cartesianChart.IsHandled = true;
            }
#endif
        }
        
        internal override void OnSingleTap(IChart chart, float pointX, float pointY)
        {
#if MACCATALYST || IOS
            isSingleTapActivated = true;
#endif
        }

        internal override void OnDoubleTap(IChart chart, float x, float y)
        {
            base.OnDoubleTap(chart, x, y);

            var clipRect = chart.ActualSeriesClipRect;
            if (clipRect.Contains(x, y) && chart is SfCartesianChart cartesianChart)
                OnDoubleTap(cartesianChart.ChartArea, clipRect, x, y);
        }

        internal void OnScrollChanged(IChart chart, Point touchPoint, Point translatePoint)
        {
            var clipRect = chart.ActualSeriesClipRect;
#if MACCATALYST || IOS
            isScrollChanged = true;
#endif
            if (chart is SfCartesianChart cartesianChart)
            {
                cartesianChart.IsHandled = TouchHandled(cartesianChart, translatePoint);

                if (!IsSelectionZoomingActivated && clipRect.Contains(touchPoint))
                {
                    PanTranslate(cartesianChart, clipRect, translatePoint);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        protected internal override void OnTouchMove(ChartBase chart, float pointX, float pointY)
        {
            if (chart is IChart iChart && chart is SfCartesianChart cartesianChart)
            {
                var clipRect = iChart.ActualSeriesClipRect;

                if (IsSelectionZoomingActivated && clipRect.Contains(startX, startY))
                {
                    isSelectionActivated = true;
                    OnSelectionZoomDelta(cartesianChart, pointX, pointY);
                    Invalidate();
                }
            }
        }
        
        internal void OnMouseWheelChanged(IChart chart, Point position, double delta)
        {
            var width = (float)chart.ActualSeriesClipRect.Width;
            var height = (float)chart.ActualSeriesClipRect.Height;

            if (EnablePinchZooming && chart.ActualSeriesClipRect.Contains(position) && chart.Area is CartesianChartArea area)
            {
                var direction = delta > 0 ? 1 : -1;

                foreach (var chartAxis in area.XAxes)
                {
                    var zoomFactor = chartAxis.ZoomFactor;
                    var cumulativeZoomLevel = (float)Math.Max(1f / ChartMath.MinMax(zoomFactor, 0f, 1f), 1f);
                    cumulativeZoomLevel = Math.Max(cumulativeZoomLevel + (0.25f * direction), 1);

                    if (CanZoom(chartAxis, double.NaN, area.IsTransposed))
                    {
                        var origin = GetOrigin((float)position.X, (float)position.Y, width, height, chartAxis);
                        if (!float.IsNaN(MaximumZoomLevel))
                        {
                            cumulativeZoomLevel = cumulativeZoomLevel <= MaximumZoomLevel ? cumulativeZoomLevel : MaximumZoomLevel;
                        }
                        Zoom(cumulativeZoomLevel, origin, chartAxis);
                    }
                } 
                
                foreach (var chartAxis in area.YAxes)
                {
                    var zoomFactor = chartAxis.ZoomFactor;
                    var cumulativeZoomLevel = (float)Math.Max(1f / ChartMath.MinMax(zoomFactor, 0f, 1f), 1f);
                    cumulativeZoomLevel = Math.Max(cumulativeZoomLevel + (0.25f * direction), 1);

                    if (CanZoom(chartAxis, double.NaN, area.IsTransposed))
                    {
                        var origin = GetOrigin((float)position.X, (float)position.Y, width, height, chartAxis);
                        if (!float.IsNaN(MaximumZoomLevel))
                        {
                            cumulativeZoomLevel = cumulativeZoomLevel <= MaximumZoomLevel ? cumulativeZoomLevel : MaximumZoomLevel;
                        }
                        Zoom(cumulativeZoomLevel, origin, chartAxis);
                    }
                }
            }
        }

        bool TouchHandled(SfCartesianChart cartesian, Point velocity)
        {
            var area = cartesian.ChartArea;

            if (!EnablePanning || area == null)
            {
                return false;
            }

            bool isPanEnd = true;

            if (Math.Abs(velocity.Y) < Math.Abs(velocity.X))
            {
                foreach (ChartAxis axis in area.XAxes)
                {
                    double position = axis.ZoomPosition;
                    double factor = 1.0f - axis.ZoomFactor;
                    bool velocityIsCrossed = axis.IsInversed ? velocity.X > 0 : velocity.X < 0;

                    if ((position == factor && velocityIsCrossed) || (position == 0 && !velocityIsCrossed))
                    {
                        isPanEnd = false;
                    }
                    else
                    {
                        isPanEnd = true;
                        break;
                    }
                }

                return isPanEnd;
            }
            else
            {
                foreach (ChartAxis axis in area.YAxes)
                {
                    double position = axis.ZoomPosition;
                    double factor = 1.0f - axis.ZoomFactor;
#if MONOANDROID || WINDOWS
                    bool velocityIsCrossed = axis.IsInversed ? velocity.Y < 0 : velocity.Y > 0;

                    if ((position == factor && velocity.Y < 0) || (position == 0 && velocityIsCrossed))
#else
                    bool velocityIsCrossed = axis.IsInversed ? velocity.Y > 0 : velocity.Y <= 0;

                    if ((position == factor && velocity.Y > 0) || (position == 0 && velocityIsCrossed))
#endif
                    {
                        isPanEnd = false;
                    }
                    else
                    {
                        isPanEnd = true;
                        break;
                    }
                }

                return isPanEnd;
            }
        }

        internal void OnPinchStateChanged(IChart chart, GestureStatus action, Point location, double angle, float scale)
        {
            var clipRect = chart.ActualSeriesClipRect;
            pinchStatus = action;

            if ((pinchStatus != GestureStatus.Completed) && EnablePinchZooming && clipRect.Contains(location) && chart is SfCartesianChart cartesianChart)
            {
                PinchZoom(cartesianChart, clipRect, location, angle, scale);
            }
            else
            {
                isPinchZoomingActivated = false;
            }

#if WINDOWS
              if (pinchStatus == GestureStatus.Completed && chart is SfCartesianChart sfCartesianChart)
                sfCartesianChart.IsHandled = false;
#endif
        }

#endregion

        #region Public Methods

        /// <summary>
        /// Zooms in on the chart.
        /// </summary>
        /// <remarks>This method increases the zoom level of the chart.</remarks>
        public void ZoomIn()
        {
            if (Chart == null)
                return;

            var area = Chart.ChartArea;
            if (area.XAxes.Count > 0 && area.XAxes[0].ZoomPosition < 1)
            {
                cumulativeZoomLevel = cumulativeZoomLevel + 0.25f;
            }

            var origin = 0.5f;

            foreach (var chartAxis in area.XAxes)
            {
                if (CanZoom(chartAxis, double.NaN, area.IsTransposed))
                {
                    Zoom(cumulativeZoomLevel, origin, chartAxis);
                }
            }

            foreach (var chartAxis in area.YAxes)
            {
                if (CanZoom(chartAxis, double.NaN, area.IsTransposed))
                {
                    Zoom(cumulativeZoomLevel, origin, chartAxis);
                }
            }
        }

        /// <summary>
        /// Zooms out from the chart.
        /// </summary>
        /// <remarks>This method decreases the zoom level of the chart.</remarks>
        public void ZoomOut()
        {
            if (Chart == null)
                return;

            var area = Chart.ChartArea;

            if (area.XAxes.Count > 0 && area.XAxes[0].ZoomPosition > 0)
            {
                cumulativeZoomLevel = cumulativeZoomLevel - 0.25f;
            }

            var origin = 0.5f;

            foreach (var chartAxis in area.XAxes)
            {
                if (chartAxis != null && CanZoom(chartAxis, double.NaN, area.IsTransposed))
                {
                    Zoom(cumulativeZoomLevel, origin, chartAxis);
                }
            }

            foreach (var chartAxis in area.YAxes)
            {
                if (chartAxis != null && CanZoom(chartAxis, double.NaN, area.IsTransposed))
                {
                    Zoom(cumulativeZoomLevel, origin, chartAxis);
                }
            }
        }

        /// <summary>
        ///  Resets the zoom factor and zoom position for all the axis.
        /// </summary>
        public void Reset()
        {
            if (Chart == null) return;
            var area = Chart.ChartArea;
            var isTransposed = area.IsTransposed;
            var layout = area.AxisLayout;

            Reset(layout.HorizontalAxes, isTransposed);
            Reset(layout.VerticalAxes, isTransposed);
        }

        /// <summary>
        /// Zooms the chart by a specified range.
        /// </summary>
        /// <remarks>This method changes the zoom position and zoom factor of the given chart axis by a specified range.</remarks>
        public void ZoomByRange(ChartAxis chartAxis, double start, double end)
        {
            if (chartAxis != null && chartAxis.CartesianArea != null)
            {
                if (CanZoom(chartAxis, double.NaN, chartAxis.CartesianArea.IsTransposed))
                {
                    if (start > end)
                    {
                        var temp = start;
                        start = end;
                        end = temp;
                    }

                    DoubleRange axisRange = chartAxis.ActualRange;

                    if (start >= axisRange.End || end <= axisRange.Start)
                    {
                        return;
                    }

                    if (start < axisRange.Start)
                    {
                        start = axisRange.Start;
                    }

                    if (end > axisRange.End)
                    {
                        end = axisRange.End;
                    }

                    chartAxis.ZoomPosition = (start - axisRange.Start) / axisRange.Delta;
                    chartAxis.ZoomFactor = (end - start) / axisRange.Delta;
                }
            }
        }

        /// <summary>
        /// Zooms the chart by a specified range.
        /// </summary>
        /// <remarks>This method changes the zoom position and zoom factor of the given date time axis by a specified range.</remarks>
        public void ZoomByRange(DateTimeAxis dateTimeAxis, DateTime start, DateTime end)
        {
            ZoomByRange(dateTimeAxis, start.ToOADate(), end.ToOADate());
        }

        /// <summary>
        /// Zooms the chart by the specified zoom position and zoom factor.
        /// </summary>
        /// <remarks>This method changes the zoom position and zoom factor of the given chart axis.</remarks>
        public void ZoomToFactor(ChartAxis chartAxis, double zoomPosition, double zoomFactor)
        {
            if (chartAxis.CartesianArea != null &&
                (chartAxis.ZoomFactor != zoomFactor || chartAxis.ZoomPosition != zoomPosition))
            {
                if (CanZoom(chartAxis, double.NaN, chartAxis.CartesianArea.IsTransposed))
                {
                    chartAxis.ZoomFactor = zoomFactor;
                    chartAxis.ZoomPosition = zoomPosition;
                }
            }
        }

        /// <summary>
        /// Zooms the chart by the specified zoom factor.
        /// </summary>
        /// <remarks>This method changes the zoom factor of the horizontal and verticle axes.</remarks>
        public void ZoomToFactor(double zoomFactor)
        {
            if (Chart == null) return;
            var area = Chart.ChartArea;
            var isTransposed = area.IsTransposed;
            var layout = area.AxisLayout;

            Zoom(layout.HorizontalAxes, zoomFactor, isTransposed);
            Zoom(layout.VerticalAxes, zoomFactor, isTransposed);
        }

        private void Zoom(ObservableCollection<ChartAxis> axes, double zoomFactor, bool transposed)
        {
            foreach (var chartAxis in axes)
            {
                if (CanZoom(chartAxis, double.NaN, transposed))
                {
                    if (chartAxis.ZoomFactor <= 1 && chartAxis.ZoomFactor >= 0.1)
                    {
                        chartAxis.ZoomFactor = zoomFactor;
                        chartAxis.ZoomPosition = 0.5f;
                    }
                }
            }
        }

        #endregion

        #region Private Methods
        private bool CanReset(ObservableCollection<ChartAxis> axes)
        {
            if (axes != null)
            {
                return axes.Any(axis => axis.ZoomFactor < 1);
            }

            return false;
        }

        private void Reset(ObservableCollection<ChartAxis> axes, bool transpose)
        {
            foreach (var chartAxis in axes)
            {
                if (CanZoom(chartAxis, double.NaN, transpose))
                {
                    chartAxis.ZoomPosition = 0;
                    chartAxis.ZoomFactor = 1;
                    cumulativeZoomLevel = 1f;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        protected internal override void OnTouchUp(ChartBase chart, float pointX, float pointY)
        {
            if (chart is IChart iChart && chart is SfCartesianChart cartesianChart)
            {
                if (EnableSelectionZooming && IsSelectionZoomingActivated)
                {
                    if (isSelectionActivated)
                    {
                        if (Math.Abs(SelectionRect.Width) > 20 || Math.Abs(SelectionRect.Height) > 20)
                        {
                            Zoom(SelectionRect, cartesianChart.ChartArea);
                        }
                        
                        SelectionRect = new Rect(0, 0, 0, 0);
                        Invalidate();
                        isSelectionActivated = false;
                        IsSelectionZoomingActivated = false;
#if MACCATALYST || IOS
                        isSelected = false;
                        isDoubleTapped = false;
#endif
                    }
#if WINDOWS || ANDROID
                    else
                    {
                        isSelectionZoomingEnded = true;
                        SelectionRect = new Rect(0, 0, 0, 0);
                        OnDoubleTap(cartesianChart.ChartArea, iChart.ActualSeriesClipRect, pointX, pointY);
                    }
#endif
                }
            }
        }

        internal override void OnTouchDown(float pointX, float pointY)
        {
            base.OnTouchDown(pointX, pointY);
#if MACCATALYST || IOS
            downEntered++;
            
            if (isSingleTapActivated || isScrollChanged)
            {
                downEntered = 1;
            }

            if (downEntered == 1)
            {
                selectedRegionRect = new Rect(pointX - 10, pointY - 10, 20, 20);
                startTime = DateTime.Now;
                isScrollChanged = false;
                isSingleTapActivated = false;
            }
            else if (downEntered == 2)
            {
                endTime = DateTime.Now;
                var ellapsedTime = startTime - endTime;

                isDoubleTapped = ellapsedTime < TimeSpan.FromMilliseconds(150) && selectedRegionRect.Contains(pointX, pointY);

                if (isDoubleTapped && !isSelected)
                {
                    if (EnableSelectionZooming)
                    {
                        IsSelectionZoomingActivated = true;
                        SelectionRect = new Rect(pointX, pointY, pointX, pointY);
                        startX = pointX;
                        startY = pointY;
                        isSelected = true;
                    }
                }
                
                downEntered = 0;
            }
#endif
        }

        private void OnDoubleTap(CartesianChartArea area, Rect clipRect, float pointX, float pointY)
        {
#if !MACCATALYST && !IOS
            if (EnableSelectionZooming && !isSelectionZoomingEnded)
            {
                IsSelectionZoomingActivated = true;
                SelectionRect = new Rect(pointX, pointY, pointX, pointY);
                startX = pointX;
                startY = pointY;
            }

            isSelectionZoomingEnded = EnableSelectionZooming ? isSelectionZoomingEnded : true;

            if (EnableDoubleTap && isSelectionZoomingEnded)
#else
            if (EnableDoubleTap)
#endif
            {
                var clip = clipRect;
                var axislayout = area.AxisLayout;
                var xAxes = axislayout.HorizontalAxes;
                var yAxes = axislayout.VerticalAxes;

                float manipulationX = (float)(pointX - clip.Left);
                float manipulationY = (float)(pointY - clip.Top);

                float width = (float)clip.Width;
                float height = (float)clip.Height;

                if (CanReset(xAxes) || CanReset(yAxes))
                {
                    Reset(xAxes, area.IsTransposed);
                    Reset(yAxes, area.IsTransposed);
                }
                else
                {
                    foreach (var chartAxis in xAxes)
                    {
                        if (CanZoom(chartAxis, double.NaN, area.IsTransposed))
                        {
                            var origin = GetOrigin(manipulationX, manipulationY, width, height, chartAxis);
                            Zoom(2.5f, origin, chartAxis);
                        }
                    }

                    foreach (var chartAxis in yAxes)
                    {
                        if (CanZoom(chartAxis, double.NaN, area.IsTransposed))
                        {
                            var origin = GetOrigin(manipulationX, manipulationY, width, height, chartAxis);

                            Zoom(2.5f, origin, chartAxis);
                        }
                    }
                    cumulativeZoomLevel = 2.5f;
                }
                
                IsSelectionZoomingActivated = false;
                
#if MACCATALYST || IOS
                isDoubleTapped = false;
                isSelected = false;
#else
                isSelectionZoomingEnded = false;
#endif
            }
        }

        private bool CanZoom(ChartAxis chartAxis, double angle, bool transposed)
        {
            if (Chart == null) return false;

            bool canDirectionalZoom = ZoomMode == ZoomMode.XY;

            if (isPinchZoomingActivated && !double.IsNaN(angle) && EnableDirectionalZooming && canDirectionalZoom)
            {
                bool isXDirection = (angle >= 340 && angle <= 360) || (angle >= 0 && angle <= 20) || (angle >= 160 && angle <= 200);
                bool isYDirection = (angle >= 70 && angle <= 110) || (angle >= 250 && angle <= 290);
                bool isBothDirection = (angle > 20 && angle < 70) || (angle > 110 && angle < 160) || (angle > 200 && angle < 250) || (angle > 290 && angle < 340);

                canDirectionalZoom = (!chartAxis.IsVertical && isXDirection) || (chartAxis.IsVertical && isYDirection) || isBothDirection;
            }

            if (chartAxis.RegisteredSeries.Count > 0 && chartAxis.RegisteredSeries[0] != null
                && transposed)
            {
                if ((!chartAxis.IsVertical && ZoomMode == ZoomMode.Y) || (chartAxis.IsVertical && ZoomMode == ZoomMode.X) || canDirectionalZoom)
                {
                    Chart.IsRequiredDataLabelsMeasure = false;
                    return true;
                }
            }
            else
            {
                if ((chartAxis.IsVertical && ZoomMode == ZoomMode.Y) || (!chartAxis.IsVertical && ZoomMode == ZoomMode.X) || canDirectionalZoom)
                {
                    Chart.IsRequiredDataLabelsMeasure = false;
                    return true;
                }
            }

            return false;
        }

        private static float GetOrigin(float manipulationX, float manipulationY, float width, float height, ChartAxis chartAxis)
        {
            float origin;
            double plotOffsetStart = chartAxis.ActualPlotOffsetStart;
            double plotOffsetEnd = chartAxis.ActualPlotOffsetEnd;

            if (chartAxis.IsVertical)
            {
                origin = (float)(chartAxis.IsInversed
                    ? ((manipulationY - plotOffsetEnd) / height)
                    : 1 - ((manipulationY - plotOffsetStart) / height));
            }
            else
            {
                origin = (float)(chartAxis.IsInversed
                    ? 1.0 - ((manipulationX - plotOffsetEnd) / width)
                    : (manipulationX - plotOffsetStart) / width);
            }

            return origin;
        }

        private void PinchZoom(SfCartesianChart cartesianChart, Rect clipRect, Point scaleOrgin, double angle, double scale)
        {
            var clip = clipRect;
            var chartArea = cartesianChart.ChartArea;

            foreach (var chartAxis in chartArea.XAxes)
            {
                PinchZoom(chartAxis, scaleOrgin, angle, clip.Size, scale, chartArea.IsTransposed);
            }

            foreach (var chartAxis in chartArea.YAxes)
            {
                PinchZoom(chartAxis, scaleOrgin, angle, clip.Size, scale, chartArea.IsTransposed);
            }

            isPinchZoomingActivated = true;
        }

        private void PinchZoom(ChartAxis chartAxis, Point scaleOrgin, double angle, SizeF size, double scale, bool transpose)
        {
            if (CanZoom(chartAxis, angle, transpose))
            {
                var zoomFactor = chartAxis.ZoomFactor;
                var currentScale = (float)Math.Max(1f / ChartMath.MinMax(zoomFactor, 0f, 1f), 1f);
                currentScale *= (float)scale;
                if (!float.IsNaN(MaximumZoomLevel))
                {
                    currentScale = currentScale <= MaximumZoomLevel ? currentScale : MaximumZoomLevel;
                }

                var origin = GetOrigin((float)scaleOrgin.X, (float)scaleOrgin.Y, size.Width, size.Height, chartAxis);

                Zoom(currentScale, origin, chartAxis);
            }
        }

        private void PanTranslate(SfCartesianChart cartesianChart, Rect clipRect, Point translatePoint)
        {
            var area = cartesianChart.ChartArea;

            foreach (var axis in area.XAxes)
            {
                if (EnablePanning && !isPinchZoomingActivated && CanZoom(axis, double.NaN, area.IsTransposed))
                {
                    axis.IsScrolling = true;
                    Translate(axis, clipRect, translatePoint, Math.Max(1 / ChartMath.MinMax(axis.ZoomFactor, 0, 1), 1));
                }
            }
            
            foreach (var axis in area.YAxes)
            {
                if (EnablePanning && !isPinchZoomingActivated && CanZoom(axis, double.NaN, area.IsTransposed))
                {
                    axis.IsScrolling = true;
                    Translate(axis, clipRect, translatePoint, Math.Max(1 / ChartMath.MinMax(axis.ZoomFactor, 0, 1), 1));
                }
            }
        }

        private void Translate(ChartAxis axis, RectF clip, Point translatePoint, double currentScale)
        {
            double previousZoomPosition = axis.ZoomPosition;
            double currentZoomPosition;

            //Todo : Need to check the translate value with android and iOS.
            if(axis.IsVertical)
            {
                double offset = translatePoint.Y / clip.Height / currentScale;
#if ANDROID
                offset = axis.IsInversed ? previousZoomPosition + offset : previousZoomPosition - offset;
#else
                offset = axis.IsInversed ? previousZoomPosition - offset : previousZoomPosition + offset;
#endif
                currentZoomPosition = ChartMath.MinMax(offset, 0, 1 - axis.ZoomFactor);
            }
            else
            {
                double offset = translatePoint.X / clip.Width / currentScale;
#if ANDROID
                offset = axis.IsInversed ? previousZoomPosition - offset : previousZoomPosition + offset;
#else
                offset = axis.IsInversed ? previousZoomPosition + offset : previousZoomPosition - offset;
#endif
                currentZoomPosition = ChartMath.MinMax(offset, 0, 1 - axis.ZoomFactor);
            }

            if ((pinchStatus == GestureStatus.Completed || pinchStatus == GestureStatus.Canceled) && previousZoomPosition != currentZoomPosition)
            {
                axis.ZoomPosition = currentZoomPosition;
            }
        }
        
        /// <summary>
        /// Zoom at cumulative level to the corresponding origin.
        /// </summary>
        /// <param name="cumulativeLevel"></param>
        /// <param name="origin"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        private static bool Zoom(float cumulativeLevel, float origin, ChartAxis axis)
        {
            if (axis != null)
            {
                double calcZoomPos;
                double calcZoomFactor;

                if (cumulativeLevel == 1)
                {
                    calcZoomFactor = 1;
                    calcZoomPos = 0;
                }
                else
                {
                    calcZoomFactor = ChartMath.MinMax(1 / cumulativeLevel, 0, 1);
                    calcZoomPos = axis.ZoomPosition + ((axis.ZoomFactor - calcZoomFactor) * origin);
                }

                if (axis.ZoomPosition != calcZoomPos || axis.ZoomFactor != calcZoomFactor)
                {
                    axis.ZoomPosition = calcZoomPos;
                    axis.ZoomFactor = calcZoomPos + calcZoomFactor > 1 ? 1 - calcZoomPos : calcZoomFactor;

                    return true;
                }
            }

            return false;
        }

        private void OnSelectionZoomDelta(SfCartesianChart chart, float pointX, float pointY)
        {
            if (EnableSelectionZooming)
            {
                var iChart = chart as IChart;
                var chartBounds = iChart.ActualSeriesClipRect;
                var plotBounds = chart.ChartArea.PlotArea.PlotAreaBounds;
                var actualClipRect = chart.ChartArea.ActualSeriesClipRect;
                endX = pointX;
                endY = pointY;
                var left = this.startX;
                var top = this.startY;

                if (chartBounds.Left > endX)
                {
                    endX = (float)chartBounds.Left;
                }

                if (chartBounds.Right < endX)
                {
                    endX = (float)chartBounds.Right;
                }

                if (chartBounds.Top > endY)
                {
                    endY = (float)chartBounds.Top;
                }

                if (actualClipRect.Bottom + chartBounds.Top - actualClipRect.Top < endY)
                {
                    endY = actualClipRect.Bottom + (float)chartBounds.Top - actualClipRect.Top;
                }

                currentSelectionRect = new Rect(left - plotBounds.Left, top - chartBounds.Top + actualClipRect.Top, endX - left, endY - top);
                SelectionRect = currentSelectionRect;
            }
        }

        private void Zoom(Rect selectionRect, CartesianChartArea area)
        {
            var clipRect = area.ActualSeriesClipRect;
            var axislayout = area.AxisLayout;
            var xAxes = axislayout.HorizontalAxes;
            var yAxes = axislayout.VerticalAxes;

            foreach (var chartAxis in xAxes)
            {
                var previousZoomFactor = chartAxis.ZoomFactor;
                var previousZoomPosition = chartAxis.ZoomPosition;
                double currentZoomFactor;
                currentZoomFactor = previousZoomFactor * (selectionRect.Width / clipRect.Width);
                if (!float.IsNaN(MaximumZoomLevel))
                {
                    currentZoomFactor = 1 / currentZoomFactor <= MaximumZoomLevel
                        ? currentZoomFactor
                        : 1 / MaximumZoomLevel;
                }

                chartAxis.ZoomFactor = ZoomMode != ZoomMode.Y ? currentZoomFactor : 1f;
                if (currentZoomFactor != previousZoomFactor)
                {
                    var zoomPosition = ZoomMode != ZoomMode.Y
                        ? previousZoomPosition +
                          (Math.Abs((selectionRect.Left - clipRect.Left + (float)(chartAxis.IsInversed ? selectionRect.Width : 0.0)) / clipRect.Width) * previousZoomFactor) : 0;
                    chartAxis.ZoomPosition = chartAxis.IsInversed ? 1 - zoomPosition : zoomPosition;
                }
            }
            foreach (var chartAxis in yAxes)
            {
                var previousZoomFactor = chartAxis.ZoomFactor;
                var previousZoomPosition = chartAxis.ZoomPosition;
                double currentZoomFactor;
           
                currentZoomFactor = previousZoomFactor * selectionRect.Height / clipRect.Height;
                if (!float.IsNaN(MaximumZoomLevel))
                {
                    currentZoomFactor = 1 / currentZoomFactor <= MaximumZoomLevel
                        ? currentZoomFactor
                        : 1 / MaximumZoomLevel;
                }

                chartAxis.ZoomFactor = ZoomMode != ZoomMode.X ? currentZoomFactor : 1;
                if (currentZoomFactor != previousZoomFactor)
                {
                    var zoomPosition = ZoomMode != ZoomMode.X
                        ? previousZoomPosition +
                            ((1 - Math.Abs(((chartAxis.IsInversed ? 0 : selectionRect.Height) + selectionRect.Top - clipRect.Top) / clipRect.Height)) * previousZoomFactor) : 0;

                    chartAxis.ZoomPosition = chartAxis.IsInversed ? 1 - zoomPosition : zoomPosition;
                }
            }
        }
        private void Invalidate()
        {
            if (Chart != null)
            {
                Chart.ZoomPanView.InvalidateDrawable();
            }
        }

        private static void OnEnableDoubleTapPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        private static void OnEnableZoomingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        private static void OnEnablePanningPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        private static void OnZoomModePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        private static void OnMaximumZoomLevelPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

#endregion

        #endregion
    }
}
