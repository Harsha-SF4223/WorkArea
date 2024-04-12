using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

namespace Syncfusion.Maui.Charts
{
    internal interface ISBSDependent
    {
        public double Spacing { get; set; }

        public double Width { get; set; }

        public CornerRadius CornerRadius { get; set; }
    }

    internal interface IDrawCustomLegendIcon
    {
        void DrawSeriesLegend(ICanvas canvas, RectF rect, Brush fillColor, bool isSaveState);
    }
}
