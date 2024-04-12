using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.Core;
using System.ComponentModel;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// ChartDataLabel is used to customize the appearance of the data label.
    /// </summary>
    public class ChartDataLabel : INotifyPropertyChanged
    {
        #region Fields

        private string label;
        private Brush background;
        private int index = -1;
        private double xPosition = double.NaN;
        private double yPosition = double.NaN;
        private object? item;
        private ChartDataLabelStyle labelStyle;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartDataLabel"/> class.
        /// </summary>
        public ChartDataLabel()
        {
            label = string.Empty;
            background = Brush.Transparent;
            labelStyle = new ChartDataLabelStyle();
        }

        #region public Properties

        /// <summary>
        /// Gets or sets the data label content.
        /// </summary>
        public string Label
        {
            get
            {
                return label;
            }

            set
            {
                if (label == value)
                {
                    return;
                }

                label = value;
                OnPropertyChanged(nameof(Label));
            }
        }

        /// <summary>
        /// Returns the background color of the data label.
        /// </summary>
        public Brush Background
        {
            get
            {
                return background;
            }

            internal set
            {
                if (background == value)
                {
                    return;
                }

                background = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        /// <summary>
        /// Returns the index of the data label.
        /// </summary>
        public int Index
        {
            get
            {
                return index;
            }

            internal set
            {
                if (index == value)
                {
                    return;
                }

                index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        /// <summary>
        /// Returns the x-position of data label placement.
        /// </summary>
        public double XPosition
        {
            get
            {
                return xPosition;
            }

            internal set
            {
                if (xPosition == value)
                {
                    return;
                }

                xPosition = value;
                OnPropertyChanged(nameof(XPosition));
            }
        }

        /// <summary>
        /// Returns the y-position of data label placement.
        /// </summary>
        public double YPosition
        {
            get
            {
                return yPosition;
            }

            internal set
            {
                if (yPosition == value)
                {
                    return;
                }

                yPosition = value;
                OnPropertyChanged(nameof(YPosition));
            }
        }

        /// <summary>
        /// Gets the underlying item of the data label.
        /// </summary>
        public object? Item
        {
            get
            {
                return item;
            }

            internal set
            {
                if (item == value)
                {
                    return;
                }

                item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        /// <summary>
        /// Gets or sets the label style to customize the appearance of the data label.
        /// </summary>
        internal ChartDataLabelStyle LabelStyle
        {
            get
            {
                return labelStyle;
            }

            set
            {
                if (labelStyle == value)
                {
                    return;
                }

                labelStyle = value;
                OnPropertyChanged(nameof(LabelStyle));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
