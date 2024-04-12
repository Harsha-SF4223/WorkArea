using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    ///  An abstract class in the chart API that serves as a base for candle and OHLC (Open-High-Low-Close) series. It provides common options and functionality for these types of financial chart series.
    /// </summary>
    public abstract class FinancialSeriesBase: CartesianSeries
    {
        #region Bindable Property
        /// <summary>
        /// Identifies the <see cref="BearishFill"/> bindable property.
        /// </summary>
        public static readonly BindableProperty BearishFillProperty = BindableProperty.Create(nameof(BearishFill),
            typeof(Brush),
            typeof(FinancialSeriesBase),
            new SolidColorBrush(Color.FromArgb("#C15146")),
            BindingMode.Default,
            null, OnFillPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="BullishFill"/> bindable property.
        /// </summary>
        public static readonly BindableProperty BullishFillProperty = BindableProperty.Create(nameof(BullishFill),
            typeof(Brush),
            typeof(FinancialSeriesBase),
            new SolidColorBrush(Color.FromArgb("#90A74F")),
            BindingMode.Default,
            null, OnFillPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="High"/> bindable property.
        /// </summary>
        public static readonly BindableProperty HighProperty = BindableProperty.Create(nameof(High),
            typeof(string),
            typeof(FinancialSeriesBase),
            string.Empty,
            BindingMode.Default,
            null, OnHighPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="Low"/> bindable property.
        /// </summary>
        public static readonly BindableProperty LowProperty = BindableProperty.Create(nameof(Low), 
            typeof(string), 
            typeof(FinancialSeriesBase),
            string.Empty,
            BindingMode.Default, 
            null, OnLowPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="Open"/> bindable property.
        /// </summary>
        public static readonly BindableProperty OpenProperty = BindableProperty.Create(nameof(Open), 
            typeof(string), 
            typeof(FinancialSeriesBase),
            string.Empty, 
            BindingMode.Default, 
            null, OnOpenPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="Close"/> bindable property.
        /// </summary>
        public static readonly BindableProperty CloseProperty = BindableProperty.Create(nameof(Close), 
            typeof(string),
            typeof(FinancialSeriesBase),
            string.Empty, 
            BindingMode.Default, 
            null, OnClosePropertyChanged);

        /// <summary>
        /// Identifies the <see cref="Spacing"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), 
            typeof(double),
            typeof(FinancialSeriesBase), 
            0d, 
            BindingMode.Default,
            null, OnSpacingPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="Width"/> bindable property.
        /// </summary>
        public static readonly BindableProperty WidthProperty = BindableProperty.Create(nameof(Width), 
            typeof(double),
            typeof(FinancialSeriesBase), 
            0.8d, 
            BindingMode.Default, 
            null, OnWidthPropertyChanged);

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the brush to be used for bearish points in a financial chart. It is typically used in conjunction with a <see cref="CandleSeries"/> or <see cref="HiLoOpenCloseSeries"/> series to visually represent negative price movements or bearish market conditions.
        /// </summary>
        public Brush BearishFill
        {
            get { return (Brush)GetValue(BearishFillProperty); }
            set { SetValue(BearishFillProperty, value);}
        }

        /// <summary>
        /// Gets or sets the brush to be used for bullish points in a financial chart. It is typically used in conjunction with a <see cref="CandleSeries"/> or <see cref="HiLoOpenCloseSeries"/> series to visually represent positive price movements or bullish market conditions.
        /// </summary>
        public Brush BullishFill
        {
            get { return (Brush)GetValue(BullishFillProperty); }
            set { SetValue(BullishFillProperty, value); }
        }

        /// <summary>
        /// Gets or sets a path value on the source object to serve a high value to the series.
        /// </summary>
        /// <value>
        /// The string that represents the property name for the y (high) plotting data, and its default value is empty.
        /// </value>
        public string High
        {
            get { return (string)GetValue(HighProperty); }
            set { SetValue(HighProperty, value); }
        }

        /// <summary>
        /// Gets or sets a path value on the source object to serve a low value to the series.
        /// </summary>
        /// <value>
        /// The string that represents the property name for the y (low) plotting data, and its default value is empty.
        /// </value>
        public string Low
        {
            get { return (string)GetValue(LowProperty); }
            set { SetValue(LowProperty, value); }
        }

        /// <summary>
        /// Gets or sets a path value on the source object to serve a open value to the series.
        /// </summary>
        /// <value>
        /// The string that represents the property name for the y (open) plotting data, and its default value is empty.
        /// </value>
        public string Open
        {
            get { return (string)GetValue(OpenProperty); }
            set { SetValue(OpenProperty, value); }
        }

        /// <summary>
        /// Gets or sets a path value on the source object to serve a close value to the series.
        /// </summary>
        /// <value>
        /// The string that represents the property name for the y (close) plotting data, and its default value is empty.
        /// </value>
        public string Close
        {
            get { return (string)GetValue(CloseProperty); }
            set { SetValue(CloseProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value to indicate spacing between the data points across the series.
        /// </summary>
        ///  <value>It accepts values between 0 and 1, and its default is 0.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-4)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:CandleSeries ItemsSource="{Binding Data}"
        ///                            XBindingPath="XValue"
        ///                            High="High"
        ///                            Low="Low"
        ///                            Open="Open"
        ///                            Close="Close"
        ///                            Spacing="0.3"/>
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
        ///     CandleSeries candleSeries = new CandleSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           High = "High"
        ///           Low = "Low"
        ///           Open = "Open"
        ///           Close = "Close"
        ///           Spacing = 0.3,
        ///     };
        ///     
        ///     chart.Series.Add(candleSeries);
        ///
        /// ]]></code>
        /// ***
        /// </example>
        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty);}
            set { SetValue(SpacingProperty, value);}
        }

        /// <summary>
        /// Gets or sets a value to change the width of the data points across the series.
        /// </summary>
        /// <value>It accepts values between 0 and 1, and default is 0.8.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-4)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:CandleSeries ItemsSource="{Binding Data}"
        ///                              XBindingPath="XValue"
        ///                              High="High"
        ///                              Low="Low"
        ///                              Open="Open"
        ///                              Close="Close"
        ///                              Width="0.5"/>
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
        ///     CandleSeries candleSeries = new CandleSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           High="High",
        ///           Low="Low"
        ///           Open="Open"
        ///           Close="Close",
        ///           Width = 0.5,
        ///     };
        ///     
        ///     chart.Series.Add(candleSeries);
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

        #region Internal Properties

        internal IList<double> HighValues { get; set; }      
        internal IList<double> LowValues { get; set; }
        internal IList<double> OpenValues { get; set; }
        internal IList<double> CloseValues { get; set; }
        internal override bool IsMultipleYPathRequired => true;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public FinancialSeriesBase()
        {
            HighValues = new List<double>();
            LowValues = new List<double>();
            OpenValues = new List<double>();
            CloseValues = new List<double>();              
        }

        #endregion

        #region Method

        #region Internal Override

        internal override void OnDataSourceChanged(object oldValue, object newValue)
        {
            HighValues.Clear();
            LowValues.Clear();
            OpenValues.Clear();
            CloseValues.Clear();
            GeneratePoints(new string[] { High, Low, Open, Close }, HighValues, LowValues, OpenValues, CloseValues);
            base.OnDataSourceChanged(oldValue, newValue);
        }

        internal override void GenerateDataPoints()
        {
            GeneratePoints(new string[] { High, Low,Open,Close},HighValues, LowValues, OpenValues, CloseValues);
        }

        internal override void OnBindingPathChanged()
        {
            ResetData();
            HighValues.Clear(); 
            LowValues.Clear();
            OpenValues.Clear(); 
            CloseValues.Clear();

            GeneratePoints(new string[] { High, Low, Open, Close }, HighValues, LowValues, OpenValues, CloseValues);
            base.OnBindingPathChanged();
        } 

        internal override void CalculateDataPointPosition(int index, ref double x, ref double y)
        {
            if (ChartArea == null) return;

            var x1 = SbsInfo.Start + x;
            var x2 = SbsInfo.End + x;
            var xCal = x1 + ((x2 - x1) / 2);
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

        internal override void DrawDataLabels(ICanvas canvas)
        {
            var dataLabelSettings = ChartDataLabelSettings;
            if (dataLabelSettings == null) return;

            ChartDataLabelStyle labelStyle = dataLabelSettings.LabelStyle;

            foreach (HiLoOpenCloseSegment datalabel in Segments)
            {
                if (!datalabel.InVisibleRange || datalabel.IsEmpty) continue;
                CandleSeriesDataLabelAppearance(canvas, datalabel, dataLabelSettings, labelStyle);
            }
        }

        private void CandleSeriesDataLabelAppearance(ICanvas canvas, HiLoOpenCloseSegment datalabel, ChartDataLabelSettings dataLabelSettings, ChartDataLabelStyle labelStyle)
        {
            for (int i = 0; i < 4; i++)
            {
                string labelText;
                PointF position;
                labelText = datalabel.DataLabel[i] ?? string.Empty;
                position = datalabel.LabelPositions[i];
          
                if (labelStyle.Angle != 0)
                {
                    float angle = (float)(labelStyle.Angle > 360 ? labelStyle.Angle % 360 : labelStyle.Angle);
                    canvas.SaveState();
                    canvas.Rotate(angle, position.X, position.Y);
                }

                canvas.StrokeSize = (float)labelStyle.StrokeWidth;
                canvas.StrokeColor = labelStyle.Stroke.ToColor();

                var fillcolor = labelStyle.IsBackgroundColorUpdated ? labelStyle.Background : dataLabelSettings.UseSeriesPalette ? datalabel.Fill : labelStyle.Background;
                DrawDataLabel(canvas, fillcolor, labelText, position, datalabel.Index);
            }
        }

        internal override void InitiateDataLabels(ChartSegment segment)
        {
            for(int i =0; i< 4; i++)
            {
                var datalabel = new ChartDataLabel();
                segment.DataLabels.Add(datalabel);
                DataLabels.Add(datalabel);
            }
        }

        internal SizeF GetLabelTemplateSize(ChartSegment segment, string valueTyepe)
        {
            int labelIndex = (valueTyepe == "LowType") ? 1 : ((valueTyepe == "OpenType") ? 2 : ((valueTyepe == "CloseType") ? 3 : 0));

            if (LabelTemplateView != null && LabelTemplateView.Any())
            {
                var templateView = LabelTemplateView.Cast<View>().FirstOrDefault(child => segment.DataLabels[labelIndex] == child.BindingContext) as DataLabelItemView;

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

        #endregion

        #region Private Method

        private static void OnFillPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var series = bindable as FinancialSeriesBase; 

            if (series != null) 
            {
                series.UpdateColor();
                series.InvalidateSeries();
            }
        }

        private static void OnHighPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var series = bindable as FinancialSeriesBase;

            if (series != null)
            {
                series.OnBindingPathChanged();
            }
        }

        private static void OnLowPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var series = bindable as FinancialSeriesBase;

            if (series != null)
            {
                series.OnBindingPathChanged();
            }
        }

        private static void OnOpenPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var series = bindable as FinancialSeriesBase;

            if (series != null)
            {
                series.OnBindingPathChanged();
            }
        }

        private static void OnClosePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var series = bindable as FinancialSeriesBase;

            if (series != null)
            {
                series.OnBindingPathChanged();
            }
        }

        private static void OnWidthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is FinancialSeriesBase series)
            {
                series.UpdateSbsSeries();
            }
        }

        private static void OnSpacingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var series = bindable as FinancialSeriesBase;

            if (series != null && series.ChartArea != null)
            {
                series.InvalidateSeries();
            }
        }

        #endregion

        #endregion
    }
}
