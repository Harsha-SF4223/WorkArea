using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// It's a collection class that holds PolarSeries.
    /// </summary>
    public class ChartPolarSeriesCollection : ObservableCollection<PolarSeries>
    {
        #region Fields

        private readonly ReadOnlyObservableCollection<PolarSeries> readOnlyItems;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartPolarSeriesCollection"/> class.
        /// </summary>
        public ChartPolarSeriesCollection()
        {
            readOnlyItems = new ReadOnlyObservableCollection<PolarSeries>(this);
        }

        #endregion

        #region Internal Method

        internal ReadOnlyObservableCollection<ChartSeries>? GetVisibleSeries()
        {
            return new ReadOnlyObservableCollection<ChartSeries>(new ObservableCollection<ChartSeries>(from chartSeries in this
                                                                                                               where chartSeries.IsVisible
                                                                                                               select chartSeries));
        }

        internal ReadOnlyObservableCollection<PolarSeries> AsReadOnly()
        {
            return readOnlyItems;
        } 

        #endregion
    }
}