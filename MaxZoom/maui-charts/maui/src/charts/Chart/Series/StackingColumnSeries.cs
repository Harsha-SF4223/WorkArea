using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;
using System.Reflection;
using System.Collections.ObjectModel;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// The <see cref="StackingColumnSeries"/> is a chart type that shows a set of vertical bars stacked on top of each other to represent data point values.
    /// </summary>
    /// <remarks>
    /// <para>To render a series, create an instance of <see cref="StackingColumnSeries"/> class, and add it to the <see cref="SfCartesianChart.Series"/> collection.</para>
    /// 
    /// <para>It provides options for <see cref="ChartSeries.Fill"/>, <see cref="ChartSeries.PaletteBrushes"/>, <see cref="XYDataSeries.StrokeWidth"/>, <see cref="StackingSeriesBase.Stroke"/>, and <see cref="ChartSeries.Opacity"/> to customize the appearance.</para>
    /// 
    /// <para> <b>EnableTooltip - </b> A tooltip displays information while tapping or mouse hovering above a segment. To display the tooltip on a chart, you need to set the <see cref="ChartSeries.EnableTooltip"/> property as <b>true</b> in <see cref="StackingColumnSeries"/> class, and also refer <seealso cref="ChartBase.TooltipBehavior"/> property.</para>
    /// <para> <b>Data Label - </b> Data labels are used to display values related to a chart segment. To render the data labels, you need to set the <see cref="ChartSeries.ShowDataLabels"/> property as <b>true</b> in <see cref="StackingColumnSeries"/> class. To customize the chart data labels alignment, placement, and label styles, you need to create an instance of <see cref="CartesianDataLabelSettings"/> and set to the <see cref="CartesianSeries.DataLabelSettings"/> property.</para>
    /// <para> <b>Animation - </b> To animate the series, set <b>True</b> to the <see cref="ChartSeries.EnableAnimation"/> property.</para>
    /// <para> <b>LegendIcon - </b> To customize the legend icon using the <see cref="ChartSeries.LegendIcon"/> property.</para>
    /// <para><b>Spacing - </b> To specify the spacing between segments using the <see cref="Spacing"/> property.</para>
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
    ///               <chart:StackingColumnSeries
    ///                   ItemsSource="{Binding Data}"
    ///                   XBindingPath="XValue"
    ///                   YBindingPath="YValue"/>
    ///                   
    ///               <chart:StackingColumnSeries
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
    ///     StackingColumnSeries series = new StackingColumnSeries();
    ///     series.ItemsSource = viewModel.Data;
    ///     series.XBindingPath = "XValue";
    ///     series.YBindingPath = "YValue";
    ///     
    ///     StackingColumnSeries series_1 = new StackingColumnSeries();
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
    /// <seealso cref="ColumnSegment"/>
    public class StackingColumnSeries : StackingSeriesBase,IDrawCustomLegendIcon, ISBSDependent
    {
        #region Bindable Properties

        /// <summary>
        ///  Identifies the <see cref="Spacing"/> bindable property.
        /// </summary>        
        public static readonly BindableProperty SpacingProperty =
            BindableProperty.Create(
                nameof(Spacing),
                typeof(double),
                typeof(ColumnSeries),
                0d,
                BindingMode.Default,
                null,
                OnSpacingChanged);

        /// <summary>
        /// Identifies the <see cref="Width"/> bindable property
        /// </summary>  
        public static readonly BindableProperty WidthProperty =
            BindableProperty.Create(
                nameof(Width),
                typeof(double),
                typeof(ColumnSeries),
                0.8d,
                BindingMode.Default,
                null,
                OnWidthChanged);

        /// <summary>
        /// Identifies the <see cref="CornerRadius"/> bindable property.
        /// </summary>        
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(
                nameof(CornerRadius),
                typeof(CornerRadius),
                typeof(ColumnSeries),
                null,
                BindingMode.Default,
                null,
                OnCornerRadiusChanged);

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value to indicate spacing between the segments across the series.
        /// </summary>
        /// <value>It accepts double values and the default value is 0. Here, the value is between 0 and 1.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-4)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:StackingColumnSeries ItemsSource="{Binding Data}"
        ///                            XBindingPath="XValue"
        ///                            YBindingPath="YValue"
        ///                            Spacing = "0.3"/>
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
        ///     StackingColumnSeries stackingSeries = new StackingColumnSeries()
        ///     {
        ///           ItemsSource = viewModel.Data,
        ///           XBindingPath = "XValue",
        ///           YBindingPath = "YValue",
        ///           Spacing = 0.3,
        ///     };
        ///     
        ///     chart.Series.Add(stackingSeries);
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
        /// Gets or sets a value to change the width of the rectangle.
        /// </summary>
        /// <value>It accepts double values and the default value is 0.8. Here, the value is between 0 and 1.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-6)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:StackingColumnSeries ItemsSource="{Binding Data}"
        ///                            XBindingPath="XValue"
        ///                            YBindingPath="YValue"
        ///                            Width = "0.7"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]></code>
        /// # [C#](#tab/tabid-7)
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
        ///           Width = 0.7,
        ///     };
        ///     
        ///     chart.Series.Add(stackingSeries);
        ///
        /// ]]></code>
        /// ***
        /// </example>
       public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value for the corner radius that helps to smooth column edges in stacking column series.
        /// </summary>
        /// <value>It accepts <see cref="Microsoft.Maui.CornerRadius"/> value, and its default value is null.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-8)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///
        ///     <!-- ... Eliminated for simplicity-->
        ///
        ///          <chart:StackingColumnSeries ItemsSource="{Binding Data}"
        ///                            XBindingPath="XValue"
        ///                            YBindingPath="YValue"
        ///                            CornerRadius = "5"/>
        ///
        ///     </chart:SfCartesianChart>
        /// ]]></code>
        /// # [C#](#tab/tabid-9)
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
        ///           CornerRadius = new CornerRadius(5)
        ///     };
        ///     
        ///     chart.Series.Add(stackingSeries);
        ///
        /// ]]></code>
        /// ***
        /// </example>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        #endregion

        #region Internal Properties

        internal double yValue;
        internal override bool IsSideBySide => true;

        #endregion

        #region Methods

        #region Interface Implementation

        void IDrawCustomLegendIcon.DrawSeriesLegend(ICanvas canvas, RectF rect, Brush fillColor, bool isSaveState)
        {
            if (isSaveState)
            {
                canvas.SaveState();
            }

            if(this is StackingColumn100Series)
            {
                var pathF = new PathF();
                pathF.MoveTo(1, 0);
                pathF.LineTo(3, 0);
                pathF.LineTo(3, 3);
                pathF.LineTo(1, 3);
                pathF.LineTo(1, 0);
                pathF.Close();
                pathF.MoveTo(5, 0);
                pathF.LineTo(7, 0);
                pathF.LineTo(7, 5);
                pathF.LineTo(5, 5);
                pathF.LineTo(5, 0);
                pathF.Close();
                pathF.MoveTo(7, 6);
                pathF.LineTo(5, 6);
                pathF.LineTo(5, 9);
                pathF.LineTo(7, 9);
                pathF.LineTo(7, 6);
                pathF.Close();
                pathF.MoveTo(1, 4);
                pathF.LineTo(3, 4);
                pathF.LineTo(3, 7);
                pathF.LineTo(1, 7);
                pathF.LineTo(1, 4);
                pathF.Close();
                pathF.MoveTo(7, 10);
                pathF.LineTo(5, 10);
                pathF.LineTo(5, 12);
                pathF.LineTo(7, 12);
                pathF.LineTo(7, 10);
                pathF.Close();
                pathF.MoveTo(1, 8);
                pathF.LineTo(3, 8);
                pathF.LineTo(3, 12);
                pathF.LineTo(1, 12);
                pathF.LineTo(1, 8);
                pathF.Close();
                pathF.MoveTo(11, 8);
                pathF.LineTo(9, 8);
                pathF.LineTo(9, 12);
                pathF.LineTo(11, 12);
                pathF.LineTo(11, 8);
                pathF.Close();
                pathF.MoveTo(9, 3);
                pathF.LineTo(11, 3);
                pathF.LineTo(11, 7);
                pathF.LineTo(9, 7);
                pathF.LineTo(9, 3);
                pathF.Close();
                pathF.MoveTo(11, 0);
                pathF.LineTo(9, 0);
                pathF.LineTo(9, 2);
                pathF.LineTo(11, 2);
                pathF.LineTo(11, 0);
                pathF.Close();
                canvas.FillPath(pathF);
            }
            else
            {
                RectF innerRect1 = new(1, 7, 2, 2);
                canvas.SetFillPaint(fillColor, innerRect1);
                canvas.FillRectangle(innerRect1);

                RectF innerRect2 = new(1, 10, 2, 2);
                canvas.SetFillPaint(fillColor, innerRect2);
                canvas.FillRectangle(innerRect2);

                RectF innerRect3 = new(1, 4, 2, 2);
                canvas.SetFillPaint(fillColor, innerRect3);
                canvas.FillRectangle(innerRect3);

                RectF innerRect4 = new(5, 0, 2, 3);
                canvas.SetFillPaint(fillColor, innerRect4);
                canvas.FillRectangle(innerRect4);

                RectF innerRect5 = new(5, 4, 2, 3);
                canvas.SetFillPaint(fillColor, innerRect5);
                canvas.FillRectangle(innerRect5);

                RectF innerRect6 = new(5, 8, 2, 4);
                canvas.SetFillPaint(fillColor, innerRect6);
                canvas.FillRectangle(innerRect6);

                RectF innerRect7 = new(9, 9, 2, 3);
                canvas.SetFillPaint(fillColor, innerRect7);
                canvas.FillRectangle(innerRect7);

                RectF innerRect8 = new(9, 5, 2, 3);
                canvas.SetFillPaint(fillColor, innerRect8);
                canvas.FillRectangle(innerRect8);

                RectF innerRect9 = new(9, 2, 2, 2);
                canvas.SetFillPaint(fillColor, innerRect9);
                canvas.FillRectangle(innerRect9);

            }

            if (isSaveState)
            {
                canvas.RestoreState();
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// This method used to create the segment for the series.
        /// </summary>
        protected override ChartSegment CreateSegment()
        {
            return new ColumnSegment();
        }

        #endregion

        #region Internal Methods

        internal override void GenerateSegments(SeriesView seriesView)
        {
            var xValues = GetXValues();
            if (xValues == null)
            {
                return;
            }

            if (BottomValues == null)
            {
                ChartArea?.UpdateStackingSeries();
            }

            if (BottomValues == null)
            {
                return;
            }

            var yStartValues = BottomValues;
            var yEndValues = TopValues;

            if (yEndValues != null && yStartValues != null)
            {
                if (IsGrouped && (IsIndexed || xValues == null))
                {
                    for (int i = 0; i < PointsCount; i++)
                    {
                        if (i < Segments.Count && Segments[i] is ColumnSegment)
                        {
                            ((ColumnSegment)Segments[i]).SetData(new[] { i + SbsInfo.Start, i + SbsInfo.End, yEndValues[i], yStartValues[i], i, YValues[i] });
                        }
                        else
                        {
                            CreateSegment(i, new[] { i + SbsInfo.Start, i + SbsInfo.End, yEndValues[i], yStartValues[i], i, YValues[i] });
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < PointsCount; i++)
                    {
                        if (xValues != null)

                        {
                            var x = xValues[i];

                            if (i < Segments.Count && Segments[i] is ColumnSegment)
                            {
                                ((ColumnSegment)Segments[i]).SetData(new[] { x + SbsInfo.Start, x + SbsInfo.End, yEndValues[i], yStartValues[i], x, YValues[i] });
                            }
                            else
                            {
                                CreateSegment(i, new[] { x + SbsInfo.Start, x + SbsInfo.End, yEndValues[i], yStartValues[i], x, YValues[i] });
                            }
                        }
                    }
                }
            }
        }

        internal override double GetActualSpacing()
        {
            return Spacing;
        }

        internal override double GetActualWidth()
        {
            return Width;
        }

        internal override void SetTooltipTargetRect(TooltipInfo tooltipInfo, Rect seriesBounds)
        {
            if (ChartArea == null) return;

            float xPosition = tooltipInfo.X;
            float yPosition = tooltipInfo.Y;
            ColumnSegment? columnSegment = Segments[tooltipInfo.Index] as ColumnSegment;

            if (columnSegment != null)
            {
                var series = columnSegment.Series as CartesianSeries;

                if (ChartArea.IsTransposed && series != null && series.ActualXAxis != null)
                {
                    float width = columnSegment.SegmentBounds.Width;
                    float height = columnSegment.SegmentBounds.Height;
                    Rect targetRect;
                    var actualCrossingValue = series.ActualXAxis.ActualCrossingValue;

                    if (yValue < (double.IsNaN(actualCrossingValue) ? 0 : actualCrossingValue))
                    {
                        targetRect = new Rect(xPosition, yPosition - height / 2, width, height);
                    }
                    else
                    {
                        //In negative segment target rect
                        targetRect = new Rect(xPosition - width, yPosition - height / 2, width, height);
                    }

                    tooltipInfo.TargetRect = targetRect;
                }
                else
                {
                    float width = columnSegment.SegmentBounds.Width;
                    float height = 1;
                    Rect targetRect = new Rect(xPosition - width / 2, yPosition, width, height);
                    tooltipInfo.TargetRect = targetRect;
                }
            }
        }
        
        internal override TooltipInfo? GetTooltipInfo(ChartTooltipBehavior tooltipBehavior, float x, float y)
        {
            if (Segments == null) return null;

            int index = IsSideBySide ? GetDataPointIndex(x, y) : SeriesContainsPoint(new PointF(x, y)) ? TooltipDataPointIndex : -1;

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
             yValue = TopValues[index];
            float xPosition = TransformToVisibleX(xValue, yValue);

            if (!double.IsNaN(xPosition) && !double.IsNaN(yValue) && !double.IsNaN(content))
            {
                float yPosition = TransformToVisibleY(xValue, yValue);

                if (IsSideBySide)
                {
                    double xMidVal = xValue + SbsInfo.Start + ((SbsInfo.End - SbsInfo.Start) / 2);
                    xPosition = TransformToVisibleX(xMidVal, yValue);
                    yPosition = TransformToVisibleY(xMidVal, yValue);
                }

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
                tooltipInfo.Text = content.ToString(); ;
                tooltipInfo.Item = dataPoint;
                
                return tooltipInfo;
            }
            
            return null;
        }

        internal override double GetDataLabelPositionAtIndex(int index)
        {
            if (DataLabelSettings == null) return 0;
            double yValue = 0; 
            double top = TopValues?[index] ?? 0f;
            double bottom = BottomValues?[index] ?? 0f; 
            switch (DataLabelSettings.BarAlignment)
            {
                case DataLabelAlignment.Bottom:
                    yValue = bottom;
                    break;
                case DataLabelAlignment.Middle:
                    yValue = bottom + ((top - bottom) / 2);
                    break;
                case DataLabelAlignment.Top:
                    yValue = top;
                    break;
            }
            
            return yValue;
        }

        internal override void CalculateDataPointPosition(int index, ref double x, ref double y)
        {
            if (ChartArea == null) return; var x1 = SbsInfo.Start + x;
            var x2 = SbsInfo.End + x; var xCal = x1 + ((x2 - x1) / 2);
            var yCal = y; if (ActualYAxis != null && ActualXAxis != null && !double.IsNaN(yCal))
            {
                y = ChartArea.IsTransposed ? ActualXAxis.ValueToPoint(xCal) : ActualYAxis.ValueToPoint(yCal);
            }
            
            if (ActualXAxis != null && ActualYAxis != null && !double.IsNaN(x))
            {
                x = ChartArea.IsTransposed ? ActualYAxis.ValueToPoint(yCal) : ActualXAxis.ValueToPoint(xCal);
            }
        }

        internal override PointF GetDataLabelPosition(ChartSegment dataLabel, SizeF labelSize, PointF labelPosition, float padding)
        {
            if (ChartArea == null) return labelPosition; if (ChartArea.IsTransposed)
            {
                return DataLabelSettings.GetLabelPositionForTransposedRectangularSeries(this, dataLabel.Index, labelSize, labelPosition, padding, DataLabelSettings.BarAlignment);
            }

            return DataLabelSettings.GetLabelPositionForRectangularSeries(this, dataLabel.Index, labelSize, labelPosition, padding, DataLabelSettings.BarAlignment);
        }

        internal override void GenerateTrackballPointInfo(List<object> nearestDataPoints, List<TrackballPointInfo> PointInfos, ref bool isSidebySide)
        {
            
        }

        #endregion

        #region Private Methods

        private static void OnCornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            StackingColumnSeries? columnseries = bindable as StackingColumnSeries;

            if (columnseries != null && columnseries.Chart != null)
            {
                columnseries.InvalidateSeries();
            }
        }

        private static void OnWidthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            StackingColumnSeries? columnseries = bindable as StackingColumnSeries;

            if (columnseries != null && columnseries.ChartArea != null)
            {
                columnseries.UpdateSbsSeries();
            }
        }

        private static void OnSpacingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            StackingColumnSeries? columnseries = bindable as StackingColumnSeries;

            if (columnseries != null && columnseries.ChartArea != null)
            {
                columnseries.UpdateSbsSeries();
            }
        }
        private void CreateSegment(int index,double[] values)
        {
            ColumnSegment? segment = CreateSegment() as ColumnSegment;
            if (segment != null)
            {
                segment.Series = this;
                segment.SetData(values);
                segment.Index =index;
                segment.Item = ActualData?[index];
                InitiateDataLabels(segment);
                Segments.Add(segment);
            }
        }
       

        #endregion

        #endregion

    }
}
