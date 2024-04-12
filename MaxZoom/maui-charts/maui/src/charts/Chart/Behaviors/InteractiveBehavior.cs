using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Graphics.Internals;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// <see cref = "ChartInteractiveBehavior" /> is a class used to get the methods that are called when the touch interactions are made in the Chart area. By using that, the user can change the behavior for different actions like <b>OnTouchDown</b>, <b>OnTouchMove</b>, and <b>OnTouchUP</b>.
    /// </summary>
    /// <remarks>
    /// <para><b>OnTouchDown() - </b>gets called when the user makes the initial contact of a user's finger or touch input device with the Chart Area. </para>
    /// <para><b>OnTouchMove() - </b>gets called when a user's finger or touch input device is in contact with the Chart area and moves across its surface. </para>
    /// <para><b>OnTouchUp() - </b>gets called when a user lifts their finger or releases their touch input from the Chart area. </para>
    /// 
    /// <para>To use this method, create the class inherited by the ChartInteractiveBehavior class.</para>
    /// <para>Then, create the instance for that class, and it must be added in the chart's <see cref="ChartBase.InteractiveBehavior"/> as per the following code sample. </para>
    /// # [ChartInteractionExt.cs](#tab/tabid-1)
    /// <code><![CDATA[
    /// public class ChartInteractionExt : ChartInteractiveBehavior
    /// {
    ///    <!--omitted for brevity-->
    /// }
    /// ]]>
    /// </code>
    /// # [MainPage.xaml](#tab/tabid-2)
    /// <code><![CDATA[
    /// <chart:SfCartesianChart>
    ///
    ///     <!--omitted for brevity-->
    ///
    ///     <chart:SfCartesianChart.InteractiveBehavior>
    ///          <local:ChartInteractionExt/>
    ///     </chart:SfCartesianChart.InteractiveBehavior>
    /// 
    /// </chart:SfCartesianChart>
    ///
    /// ]]>
    /// </code>
    /// # [MainPage.xaml.cs](#tab/tabid-3)
    /// <code><![CDATA[
    /// SfCartesianChart chart = new SfCartesianChart();
    /// 
    /// ChartInteractionExt interaction = new ChartInteractionExt();
    /// chart.ChartInteractiveBehavior = interaction;
    /// ]]>
    /// </code>
    /// </remarks>
    public class ChartInteractiveBehavior : ChartBehavior
    {

       
    }

}
