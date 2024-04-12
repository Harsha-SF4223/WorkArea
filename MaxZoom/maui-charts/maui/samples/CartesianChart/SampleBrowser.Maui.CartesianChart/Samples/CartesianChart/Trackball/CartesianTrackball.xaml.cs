using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using MAUIPicker = Microsoft.Maui.Controls.Picker;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class CartesianTrackball : SampleView
    {
        public CartesianTrackball()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            trackballChart.Handler?.DisconnectHandler();
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (MAUIPicker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex == 0)
            {
                trackball.DisplayMode = LabelDisplayMode.FloatAllPoints;
            }
            else if (selectedIndex == 1)
            {
                trackball.DisplayMode = LabelDisplayMode.NearestPoint;
            }
        }
    }
}
