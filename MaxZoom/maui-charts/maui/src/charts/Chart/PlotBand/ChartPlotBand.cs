using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Graphics.Internals;
using System;
using System.Linq;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// Serves as a base class for <see cref="NumericalPlotBand"/>, <see cref="DateTimePlotBand"/>. This class provides options to customize the appearance of plot bands.
    /// </summary>
    /// <remarks>
    ///   
    /// <para> <b>IsVisible - </b> To customize the visiblity of plot band, refer to this <see cref="IsVisible"/> property. </para>  
    /// <para> <b>AssociatedAxisStart - </b> To customize the segment start of the plot band, refer to this <see cref="AssociatedAxisStart"/> property. </para>
    /// <para> <b>AssociatedAxisEnd - </b> To customize the segment end of the plot band, refer to this <see cref="AssociatedAxisEnd"/> property. </para>
    /// <para> <b>AssociatedAxisName - </b> To set the axis name for the plot band, refer to this <see cref="AssociatedAxisName"/> property. </para>
    /// <para> <b>Fill - </b> To customize the background color of the plot band, refer to this <see cref="Fill"/> property. </para>
    /// <para> <b>Stroke - </b> To customize the stroke color, refer to this <see cref="Stroke"/> property. </para>
    /// <para> <b>StrokeWidth - </b> To customize the stroke width, refer to this <see cref="StrokeWidth"/> property. </para>
    /// <para> <b>Text - </b> To set the label for the plot band, refer to this <see cref="Text"/> property. </para>
    /// <para> <b>Size - </b> To customize the width of the plot band, refer to this <see cref="Size"/> property. </para>
    /// <para> <b>IsRepeatable - </b> To customize the repeatation of the plot band, refer to this <see cref="IsRepeatable"/> property. </para>
    /// <para> <b>LabelStyle - </b> To customize the label for the plot band, refer to this <see cref="LabelStyle"/> property. </para>   
    /// 
    /// </remarks>
    public abstract class ChartPlotBand : Element
    {
        #region Fields

        #region Private Fields

        private ChartPlotBandLabelStyle actualLabelStyle;

        #endregion

        #endregion

        #region Bindable Properties

        /// <summary>
        /// Identifies the <see cref="IsPixelWidth"/> bindable property.
        /// </summary>
        internal static readonly BindableProperty IsPixelWidthProperty = BindableProperty.Create(nameof(IsPixelWidth), typeof(bool), typeof(ChartPlotBand), false, BindingMode.Default, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="AssociatedAxisStart"/> bindable property.
        /// </summary>
        public static readonly BindableProperty AssociatedAxisStartProperty = BindableProperty.Create(nameof(AssociatedAxisStart), typeof(object), typeof(ChartPlotBand), null, BindingMode.Default, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="AssociatedAxisEnd"/> bindable property.
        /// </summary>
        public static readonly BindableProperty AssociatedAxisEndProperty = BindableProperty.Create(nameof(AssociatedAxisEnd), typeof(object), typeof(ChartPlotBand), null, BindingMode.Default, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="AssociatedAxisName"/> bindable property.
        /// </summary>
        public static readonly BindableProperty AssociatedAxisNameProperty = BindableProperty.Create(nameof(AssociatedAxisName), typeof(string), typeof(ChartPlotBand), string.Empty, BindingMode.Default, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="Fill"/> bindable property.
        /// </summary>
        public static readonly BindableProperty FillProperty = BindableProperty.Create(nameof(Fill), typeof(Brush), typeof(ChartPlotBand), null, BindingMode.Default, null, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="Stroke"/> bindable property.
        /// </summary>
        public static readonly BindableProperty StrokeProperty = BindableProperty.Create(nameof(Stroke), typeof(Brush), typeof(ChartPlotBand), Brush.Default, BindingMode.Default, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="StrokeWidth"/> bindable property.
        /// </summary>
        public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create(nameof(StrokeWidth), typeof(double), typeof(ChartPlotBand), 1d, BindingMode.Default, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="StrokeDashArray"/> bindable property.
        /// </summary>
        public static readonly BindableProperty StrokeDashArrayProperty = BindableProperty.Create(nameof(StrokeDashArray), typeof(DoubleCollection), typeof(ChartPlotBand), null, BindingMode.Default, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="IsVisible"/> bindable property.
        /// </summary>
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(ChartPlotBand), true, BindingMode.Default, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="Text"/> bindable property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(ChartPlotBand), string.Empty, BindingMode.Default, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="Size"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(double), typeof(ChartPlotBand), double.NaN, BindingMode.Default, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="RepeatEvery"/> bindable property.
        /// </summary>
        public static readonly BindableProperty RepeatEveryProperty = BindableProperty.Create(nameof(RepeatEvery), typeof(double), typeof(ChartPlotBand), double.NaN, BindingMode.Default, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="IsRepeatable"/> bindable property.
        /// </summary>
        public static readonly BindableProperty IsRepeatableProperty = BindableProperty.Create(nameof(IsRepeatable), typeof(bool), typeof(ChartPlotBand), false, BindingMode.Default, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="LabelStyle"/> bindable property.
        /// </summary>
        public static readonly BindableProperty LabelstyleProperty = BindableProperty.Create(nameof(LabelStyle), typeof(ChartPlotBandLabelStyle), typeof(ChartPlotBand), null, BindingMode.Default, null, OnLabelStylePropertyChanged);

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public ChartPlotBand()
        {
            Fill = new SolidColorBrush(Color.FromArgb("#66E7E0EC"));
            actualLabelStyle = new ChartPlotBandLabelStyle()
            {
                FontSize = 12f,
                TextColor = Color.FromArgb("#000000"),
            };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the start value for the plot band segmentation. It can be a double, date ,time or logarithmic value.
        /// </summary>
        /// <value> This property takes <see cref= "object"/> value</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///              <chart:NumericalPlotBandCollection>
        ///                <chart:NumericalPlotBand Start="1" End="2" AssociatedAxisStart="10"/>
        ///                 </chart:NumericalPlotBandCollection>
        ///            </chart:CategoryAxis.PlotBands>
        ///        </chart:CategoryAxis>
        ///     </chart:SfCartesianChart.XAxes>
        /// 
        /// </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// CategoryAxis xaxis = new CategoryAxis();
        /// NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        /// NumericalPlotBand plotBand = new NumericalPlotBand();
        /// plotBand.Start = 1;
        /// plotBand.End = 2;
        /// plotBand.AssociatedAxisStart = 10;
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code>
        public object AssociatedAxisStart
        {
            get { return (object)GetValue(AssociatedAxisStartProperty); }
            set { SetValue(AssociatedAxisStartProperty, value); }
        }

        /// <summary>
        /// Gets or sets the end value for the plot band segmentation. It can be a double, date, time or logarithmic value.
        /// </summary>
        /// <value> This property takes <see cref= "object"/> value</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///               <chart:NumericalPlotBandCollection>
        ///                <chart:NumericalPlotBand Start="1" End="2" AssociatedAxisEnd="20"/>
        ///               </chart:NumericalPlotBandCollection>
        ///            </chart:CategoryAxis.PlotBands>
        ///        </chart:CategoryAxis>
        ///     </chart:SfCartesianChart.XAxes>
        /// 
        /// </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// CategoryAxis xaxis = new CategoryAxis();
        /// NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        /// NumericalPlotBand plotBand = new NumericalPlotBand();
        /// plotBand.Start = 1;
        /// plotBand.End = 2;
        /// plotBand.AssociatedAxisEnd = 20;
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code>
        public object AssociatedAxisEnd
        {
            get { return (object)GetValue(AssociatedAxisEndProperty); }
            set { SetValue(AssociatedAxisEndProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name of the segment axis for the plot band.
        /// </summary>
        /// <value> This property takes <see cref= "string"/> value</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///               <chart:NumericalPlotBandCollection>
        ///                <chart:NumericalPlotBand Start="1" End="2" AssociatedAxisName="YAxes"/>
        ///               </chart:NumericalPlotBandCollection>
        ///            </chart:CategoryAxis.PlotBands>
        ///        </chart:CategoryAxis>
        ///     </chart:SfCartesianChart.XAxes>
        /// 
        /// </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// CategoryAxis xaxis = new CategoryAxis();
        /// NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        /// NumericalPlotBand plotBand = new NumericalPlotBand();
        /// plotBand.Start = 1;
        /// plotBand.End = 2;
        /// plotBand.AssociatedAxisName = "YAxes";
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code>
        public string AssociatedAxisName
        {
            get { return (string)GetValue(AssociatedAxisNameProperty); }
            set { SetValue(AssociatedAxisNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the plot band. 
        /// </summary>
        /// <value> This property takes <see cref= "Brush"/> value</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///               <chart:NumericalPlotBandCollection>
        ///                <chart:NumericalPlotBand Start="1" Size="2" Fill="Orange"/>
        ///               </chart:NumericalPlotBandCollection>
        ///            </chart:CategoryAxis.PlotBands>
        ///        </chart:CategoryAxis>
        ///     </chart:SfCartesianChart.XAxes>
        /// 
        /// </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// CategoryAxis xaxis = new CategoryAxis();
        /// NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        /// NumericalPlotBand plotBand = new NumericalPlotBand();
        /// plotBand.Start = 1;
        /// plotBand.End = 2;
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code>
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value to customize the outer stroke appearance of the plot band.
        /// </summary>
        /// <value>It accepts <see cref="Brush"/> values.</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///              <chart:NumericalPlotBandCollection>
        ///                <chart:NumericalPlotBand Start="1" Size="2" Stroke="Red"/>
        ///              </chart:NumericalPlotBandCollection>
        ///            </chart:CategoryAxis.PlotBands>
        ///        </chart:CategoryAxis>
        ///     </chart:SfCartesianChart.XAxes>
        /// 
        /// </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// CategoryAxis xaxis = new CategoryAxis();
        /// NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        /// NumericalPlotBand plotBand = new NumericalPlotBand();
        /// plotBand.Start = 1;
        /// plotBand.Size = 2;
        /// plotBand.Stroke = Colors.Red;
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the stroke thickness of the plot band.
        /// </summary>
        /// <value>It accepts <see cref="double"/> values and the default value is 1.</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///              <chart:NumericalPlotBandCollection>
        ///                <chart:NumericalPlotBand Start="1" Size="2" StrokeWidth="3"/>
        ///              </chart:NumericalPlotBandCollection>
        ///            </chart:CategoryAxis.PlotBands>
        ///        </chart:CategoryAxis>
        ///     </chart:SfCartesianChart.XAxes>
        /// 
        /// </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// CategoryAxis xaxis = new CategoryAxis();
        /// NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        /// NumericalPlotBand plotBand = new NumericalPlotBand();
        /// plotBand.Start = 1;
        /// plotBand.Size = 2;
        /// plotBand.StrokeWidth = 3;
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code>
        public double StrokeWidth
        {
            get { return (double)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the stroke dash array to customize the appearance of the stroke.
        /// </summary>
        /// <value>It accepts the <see cref="DoubleCollection"/> value and the default value is null.</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///              <chart:NumericalPlotBandCollection>
        ///                <chart:NumericalPlotBand Start="1" Size="2" StrokeDashArray ="3,3"/>
        ///               </chart:NumericalPlotBandCollection>
        ///            </chart:CategoryAxis.PlotBands>
        ///        </chart:CategoryAxis>
        ///     </chart:SfCartesianChart.XAxes>
        /// 
        /// </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// CategoryAxis xaxis = new CategoryAxis();
        /// NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        /// DoubleCollection doubleCollection = new DoubleCollection();
        /// doubleCollection.Add(5);
        /// doubleCollection.Add(3);
        /// NumericalPlotBand plotBand = new NumericalPlotBand();
        /// plotBand.Start = 1;
        /// plotBand.Size = 2;
        /// plotBand.StrokeDashArray = doubleCollection;
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code>
        public DoubleCollection StrokeDashArray
        {
            get { return (DoubleCollection)GetValue(StrokeDashArrayProperty); }
            set { SetValue(StrokeDashArrayProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the plot band is visible on the axis.
        /// </summary>
        /// <value>It accepts <see cref="bool"/> values.</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///              <chart:NumericalPlotBandCollection>
        ///                <chart:NumericalPlotBand Start="1" Size="2" IsVisible="Fasle"/>
        ///              </chart:NumericalPlotBandCollection>
        ///            </chart:CategoryAxis.PlotBands>
        ///        </chart:CategoryAxis>
        ///     </chart:SfCartesianChart.XAxes>
        /// 
        /// </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// CategoryAxis xaxis = new CategoryAxis();
        /// NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        /// NumericalPlotBand plotBand = new NumericalPlotBand();
        /// plotBand.Start = 1;
        /// plotBand.Size = 2;
        /// plotBand.IsVisible = false;
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code> 
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text to be displayed on the plot band.
        /// </summary>
        /// <value>This property takes the <see cref="string"/>value.</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///              <chart:NumericalPlotBandCollection>
        ///                <chart:NumericalPlotBand Start="1" Size="2" Text="Category PlotBand"/>
        ///              </chart:NumericalPlotBandCollection>
        ///            </chart:CategoryAxis.PlotBands>
        ///        </chart:CategoryAxis>
        ///     </chart:SfCartesianChart.XAxes>
        /// 
        /// </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// CategoryAxis xaxis = new CategoryAxis();
        /// NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        /// NumericalPlotBand plotBand = new NumericalPlotBand();
        /// plotBand.Start = 1;
        /// plotBand.Size = 2;
        /// plotBand.Text = "Category PlotBand";
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code> 
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size of the plot band.
        /// </summary>
        /// <value>This property takes the <see cref="double"/>value. Its default value is double.NaN.</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///               <chart:NumericalPlotBandCollection>
        ///                <chart:NumericalPlotBand Start="1" Size="1" RepeatEvery="2"/>
        ///               </chart:NumericalPlotBandCollection>
        ///            </chart:CategoryAxis.PlotBands>
        ///        </chart:CategoryAxis>
        ///     </chart:SfCartesianChart.XAxes>
        /// 
        /// </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// CategoryAxis xaxis = new CategoryAxis();
        /// NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        /// NumericalPlotBand plotBand = new NumericalPlotBand();
        /// plotBand.Start = 1;
        /// plotBand.Size = 1;
        /// plotBand.RepeatEvery = 2;
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code> 
        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the frequency of the plot band.
        /// </summary>
        /// <value>This property takes the <see cref="double"/>value. Its default value is double.NaN.</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///              <chart:NumericalPlotBandCollection>
        ///                <chart:NumericalPlotBand Start="1" Size="1" RepeatEvery="2"/>
        ///              </chart:NumericalPlotBandCollection>
        ///            </chart:CategoryAxis.PlotBands>
        ///        </chart:CategoryAxis>
        ///     </chart:SfCartesianChart.XAxes>
        /// 
        /// </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// CategoryAxis xaxis = new CategoryAxis();
        /// NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        /// NumericalPlotBand plotBand = new NumericalPlotBand();
        /// plotBand.Start = 1;
        /// plotBand.Size = 1;
        /// plotBand.RepeatEvery = 2;
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code> 
        public double RepeatEvery
        {
            get { return (double)GetValue(RepeatEveryProperty); }
            set { SetValue(RepeatEveryProperty, value); }
        }

        /// <summary>
        /// Gets or sets the bool value to indicate the plot band recurrence.
        /// </summary>
        /// <value>This property takes the <see cref="bool"/>value. Its default value is false.</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///              <chart:NumericalPlotBandCollection>
        ///                <chart:NumericalPlotBand Start="1" Size="1" RepeatEvery="2" IsRepeatable="True"/>
        ///              </chart:NumericalPlotBandCollection>
        ///            </chart:CategoryAxis.PlotBands>
        ///        </chart:CategoryAxis>
        ///     </chart:SfCartesianChart.XAxes>
        /// 
        /// </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [MainPage.xaml.cs](#tab/tabid-2)
        /// <code><![CDATA[
        /// SfCartesianChart chart = new SfCartesianChart();
        /// 
        /// CategoryAxis xaxis = new CategoryAxis();
        /// NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        /// NumericalPlotBand plotBand = new NumericalPlotBand();
        /// plotBand.Start = 1;
        /// plotBand.Size = 1;
        /// plotBand.RepeatEvery = 2;
        /// plotBand.IsRepeatable = true;
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code> 
        public bool IsRepeatable
        {
            get { return (bool)GetValue(IsRepeatableProperty); }
            set { SetValue(IsRepeatableProperty, value); }
        }

        /// <summary>
        ///  Gets or sets the customized style for the axis labels. 
        /// </summary>
        /// <value>This property takes the <see cref="ChartPlotBandLabelStyle"/> as its value.</value>
        public ChartPlotBandLabelStyle LabelStyle
        {
            get { return (ChartPlotBandLabelStyle)GetValue(LabelstyleProperty); }
            set { SetValue(LabelstyleProperty, value); }
        }

        #region Internal Properties

        internal ChartAxis? Axis { get; set; }

        internal bool IsSegmented => (AssociatedAxisStart != null) || (AssociatedAxisEnd != null);

        internal bool IsPixelWidth
        {
            get { return (bool)GetValue(IsPixelWidthProperty); }
            set { SetValue(IsPixelWidthProperty, value); }
        }

        #endregion

        #endregion

        #region Methods

        #region Internal Methods

        internal RectF GetVerticalDrawRect(ChartAxis axis, ChartAxis associatedAxis, double startBand, double endBand)
        {
            var y1 = axis.ValueToPoint(startBand);
            float y2;
            if (!IsSegmented)
            {
                if (!(endBand < axis.VisibleRange.Start) && !(startBand > axis.VisibleRange.End))
                {
                    y2 = axis.ValueToPoint(endBand);

                    var top = axis.ArrangeRect.Top - axis.Area!.ActualSeriesClipRect.Top;

                    if (y1 > y2)
                    {
                        var temp = y1;
                        y1 = y2;
                        y2 = temp;
                    }

                    y2 = (float)(y2 > top ? y2 : top);
                    var x1 = associatedAxis.ArrangeRect.Left - axis.Area.ActualSeriesClipRect.Left;
                    var x2 = associatedAxis.ArrangeRect.Width + associatedAxis.ArrangeRect.Left -
                             axis.Area.ActualSeriesClipRect.Left;
                    return new RectF((float)x1!, y1, (float)(x2 - x1)!, y2 - y1);
                }
            }
            else
            {
                if (AssociatedAxisStart == null && AssociatedAxisEnd == null)
                {
                    return RectF.Zero;
                }

                var associatedAxisStart = AssociatedAxisStart == null ? associatedAxis.VisibleRange.Start : ChartUtils.ConvertToDouble(AssociatedAxisStart);
                var associatedAxisEnd = AssociatedAxisEnd == null ? associatedAxis.VisibleRange.End : ChartUtils.ConvertToDouble(AssociatedAxisEnd);

                if (!double.IsNaN(associatedAxisStart) && !double.IsNaN(associatedAxisEnd))
                {                    
                    endBand = endBand > axis.VisibleRange.End
                    ? axis.VisibleRange.End
                    : endBand;
                    y2 = axis.ValueToPoint(endBand);

                    var startValue = associatedAxis.ValueToPoint(associatedAxisStart);
                    var endValue = associatedAxis.ValueToPoint(associatedAxisEnd);

                    if (startValue > endValue)
                    {
                        var temp = startValue;
                        startValue = endValue;
                        endValue = temp;
                    }

                    if (y1 > y2)
                    {
                        var temp = y1;
                        y1 = y2;
                        y2 = temp;
                    }

                    return new RectF(startValue, y1, endValue - startValue, y2 - y1);
                }
            }

            return RectF.Zero;
        }       

        internal RectF GetHorizontalDrawRect(ChartAxis axis, ChartAxis associatedAxis, double startBand, double endBand)
        {
            var x1 = axis.ValueToPoint(startBand);
            float x2;

            if (!IsSegmented)
            {
                if (!(endBand < axis.VisibleRange.Start) && !(startBand > axis.VisibleRange.End))
                {
                    x2 = axis.ValueToPoint(endBand);

                    var left = axis.ArrangeRect.Width + axis!.ArrangeRect.Left - axis.Area!.ActualSeriesClipRect.Left;
                    x2 = (float)(left > x2 ? x2 : left);

                    if (x1 > x2)
                    {
                        var temp = x1;
                        x1 = x2;
                        x2 = temp;
                    }

                    var y1 = 0;
                    var y2 = associatedAxis.ArrangeRect.Height + associatedAxis!.ArrangeRect.Top;
                    return new RectF(x1, y1, x2 - x1, (float)(y2 - y1));
                }
            }
            else
            {
                if (AssociatedAxisStart == null && AssociatedAxisEnd == null)
                {
                    return RectF.Zero;
                }

                var associatedAxisStart = AssociatedAxisStart == null ? associatedAxis.VisibleRange.Start : ChartUtils.ConvertToDouble(AssociatedAxisStart);
                var associatedAxisEnd = AssociatedAxisEnd == null ? associatedAxis.VisibleRange.End : ChartUtils.ConvertToDouble(AssociatedAxisEnd);

                if (!double.IsNaN(associatedAxisStart) && !double.IsNaN(associatedAxisEnd))
                {                    
                    endBand = endBand > axis.VisibleRange.End ? axis.VisibleRange.End : endBand;
                    x2 = axis.ValueToPoint(endBand);

                    var startValue = associatedAxis.ValueToPoint(associatedAxisStart);
                    var endValue = associatedAxis.ValueToPoint(associatedAxisEnd);

                    if (startValue > endValue)
                    {
                        var temp = startValue;
                        startValue = endValue;
                        endValue = temp;
                    }

                    if (x1 > x2)
                    {
                        var temp = x1;
                        x1 = x2;
                        x2 = temp;
                    }

                    return new RectF(x1, startValue, x2 - x1, endValue - startValue);
                }
            }

            return RectF.Zero;
        }

        internal void DrawPlotBandRect(ICanvas canvas, RectF drawRect)
        {
            if (!drawRect.IsEmpty)
            {
                canvas.SaveState();

                canvas.SetFillPaint(Fill, drawRect);
                canvas.FillRectangle(drawRect);

                if (StrokeWidth > 0 && !ChartColor.IsEmpty(Stroke.ToColor()))
                {
                    canvas.StrokeSize = (float)StrokeWidth;
                    canvas.StrokeColor = Stroke.ToColor();

                    if (StrokeDashArray != null && StrokeDashArray.Count > 0)
                    {
                        canvas.StrokeDashPattern = StrokeDashArray.ToFloatArray();
                    }

                    canvas.DrawRectangle(drawRect);
                }

                if (!string.IsNullOrEmpty(Text))
                {
                    DrawText(canvas, drawRect);
                }

                canvas.RestoreState();
            }
        }

        internal static void OnPlotBandPropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            if (bindable is ChartPlotBand plotBand)
            {
                plotBand.Axis?.InvalidatePlotBands();
            }
        }

        internal virtual void DrawPlotBands(ICanvas canvas, RectF dirtyRect)
        {

        }

        internal ChartAxis? GetAssociatedAxis(ChartAxis axis)
        {           
            if (axis.CartesianArea is CartesianChartArea area)
            {
                if (area.XAxes.Contains(axis))
                {
                    if (!string.IsNullOrEmpty(AssociatedAxisName))
                    {
                        foreach (var yAxis in area.YAxes)
                        {
                            if (yAxis.Name.Equals(AssociatedAxisName))
                            {
                                return yAxis;
                            }
                        }
                    }
                }
                else if (area.YAxes.Contains(axis))
                {
                    if (!string.IsNullOrEmpty(AssociatedAxisName))
                    {
                        foreach (var xAxis in area.XAxes)
                        {
                            if (xAxis.Name.Equals(AssociatedAxisName))
                            {
                                return xAxis;
                            }
                        }
                    }
                }
            }

            return axis.AssociatedAxes.Count > 0 ? axis.AssociatedAxes[0] : null;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (actualLabelStyle != null)
            {
                actualLabelStyle.Parent = this.Parent;
            }   
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (actualLabelStyle != null)
                SetInheritedBindingContext(actualLabelStyle, BindingContext);            
        }

        #endregion

        #region Private Methods

        private void DrawText(ICanvas canvas, RectF bandRect)
        {
            float x = (float)actualLabelStyle.OffsetX, y = (float)actualLabelStyle.OffsetY;
            Brush? fillColor = actualLabelStyle.Background ?? Fill;
            var MarginLeft = (float)actualLabelStyle.Margin.Left;
            var MarginRight = (float)actualLabelStyle.Margin.Right;
            var MarginTop = (float)actualLabelStyle.Margin.Top;
            var MarginBottom = (float)actualLabelStyle.Margin.Bottom;
            SizeF labelSize = Text.Measure(actualLabelStyle);
            SizeF actualSize = new SizeF(labelSize.Width + MarginLeft + MarginRight, labelSize.Height + MarginTop + MarginBottom);

            switch (actualLabelStyle.HorizontalTextAlignment)
            {
                case ChartLabelAlignment.Center:
                    {
                        x += bandRect.Left + (bandRect.Width - actualSize.Width) / 2;
                        break;
                    }
                case ChartLabelAlignment.End:
                    {
                        x += (float)(bandRect.Right - actualSize.Width);
                        break;
                    }
                default:
                    x += bandRect.Left;
                    break;
            }

            switch (actualLabelStyle.VerticalTextAlignment)
            {
                case ChartLabelAlignment.Center:
                    {
                        y += bandRect.Top + (bandRect.Height - actualSize.Height) / 2;
                        break;
                    }
                case ChartLabelAlignment.End:
                    {
                        y += (float)(bandRect.Bottom - actualSize.Height);
                        break;
                    }
                default:
                    y += bandRect.Top;
                    break;
            }

            var backgroundRect = new RectF();

            DrawPlotBandBackgroundRect(canvas, fillColor, x, y, actualSize, backgroundRect);
            x += (float)actualLabelStyle.Margin.Left;
            y += (float)actualLabelStyle.Margin.Top;
            var yPos = y + actualSize.Height / 2;
#if ANDROID
            canvas.DrawText(Text, x, yPos, actualLabelStyle);
#else
            canvas.DrawText(Text, x, y, actualLabelStyle);
#endif
            canvas.RestoreState();
        }

        private void DrawPlotBandBackgroundRect(ICanvas canvas, Brush fillColor, float x, float y, SizeF labelSize, RectF backgroundRect)
        {
            if (!double.IsNaN(actualLabelStyle.Angle))
            {
                float angle = (float)(actualLabelStyle.Angle > 360 ? actualLabelStyle.Angle % 360 : actualLabelStyle.Angle);
                canvas.SaveState();
                canvas.Rotate(angle, x + labelSize.Width / 2, y + labelSize.Height / 2);
            }

            canvas.StrokeSize = (float)actualLabelStyle.StrokeWidth;
            canvas.StrokeColor = actualLabelStyle.Stroke.ToColor();

            backgroundRect = new RectF(x, y, labelSize.Width, labelSize.Height);
            canvas.SetFillPaint(fillColor, backgroundRect);
            canvas.FillRectangle(backgroundRect);
        }

        private static void OnLabelStylePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ChartPlotBand plotBand)
            {
                ChartBase.SetParent((Element?)oldValue, (Element?)newValue, plotBand.Parent);

                if (oldValue is ChartPlotBandLabelStyle style)
                {
                    style.PropertyChanged -= plotBand.Style_PropertyChanged;
                }

                if (newValue is ChartPlotBandLabelStyle newStyle)
                {
                    plotBand.actualLabelStyle = newStyle;
                    newStyle.Parent = plotBand.Parent;
                    SetInheritedBindingContext(newStyle, plotBand.BindingContext);
                    newStyle.PropertyChanged += plotBand.Style_PropertyChanged;
                }
                else
                {
                    plotBand.actualLabelStyle = new ChartPlotBandLabelStyle();
                    plotBand.actualLabelStyle.Parent = plotBand.Parent;
                    SetInheritedBindingContext(plotBand.actualLabelStyle, plotBand.BindingContext);
                }

                plotBand.Axis?.InvalidatePlotBands();
            }
        }

        private void Style_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.Axis?.InvalidatePlotBands();
        }

        #endregion

        #endregion
    }
}