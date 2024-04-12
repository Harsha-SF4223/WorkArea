using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Graphics.Internals;
using System.Collections.Generic;
using System.Linq;

namespace Syncfusion.Maui.Charts
{
    internal class ChartPlotBandView : SfDrawableView
    {
        #region Private Property

        private CartesianChartArea Area { get; set; }

        #endregion

        #region Constructor

        internal ChartPlotBandView(CartesianChartArea area)
        {
            Area = area;
        }

        #endregion

        #region Methods

        #region Protected Methods

        protected override void OnDraw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();
            canvas.ClipRectangle(dirtyRect);

            DrawPlotBandsForAxes(Area.XAxes, canvas, dirtyRect);
            DrawPlotBandsForAxes(Area.YAxes, canvas, dirtyRect);

            canvas.RestoreState();
        }

        #endregion

        #region Private Methods

        private void DrawPlotBandsForAxes(IEnumerable<ChartAxis> axes, ICanvas canvas, RectF dirtyRect)
        {
            foreach (var axis in axes)
            {
                if (axis.ActualPlotBands != null && axis.ActualPlotBands.Count > 0)
                {
                    foreach (ChartPlotBand plotBand in axis.ActualPlotBands)
                    {
                        if (plotBand.IsVisible)
                        {
                            plotBand.DrawPlotBands(canvas, dirtyRect);
                        }
                    }
                }
            }
        }

        #endregion 

        #endregion
    }
}