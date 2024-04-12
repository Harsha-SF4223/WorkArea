using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.ObjectModel;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// The NumericalPlotBand class can be used to render plot band using numerical,category, and logarthmic values.This should be used with <see cref="NumericalPlotBand"/>. 
    /// </summary>
    /// <remarks>
    /// <para>NumericalPlotBand requires <see cref="Start"/> and <see cref="End"/> properties to be set to render the Plot band. </para>    
    /// # [MainPage.xaml](#tab/tabid-1)
    /// <code><![CDATA[
    /// <chart:SfCartesianChart>
    /// 
    ///     <chart:SfCartesianChart.XAxes>
    ///         <chart:CategoryAxis>
    ///            <chart:CategoryAxis.PlotBands>
    ///                 <chart:NumericalPlotBandCollection>
    ///                     <chart:NumericalPlotBand Start="1" End="2"/>
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
    /// *** 
    /// </remarks>
    public class NumericalPlotBand : ChartPlotBand
    {
        #region Bindable Properties

        /// <summary>
        /// Identifies the <see cref="Start"/> bindable property.
        /// </summary>
        public static readonly BindableProperty StartProperty = BindableProperty.Create(nameof(Start), typeof(double), typeof(NumericalPlotBand), double.NaN, BindingMode.Default, null, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="RepeatUntil"/> bindable property.
        /// </summary>
        public static readonly BindableProperty RepeatUntilProperty = BindableProperty.Create(nameof(RepeatUntil), typeof(double), typeof(NumericalPlotBand), double.NaN, BindingMode.Default, null, null, OnPlotBandPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="End"/> bindable property.
        /// </summary>
        public static readonly BindableProperty EndProperty = BindableProperty.Create(nameof(End), typeof(double), typeof(NumericalPlotBand), double.NaN, BindingMode.Default, null, OnPlotBandPropertyChanged);

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the axis value representing where the plot bands should start on the axis.
        /// </summary>
        /// <value>This property takes the <see cref="double"/>double value.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-28)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///        <chart:SfCartesianChart.XAxes>
        ///           <chart:CategoryAxis>
        ///              <chart:CategoryAxis.PlotBands>
        ///                 <chart:NumericalPlotBandCollection>
        ///                     <chart:NumericalPlotBand Start="1" End="2"/>
        ///                 </chart:NumericalPlotBandCollection>
        ///              </chart:CategoryAxis.PlotBands>
        ///             </chart:CategoryAxis>
        ///         </chart:SfCartesianChart.XAxes>
        ///     <!-- ... Eliminated for simplicity-->             
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-29)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        /// 
        ///     CategoryAxis xaxis = new CategoryAxis();
        ///     NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        ///     NumericalPlotBand plotBand = new NumericalPlotBand();
        ///     plotBand.Start = 1;
        ///     plotBand.End = 2;
        ///     bands.Add(plotBand);
        ///     xaxis.PlotBands = bands;
        /// 
        ///     chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public double Start
        {
            get { return (double)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        /// <summary>
        /// Gets or sets the end of the plot band, which is considered as screen points or axis range, based on the <c>End</c> property
        /// </summary>
        /// <value>This property takes the <see cref="double"/>value. Its default value is Nan.</value>
        /// # [MainPage.xaml](#tab/tabid-1)
        /// <code><![CDATA[
        /// <chart:SfCartesianChart>
        /// 
        ///     <chart:SfCartesianChart.XAxes>
        ///         <chart:CategoryAxis>
        ///            <chart:CategoryAxis.PlotBands>
        ///                 <chart:NumericalPlotBandCollection>
        ///                     <chart:NumericalPlotBand Start="1" End="5"/>
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
        /// plotBand.End = 5;
        /// bands.Add(plotBand);
        /// xaxis.PlotBands = bands;
        /// 
        /// chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code> 
        public double End
        {
            get { return (double)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        /// <summary>
        /// Gets or sets the axis value, determining where the plot bands must be repeated on the axis.
        /// </summary>
        /// <value>This property takes the <see cref="double"/>double value.</value>
        /// <example>
        /// # [Xaml](#tab/tabid-28)
        /// <code><![CDATA[
        ///     <chart:SfCartesianChart>
        ///        <chart:SfCartesianChart.XAxes>
        ///           <chart:CategoryAxis>
        ///              <chart:CategoryAxis.PlotBands>
        ///                 <chart:NumericalPlotBandCollection>
        ///                     <chart:NumericalPlotBand Start="1" End="1" RepeatUntil="4" RepeatEvery="2"/>
        ///                 </chart:NumericalPlotBandCollection>
        ///              </chart:CategoryAxis.PlotBands>
        ///             </chart:CategoryAxis>
        ///         </chart:SfCartesianChart.XAxes>
        ///     <!-- ... Eliminated for simplicity-->             
        ///     </chart:SfCartesianChart>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-29)
        /// <code><![CDATA[
        ///     SfCartesianChart chart = new SfCartesianChart();
        /// 
        ///     CategoryAxis xaxis = new CategoryAxis();
        ///     NumericalPlotBandCollection bands = new NumericalPlotBandCollection();
        ///     NumericalPlotBand plotBand = new NumericalPlotBand();
        ///     plotBand.Start = 1;
        ///     plotBand.End = 1;
        ///     plotBand.RepeatUntil = 4;
        ///     plotBand.RepeatEvery = 2;
        ///     bands.Add(plotBand);
        ///     xaxis.PlotBands = bands;
        /// 
        ///     chart.XAxes.Add(xaxis);
        ///
        /// ]]>
        /// </code>
        /// ***
        /// </example>
        public double RepeatUntil
        {
            get { return (double)GetValue(RepeatUntilProperty); }
            set { SetValue(RepeatUntilProperty, value); }
        }

        #endregion

        #region Methods

        #region Internal Methods

        internal override void DrawPlotBands(ICanvas canvas, RectF dirtyRect)
        {
            var axis = Axis;
            if (axis == null || double.IsNaN(Start))
                return;            

            ChartAxis? associatedAxis = GetAssociatedAxis(axis);
            if (associatedAxis == null)
                return;

            var repeatUntil = double.IsNaN(RepeatUntil) || RepeatUntil == 0 ? axis.VisibleRange.End : RepeatUntil;
            
            var repeatEvery = RepeatEvery;
            var periodBand = Math.Abs(repeatEvery);
            periodBand = repeatUntil < Start ? -periodBand : periodBand;

            DrawPlotBandsInRange(canvas, axis, associatedAxis, Start, repeatUntil, periodBand);
        }

        #endregion

        #region Private Methods

        private double GetBandWidth(double startBand)
        {
            var width = double.NaN;
            if (!double.IsNaN(End))
                width = startBand < End ? Math.Abs(End - startBand) : Math.Abs(startBand - End);
            else if (!double.IsNaN(Size))
                width = Size;
            return width;
        }

        private void DrawPlotBandsInRange(ICanvas canvas, ChartAxis axis, ChartAxis associatedAxis, double startBand, double repeatUntil, double periodBand)
        {
            var width = GetBandWidth(startBand);
            if (double.IsNaN(width))
            {
                width = axis.VisibleRange.End;
            }

            do
            {
                var endBand = startBand + width;
                var dirtyRect = !axis.IsVertical
                    ? GetHorizontalDrawRect(axis, associatedAxis, startBand, endBand)
                    : GetVerticalDrawRect(axis, associatedAxis, startBand, endBand);

                if (Start == End && (axis.IsVertical ? dirtyRect.Height : dirtyRect.Width) == 0)
                {
                    if (axis.IsVertical)
                    {
                        dirtyRect.Height = 1;
                    }
                    else
                    {
                        dirtyRect.Width = 1;
                    }
                }

                DrawPlotBandRect(canvas, dirtyRect);

                if (axis is LogarithmicAxis logarthmicAxis)
                {
                    startBand = logarthmicAxis.GetPowValue(logarthmicAxis.GetLogValue(startBand) + RepeatEvery);
                }
                else
                {
                    startBand += periodBand;
                }
            } while (IsRepeatable && (periodBand != 0) && (repeatUntil > startBand));
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Represents a collection of <see cref="NumericalPlotBand"/>
    /// </summary>
    public class NumericalPlotBandCollection : ObservableCollection<NumericalPlotBand>
    {

    }
}