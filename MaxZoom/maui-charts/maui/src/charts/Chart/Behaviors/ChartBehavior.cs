﻿using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core.Internals;
using System.Collections.ObjectModel;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// The ChartBehavior is the base class for <see cref="ChartSelectionBehavior"/>, <see cref="ChartTooltipBehavior"/>, <see cref="ChartTrackballBehavior"/>, and <see cref="ChartZoomPanBehavior"/>.
    /// </summary>
    public abstract class ChartBehavior : Element
    {
        #region Methods
        internal void OnTapped(IChart chart, Point touchPoint, int tapCount)
        {
            if (tapCount == 1)
            {
                OnSingleTap(chart, (float)touchPoint.X, (float)touchPoint.Y);
            }
            else if(tapCount == 2)
            {
                OnDoubleTap(chart, (float)touchPoint.X, (float)touchPoint.Y);
            }
        }

        /// <summary>
        /// This method is triggered when a touch event is detected on the chart.
        /// </summary>
        internal virtual void OnTouchDown(float pointX, float pointY)
        {
        }

        /// <summary>
        /// This method is triggered when the touch action is canceled.
        /// </summary>
        internal virtual void OnTouchCancel(float pointX, float pointY)
        {
        }

        /// <summary>
        /// This method is triggered when a touch event is detected on the chart.
        /// </summary>
        /// <remarks>
        /// <para>Use the provided points X and Y to determine the exact location of the touch.</para>
        /// <para>You can use these coordinates to interact with the chart, such as highlighting data points, showing tooltips, or performing other actions.</para>
        /// <para>
        /// Example code:
        /// </para>
        /// <b>Convert the touch coordinates to value coordinates if needed.</b>
        ///  <code><![CDATA[ 
        /// chart.ChartPointToValue(xAxis, pointY);
        /// ]]></code>
        /// <b>Show a tooltip with information about the data point.</b>
        /// <code><![CDATA[
        /// tooltipBehavior.Show(pointX, pointY);
        /// ]]></code> 
        /// </remarks>
        protected internal virtual void OnTouchDown(ChartBase chart, float pointX, float pointY)
        {

        }

        /// <summary>
        /// This method is triggered when a touch event comes to an end. It is used to finalize the action initiated by the <see cref="OnTouchDown(ChartBase, float, float)"/>.
        /// </summary>
        /// <remarks>
        /// <para>Use the provided points X and Y to determine the exact location of the touch.</para>
        /// <para>You can use these coordinates to interact with the chart, such as highlighting data points, hiding tooltips, or performing other actions when the touch is released.</para>
        /// <para>
        /// Example code:
        /// </para>
        /// <b>Convert the touch coordinates to value coordinates if needed.</b>
        ///  <code><![CDATA[ 
        /// chart.ChartPointToValue(xAxis, pointY);
        /// ]]></code>
        /// <b>Hide a tooltip with information about the data point.</b>
        /// <code><![CDATA[
        /// tooltipBehavior.Hide(pointX, pointY);
        /// ]]></code> 
        /// </remarks>
        protected internal virtual void OnTouchUp(ChartBase chart, float pointX, float pointY)
        {

        }

        internal PointerDeviceType DeviceType { get; set; }

        /// <summary>
        /// This Method is triggered when a touch point is moved along the touch surface.
        /// </summary>
        /// <remarks>
        /// <para>Use the provided points X and Y to determine the exact location of the touch.</para>
        /// <para>You can use these coordinates to define the desired actions to be executed when navigating the touch point across the chart area.</para>
        /// <para>
        /// Example code:
        /// </para>
        /// <b>Convert the touch coordinates to value coordinates if needed.</b>
        ///  <code><![CDATA[ 
        /// chart.ChartPointToValue(xAxis, pointY);
        /// ]]></code>
        /// <b>Show a tooltip with information about the data point.</b>
        /// <code><![CDATA[
        /// tooltipBehavior.Show(pointX, pointY);
        /// ]]></code> 
        /// </remarks>
        protected internal virtual void OnTouchMove(ChartBase chart, float pointX, float pointY)
        {
        }

        internal virtual void OnSingleTap(IChart chart, float pointX, float pointY)
        {

        }

        internal virtual void OnDoubleTap(IChart chart, float x, float y)
        {

        }

        internal virtual void SetTouchHandled(IChart chart)
        {
 
        }

        internal virtual void OnLongPressActivation(IChart chart, float x, float y, GestureStatus status)
        {

        }

        /// <summary>
        /// This method is triggered when a long press action happens.
        /// </summary>
        internal virtual void OnLongPress(IChart chart, float pointX, float pointY)
        {

        }

        internal virtual void OnTouchExit()
        {

        }

        #endregion
    }
}
