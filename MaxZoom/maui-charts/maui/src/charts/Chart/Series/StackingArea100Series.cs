namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// The <see cref="StackingArea100Series"/> is a collection of data points, where the areas are stacked on top of each other to represent a percentage of the total for each category.
    /// </summary>
    /// <remarks>
    /// <para>The cumulative portion of each stacked element always total to 100% </para>
    /// <para>To render a series, create an instance of <see cref="StackingArea100Series"/> class, and add it to the <see cref="SfCartesianChart.Series"/> collection.</para>
    /// <para>It provides options for <see cref="ChartSeries.Fill"/>, <see cref="ChartSeries.PaletteBrushes"/>, <see cref="XYDataSeries.StrokeWidth"/>, <see cref="StackingSeriesBase.Stroke"/>, <see cref="StackingSeriesBase.StrokeDashArray"/> and <see cref="ChartSeries.Opacity"/> to customize the appearance.</para>
    /// <para> <b>EnableTooltip - </b> A tooltip displays information while tapping or mouse hovering above a segment. To display the tooltip on a chart, you need to set the <see cref="ChartSeries.EnableTooltip"/> property as <b>true</b> in <see cref="StackingArea100Series"/> class, and also refer <seealso cref="ChartBase.TooltipBehavior"/> property.</para>
    /// <para> <b>Data Label - </b> Data labels are used to display values related to a chart segment. To render the data labels, you need to set the <see cref="ChartSeries.ShowDataLabels"/> property as <b>true</b> in <see cref="StackingArea100Series"/> class. To customize the chart data labels placement, and label styles, you need to create an instance of <see cref="CartesianDataLabelSettings"/> and set to the <see cref="CartesianSeries.DataLabelSettings"/> property.</para>
    /// <para> <b>Animation - </b> To animate the series, set <b>True</b> to the <see cref="ChartSeries.EnableAnimation"/> property.</para>
    /// <para> <b>LegendIcon - </b> To customize the legend icon using the <see cref="ChartSeries.LegendIcon"/> property.</para>
    /// </remarks>
    /// <example>
    /// # [Xaml](#tab/tabid-1)
    /// <code><![CDATA[
    ///     <chart:SfCartesianChart>
    ///
    ///           <chart:SfCartesianChart.XAxes>
    ///               <chart:CategoryAxis/>
    ///           </chart:SfCartesianChart.XAxes>
    ///
    ///           <chart:SfCartesianChart.YAxes>
    ///               <chart:NumericalAxis/>
    ///           </chart:SfCartesianChart.YAxes>
    ///
    ///               <chart:StackingArea100Series
    ///                   ItemsSource="{Binding Data}"
    ///                   XBindingPath="XValue"
    ///                   YBindingPath="YValue"/>
    ///                   
    ///               <chart:StackingArea100Series
    ///                   ItemsSource="{Binding Data_1}"
    ///                   XBindingPath="XValue"
    ///                   YBindingPath="YValue"/>
    ///                   
    ///                <chart:StackingArea100Series
    ///                   ItemsSource="{Binding Data_2}"
    ///                   XBindingPath="XValue"
    ///                   YBindingPath="YValue"/>
    ///           
    ///     </chart:SfCartesianChart>
    /// ]]></code>
    /// # [C#](#tab/tabid-2)
    /// <code><![CDATA[
    ///     SfCartesianChart chart = new SfCartesianChart();
    ///     
    ///     CategoryAxis xAxis = new CategoryAxis();
    ///     NumericalAxis yAxis = new NumericalAxis();
    ///     
    ///     chart.XAxes.Add(xAxis);
    ///     chart.YAxes.Add(yAxis);
    ///     
    ///     ViewModel viewModel = new ViewModel();
    /// 
    ///     StackingArea100Series series = new StackingArea100Series();
    ///     series.ItemsSource = viewModel.Data;
    ///     series.XBindingPath = "XValue";
    ///     series.YBindingPath = "YValue";
    ///     
    ///     StackingArea100Series series_1 = new StackingArea100Series();
    ///     series.ItemsSource = viewModel.Data_1;
    ///     series.XBindingPath = "XValue";
    ///     series.YBindingPath = "YValue";
    ///     
    ///     StackingArea100Series series_2 = new StackingArea100Series();
    ///     series.ItemsSource = viewModel.Data_2;
    ///     series.XBindingPath = "XValue";
    ///     series.YBindingPath = "YValue";
    ///     
    ///     chart.Series.Add(series);
    ///     chart.Series.Add(series_1);
    ///     chart.Series.Add(series_2);
    ///     
    ///     this.Content = chart;
    ///     
    /// ]]></code>
    /// # [ViewModel](#tab/tabid-3)
    /// <code><![CDATA[
    ///     public ObservableCollection<Model> Data { get; set; }
    ///     public ObservableCollection<Model> Data_1 { get; set; }
    ///     public ObservableCollection<Model> Data_2 { get; set; }
    /// 
    ///     public ViewModel()
    ///     {
    ///        Data = new ObservableCollection<Model>();
    ///        Data.Add(new Model() { XValue = Q1, YValue = 100 });
    ///        Data.Add(new Model() { XValue = Q2, YValue = 150 });
    ///        Data.Add(new Model() { XValue = Q3, YValue = 110 });
    ///        Data.Add(new Model() { XValue = Q4, YValue = 230 });
    ///        
    ///        Data_1 = new ObservableCollection<Model>();
    ///        Data.Add(new Model() { XValue = Q1, YValue = 250 });
    ///        Data.Add(new Model() { XValue = Q2, YValue = 270 });
    ///        Data.Add(new Model() { XValue = Q3, YValue = 280 });
    ///        Data.Add(new Model() { XValue = Q4, YValue = 310 });
    ///        
    ///        Data_2 = new ObservableCollection<Model>();
    ///        Data.Add(new Model() { XValue = Q1, YValue = 150 });
    ///        Data.Add(new Model() { XValue = Q2, YValue = 200 });
    ///        Data.Add(new Model() { XValue = Q3, YValue = 180 });
    ///        Data.Add(new Model() { XValue = Q4, YValue = 260 });
    ///     }
    /// ]]></code>
    /// ***
    /// </example>
    /// <seealso cref="StackingAreaSegment"/>
    public class StackingArea100Series : StackingAreaSeries
    {
        #region Override Method

        /// <summary>
        /// This method used to Create the Segment for the series
        /// </summary>
        /// <returns></returns>
        protected override ChartSegment? CreateSegment()
        {
            return base.CreateSegment();
        }

        #endregion
    }
}