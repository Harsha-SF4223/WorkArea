using Syncfusion.Maui.Core.Localization;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// 
    /// </summary>
    public class SfCircularChartResources : LocalizationResourceAccessor
    {
        internal static string Others
        {
            get
            {
                var others = GetString("Others");
                return string.IsNullOrEmpty(others) ? "Others" : others;
            }
        }
    }
}
