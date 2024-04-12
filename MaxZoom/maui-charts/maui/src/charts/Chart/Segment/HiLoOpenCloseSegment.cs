using Microsoft.Maui.Graphics;
using System;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// Represents the segments for <see cref="HiLoOpenCloseSeries"/>.
    /// </summary>
    public class HiLoOpenCloseSegment : CartesianSegment
    {
        #region Fields

        internal string[] DataLabel = new string[4];
        internal PointF[] LabelPositions = new PointF[4];

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the left position value for segment
        /// </summary>
        internal float Left
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Right position value for segment
        /// </summary>
        internal float Right
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the HighPointY value for segment
        /// </summary>
        internal float HighPointY
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the LowPointY value for segment
        /// </summary>
        internal float LowPointY
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Top position value for segment
        /// </summary>
        internal float Top
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Bottom position value for segment
        /// </summary>
        internal float Bottom
        {
            get;  set;
        }

        #endregion

        #region Internal Properties

        internal double StartX
        {
            get; set;
        }

        internal double CenterX
        {
            get; set;
        }

        internal double EndX
        {
            get; set;
        }

        internal double High
        {
            get; set;
        }

        internal double Low
        {
            get; set;
        }

        internal double Open
        {
            get; set;
        }

        internal double Close
        {
            get; set;
        }

        internal double XValue
        {
            get; set;
        }

        internal bool IsBull { get; set; } = false;

        internal bool IsFill { get; set; } = false;

        internal float CenterLow
        {
            get; set;
        }

        internal float CenterHigh
        {
            get; set;
        }

        #endregion

        #region Methods

        #region Internal method

        internal override void SetData(double[] values, bool isFill,bool isBull)
        {
            if (Series is not HiLoOpenCloseSeries series)
            {
                return;
            }

            this.StartX = values[0];
            this.CenterX = values[1];
            this.EndX = values[2];
            this.Open = values[3];
            this.High = values[4];
            this.Low = values[5];
            this.Close = values[6];
            this.XValue = values[7];
            this.IsFill = isFill;
            this.IsBull = isBull;

            series.XRange += DoubleRange.Union(XValue);
            series.YRange += new DoubleRange(High, Low);            
        }

        internal override int GetDataPointIndex(float x, float y)
        {
            bool verticalRect = IsRectContains(CenterHigh, HighPointY, CenterLow, LowPointY, x, y, (float)StrokeWidth);
            bool lefHorizontal = IsRectContains(Left, Top, CenterHigh, Top, x, y, (float)StrokeWidth);
            bool rightHorizontal = IsRectContains(CenterHigh, Bottom, Right, Bottom, x, y, (float)StrokeWidth);

            if (Series != null && (verticalRect || lefHorizontal || rightHorizontal))
            {
                return Series.Segments.IndexOf(this);
            }

            return -1;
        }

        internal override void OnDataLabelLayout()
        {
            if (Series is FinancialSeriesBase series)
            {
                CalculateDataLabelPositions(XValue, High, Low, Open, Close, series);
            }  
        }

        private void UpdateDataLabels(PointF highPoint, PointF lowPoint, PointF openPoint, PointF closePoint)
        {
            if(Series is FinancialSeriesBase series)
            {
                var dataLabelSettings = series.DataLabelSettings;

                for (int i = 0; i < 4; i++)
                {
                    if (DataLabels != null && DataLabels.Count > i)
                    {
                        var dataLabel = DataLabels[i];

                        dataLabel.Index = Index;
                        dataLabel.Item = Item;
                        dataLabel.LabelStyle = dataLabelSettings.LabelStyle;
                        dataLabel.Label = DataLabel[i];

                        if(i == 0)
                        {
                            LabelPositions[i] = series.IsDataInVisibleRange(XValue, High) ? dataLabelSettings.CalculateLabelPositionForCandleSeries(series, this, highPoint, dataLabelSettings.LabelStyle, "HighType") : new PointF(float.NaN, float.NaN);

                        }
                        else if(i == 1)
                        {
                            LabelPositions[i] = series.IsDataInVisibleRange(XValue, Low) ? dataLabelSettings.CalculateLabelPositionForCandleSeries(series, this, lowPoint, dataLabelSettings.LabelStyle, "LowType") : new PointF(float.NaN, float.NaN);
                        }
                        else if(i == 2)
                        {
                            LabelPositions[i] = series.IsDataInVisibleRange(XValue, Open) ? dataLabelSettings.CalculateLabelPositionForCandleSeries(series, this, openPoint, dataLabelSettings.LabelStyle, "OpenType") : new PointF(float.NaN, float.NaN);
                        }
                        else if(i == 3)
                        {
                            LabelPositions[i] = series.IsDataInVisibleRange(XValue, Close) ? dataLabelSettings.CalculateLabelPositionForCandleSeries(series, this, closePoint, dataLabelSettings.LabelStyle, "CloseType") : new PointF(float.NaN, float.NaN);
                        }

                        dataLabel.XPosition = LabelPositions[i].X;
                        dataLabel.YPosition = LabelPositions[i].Y;
                    }
                }
            }    
        }

        private void CalculateDataLabelPositions(double xValue, double high, double low, double open, double close, FinancialSeriesBase series)
        {
            var dataLabelSettings = series.DataLabelSettings;
            IsEmpty = double.IsNaN(high) && double.IsNaN(low);
            InVisibleRange = series.IsDataInVisibleRange(xValue, high) && series.IsDataInVisibleRange(xValue, low);
            PointF highPoint = GetDataLabelPosition(xValue, high, series);
            PointF lowPoint = GetDataLabelPosition(xValue, low, series);
            PointF openPoint = GetDataLabelPosition(xValue, open, series);
            PointF closePoint = GetDataLabelPosition(xValue, close, series);
            DataLabel[0] = series.GetLabelContent(high, series.SumOfValues(series.HighValues));
            DataLabel[1] = series.GetLabelContent(low,series.SumOfValues(series.LowValues));
            DataLabel[2] = series.GetLabelContent(open, series.SumOfValues(series.OpenValues));
            DataLabel[3] = series.GetLabelContent(close, series.SumOfValues(series.CloseValues));
            UpdateDataLabels(highPoint, lowPoint, openPoint, closePoint);
        }

        private PointF GetDataLabelPosition(double xValue, double yValue, FinancialSeriesBase series)
        {
            double yPosition = yValue;
            series.CalculateDataPointPosition(Index, ref xValue, ref yPosition);
            return new PointF((float)xValue, (float)yPosition);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 
        /// </summary>
        protected internal override void OnLayout()
        {
            if (Series is not HiLoOpenCloseSeries series)
            {
                return;
            }

            Layout(series);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        protected internal override void Draw(ICanvas canvas)
        {
            HiLoOpenCloseSeries? series = Series as HiLoOpenCloseSeries;

            if (series == null || double.IsNaN(Left))
            {
                return;
            }

            if (series.CanAnimate())
            {
                Layout(series);
            }

            canvas.Alpha = Opacity;
            canvas.StrokeSize = (float)StrokeWidth;     
            canvas.StrokeColor = Fill.ToColor();                   
            canvas.DrawLine(CenterHigh, HighPointY, CenterLow, LowPointY);

            if (series?.ChartArea?.IsTransposed is true)
            {
                canvas.DrawLine(Left, Top, Left, HighPointY);
                canvas.DrawLine(Right, HighPointY, Right, Bottom);
            }
            else
            {
                canvas.DrawLine(Left, Top, CenterHigh, Top);
                canvas.DrawLine(CenterHigh, Bottom, Right, Bottom);
            }
        }

        #endregion

        #region Private Method

        private void Layout(HiLoOpenCloseSeries series)
        {
            var xAxis = series.ActualXAxis;

            if (xAxis == null)
            {
                return;
            }

            var temp = High;

            if (High < Low)
            {
                High = Low;
                Low = temp;
            }
            if (High < Open)
            {
                High = Open;
            }
            if (High < Close)
            {
                High = Close;
            }

            temp = Close;

            if (Low > High)
            {
                High = Low;
                Low = temp;
            }
            if (Low > Open)
            {
                Low = Open;
            }
            if (Low > Close)
            {
                Low = Close;
            }

            var start = Math.Floor(xAxis.VisibleRange.Start);
            var end = Math.Ceiling(xAxis.VisibleRange.End);

            Left = Right = Top = Bottom = float.NaN;

            if (StartX <= end && EndX >= StartX)
            {
                Left = series.TransformToVisibleX(StartX, Open);
                Right = series.TransformToVisibleX(EndX, Close);

                Top = series.TransformToVisibleY(StartX, Open);
                Bottom = series.TransformToVisibleY(EndX, Close);

                CenterLow = series.TransformToVisibleX(CenterX, Low);
                CenterHigh = series.TransformToVisibleX(CenterX, High);

                HighPointY = series.TransformToVisibleY(CenterX, High);
                LowPointY = series.TransformToVisibleY(CenterX, Low);

                if(series.CanAnimate())
                {
                    AnimationValueCalculation(series);
                }             
            }
            else
            {
                this.Left = float.NaN;
            }

            SegmentBounds = new RectF(Left, Top, Right - Left, Bottom - Top);
        }

        private void AnimationValueCalculation(HiLoOpenCloseSeries series)
        {
            float animationValue = series.AnimationValue;

            if(series!.ChartArea!.IsTransposed)
            {
                float midPointX = Right + ((Right - Left) / 2);
                Left = midPointX + ((Left - midPointX) * animationValue);
                Right = midPointX - ((midPointX -Right) * animationValue);
                CenterHigh = midPointX - ((midPointX - CenterHigh) * animationValue);
                CenterLow = midPointX + ((CenterLow - midPointX) * animationValue);
            }
            else
            {
                float midPointY = HighPointY + ((LowPointY - HighPointY) / 2);
                HighPointY = midPointY - ((midPointY - HighPointY) * animationValue);
                LowPointY = midPointY + ((LowPointY - midPointY) * animationValue);
                Top = midPointY - ((midPointY - Top) * animationValue);
                Bottom = midPointY + ((Bottom - midPointY) * animationValue);
            }
        }

        #endregion

        #endregion

    }
}
