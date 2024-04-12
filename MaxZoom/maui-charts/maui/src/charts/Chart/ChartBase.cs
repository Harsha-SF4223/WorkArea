using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Core.Internals;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// The <see cref="ChartBase"/> class is the base for <see cref="SfCartesianChart"/> and <see cref="SfCircularChart"/> types.
    /// </summary>
    public abstract class ChartBase : View, IContentView, IChart
    {
        #region Fields
        internal readonly LegendLayout LegendLayout;
        private ChartTooltipBehavior? defaultToolTipBehavior;
        private ChartTooltipBehavior? toolTipBehavior;
        private AreaBase area;
        private SfTooltip? tooltipView;
        private Rect actualSeriesClipRect;
        #endregion

        #region Bindable Properties

        /// <summary>
        /// Identifies the <see cref="Title"/> bindable property.
        /// </summary>        
        /// <value>
        /// The identifier for <see cref="Title"/> bindable property.
        /// </value>        
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(object), typeof(ChartBase), null,
                                    propertyChanged: OnTitlePropertyChanged);

        /// <summary>
        /// Identifies the <see cref="Legend"/> bindable property.
        /// </summary>        
        /// <value>
        /// The identifier for <see cref="Legend"/> bindable property.
        /// </value>        
        public static readonly BindableProperty LegendProperty =
           BindableProperty.Create(nameof(Legend), typeof(ChartLegend), typeof(ChartBase), null,
                                   propertyChanged: OnLegendPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="TooltipBehavior"/> bindable property.
        /// </summary>        
        /// <value>
        /// The identifier for <see cref="TooltipBehavior"/> bindable property.
        /// </value>        
        public static readonly BindableProperty TooltipBehaviorProperty = BindableProperty.Create(nameof(TooltipBehavior), typeof(ChartTooltipBehavior), typeof(ChartBase), null, BindingMode.Default, null, OnTooltipBehaviorPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="InteractiveBehavior"/> bindable property.
        /// </summary>        
        /// <value>
        /// The identifier for <see cref="InteractiveBehavior"/> bindable property.
        /// </value>        
        public static readonly BindableProperty InteractiveBehaviorProperty = BindableProperty.Create(nameof(InteractiveBehavior), typeof(ChartInteractiveBehavior), typeof(ChartBase), null, BindingMode.Default, null, OnInteractiveBehaviorPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="PlotAreaBackgroundView"/> bindable property.
        /// </summary>             
        public static readonly BindableProperty PlotAreaBackgroundViewProperty = BindableProperty.Create(nameof(PlotAreaBackgroundView),typeof(View), typeof(ChartBase), null, propertyChanged: OnPlotAreaBackgroundChanged);
       
        internal static readonly BindableProperty LegendStyleProperty = BindableProperty.Create(nameof(LegendStyle), typeof(ChartLegendLabelStyle), typeof(ChartBase), defaultValueCreator: LegendStyleDefaultValueCreator, propertyChanged: OnLegendStylePropertyChanged);

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the title for chart. It supports the string or any view as title.
        /// </summary>
        /// <value>Default value is null.</value>
        /// 
        /// <remarks>
        /// 
        /// <para>Example code for string as title.</para>
        /// 
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart Title="Average High/Low Temperature">
        ///           
        ///     </chart:SfCartesianChart>
        /// ]]></code>
        /// 
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     chart.Title = "Average High / Low Temperature";
        /// ]]></code>
        /// ***
        /// 
        /// <para>Example code for View as title.</para>
        /// 
        /// # [MainPage.xaml](#tab/tabid-3)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///           <chart:SfCartesianChart.Title>
        ///               <Label Text = "Average High/Low Temperature" 
        ///                      HorizontalOptions="Fill"
        ///                      HorizontalTextAlignment="Center"
        ///                      VerticalOptions="Center"
        ///                      FontSize="16"
        ///                      TextColor="Black"/>
        ///           </chart:SfCartesianChart.Title>
        ///           
        ///     </chart:SfCartesianChart>
        /// ]]></code>
        /// 
        /// # [MainPage.xaml.cs](#tab/tabid-4)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        ///     chart.Title = new Label()
        ///     { 
        ///         Text = "Average High / Low Temperature",
        ///         HorizontalOptions = LayoutOptions.Fill,
        ///         HorizontalTextAlignment = TextAlignment.Center,
        ///         VerticalOptions = LayoutOptions.Center,
        ///         FontSize = 16,
        ///         TextColor = Colors.Black
        ///     };
        /// ]]></code>
        /// ***
        /// </remarks>
        public object Title
        {
            get { return (object)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the legend that helps to identify the corresponding series or data point in chart.
        /// </summary>
        /// <value>This property takes a <see cref="ChartLegend"/> instance as value and its default value is null.</value>
        /// <example>
        /// # [MainPage.xaml](#tab/tabid-5)
        /// <code> <![CDATA[
        /// <chart:SfCircularChart>
        ///
        ///        <chart:SfCircularChart.BindingContext>
        ///            <local:ViewModel/>
        ///        </chart:SfCircularChart.BindingContext>
        ///        
        ///        <chart:SfCircularChart.Legend>
        ///            <chart:ChartLegend/>
        ///        </chart:SfCircularChart.Legend>
        ///
        ///        <chart:SfCircularChart.Series>
        ///            <chart:PieSeries ItemsSource="{Binding Data}"
        ///                             XBindingPath="XValue"
        ///                             YBindingPath="YValue" />
        ///        </chart:SfCircularChart.Series>
        ///        
        /// </chart:SfCircularChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-6)
        /// <code><![CDATA[
        /// SfCircularChart chart = new SfCircularChart();
        /// 
        /// ViewModel viewModel = new ViewModel();
        ///	chart.BindingContext = viewModel;
        ///	
        /// chart.Legend = new ChartLegend();
        /// 
        /// PieSeries series = new PieSeries()
        /// {
        ///     ItemsSource = viewmodel.Data,
        ///     XBindingPath = "XValue",
        ///     YBindingPath = "YValue",
        /// };
        /// chart.Series.Add(series);
        /// 
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        /// 
        ///<remarks>
        /// <para>To render a legend, create an instance of <see cref="ChartLegend"/>, and assign it to the <see cref="Legend"/> property. </para>
        ///</remarks>
        public ChartLegend Legend
        {
            get { return (ChartLegend)GetValue(LegendProperty); }
            set { SetValue(LegendProperty, value); }
        }

        /// <summary>
        /// Gets or sets a tooltip behavior that allows to customize the default tooltip appearance in the chart. 
        /// </summary>
        /// <value>This property takes <see cref="ChartTooltipBehavior"/> instance as value and its default value is null.</value>
        /// <remarks>
        /// 
        /// <para>To display the tooltip on the chart, set the <see cref="ChartSeries.EnableTooltip"/> property as <b>true</b> in <b>ChartSeries</b>.</para>
        /// 
        /// <para>To customize the appearance of the tooltip elements like Background, TextColor and Font, create an instance of <see cref="ChartTooltipBehavior"/> class, modify the values, and assign it to the chart’s <see cref="TooltipBehavior"/> property. </para>
        /// 
        /// </remarks>
        /// <example>
        /// # [MainPage.xaml](#tab/tabid-7)
        /// <code><![CDATA[
        /// <chart:SfCircularChart>
        /// 
        ///         <chart:SfCircularChart.BindingContext>
        ///             <local:ViewModel/>
        ///         </chart:SfCircularChart.BindingContext>
        /// 
        ///         <chart:SfCircularChart.TooltipBehavior>
        ///             <chart:ChartTooltipBehavior/>
        ///         </chart:SfCircularChart.TooltipBehavior>
        /// 
        ///         <chart:SfCircularChart.Series>
        ///             <chart:PieSeries EnableTooltip="True" ItemsSource="{Binding Data}" XBindingPath="XValue" YBindingPath="YValue1"/>
        ///         </chart:SfCircularChart.Series>
        /// 
        /// </chart:SfCircularChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-8)
        /// <code><![CDATA[
        ///  SfCircularChart chart = new SfCircularChart();
        ///  
        ///  ViewModel viewModel = new ViewModel();
        ///  chart.BindingContext = viewModel;
        ///  
        ///  chart.TooltipBehavior = new ChartTooltipBehavior();
        ///  
        ///  PieSeries series = new PieSeries()
        ///  {
        ///     ItemsSource = viewmodel.Data,
        ///     XBindingPath = "XValue",
        ///     YBindingPath = "YValue1",
        ///     EnableTooltip = true
        ///  };
        ///  chart.Series.Add(series);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        /// <seealso cref="ChartSeries.EnableTooltip"/>
        public ChartTooltipBehavior TooltipBehavior
        {
            get { return (ChartTooltipBehavior)GetValue(TooltipBehaviorProperty); }
            set { SetValue(TooltipBehaviorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the interactive behavior that allows to override and customize the touch interaction methods.
        /// </summary>
        /// <value>This property takes <see cref="ChartInteractiveBehavior"/> instance as value and its default value is null.</value>
        /// <remarks>
        /// <para>To use those touch interaction methods, create the class inherited by ChartInteractiveBehavior class.</para>
        /// <para>Then Create the instance for that class and instance has to be added in the chart's <see cref="ChartBase.InteractiveBehavior"/> as per the following code snippet.</para>
        /// # [ChartInteractionExt.cs](#tab/tabid-1)
        /// <code><![CDATA[
        /// public class ChartInteractionExt : ChartInteractiveBehavior
        /// {
        ///    <!--omitted for brevity-->
        /// }
        /// ]]>
        /// </code>
        /// # [MainPage.xaml](#tab/tabid-2)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCircularChart.BindingContext>
        ///            <local:ViewModel/>
        ///        </chart:SfCircularChart.BindingContext>
        ///
        ///     <!--omitted for brevity-->
        ///
        ///     <chart:SfCartesianChart.InteractiveBehavior>
        ///          <local:ChartInteractionExt/>
        ///     </chart:SfCartesianChart.InteractiveBehavior>
        /// 
        /// </chart:SfCartesianChart>
        ///
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-3)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// ViewModel viewModel = new ViewModel();
        /// chart.BindingContext = viewModel;
        /// ...
        /// ChartInteractionExt interaction = new ChartInteractionExt();
        /// chart.ChartInteractiveBehavior = interaction;
        /// ...
        /// ]]>
        /// </code>
        /// </remarks>
        public ChartInteractiveBehavior InteractiveBehavior
        {
            get { return (ChartInteractiveBehavior)GetValue(InteractiveBehaviorProperty); }
            set { SetValue(InteractiveBehaviorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the view to the background of chart area.
        /// </summary>
        /// <value>Defaults to null.</value>
        /// <example>
        /// # [MainPage.xaml](#tab/tabid-9)
        /// <code><![CDATA[
        /// <chart:SfCircularChart>
        /// 
        ///         <chart:SfCircularChart.BindingContext>
        ///             <local:ViewModel/>
        ///         </chart:SfCircularChart.BindingContext>
        /// 
        ///         <chart:SfCartesianChart.PlotAreaBackgroundView>
        ///             <BoxView Color="Aqua" Margin = "10" CornerRadius = "5" />
        ///         </chart:SfCartesianChart.PlotAreaBackgroundView>
        /// 
        ///         <chart:SfCircularChart.Series>
        ///             <chart:PieSeries ItemsSource="{Binding Data}" XBindingPath="XValue" YBindingPath="YValue1"/>
        ///         </chart:SfCircularChart.Series>
        /// 
        /// </chart:SfCircularChart>
        /// ]]>
        /// </code>
        /// 
        /// # [MainPage.xaml.cs](#tab/tabid-10)
        /// <code><![CDATA[
        ///  SfCircularChart chart = new SfCircularChart();
        ///  
        ///  ViewModel viewModel = new ViewModel();
        ///  chart.BindingContext = viewModel;
        ///  
        ///  BoxView boxView = new BoxView()
        ///  {
        ///     Color = Colors.Aqua,
        ///     Margin = 10,
        ///     CornerRadius = 5,
        ///  };
        ///  
        ///  chart.PlotAreaBackgroundView = boxView
        ///  
        ///  DoughnutSeries series = new DoughnutSeries()
        ///  {
        ///     ItemsSource = viewmodel.Data,
        ///     XBindingPath = "XValue",
        ///     YBindingPath = "YValue1",
        ///     SelectionBrush = Colors.Blue
        ///  };
        ///  chart.Series.Add(series);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public View PlotAreaBackgroundView
        {
            get { return (View)GetValue(PlotAreaBackgroundViewProperty); }
            set { SetValue(PlotAreaBackgroundViewProperty, value); }
        }

        internal ChartLegendLabelStyle LegendStyle
        {
            get { return (ChartLegendLabelStyle)GetValue(LegendStyleProperty); }
            set { SetValue(LegendStyleProperty, value); }
        }

        #endregion

        #region Internal Properties
        internal View? Content { get; set; }

        internal ChartTitleView TitleView { get; set; }

        internal AbsoluteLayout BehaviorLayout { get; set; }

        internal bool IsRequiredDataLabelsMeasure
        {
            get; set;
        }

        bool IChart.IsRequiredDataLabelsMeasure { get => IsRequiredDataLabelsMeasure; set => IsRequiredDataLabelsMeasure = value; }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartBase"/> class.
        /// </summary>
        public ChartBase()
        {
            TitleView = new ChartTitleView();
            BehaviorLayout = new AbsoluteLayout();
            LegendStyle.Parent = this;
            area = CreateChartArea();
            LegendLayout = new LegendLayout(area);
            Content = CreateTemplate(LegendLayout);
        }

        internal virtual AreaBase CreateChartArea()
        {
            throw new ArgumentNullException("Chart area cannot be null");
        }

        private View CreateTemplate(LegendLayout legendLayout)
        {
            Grid grid = new Grid();
            grid.AddRowDefinition(new RowDefinition() { Height = GridLength.Auto });
            grid.AddRowDefinition(new RowDefinition() { Height = GridLength.Star });
            Grid.SetRow(TitleView, 0);
            grid.Add(TitleView);
            Grid.SetRow(legendLayout, 1);
            grid.Add(legendLayout);

            return grid;
        }
        #endregion

        #region Protected Methods

        /// <summary>
        /// Invoked when binding context changed.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (Content != null)
            {
                SetInheritedBindingContext(Content, BindingContext);
            }

            if (this.Title != null && Title is View view)
            {
                SetInheritedBindingContext(view, BindingContext);
            }

            if (this.TooltipBehavior != null)
            {
                SetInheritedBindingContext(TooltipBehavior, BindingContext);
            }
        }
        #endregion

        #region Internal Methods

        internal virtual TooltipInfo? GetTooltipInfo(ChartTooltipBehavior behavior, float x, float y)
        {
            var visibleSeries = (this.area as IChartArea)?.VisibleSeries;
            if (visibleSeries != null)
            {
                for (int i = visibleSeries.Count - 1; i >= 0; i--)
                {
                    ChartSeries chartSeries = visibleSeries[i];

                    if (!chartSeries.EnableTooltip || chartSeries.PointsCount <= 0)
                    {
                        continue;
                    }

                    TooltipInfo? tooltipInfo = chartSeries.GetTooltipInfo(behavior, x, y);
                    if (tooltipInfo != null)
                    {
                        return tooltipInfo;
                    }
                }
            }

            return null;
        }

        internal void UpdateLegendItems()
        {
            if (area != null && area.PlotArea is ChartPlotArea plotArea)
            {
                 plotArea.ShouldPopulateLegendItems = true;
            }
        }

        #endregion

        #region export

        /// <summary>
        /// <para> To convert a chart view to a stream, the <b> GetStreamAsync </b> method is used. Currently, the supported file formats are <b> JPEG and PNG </b>. </para>
        /// <para> To get the stream for the chart view in <b> PNG </b> file format, use <b> await chart.GetStreamAsync(ImageFileFormat.Png); </b> </para>
        /// <para> To get the stream for the chart view in <b> JPEG </b> file format, use <b> await chart.GetStreamAsync(ImageFileFormat.Jpeg); </b> </para>
        /// <remarks> The charts stream can only be rendered when the chart view is added to the visual tree. </remarks>
        /// <para> <b> imageFileFormat </b> Pass the required file format. </para>
        /// </summary>
        /// <param name="imageFileFormat">  Pass the required file format. </param>
        /// <returns> Returns the chart view's stream in the desired file format. </returns>
        public Task<Stream> GetStreamAsync(ImageFileFormat imageFileFormat)
        {
            return Syncfusion.Maui.Core.Internals.ViewExtensions.GetStreamAsync(this, imageFileFormat);
        }

        /// <summary>
        /// <para> To save a chart view as an image in the desired file format, the <b> SaveAsImage </b> is used. Currently, the supported image formats are <b> JPEG and PNG </b>. </para>
        /// <para> By default, the image format is <b> PNG</b>. For example, <b> chart.SaveAsImage("Test"); </b> </para>
        /// <para> To save a chart view in the <b> PNG </b> image format, the filename should be passed with the <b> ".png" extension </b> 
        /// while to save the image in the <b> JPEG </b> image format, the filename should be passed with the <b> ".jpeg" extension </b>, 
        /// for example, <b> "chart.SaveAsImage("Test.png")" and "chart.SaveAsImage("Test.jpeg")" </b> respectively. </para>
        /// <para> <b> Saved location: </b>
        /// For <b> Windows, Android, and Mac </b>, the image will be saved in the <b> Pictures folder </b>, and for <b> iOS </b>, the image will be saved in the <b> Photos Album folder </b>. </para>
        /// <remarks> <para> In <b> Windows and Mac </b>, when you save an image with an already existing filename, the existing file is replaced with a new file, but the filename remains the same. </para>
        /// <para> In <b> Android </b>, when you save the same view with an existing filename, the new image will be saved with a filename with a number appended to it, 
        /// for example, Test(1).jpeg and the existing filename Test.jpeg will be removed.When you save a different view with an already existing filename, 
        /// the new image will be saved with a filename with a number will be appended to it, for example, Test(1).jpeg, and the existing filename Test.jpeg will remain in the folder. </para>
        /// <para> In <b> iOS </b>, due to its platform limitation, the image will be saved with the default filename, for example, IMG_001.jpeg, IMG_002.png and more. </para> </remarks>
        /// <remarks> The chart view can be saved as an image only when the view is added to the visual tree. </remarks>
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveAsImage(string fileName)
        {
            Syncfusion.Maui.Core.Internals.ViewExtensions.SaveAsImage(this, fileName);
        }

        #endregion

        #region Interface Overrides

        object? IContentView.Content => Content;

        /// <summary>
        /// Gets the presented content value.
        /// </summary>
        IView? IContentView.PresentedContent => Content;

        /// <summary>
        /// Gets the padding value.
        /// </summary>
        Thickness IPadding.Padding => Thickness.Zero;

        ChartTooltipBehavior? IChart.ActualTooltipBehavior
        {
            get
            {
                if (TooltipBehavior == null)
                {
                    if (defaultToolTipBehavior == null)
                    {
                        defaultToolTipBehavior = new ChartTooltipBehavior();
                        defaultToolTipBehavior.Chart = this;
                        // defaultToolTipBehavior.IsDefault = true;
                    }

                    return defaultToolTipBehavior;
                }

                return toolTipBehavior;
            }
            set
            {
                toolTipBehavior = value;
            }
        }

        SfTooltip? IChart.TooltipView
        {
            get { return tooltipView; }

            set
            {
                tooltipView = value;
            }
        }

        Rect IChart.ActualSeriesClipRect
        {
            get { return actualSeriesClipRect; }

            set
            {
                actualSeriesClipRect = value;
            }
        }

        IArea IChart.Area => area;

        Color IChart.BackgroundColor => BackgroundColor;

        AbsoluteLayout IChart.BehaviorLayout => BehaviorLayout;

        double IChart.TitleHeight => TitleView != null ? TitleView.Height : 0;

        Brush? IChart.GetSelectionBrush(ChartSeries series)
        {
            return GetSelectionBrush(series);
        }

        //Return selection brush if the series was get selected from Selection behavior. 
        //As circular series not has series selection no need to consider.
        internal virtual Brush? GetSelectionBrush(ChartSeries series)
        {
            return null;
        }

        Size IContentView.CrossPlatformMeasure(double widthConstraint, double heightConstraint)
        {
            return this.MeasureContent(widthConstraint, heightConstraint);
        }

        Size IContentView.CrossPlatformArrange(Rect bounds)
        {
            this.ArrangeContent(bounds);
            return bounds.Size;
        }

        TooltipInfo? IChart.GetTooltipInfo(ChartTooltipBehavior behavior, float x, float y) => GetTooltipInfo(behavior, x, y);

        #endregion

        #region Property call back methods
        private static void OnTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var chart = bindable as ChartBase;
            if (chart != null && chart.TitleView != null)
            {
                if (newValue != null)
                {
                    if (newValue is View view)
                    {
                        chart.TitleView.Content = view;
                        chart.TitleView.Parent.Parent = chart;
                    }
                    else
                    {
                        chart.TitleView.InitTitle(newValue.ToString());
                    }
                }
            }
        }

        private static void OnLegendPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ChartBase chart)
            {
                var layout = chart.LegendLayout;

                if (layout != null && newValue is ILegend legend)
                {
                    layout.Legend = legend;
                    layout.AreaBase.ScheduleUpdateArea();
                }
            }
        }

        private static void OnTooltipBehaviorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ChartBase chart && newValue is ChartTooltipBehavior behavior)
            {
                SetInheritedBindingContext(behavior, chart.BindingContext);
                chart.toolTipBehavior = behavior;
                behavior.Chart = chart;
            }

            SetParent((Element)oldValue, (Element)newValue, (Element)bindable);
        }

        private static void OnInteractiveBehaviorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ChartBase chart)
            {
                if (newValue is ChartInteractiveBehavior behavior)
                {
                    SetInheritedBindingContext(behavior, chart.BindingContext);
                }

                SetParent((Element)oldValue, (Element)newValue, chart);
            }
        }

        private static void OnPlotAreaBackgroundChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var chart = bindable as ChartBase;
            if (chart != null)
            {
                chart.OnPlotAreaBackgroundChanged(newValue);
            }
        }

        internal virtual void OnPlotAreaBackgroundChanged(object newValue)
        {
            if (area.PlotArea is ChartPlotArea plotArea)
            {
                plotArea.PlotAreaBackgroundView = (View)newValue;
            }
        }

        private static void OnLegendStylePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ChartBase chart)
            {
                SetParent((Element)oldValue, (Element)newValue, chart);
            }
        }

        internal static void SetParent(Element? oldElement, Element? newElement, Element parent)
        {
            if (oldElement != null)
            {
                oldElement.Parent = null;
            }

            if (newElement != null)
            {
                newElement.Parent = parent;
            }
        }

        private static object LegendStyleDefaultValueCreator(BindableObject bindable)
        {
            return new ChartLegendLabelStyle((ChartBase)bindable);
        }

        #endregion
    }
}