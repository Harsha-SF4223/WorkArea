using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.Maui.Charts
{
    internal interface IDataTemplateDependent
    {
        ObservableCollection<ChartDataLabel> DataLabels { get; }
        DataTemplate LabelTemplate { get; }

        bool IsTemplateItemsChanged() => true;

        bool IsVisible { get;}
    }
}
