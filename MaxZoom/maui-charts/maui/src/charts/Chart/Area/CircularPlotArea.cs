using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core;

namespace Syncfusion.Maui.Charts
{
    internal class CircularPlotArea : ChartPlotArea
    {
        #region Private Fields

        private readonly CircularChartArea circularChartArea;

        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="chartArea"></param>
        public CircularPlotArea(CircularChartArea chartArea):base()
        {
            circularChartArea = chartArea;
        }

        #endregion

        #region Methods

        protected override void UpdateLegendItemsSource()
        {
            if (Series == null || legend == null || !legend.IsVisible)
            {
                return;
            }

            legendItems.Clear();

            foreach (ChartSeries series in Series)
            {
                if (series.IsVisibleOnLegend)
                {
                    var count = 0;

                    double sumOfYValues = double.NaN;

                    if (series is PieSeries pieSeries)
                    {
                        if (!double.IsNaN(pieSeries.GroupTo))
                        {
                            sumOfYValues = pieSeries.SumOfYValues();
                        }

                        for (int i = 0; i < pieSeries.PointsCount; i++)
                        {
                            if (!double.IsNaN(sumOfYValues))
                            {
                                if (pieSeries.GetGroupModeValue(pieSeries.YValues[i], sumOfYValues) > pieSeries.GroupTo)
                                {
                                    InsertLegendItemAt(i, pieSeries);
                                }
                                else
                                {
                                    count++;
                                }

                                if (i == series.PointsCount - 1 && count > 0)
                                {
                                    InsertLegendItemAt(-1, pieSeries);
                                }
                            }
                            else
                            {
                                InsertLegendItemAt(i, pieSeries);
                            }
                        }
                    }
                    else
                    {
                        int index = 0;

                        for (int i = 0; i < series.PointsCount; i++)
                        {
                            var legendItem = new LegendItem();
                            legendItem.IconType = ChartUtils.GetShapeType(series.LegendIcon);
                            Brush? solidColor = series.GetFillColor(legendItem, index);
                            legendItem.IconBrush = solidColor != null ? solidColor : new SolidColorBrush(Colors.Transparent);
                            legendItem.Text = series.GetActualXValue(index)?.ToString() ?? string.Empty;
                            legendItem.Index = index;
                            legendItem.Source = series;
                            legendItem.Item = series.ActualData?[index];
                            UpdateLegendItem(legendItem);
                            legendItems.Add(legendItem);
                            index++;
                        }
                    }
                }
            }
        }

        private void InsertLegendItemAt(int index, PieSeries series)
        {
            var legendItem = new LegendItem();

            bool isGroupTo = !double.IsNaN(series.GroupTo);

            //legendItem.Series = series;
            //Need to set source for the legend item.
            if (index == -1)
            {
                legendItem.Text = isGroupTo ? SfCircularChartResources.Others : string.Empty;
                index = legendItems.Count == 0 ? 0 : legendItems.Count + 1;
            }
            else
            {
                legendItem.Text = series.GetActualXValue(index)?.ToString() ?? string.Empty;
            }

            index = index > legendItems.Count ? legendItems.Count : index;
            legendItem.Item = isGroupTo ? series.GroupToDataPoints[index] : series.ActualData?[index];
            legendItem.Source = series;
            legendItem.Index = index;

            legendItem.IconType = ChartUtils.GetShapeType(series.LegendIcon);
            Brush? solidColor = series.GetFillColor(legendItem, index);
            legendItem.IconBrush = solidColor != null ? solidColor : new SolidColorBrush(Colors.Transparent);
            UpdateLegendItem(legendItem);

            if (legendItems.Count < index)
            {
                legendItems.Add(legendItem);
            }
            else
            {
                legendItems.Insert(index, legendItem);
            }
        }

        internal override void AddSeries(int index, object chartSeries)
        {
            var circularSeries = chartSeries as CircularSeries;
            if (circularSeries != null)
            {
                circularSeries.ChartArea = circularChartArea;
            }
            base.AddSeries(index, chartSeries);
        }

        #endregion
    }

}
